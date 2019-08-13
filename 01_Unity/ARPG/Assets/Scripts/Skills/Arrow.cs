using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

	public bool poweredAttack = false;

    //Speed
    Rigidbody _Rb;
    public float basicSpeed = 20f;
	public float poweredSpeed = 20f;


	// Start is called before the first frame update
	void Start()
    {
        _Rb = GetComponent<Rigidbody>();

		if (poweredAttack)
		{
			Destroy(gameObject, 5);
		}
    }

    // Update is called once per frame
    void Update()
    {
		if (poweredAttack)
		{
			_Rb.velocity = transform.forward * poweredSpeed;
		}
		else if (!poweredAttack)
		{
			_Rb.velocity = transform.forward * basicSpeed;
		}
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
			//Do damage
			if (poweredAttack)
			{
				print("Damage");
				PoweredAttack(other.GetComponent<EnemyStats>());
			}
			else if (!poweredAttack)
			{
				print("Damage");
				Attack(other.GetComponent<EnemyStats>());
				Destroy(gameObject);
			}
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
		targetStats.TakeDamage(PlayerStats.basicDamage);
	}

	//Powered damage
	public void PoweredAttack(EnemyStats targetStats)
	{
		targetStats.TakeDamage(PlayerStats.poweredDamage);
	}
}
