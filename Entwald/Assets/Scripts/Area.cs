using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*----------------------------------
 * 			[Credits]
 * Name: Blockade Manager
 * By: Sinh Ho
 * Email: sinhho91@gmail.com
 * Phone: (925) 413-3784
 * Institution: Arts Institute of San Francisco CA
 * Class: Team Production I
 * Location: 1170 401
 * 
 * Date: 5/14/15
 *----------------------------------*/

public class Area : MonoBehaviour {

	// Using this ID to tell which area we're working with.
	public int areaID;

	// Object to Instantiate
	public GameObject[] obstacle;

	// List to store the already generated blockades
	public List<GameObject> blockList = new List<GameObject>();

	// The array of routes that can be taken.
	public Transform[] route;

	// The Amont of blockades that will be generated.
	public int amount;

	// Use this for initialization
	void Start () {

		// Randomize amount of blockades to be created
		amount = Random.Range (1, this.route.Length);

		// Added this line because Random Generator sucks.
		// Makes sure that there will be atleast 1 Route open.
		if (amount >= route.Length) amount = route.Length - 1;

		// Create the blockades
		CreateBlockade ();
		
	}

	void CreateBlockade(){
		Debug.Log ("Creating Blockade");

		// Declaring and assigning a bool to be checked later.
		bool create = true;

		for(int i=0; i < amount; i++ ) {

			// Checks if there is still room to create another blockade by 
			// seeing if the list.Count is still less than the amount of blockades
			if (blockList.Count < amount) {

				// Re-sets and Randomizes the position.
				int position = RandomRoutePosition();

				//position = RandomizePosition();
				for(int j=0; j < blockList.Count; j++){

					// Checks to see if the position is taken already or not.
					if(route[position].position == blockList[j].transform.position) {

						// If it is taken, decrement i to redo the process until there is an open spot.
						i--;
						// Disables Creation of the Instance
						create = false;
						break;
					}
					create = true;
				}


				// Creating the instance and adding to the list.
				if(create){
					GameObject block = (GameObject)Instantiate (RandomBlockadeObject(), route[position].position, Quaternion.identity);
					blockList.Add (block);

					// Parenting the new instantiated object under it's corresponding route. A#(area ID) Route #(position)
					block.transform.SetParent(GameObject.Find ("A"+areaID+ " Route "+ position).transform);

					// Can also use this to just parent under the Area
					//block.transform.SetParent (this.transform);
				}
			}
		}
	}

	// Picks random locations depending on the size of the Route array.
	int RandomRoutePosition(){
		return Random.Range (0, route.Length);
	}

	GameObject RandomBlockadeObject(){
		return obstacle[Random.Range (0,obstacle.Length)];
	}
}
