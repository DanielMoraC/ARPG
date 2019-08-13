using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

	//Damage
	private int _damageExplosion = 40;

	// Start is called before the first frame update
	void Start()
    {
		Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//Do damage
	public void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			//Do damage
			Damage(other.GetComponent<EnemyStats>());			
		}
	}

	//Explosion damage
	public void Damage(EnemyStats targetStats)
	{
		targetStats.TakeDamage(_damageExplosion);
	}
}
