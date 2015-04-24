using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*----------------------------------
 * 			[Credits]
 * Name: Asset Generating Manager
 * By: Sinh Ho
 * Email: sinhho91@gmail.com
 * Phone: (925) 413-3784
 * Institution: Arts Institute of San Francisco CA
 * Class: Team Production I
 * Location: 1170 401
 * 
 * Date: 4/20/15
 *----------------------------------*/

public class AssetManager : MonoBehaviour {

	// Ground Dimensions
	[Tooltip("Ground Dimensions")]
	public float MinX, MaxX, MinZ, MaxZ;

	// List of Environmental Game Objects
	[Tooltip("GameObject List of Environment items. NOTE: Asset Count is dependent on the size of the array.")]
	public GameObject[] natureObjs;

	[Tooltip("This size corresponds to how many Nature Objects there are. Element 0 will determine the amount of the specified NatureObjs at Element 0")]
	public int[] natureCount;

	[Tooltip("When toggled, the maximumAssets will be all the count elements added up.")]
	public bool countOnly;

	// Maximum amount of models
	[Tooltip("Maximum amount of nature assets.")]
	public int maximumAssets;
	
	// Maximum Distance between each asset
	[Tooltip("The Maximum Distance between each asset.")]
	public float maximumDistance;

	[Tooltip("Layer mask on objects to not overlap.")]
	public LayerMask avoidLayer;

	// Nature Objects and List of Transforms
	[Tooltip("Transform List of GameObjects.")]
	public List<GameObject> naturePositions = new List<GameObject>();

	// Building Objects and Locators
	[Tooltip("GameObject List of Buildings.")]
	public GameObject[] buildingObjs;

	[Tooltip("Transform Array of Building Game Objects.")]
	public Transform[] buildingLocators;

//	[Tooltip("The MINIMUM amount of specified asset in the scene. Cannot add up to more than the Maximum Asset.")]
//	public int treeCount,bushCount,rockCount,mushroomCount;


	// Use this for initialization
	void Start () {

		// Generating Buildings
		StartCoroutine(GenerateBuildings());

		// Generating the environment
		StartCoroutine(GenerateEnvironment());

		//CountAssets (naturePositions);

	}
	
	private IEnumerator GenerateBuildings(){

		for (int i=0; i < buildingLocators.Length; i++) {

			// Creating and setting a new location by using the loop to assign each location to the gameObject specified in the list at position i.
			float x = buildingLocators[i].transform.position.x;
			float y = buildingLocators[i].transform.position.y;
			float z = buildingLocators[i].transform.position.z;

			// Referencing a new Vector3 to be used and changed to the variable 'location'
			Vector3 location = new Vector3(x,y,z);

			// Instantiates a new building object at LOCATION
			GameObject buildingObj = (GameObject)Instantiate(RandomAsset(buildingObjs),location,Quaternion.identity);

			// Childs the new item into Building GameObject
			buildingObj.transform.SetParent(GameObject.Find ("Buildings").transform);
		}

		yield return null;
	}

	private IEnumerator GenerateEnvironment(){

		while(naturePositions.Count < maximumAssets) {
			// Referencing a new Vector3 to be used and changed to the variable 'pos'
			Vector3 pos = new Vector3 (0, 0, 0);

			// Loops thru positions and checks if there are any available positions within 100 tries.
			for (int i= 0; i < 100; i++) {
				// Generates a new random position to be checked
				pos = new Vector3(Random.Range(MinX,MaxX),0.0f,Random.Range(MinZ,MaxZ));

				// Checks if the position made is overlapping any other objects within the list
				if (!CheckOverlap(pos)) break;
			}

			//Breaks of of the while loop if no more positions or is overlapping constantly
			if (CheckOverlap(pos)) break;

			// Checks if the new item intersects at layer mask position 9. If it doesn't, it creates the object
			if (!CheckIntersect(pos,avoidLayer)) {
				// Instantiates the object in the game world at POS position
				GameObject natureObj = (GameObject)Instantiate(RandomAsset(natureObjs,naturePositions,natureCount),pos,Quaternion.identity);

				// Inserts the new instantiated object's transform into the naturePositions list to check overlaps
				naturePositions.Add(natureObj);
				
				// Childs the new item into Environment GameObject
				natureObj.transform.SetParent(GameObject.Find ("Nature").transform);
				//CountAssets (naturePositions);
			}

			if(naturePositions.Count >= maximumAssets){
//				//CountAssets (naturePositions);
				Debug.Log (CountSpecificAsset(naturePositions,"Tree(Clone)"));
				Debug.Log (CountSpecificAsset(naturePositions,"WeedBush(Clone)"));
				Debug.Log (CountSpecificAsset(naturePositions,"Mushroom(Clone)"));
				Debug.Log (CountSpecificAsset(naturePositions,"Rock(Clone)"));
			}
			yield return null;
		}
	}

