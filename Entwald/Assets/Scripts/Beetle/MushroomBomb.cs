using UnityEngine;
using System.Collections;

public class MushroomBomb : MonoBehaviour 
{
	/*
	 * Need a destory with a delay of 3 seconds
	 */
	public float radius 		= 5.0f;	//Provides a radius at which the explosive will effect the rigidbodies within it.
	public float power 		= 10.0f;	//The power of the explosive
	public float explosiveLift = 1.0f;  //Determines how the explosion reacts. The higher the value the higher the object will fly
	public float explosiveDelay= 3.0f;	//Adds a delay in seconds to our explosive
	private float Timer = 0.0f;

	void Start()
	{

	}//End of Start

	void Update()
	{

	}

	/*
	 * The collider detection is base on the beetle is within the zone.
	 * If so then we want to get the Beetle.cs component and change its state inRangeOfBomb = Tru
	 */

	IEnumerator waitDuration()
	{
		yield return new WaitForSeconds (explosiveDelay);

	}


	void OnCollisionStay(Collider col)
	{
		//if inside the trigger radius, change the beetle's inRangeOfBomb status.
		if (col.gameObject.tag == "Beetle") 
		{
			waitDuration ();
			Vector3 gernadeOrigin = transform.position;

			col.rigidbody.AddExplosionForce (power, gernadeOrigin, radius, explosiveLift);
			Destroy (gameObject);
			//Beetle beetle = GameObject.FindGameObjectWithTag("Beetle").GetComponent<Beetle>();
			//beetle.inRangeOfBomb = true;
		} 

	}


}
