using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{

	public NavMeshAgent Agent;
	private CharacterController _characterController;

	[Header("Powered Attack/Right Click")]
	//Powered Attack
	public GameObject poweredArrowPrefab;
	public GameObject basicLauncher;
	//Cooldown Powered Attack
	public float poweredBasicAttackRate;
	private float _poweredBasicAttackNextFire;
	public Image coolDownImageRight;
	private bool _rOn = false;

	[Header("Trap/Q")]
	//Trap
	public GameObject trapPrefab;
	public GameObject rangeQ;
	private bool _qActive = false;
	//Cooldown Trap
	public float trapRate;
	private float _trapNextFire;
	public Image coolDownImageQ;
	private bool _qOn = false;

	[Header("Turret/W")]
	//Trap
	public GameObject turretPrefab;
	public GameObject rangeW;
	private bool _wActive = false;
	//Cooldown Turret
	public float turretRate;
	private float _turretNextFire;
	public Image coolDownImageW;
	private bool _wOn = false;

	[Header("Granade/E")]
	//Granade
	public GameObject granadePrefab;
	//Cooldown Granade
	public float granadeRate;
	private float _granadeNextFire;
	public Image coolDownImageE;
	private bool _eOn = false;

	[Header("Raycast/Layer")]
	//Raycast
	//Floor
	public LayerMask movementMask;
	//Eneviroment
	public LayerMask environmentMask;
	//Enemy
	public LayerMask enemyMask;
	//Everything except floor
	public LayerMask nullMask;
	//Q Range
	public LayerMask qMask;
	//W Range
	public LayerMask wMask;
	//Everything
	public LayerMask allMask;
	public Camera cam;
	private Ray _ray;
	private RaycastHit _hit;

	// Start is called before the first frame update
	void Start()
    {
		_characterController = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update()
    {

		CoolDownImages();


		#region RightClick
		//Right click
		if (Input.GetMouseButton(1))
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_ray, out _hit, 100, allMask))
			{
				//Attack the enemy
				if (Time.time > _poweredBasicAttackNextFire)
				{
					_poweredBasicAttackNextFire = Time.time + poweredBasicAttackRate;
					TurnToClick();
					PoweredAttack();
					_rOn = true;
				}
			}
		}
		#endregion

		#region E
		//Q Granade
		if (Input.GetKey(KeyCode.E))
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_ray, out _hit, 100, allMask))
			{
				//Attack the enemy
				if (Time.time > _granadeNextFire)
				{
					_granadeNextFire = Time.time + granadeRate;
					TurnToClick();
					Granade();
					_eOn = true;
				}
			}
		}
		#endregion

		#region Q
		//Place Q Trap
		if (Input.GetMouseButton(0) && _qActive)
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_ray, out _hit, 100, qMask))
			{
				//Attack the enemy
				if (Time.time > _trapNextFire)
				{
					_trapNextFire = Time.time + trapRate;
					PlaceQTrap();
					DeactivateQRange();
					_qOn = true;
				}
			}
			else if (Physics.Raycast(_ray, out _hit, 100, allMask))
			{
				DeactivateQRange();
			}
		}

		//Q trap
		if (Input.GetKey(KeyCode.Q))
		{
			//Trap
			if (Time.time > _trapNextFire)
			{
				ActivateQRange();
			}
		}
		#endregion

		#region W
		//Place W Turret
		if (Input.GetMouseButton(0) && _wActive)
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(_ray, out _hit, 100, wMask))
			{
				//Attack the enemy
				if (Time.time > _turretNextFire)
				{
					_turretNextFire = Time.time + turretRate;
					PlaceWTrap();
					DeactivateWRange();
					_wOn = true;
				}
			}
			else if (Physics.Raycast(_ray, out _hit, 100, allMask))
			{
				DeactivateWRange();
			}
		}

		//Q trap
		if (Input.GetKey(KeyCode.W))
		{
			//Trap
			if (Time.time > _turretNextFire)
			{
				ActivateWRange();
			}
		}
		#endregion
	}

	#region Q
	//Activate trap's range
	public void ActivateQRange()
	{
		print("Activate Range");
		_qActive = true;
		rangeQ.SetActive(true);
	}

	//Deactivate trap's range
	public void DeactivateQRange()
	{
		print("Deactivate Range");
		_qActive = false;
		rangeQ.SetActive(false);
	}

	//Trap
	public void PlaceQTrap()
	{
		print("Place trap");
		Instantiate(trapPrefab, _hit.point, _hit.collider.transform.rotation);
		Agent.SetDestination(transform.position);
	}
	#endregion

	#region W
	//Activate turret's range
	public void ActivateWRange()
	{
		print("Activate Range");
		_wActive = true;
		rangeW.SetActive(true);
	}

	//Deactivate turrets's range
	public void DeactivateWRange()
	{
		print("Deactivate Range");
		_wActive = false;
		rangeW.SetActive(false);
	}

	//Turret
	public void PlaceWTrap()
	{
		print("Place trap");
		Instantiate(turretPrefab, _hit.point, _hit.collider.transform.rotation);
		Agent.SetDestination(transform.position);
	}
	#endregion

	#region RightClick
	//Right click attack
	public void PoweredAttack()
	{
		//Stop the player
		Agent.SetDestination(transform.position);

		//Attack in some way
		float angle = -30;
		for (int i = 0; i < 3; i++)
		{
			Instantiate(poweredArrowPrefab, basicLauncher.transform.position, basicLauncher.transform.rotation * Quaternion.Euler(0, angle, 0));
			angle += 30;

		}
	}
	#endregion

	#region E
	//Right click attack
	public void Granade()
	{
		//Stop the player
		Agent.SetDestination(transform.position);

		//Attack in some way
		Instantiate(granadePrefab, basicLauncher.transform.position, basicLauncher.transform.rotation);
	}
	#endregion

	//Turn to click
	public void TurnToClick()
	{
		transform.LookAt(new Vector3(_hit.point.x, basicLauncher.transform.position.y, _hit.point.z));
	}

	//CoolDown Ui
	public void CoolDownImages()
	{
		if (_qOn)
		{
			coolDownImageQ.fillAmount += 1 / trapRate * Time.deltaTime;

			if(coolDownImageQ.fillAmount >= 1)
			{
				coolDownImageQ.fillAmount = 0;
				_qOn = false;
			}
		}

		if (_wOn)
		{
			coolDownImageW.fillAmount += 1 / turretRate * Time.deltaTime;

			if (coolDownImageW.fillAmount >= 1)
			{
				coolDownImageW.fillAmount = 0;
				_wOn = false;
			}
		}

		if (_eOn)
		{
			coolDownImageE.fillAmount += 1 / granadeRate * Time.deltaTime;

			if (coolDownImageE.fillAmount >= 1)
			{
				coolDownImageE.fillAmount = 0;
				_eOn = false;
			}
		}

		if (_rOn)
		{
			coolDownImageRight.fillAmount += 1 / poweredBasicAttackRate * Time.deltaTime;

			if (coolDownImageRight.fillAmount >= 1)
			{
				coolDownImageRight.fillAmount = 0;
				_rOn = false;
			}
		}
	}
}