	private bool CheckOverlap (Vector3 pos){
		
		// For every item in the naturePositions list, check if their distance is less than 5.0
		foreach(GameObject t in naturePositions) {

			// Checking the distance between the item in position t of naturePosition with the position that was given to see if it's less than the 'maximumDistance'
			if (Vector3.Distance(t.transform.position, pos) < maximumDistance) {
				// Return true if it the condition value is less than maximumDistance
				return true;
			}
		}
		// Return true if it the condition value is greater than maximumDistance
		return false;
	}
	
	private bool CheckIntersect(Vector3 position, LayerMask layerMask){

		// Checking if the the Vector3 'position' has anything it's colliding with within a radius of 2.0f and 
		// if it the item it is colliding with has the specified layerMask.
		if(Physics.CheckSphere(position,2.0f,layerMask)){
			Debug.Log ("Intersects a Structure.");
			return true;
		}
		return false;
	}

	// Randomly Pick an Object from the GameObject array given.
	private GameObject RandomAsset(GameObject[] array, List<GameObject> list = null, int[] countList=null) {

		if (list != null ){
			// If there is a list given, it'll loop through depending on the size of the given array. ( GameObject[] array is an array of GameObjects ).
			for( int i=0; i<array.Length; i++){

				// Checks if the size of the countList array is smaller than the amount of gameObjects given into the GameObject array.
				if(countList != null ) {
					if(array.Length != countList.Length) {
						Debug.LogError("Random Asset Function, the array size of GameObjects is larger than the countList value.");
						return null;
					}

					if(countOnly){
						ToggleCountOnly(natureCount);
					}
				}

				// Checking if the amount of objects by the given name at position 'i' is less than the assetCount value at position 'i'.
				if(CountSpecificAsset(list,array[i].name+"(Clone)") < countList[i]){

					//if it is, then it'll find the position of the specified object and returns the GameObject at the specified position.
					return array[FindAssetPosition(array,array[i].name)];
				}
			}
		}
		// If the list is null then it'll default to randomizing from the array given by its size.
		return array[Random.Range (0, array.Length)];
	}

	private int FindAssetPosition(GameObject[] array, string name){

		// Declaring the variable 'position' to an int.
		int position = 0;

		//Loop through the given array list depending on the size of given list
		for(int i=0; i < array.Length; i++){

			// Checking if the information of the item at position 'i' of the array is equal to the given name.
			if(array[i].name == name) {

				//Assign the variable 'position', that was declared as an int, the value of 'i'.
				position = i;

				// End the loop because we found the position we wanted.
				break;
			}
		}

		// Return the specified information assigned to the variable 'position'
		return position;
	}


	private void ToggleCountOnly(int[] countList){
		maximumAssets = 0;
		for ( int i=0; i < countList.Length; i++){
			maximumAssets += countList[i];
		}
	}

	private int CountSpecificAsset(List<GameObject> list,string name){

		//Declaring the variable 'count' to an int.
		int count = 0;

		// Loop through the given list depending on the size of given list.
		for ( int i=0; i< list.Count; i++){

			// Checking the list if the information of the item at position 'i' of the list is equal to the given name.
			if(list[i].name == name){

				// Increment the value whenever condition is met.
				count++;
			}
		}
		//Debug.Log (count+ " "+name);

		// Return the specified information assigned to the variable 'count' 
		return count;
	}
}
	
