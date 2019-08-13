using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Trap : MonoBehaviour
{
	EnemyAI enemyTrap;

    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject, 20f);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Trap enemy
			enemyTrap = other.GetComponent<EnemyAI>();
			enemyTrap.move = false;

			//Do damage
			print("Trap Damage");
			Attack(other.GetComponent<EnemyStats>());
			Destroy(gameObject);

		}
	}

	//Basic damage
	public void Attack(EnemyStats targetStats)
	{
		targetStats.TakeDamage(PlayerStats.trapDamage);
	}	
}
