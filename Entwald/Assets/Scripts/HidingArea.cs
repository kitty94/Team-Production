using UnityEngine;
using System.Collections;

public class HidingArea : MonoBehaviour {
	public Camera mainCamera;
	public Camera hidingCamera;

	// Use this for initialization
	void Start () {
		mainCamera.camera.enabled = true;
		this.hidingCamera.camera.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider col){
		if (col.gameObject.tag == "Player") {
			Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			player.hideShow = true;
		}
	}

	void OnTriggerExit( Collider col){
		if (col.gameObject.tag == "Player") {
			Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			player.hideShow = false;
		}
	}
	void OnTriggerStay( Collider col ) {
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		DogCommands dog = GameObject.FindGameObjectWithTag ("Dog").GetComponent<DogCommands> ();
		if(col.gameObject.tag == "Player"){
			if(Input.GetKeyDown(KeyCode.E)) {
				if(!player.isHiding && !dog.switchOn && !player.isDetected) {
					player.isHiding = true;
					player.hideShow = false;
					GameObject.FindGameObjectWithTag("Player").renderer.enabled = false;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
					GameObject.FindGameObjectWithTag("Dog").renderer.enabled = false;
					GameObject.FindGameObjectWithTag("Dog").GetComponent<DogCommands>().enabled = false;


					//Cameras
					mainCamera.camera.enabled = false;
					hidingCamera.camera.enabled = true;
				} else if(player.isHiding) {
						//Disable Player
						GameObject.FindGameObjectWithTag("Player").renderer.enabled = true;
						GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
						GameObject.FindGameObjectWithTag("Dog").renderer.enabled = true;
						GameObject.FindGameObjectWithTag("Dog").GetComponent<DogCommands>().enabled = true;
						player.hideShow = false;

							//Cameras
						mainCamera.camera.enabled = true;
						hidingCamera.camera.enabled = false;
							//Booleans
						player.isHiding = false;
					}
				}
			}
		}


}
