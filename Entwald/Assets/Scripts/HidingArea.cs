using UnityEngine;
using System.Collections;

public class HidingArea : MonoBehaviour {
	public Camera mainCamera;
	public Camera hidingCamera;

	// Setting up Game Object named 'model' to search through and disable Mesh Renderers
	public GameObject model;
	public GameObject dogModel;

	private Player player;
	private DogCommands dog;
	// Use this for initialization
	void Start () {
		mainCamera.camera.enabled = true;
		this.hidingCamera.camera.enabled = false;

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		dog = GameObject.FindGameObjectWithTag ("Dog").GetComponent<DogCommands> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider col){
		if (col.gameObject.tag == "Player") {
			//Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			player.hideShow = true;
		}
	}

	void OnTriggerExit( Collider col){
		if (col.gameObject.tag == "Player") {
			//Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
			player.hideShow = false;
		}
	}
	void OnTriggerStay( Collider col ) {
		//Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		//DogCommands dog = GameObject.FindGameObjectWithTag ("Dog").GetComponent<DogCommands> ();
		if(col.gameObject.tag == "Player"){
			if(Input.GetKeyDown(KeyCode.E)) {
				if(!player.isHiding && !dog.switchOn && !player.isDetected) {
					player.isHiding = true;
					player.hideShow = false;
					//GameObject.FindGameObjectWithTag("Player").renderer.enabled = false;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;

					// Loop through all the children of 'dogModel' and disables the mesh renderer of the child of the specified location i
					for(int i=0; i < dogModel.transform.childCount; i++){
						
						// Find the GameObject named Cube then enables it.
						if(dogModel.transform.GetChild(i).name == "Cube"){
							dogModel.transform.GetChild(i).renderer.enabled = false; // Disables the renderer of each child of the model
						}
						
					}

					//GameObject.FindGameObjectWithTag("Dog").renderer.enabled = false;
					GameObject.FindGameObjectWithTag("Dog").GetComponent<DogCommands>().enabled = false;

					// Loop through all the children of 'model' and disables the mesh renderer of the child of the specified location i
					for(int i=0; i < model.transform.childCount; i++){
						
						// Skip the child named Armature. We are skipping this because the gameObject named Armature doesn't have a renderer
						if(model.transform.GetChild(i).name == "Armature"){
							Debug.Log ("Armature Skip");
						}else
							model.transform.GetChild(i).renderer.enabled = false; // Disables the renderer of each child of the model
						
					}

					//Cameras
					mainCamera.camera.enabled = false;
					hidingCamera.camera.enabled = true;
				} else if(player.isHiding) {
					//Disable Player
					//GameObject.FindGameObjectWithTag("Player").renderer.enabled = true;
					GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;

					// Loop through all the children of 'dogModel' and disables the mesh renderer of the child of the specified location i
					for(int i=0; i < dogModel.transform.childCount; i++){
						
						// Find the GameObject named Cube then enables it.
						if(dogModel.transform.GetChild(i).name == "Cube"){
							dogModel.transform.GetChild(i).renderer.enabled = true; // Disables the renderer of each child of the model
						}
						
					}
					GameObject.FindGameObjectWithTag("Dog").GetComponent<DogCommands>().enabled = true;
					// Loop through all the children of 'model' and enables the mesh renderer of the child of the specified location i
					for(int i=0; i < model.transform.childCount; i++){

						// Skip the child named Armature. We are skipping this because the gameObject named Armature doesn't have a renderer
						if(model.transform.GetChild(i).name == "Armature"){
							Debug.Log ("Armature Skip");
						}else
							model.transform.GetChild(i).renderer.enabled = true; // Enables the renderer of each child of the model

					}

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
