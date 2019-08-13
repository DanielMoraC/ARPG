using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

	public GameObject spritePrefab;
	private Transform _inventory;

    // Start is called before the first frame update
    void Start()
    {
		_inventory = PlayerManager.instance.Inventory;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void GetItem()
	{
		Instantiate(spritePrefab);
	}
}
