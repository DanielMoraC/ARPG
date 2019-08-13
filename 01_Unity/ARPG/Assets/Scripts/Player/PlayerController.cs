using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    public NavMeshAgent Agent;

    [Header("Basic Attack")]
    //Basic Attack
    public GameObject basicLauncher;
    public GameObject basicArrowPrefab;
    //Cooldown Basic Attack
    public float basicAttackRate;
    private float _basicAttackNextFire;

	[Header("Raycast/Layer")]
	//Raycast
	//Floor
    public LayerMask movementMask;
	//Eneviroment
	public LayerMask environmentMask;
	//Enemy
	public LayerMask enemyMask;
	//Iteams
	public LayerMask itemMask;
	//Everything except floor and enemies
	public LayerMask nullMask;
	//Everything
	public LayerMask allMask;
	public Camera cam;
	public GameObject clickPrefab;
	private Ray _ray;
    private RaycastHit _hit;

	//Inventory
	private Transform _inventory;

	// Start is called before the first frame update
	void Start()
    {
		_inventory = PlayerManager.instance.Inventory;
	}

    // Update is called once per frame
    void Update()
    {
		//Left click
		if (Input.GetMouseButtonDown(0))
		{
			_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (!EventSystem.current.IsPointerOverGameObject())
			{

				//Show where the player clicked if it can move to that direction
				if (Physics.Raycast(_ray, out _hit, 100, nullMask))
				{
					print("Null");
				}
				else if (Physics.Raycast(_ray,out _hit,100,enemyMask))
				{
				   //Turn to enemy
				    TurnToEnemy();
                
				    //Attack the enemy
				    if (Time.time > _basicAttackNextFire)
				    {
				        _basicAttackNextFire = Time.time + basicAttackRate;
						Attack();
				    }
				}
				else if (Physics.Raycast(_ray, out _hit, 100, itemMask))
				{
					//Pick up the item and show it on the inventory if the player didnt reached the max
					if (_inventory.childCount <= 17)
					{
						PickUpItem(_hit.transform.GetComponent<PickUp>());
						Destroy(_hit.transform.gameObject);
					}
					else
					{
						print("Inventory Full");
					}
				}
				else if (Physics.Raycast(_ray, out _hit, 100, movementMask))
				{
					//Move to the point the player clicked
					MoveToClick();
					ShowClick();
				}
			}

		}


		if (!EventSystem.current.IsPointerOverGameObject())
		{
			//Left click
			if (Input.GetMouseButton(0))
			{
				_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(_ray,out _hit,100, nullMask))
				{

				}
				else if (Physics.Raycast(_ray, out _hit, 100, enemyMask))
				{
					//Turn to enemy
					TurnToEnemy();

					//Attack the enemy
					if (Time.time > _basicAttackNextFire)
					{
						_basicAttackNextFire = Time.time + basicAttackRate;
						Attack();
					}
				}
			}
		}
    }

	//Pick up the item
	public void PickUpItem(PickUp item)
	{
		item.GetItem();
	}

    //Move the player
    public void MoveToClick()
    {
        if (Physics.Raycast(_ray, out _hit))
        {
            Agent.SetDestination(_hit.point);
        }
    }

	//Left click attack
	public void Attack()
    {
        //Stop the player
        Agent.SetDestination(transform.position);

        //Attack in some way
        print(_hit.collider.tag);
        Instantiate(basicArrowPrefab, basicLauncher.transform.position, basicLauncher.transform.rotation);
    }

	//Turn to click
	public void TurnToClick()
	{
		transform.LookAt(new Vector3(_hit.point.x, basicLauncher.transform.position.y, _hit.point.z));
	}

	//Turn to enemy
	public void TurnToEnemy()
    {
        transform.LookAt(_hit.collider.transform.position);
    }

	//Show something where the player clicked
	public void ShowClick()
	{
		Instantiate(clickPrefab, _hit.point, _hit.collider.transform.rotation);
	}
}
