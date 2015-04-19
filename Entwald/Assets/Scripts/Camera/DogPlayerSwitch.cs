﻿using UnityEngine;
using System.Collections;

public class DogPlayerSwitch : MonoBehaviour {

	// Attach script to player
	// Change cameras when player switches characters
	
	public Movement playerScript;
	public DogMovement dogScript;

	//public Transform player;
	//public Transform dog;

	public Camera playerCam;
	public Camera dogCam;

	bool activePlayer = true; // if player is controlling the boy activePlayer = true
							  // if player is controlling dog activePlayer = false

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			activePlayer = !activePlayer;
		}
		if (activePlayer){
			// Disable dog script and camera
			dogScript.enabled = false;
			dogCam.enabled = false;

			// Enable player script and camera
			playerScript.enabled = true;
			playerCam.enabled = true;
		}
		else if (!activePlayer){
			// Disable player script and camera
			playerScript.enabled = false;
			playerCam.enabled = false;

			// Enable dog script and camera
			dogScript.enabled = true;
			dogCam.enabled = true;
		}
	}
}
