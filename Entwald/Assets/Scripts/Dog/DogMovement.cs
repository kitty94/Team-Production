using UnityEngine;
using System.Collections;

public class DogMovement : MonoBehaviour {

	public float speed = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)){
			transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}
	}
}
