// Attach script to mushrooms

using UnityEngine;
using System.Collections;

public class MushroomPickup : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay(Collider other){
		if (other.transform.tag == "Player"){
			Debug.Log("In mushroom collider");
			// Press "E" to take item
			if (Input.GetKeyDown(KeyCode.E)){
				// Add to inventory then remove object from scene
				// How do I add it to inventory?
				Destroy (this.gameObject);
			}
		}
	}
}
