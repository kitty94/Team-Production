using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public bool hideShow;
	public bool isHiding;

	public bool isDetected;

	public bool gameOver;

	public Vector3 savePoint;

	public GUISkin skin;

	//Add by Kien
	public Rigidbody bombPrefab;		//Our PreFab
	public Transform bombStartPoint;	//Location where we want to shoot the bomb from
	public float throwPower = 10.0f;		//Power variable determines how fast this object will be shotout
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Sanity player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Sanity> ();
		if (player.currentSanity >= player.maxSanity) {
			GameOver();
		}

		//Add by Kien
		if(Input.GetKeyDown (KeyCode.B))
			ThrowBomb ();

		Debug.Log ("Spawn Point: " + savePoint);
	}

//	void UpdateGame(){
//		//Game.current.inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ().inventory;
//		Game.current.playerSanity = GameObject.FindGameObjectWithTag ("Player").GetComponent<Sanity> ().currentSanity;
//		//Game.current.savePoint = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().savePoint;
//
//	}

	void OnGUI (){
		DogCommands dog = GameObject.FindGameObjectWithTag ("Dog").GetComponent<DogCommands> ();
		if(hideShow == true)
		{
			if(isDetected) {
			GUI.Box(new Rect((Screen.width-150)/2, (Screen.height-20)/2, 150, 20), "The enemy sees you.");
			} else if (dog.switchOn){
			GUI.Box(new Rect((Screen.width-150)/2, (Screen.height-20)/2, 200, 20), "Make sure lights are off!");
			GUI.Box(new Rect((Screen.width-150)/2, (Screen.height+30)/2, 200, 20), "Input <Space> to disable lights.");
			} else
				GUI.Box(new Rect((Screen.width-150)/2, (Screen.height-20)/2, 150, 20), "Input <e> to Hide.");
			//GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "This is a title");
		}

		if(Input.GetKey (KeyCode.Tab)){
			Help ();
		}
	}

	public void Help(){
		GUI.Box(new Rect((Screen.width-150)/2, (Screen.height)/5, 250, 300), 
		        "---------------\n" +
		        "|    Help!    |\n" +
		        "---------------\n\n" +
		        "<w>: Moves Player Forward.\n" +
		        "<a>: Stafes Player to the Left\n" +
		        "<s>: Moves Player Backward.\n" +
		        "<d>: Strafes Player to the Right.\n" +
		        "<e>: Interactive Key.\n" +
		        "<Spacebar>: Turns on/off Dog Lights.");
	}

	public void GameOver(){
		if(!gameOver){
		gameOver = true;
		gameObject.AddComponent<GameOverScript>();
		}
	}

	public void ThrowBomb(){

		Rigidbody bombInstance;

		//bombStartPoint.position = this.transform.forward;

		bombInstance = Instantiate(bombPrefab, bombStartPoint.position, bombStartPoint.rotation) as Rigidbody;

		//bombInstance.AddForce (bombStartPoint.forward * throwPower);
		bombInstance.velocity = bombStartPoint.TransformDirection(Vector3.forward * throwPower);
	}
}