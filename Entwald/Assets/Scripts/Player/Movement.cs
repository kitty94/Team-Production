using UnityEngine;
using System.Collections;

// A terrible, terrible, TERRIBLE script to use movement for a character - Cory :)
//just use getAxis Horizontal and Vetical for movement

public class Movement : MonoBehaviour {

	public KeyCode up; //setting up the attribute "up" with keycodes so user can choose what key to use in the Inspector
	public KeyCode down; //setting up the attribute "down" with keycodes so user can choose what key to use in the Inspector
	public KeyCode left; //setting up the attribute "left" etc...
	public KeyCode right; //setting up the attribute "right" etc..
	public float speed = 10.0f; //Setting up the speed variable

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.LeftShift)) {

			speed = 15f;

		}

		else if(Input.GetKeyDown (KeyCode.LeftControl)) {

			speed = 5f;

		}

		else {

			speed = 10f;
		}
		
		if (Input.GetKey(up)) {

			transform.Translate(Vector3.forward * Time.deltaTime * speed);

		}
	
		if (Input.GetKey(down)) {
			
			transform.Translate(Vector3.forward * Time.deltaTime * -speed);
			
		}

		if (Input.GetKey(left)) {
			
			transform.Translate(Vector3.left * Time.deltaTime * speed);
			
		}

		if (Input.GetKey(right)) {
			
			transform.Translate(Vector3.right * Time.deltaTime * speed); 
			
		}
	}
}
