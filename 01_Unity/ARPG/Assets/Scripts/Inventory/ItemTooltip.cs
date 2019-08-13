using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{

	public string itemName;
	public int itemStat;
	//0 = Sword 1 = Armor 2 = Helmet
	public int itemID;

	public Text ItemNameText;
	public Text ItemStatsText;

	public GameObject Tooltip;

	// Use this for initialization
	void Start ()
	{
		ItemNameText = PlayerManager.instance.DescriptionNames;
		Tooltip = PlayerManager.instance.DescriptionItems;
		ItemStatsText = PlayerManager.instance.DescriptionStats;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	//Detect if the Cursor starts to pass over an item
	public void OnPointerEnter(PointerEventData pointerEventData)
	{
		//Active the Tooltip and set the name and the slot of the item
		ItemNameText.text = itemName;
		Tooltip.SetActive(true);

		//Set helmet stats
		if (itemID == 2)
		{
			ItemStatsText.text = "Health +" + itemStat.ToString();
		}

		//Set sword stats
		else if (itemID == 0)
		{
			ItemStatsText.text = "Damage +" + itemStat.ToString();
		}

		//Set armor stats
		else if(itemID == 1)
		{
			ItemStatsText.text = "Health +" + itemStat.ToString();
		}
	}

	//Detect when Cursor leaves the item
	public void OnPointerExit(PointerEventData pointerEventData)
	{
		//Deactivate the tooltip
		Tooltip.SetActive(false);
	}
}
