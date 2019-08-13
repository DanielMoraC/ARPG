using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

	//1 = Weapon 2 = Helmet 3 = Armor
	public int IDSlot;
	public int maximoH = 0;
	public int maximoW = 0;
	public int maximoA = 0;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	public void OnTriggerEnter2D(Collider2D collision)
	{
		ItemTooltip item = collision.GetComponent<ItemTooltip>();

		//To detect if the slot is occupied
		if (IDSlot == 1)
		{
			maximoW++;
		}
		else if (IDSlot == 2)
		{
			maximoH++;
		}
		else if (IDSlot == 3)
		{
			maximoA++;
		}
		
		//Change Weapon Stats
		if (IDSlot == 1 && maximoW == 1)
		{
			PlayerStats.basicDamage += item.itemStat;
		}

		//Change Helmet Stats
		else if (IDSlot == 2 && maximoH == 1)
		{
			PlayerStats.maxHealth += item.itemStat;
		}

		//Change Armor Stats
		else if (IDSlot == 3 && maximoA == 1)
		{
			PlayerStats.maxHealth += item.itemStat;
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{

		ItemTooltip item = collision.GetComponent<ItemTooltip>();

		//To detect if the slot is occupied
		if (IDSlot == 1)
		{
			maximoW--;
		}
		else if (IDSlot == 2)
		{
			maximoH--;
		}
		else if (IDSlot == 3)
		{
			maximoA--;
		}

		
		//Change Sword Stats
		if (IDSlot == 1 && maximoW != 1)
		{
			PlayerStats.basicDamage -= item.itemStat;
		}

		//Change Helmet Stats
		else if (IDSlot == 2 && maximoH != 1)
		{
			PlayerStats.maxHealth -= item.itemStat;
		}

		//Change Armor Stats
		else if (IDSlot == 3 && maximoA != 1)
		{
			PlayerStats.maxHealth -= item.itemStat;
		}
	}
}
