using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurret : MonoBehaviour
{

	//Speed
	Rigidbody _Rb;
	private float _basicSpeed = 20f;

	// Start is called before the first frame update
	void Start()
    {
		_Rb = GetComponent<Rigidbody>();

		Destroy(gameObject, 5);	
	}

    // Update is called once per frame
    void Update()
    {
		_Rb.velocity = transform.forward * _basicSpeed;
	}

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Do damage			
			print("Damage");
			Attack(other.GetComponent<EnemyStats>());
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "Wall")
		{
			//Destroy this object
			Destroy(gameObject);
		}
	}

	//Basic damage
	public void Attack(EnemyStats targetStats)
	{
		targetStats.TakeDamage(Turret.damageTurret);
	}
}
