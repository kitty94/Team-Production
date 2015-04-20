﻿using UnityEngine;
using System.Collections;

// Attach script to the dog
// Allows player to click a location and the object will move towards that location

// Add component Nav Mesh Agent to the dog

public class DogCommands : MonoBehaviour {
	
	NavMeshAgent agent; // Variable to store NavMeshAgent
	Animator lightAnim;
	GameObject dogLight;
	public bool switchOn = true;
	public bool turnOn = true;

	void Start () {
		agent = this.GetComponent<NavMeshAgent>();
		dogLight = GameObject.FindGameObjectWithTag("Light");
		lightAnim = dogLight.GetComponent<Animator>();
	}
	
	void Update () {
		// The player can left click somewhere to move the dog

		if (Input.GetMouseButtonDown(0)){
			// Raycasts are used to find location to send object
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			// If the raycast hit something, move agent towards that location
			if (Physics.Raycast(ray, out hit, 100)){
				
				// If raycast hits a moveable object and dog is close to object, the object is parented to dog
				if (hit.collider.gameObject.tag == "Moveable"){
					agent.SetDestination(hit.point);
					if (Vector3.Distance(this.transform.position, hit.collider.transform.position)<2){
						hit.collider.transform.parent = this.transform;
					}
				}
				else{
					agent.SetDestination(hit.point);
				}
			}
		}
		
		
		// Right click to call dog back to the player
		if (Input.GetMouseButtonDown(1)){
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			agent.SetDestination(player.transform.position);
		}
		
		
		
		// Light fades in/out
		if (Input.GetKeyDown(KeyCode.Space)){
			// Turns true bool false, and false bool true everytime there's an input
			switchOn = !switchOn;
			if (switchOn){
				lightAnim.Play("TurningOn");
			}
			if (!switchOn){
				lightAnim.Play("TurningOff");
			}
		}
		
		
		// Light turns on/off immediately
		if (Input.GetKeyDown(KeyCode.B)){
			// Turns true bool false, and false bool true everytime there's an input
			turnOn = !turnOn;
			if (turnOn){
				lightAnim.Play("On");
			}
			if(!turnOn){
				lightAnim.Play("Off");
			}
		}
	}


	void OnTriggerStay( Collider col){
		Sanity sanity = GameObject.FindGameObjectWithTag ("Player").GetComponent<Sanity> ();
		if(col.gameObject.tag == "Player"){
			sanity.calculateSanityRate();
		}
	}
}
