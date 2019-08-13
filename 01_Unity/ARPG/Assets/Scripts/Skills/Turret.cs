using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

	[Header("Stats")]
	//Cooldown Shoot
	public float bulletRate;
	private float _bulletNextFire;
	//Damage
	static public int damageTurret = 20;


	[Header("Shoot")]
	public GameObject launcher;
	public GameObject bulletPrefab;

	private GameObject _target;


	// Start is called before the first frame update
	void Start()
    {
		Destroy(gameObject, 10);
    }

    // Update is called once per frame
    void Update()
    {
		if (_target != null)
		{
			LookEnemy();

			//Attack the enemy
			if (Time.time > _bulletNextFire)
			{
				_bulletNextFire = Time.time + bulletRate;
				Shoot();
			}
		}   
    }

	//Check for enemies
	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Get target
			if (_target == null)
			{
				_target = other.gameObject;
			}
		}
	}

	//Change target
	public void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Null the target if the enemy dies or run away
			if (_target != null)
			{
				_target = null;
			}
		}
	}

	//Look at enemy
	public void LookEnemy()
	{
		transform.LookAt(new Vector3(_target.transform.position.x, launcher.transform.position.y, _target.transform.position.z));
	}

	//Shoot at enemy
	public void Shoot()
	{
		Instantiate(bulletPrefab, launcher.transform.position, launcher.transform.rotation);
	}
}
