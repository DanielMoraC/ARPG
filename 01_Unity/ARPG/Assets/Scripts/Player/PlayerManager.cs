using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
	#region Singleton

	public static PlayerManager instance;

	void Awake()
	{
		instance = this;
	}

	#endregion

	public GameObject Camera;

	[Header("Enemy")]
	public GameObject Player;

	[Header("Item Tooltip")]
	public GameObject DescriptionItems;
	public Text DescriptionNames;
	public Text DescriptionStats;

	[Header("Item Slots")]
	public Transform HelmetSlot;
	public Transform WeaponSlot;
	public Transform ArmorSlot;
	public Transform Inventory;
}
