using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

	[Header("Enemy")]
	//Enemy ID
	//0 = Melee 1 = Range
	public int enemyID;
	public bool move = true;
	NavMeshAgent agent;

	[Header("Radius")]
	//Range for detetction and damage
	public float lookRadius = 10f;
	public float damageRadius = 3f;

	[Header("Range")]
	//Things for ranged enemies
	public GameObject launcher;
	public GameObject arrowPrefab;

	[Header("Cooldown")]
	//Cooldown Basic Attack
	public float basicAttackRate;
	private float _basicAttackNextFire;

	GameObject player;


	// Start is called before the first frame update
	void Start()
    {
		player = PlayerManager.instance.Player;
		agent = GetComponent<NavMeshAgent>();
		agent.stoppingDistance = damageRadius;
    }

    // Update is called once per frame
    void Update()
    {
		float distance = Vector3.Distance(player.transform.position, transform.position);

		if (distance <= lookRadius && move)
		{
			agent.SetDestination(player.transform.position);

			if (distance <= damageRadius)
			{
				//Attack the player
				//Melee
				if (Time.time > _basicAttackNextFire && enemyID == 0)
				{
					_basicAttackNextFire = Time.time + basicAttackRate;
					Attack(player.GetComponent<PlayerStats>());
				}
				//Range
				else if (Time.time > _basicAttackNextFire && enemyID == 1)
				{
					_basicAttackNextFire = Time.time + basicAttackRate;
					Shoot();
				}

				//Face target
				LookPlayer();
			}
		}
		else if (!move)
		{
			agent.SetDestination(transform.position);
			StartCoroutine(Untrap());
		}
    }

	//Untrap enemy
	public IEnumerator Untrap()
	{
		yield return new WaitForSeconds(3);
		move = true;
	}

	//Shoot 
	public void Shoot()
	{
		Instantiate(arrowPrefab, launcher.transform.position, launcher.transform.rotation);
	}

	//Do damage
	public void Attack(PlayerStats targetStats)
	{
		targetStats.TakeDamage(EnemyStats.basicDamage);
	}

	//Look at player
	public void LookPlayer()
	{
		Vector3 direction = (player.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
	}

	private void OnDrawGizmosSelected()
	{
		//Show look radius on blue
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, lookRadius);
		//Show damage radius on red
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, damageRadius);
	}
}
