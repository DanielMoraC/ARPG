using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

	[Header("Base Stats")]
	//Health
	static public float maxHealth = 100;
	public float currentHealth;// { get; private set; }
	//Damage
	static public int basicDamage = 20;
	//HPBar
	public Image HPImage;
	//Indicators
	public Text damage;
	public Text health;


	[Header("Skill Stats")]
	//Powered Damage
	static public int poweredDamage = basicDamage + 40;
	//Trap Damage
	static public int trapDamage = 10;

	// Start is called before the first frame update
	void Start()
    {
		currentHealth = maxHealth;
	}

    // Update is called once per frame
    void Update()
    {
		//Update HPBar
		UpdateHealthbar();
		damage.text = basicDamage.ToString();
		health.text = maxHealth.ToString();
	}

	//Damage the character
	public void TakeDamage(int damage)
	{
		//Damage the character
		currentHealth -= damage;

		//If health reaches zero
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	//Destroy the character
	public virtual void Die()
	{
		Destroy(gameObject);
	}

	//Change the HP bar with the current HP
	private void UpdateHealthbar()
	{
		HPImage.fillAmount = currentHealth / maxHealth;
	}

}
