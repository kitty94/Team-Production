using UnityEngine;
using System.Collections;

public class Nature : MonoBehaviour {
	
	public int id;
	public bool dogLoot = false;
	Inventory inventory;

	void Start(){
		inventory = Inventory.Instance;
	}
	
	void OnTriggerStay(Collider col){

		if (col.gameObject.tag == "Player" && this.gameObject.tag == "Pickup"){
			if (Input.GetKeyDown(KeyCode.E)){

				// Checks if it has enough room
				if(Inventory.Instance.InventoryContains(-1)){
					//inventory.AddItem(id);
					Inventory.Instance.AddItem(id);
					// Delete the game object
					Destroy (this.gameObject);
				}
			}
		} else if(dogLoot && col.transform.tag == "Dog" && this.gameObject.tag == "Pickup"){

			if(inventory.InventoryContains(-1)){
				//inventory.AddItem(id);
				inventory.AddItem(id);
				// Delete the game object
				Destroy (this.gameObject);
			}
		}

		// Levels the object so that it isn't going through the ground
		if (col.gameObject.tag == "Ground") {
			Vector3 newPos = new Vector3(this.gameObject.transform.position.x,this.gameObject.transform.position.y+0.1f,this.gameObject.transform.position.z);
			this.gameObject.transform.position = newPos;
		}
	}
}
