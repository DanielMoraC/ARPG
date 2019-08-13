using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	//1 = Weapon 2 = Helmet 3 = Armor
	public int IDItem;

	public bool possible = false;
	public bool possibleI = true;

	private bool _destroy = false;

	public Transform HelmetSlot;
	public Transform WeaponSlot;
	public Transform ArmorSlot;
	public Transform RandomSlot;

	Vector3 startPosition;
	Transform startParent;

	void Start()
	{
		HelmetSlot = PlayerManager.instance.HelmetSlot;
		WeaponSlot = PlayerManager.instance.WeaponSlot;
		ArmorSlot = PlayerManager.instance.ArmorSlot;
	}

	//When the player clicks on an item the position and the initial parent is set
	public void OnBeginDrag(PointerEventData eventData){
		startPosition = transform.position;
		startParent = transform.parent;
	}

	//When the player is moving the item it follows the mouse
	public void OnDrag(PointerEventData eventData){
		transform.position = Input.mousePosition;
	}

	//when the player releases the click if the item is on the inventory or in its slot and there is space the object is placed where you leave it
	public void OnEndDrag(PointerEventData eventData){

		Detector slotH = HelmetSlot.GetComponent<Detector>();
		Detector slotW = WeaponSlot.GetComponent<Detector>();
		Detector slotA = ArmorSlot.GetComponent<Detector>();

		//Helmet
		if (IDItem == 2)
		{
			//Equip
			if (!possible)
			{
				transform.position = startPosition;
			}
			else if (possible && slotH.maximoH == 1)
			{
				gameObject.transform.SetParent(HelmetSlot);
			}

			//Inventory
			if (!possibleI)
			{
				transform.position = startPosition;
			}
			else if (possibleI)
			{
				gameObject.transform.SetParent(RandomSlot);
			}

			//Destroy
			if (_destroy)
			{
				Destroy(gameObject);
			}
		}

		//Weapon
		else if (IDItem == 1)
		{
			//Equip
			if (!possible)
			{
				transform.position = startPosition;
			}
			else if (possible && slotW.maximoW == 1)
			{
				gameObject.transform.SetParent(WeaponSlot);
			}

			//Inventory
			if (!possibleI)
			{
				transform.position = startPosition;
			}
			else if (possibleI)
			{
				gameObject.transform.SetParent(RandomSlot);
			}

			//Destroy
			if (_destroy)
			{
				Destroy(gameObject);
			}
		}

		//Armor
		else if (IDItem == 3)
		{
			//Equip
			if (!possible)
			{
				transform.position = startPosition;
			}
			else if (possible && slotA.maximoA == 1)
			{
				gameObject.transform.SetParent(ArmorSlot);
			}

			//Inventory
			if (!possibleI)
			{
				transform.position = startPosition;
			}
			else if (possibleI)
			{
				gameObject.transform.SetParent(RandomSlot);
			}

			//Destroy
			if (_destroy)
			{
				Destroy(gameObject);
			}
		}
	}

	//To know where the item is placed and can be placed
	public void OnTriggerEnter2D(Collider2D collision)
	{
		//Helmet
		if (IDItem == 2)
		{
			if (collision.gameObject.tag == "Helmet")
			{
				possible = true;
			}
			else if (collision.gameObject.tag == "Inventory")
			{
				possibleI = true;
				RandomSlot = collision.gameObject.transform;
			}
			else if (collision.gameObject.tag == "Bin")
			{
				_destroy = true;
			}
		}

		//Weapon
		else if (IDItem == 1)
		{
			if (collision.gameObject.tag == "Weapon")
			{
				possible = true;
			}
			else if (collision.gameObject.tag == "Inventory")
			{
				possibleI = true;
				RandomSlot = collision.gameObject.transform;
			}
			else if (collision.gameObject.tag == "Bin")
			{
				_destroy = true;
			}
		}

		//Armor
		else if (IDItem == 3)
		{
			if (collision.gameObject.tag == "Armor")
			{
				possible = true;
			}
			else if (collision.gameObject.tag == "Inventory")
			{
				possibleI = true;
				RandomSlot = collision.gameObject.transform;
			}
			else if (collision.gameObject.tag == "Bin")
			{
				_destroy = true;
			}
		}
	}

	//To know where the item is placed and can be placed
	public void OnTriggerExit2D(Collider2D collision)
	{
		//Helmet
		if (IDItem == 2)
		{
			if (collision.gameObject.tag == "Helmet")
			{
				possible = false;
			}
			else if (collision.gameObject.tag == "Inventory")
			{
				possibleI = false;
			}
			else if (collision.gameObject.tag == "Bin")
			{
				_destroy = false;
			}
		}

		//Sword
		else if (IDItem == 1)
		{
			if (collision.gameObject.tag == "Weapon")
			{
				possible = false;
			}
			else if (collision.gameObject.tag == "Inventory")
			{
				possibleI = false;
			}
			else if (collision.gameObject.tag == "Bin")
			{
				_destroy = false;
			}
		}

		//Armor
		else if (IDItem == 3)
		{
			if (collision.gameObject.tag == "Armor")
			{
				possible = false;
			}
			else if (collision.gameObject.tag == "Inventory")
			{
				possibleI = false;
			}
			else if (collision.gameObject.tag == "Bin")
			{
				_destroy = false;
			}
		}
	}

}
