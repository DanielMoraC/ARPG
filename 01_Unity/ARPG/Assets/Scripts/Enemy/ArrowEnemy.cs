using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEnemy : MonoBehaviour
{

    //Speed
    Rigidbody _Rb;
    public float basicSpeed = 20f;


	// Start is called before the first frame update
	void Start()
    {
        _Rb = GetComponent<Rigidbody>();
		Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
		_Rb.velocity = transform.forward * basicSpeed;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			//Do damage
			Attack(other.GetComponent<PlayerStats>());
			Destroy(gameObject);
        }
		if (other.gameObject.tag == "Wall")
		{
			//Destroy this object
			Destroy(gameObject);
		}
	}

	//Basic damage
	public void Attack(PlayerStats targetStats)
	{
		targetStats.TakeDamage(EnemyStats.basicDamage);
	}
}
