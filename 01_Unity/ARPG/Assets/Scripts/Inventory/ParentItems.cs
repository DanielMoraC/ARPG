using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentItems : MonoBehaviour
{

	private Transform _inventory;

    // Start is called before the first frame update
    void Start()
    {
		_inventory = PlayerManager.instance.Inventory;
		gameObject.transform.SetParent(_inventory);
	}
}
