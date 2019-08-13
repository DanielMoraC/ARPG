using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

	public GameObject inventory;
	private bool inventoryEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.I) && !inventoryEnabled)
		{
			inventory.SetActive(true);
			inventoryEnabled = true;
		}
		else if (Input.GetKeyDown(KeyCode.I) && inventoryEnabled)
		{
			inventory.SetActive(false);
			inventoryEnabled = false;
		}
	}
}
