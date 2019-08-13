using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
	[Header("Base Stats")]
	//Damage
	public int damage;
	static public int basicDamage;
	//Health
	public float maxHealth;
	public float currentHealth;	
	//HPBar
	public Image HPImage;

	[Header("Item Drop")]
	//Items
	//Normal items
	//0 = weapon (cube) 1 = helmet (sphere)
	public GameObject[] normal;
	//Rare items
	//0 = weapon (cube) 1 = armor (cylinder)
	public GameObject[] rare;
	//Legendary items
	//0 = weapon (cube) 1 = armor (cylinder)
	public GameObject[] legendary;
	//Probabilities
	//Drop 1/70
	private int pDrop;
	//Rarity
	//Normal = 1/50 Rare = 51/85 Legendary = 86/100
	private int pRarity;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		basicDamage = damage;
	}

	// Update is called once per frame
	void Update()
	{

	}

	//Damage the character
	public void TakeDamage(int damage)
	{
		//Damage the character
		currentHealth -= damage;

		//Update HPBar
		UpdateHealthbar();

		//If health reaches zero
		if (currentHealth <= 0)
		{
			DropItem();
			Die();
		}
	}

	//Destroy the character
	public virtual void Die()
	{
		Destroy(gameObject);
	}

	//Drop an item based on a probability when the enemy dies
	public void DropItem()
	{
		//Drop rol
		pDrop = Random.Range(1, 100);
		if (pDrop <= 70)
		{
			print("Drop " + pDrop);

			//Rarity rol
			pRarity = Random.Range(1, 100);
			if (pRarity <=50)
			{
				print("Drop normal " + pRarity);
				//Instanriate normal
				Instantiate(normal[Random.Range(0, normal.Length)], transform.position, transform.rotation);
			}
			else if (pRarity >= 51 && pRarity <=85)
			{
				print("Drop rare " + pRarity);
				//Instanriate rare
				Instantiate(rare[Random.Range(0, rare.Length)], transform.position, transform.rotation);
			}
			else if (pRarity >= 86 && pRarity <= 100)
			{
				print("Drop legendary " + pRarity);
				//Instanriate legendary
				Instantiate(legendary[Random.Range(0, legendary.Length)], transform.position, transform.rotation);
			}

		}
		else if (pDrop >= 70)
		{
			print("No drop " + pDrop);
		}

	}

	//Change the HP bar with the current HP
	private void UpdateHealthbar()
	{
		HPImage.fillAmount = currentHealth / maxHealth;
	}
}
