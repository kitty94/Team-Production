using UnityEngine;
using System.Collections;

public class EnemyDetection : MonoBehaviour {
	
	
	RaycastHit hitInfo;
	Vector3[] direction = new Vector3[5];//Array of Raycasts 
	public float rayDist = 10.0f;//Length of raycast
	public float rayGameOverDist = 5.0f;//Distance where the player triggers gameover
	
	// Update is called once per frame
	void Update () {

		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		AIState state = GetComponent<AIState> ();
		if(!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().gameOver)//if gameOVer isnt true then Raycasting will not be called.
			Raycasting ();//Stops raycasting

		if(player.isHiding){
			state.Wander();
		}
		else
		{
			state.Follow();

		}


		//Debug.Log ("I hit: " + " "+hitInfo.collider.name+" ");
		
	}

	void Raycasting()
	{
		//These are the directions of our raycast
		this.direction [0] = transform.TransformDirection (new Vector3 (-1f, 0f, 3)) * 3;
		this.direction [1] = transform.TransformDirection (new Vector3 (-.5f, 0f, 3)) * 3;
		this.direction [2] = transform.TransformDirection (new Vector3 (0f, 0f, 3)) * 3;
		this.direction [3] = transform.TransformDirection (new Vector3 (.5f, 0f, 3)) * 3;
		this.direction [4] = transform.TransformDirection (new Vector3 (1f, 0f, 3)) * 3;

		
		//Shows the raycast lines in game
		Debug.DrawRay (new Vector3 (transform.position.x - .2f, transform.position.y, transform.position.z), direction [0], Color.green);
		Debug.DrawRay (transform.position, direction [1], Color.red);
		Debug.DrawRay (transform.position, direction [2], Color.blue);
		Debug.DrawRay (transform.position, direction [3], Color.yellow);
		Debug.DrawRay (new Vector3 (transform.position.x + .2f, transform.position.y, transform.position.z), direction [4], Color.green);
		
		//checks to see raycast hits the player
		for (int i = 0; i < direction.Length; i++) 
		{ 
			// Changed size to direction.Length, this function tells you what the array size of whatever variable is before it.
			if (Physics.Raycast (transform.position, this.direction [i], out hitInfo, rayDist)) 
			{
				Debug.Log ("COLLIDED WITH: " + hitInfo.collider.name);
				//if the monster finds the player
				if (this.hitInfo.collider.tag == "Player" && this.hitInfo.collider.tag != "Wall") 
				{
					//checks to see if your close enough to the player
					if (hitInfo.distance <= rayGameOverDist) 
					{
						//AIState other = GetComponent<AIState> ();
						//GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().GameOver ();//calls the gameover script
					}
				}
			}
		}
	}

	void OnTriggerStay(Collider col)
	{
		//if inside the trigger radius, sanity increases
		if(col.gameObject.tag == "Player")
		{
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			player.isDetected = true;

			Sanity sanity = GameObject.FindGameObjectWithTag ("Player").GetComponent<Sanity> ();
			sanity.calculateSanityRate ();
			// Or call Game Over
			//GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().GameOver();
			
		} 
	}

	void OnTriggerExit(Collider col)
	{
		//if outside the trigger radius, sanity stops
		if(col.gameObject.tag == "Player")
		{
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			player.isDetected = false;

		}

	}


}