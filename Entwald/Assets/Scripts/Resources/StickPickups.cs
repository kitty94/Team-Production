// Attach script to sticks

using UnityEngine;
using System.Collections;

public class StickPickups : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		if (other.transform.tag == "Dog"){
			// Only take item if dog is close enough
			if (Vector3.Distance(this.transform.position, other.transform.position)<2.0f){
				// Add to inventory then remove object from scene
				// How do I add it to inventory?
				Destroy (this.gameObject);
			}
		}
	}
}
