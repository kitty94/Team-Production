using UnityEngine;
using System.Collections;

public class MouseCam : MonoBehaviour {

	public float sensitivity = 10f; //How fast the camera will move when pushed to the side
	public Transform CameraTarget; //Variable for what the camera will follow
	private Transform MyTransform;
	public float CameraDistance = 3.0f; //setting how far the camera will be from the player on the Z axis
	public float CameraHeight = 1.0f; //setting how high the camera will be on the Y axis

	// Use this for initialization
	void Start () {
	
		MyTransform = transform;

	}
	
	// Update is called once per frame
	void Update () {


		//MyTransform.LookAt (CameraTarget);
		//transform.Translate(Vector3.right * Time.deltaTime);

		if (Input.mousePosition.x < sensitivity) 
		
		{ //checks how fast the screen will move based on how high the sensitivy number is

			transform.Rotate (-Vector3.up * Time.deltaTime * Mathf.Abs (sensitivity - Input.mousePosition.x));


		}

		else if (Input.mousePosition.x > Screen.width - sensitivity) 

		{
			transform.Rotate (Vector3.up * Time.deltaTime * Mathf.Abs (Screen.width - Input.mousePosition.x));
		}

//		if (Input.mousePosition.y < sensitivity) 
//
//		{
//			transform.Rotate (-Vector3.left * Time.deltaTime * Mathf.Abs (sensitivity - Input.mousePosition.y));
//		}
//
//		else if (Input.mousePosition.y > Screen.height - sensitivity) 
//
//		{
//			transform.Rotate (Vector3.left * Time.deltaTime * Mathf.Abs (Screen.height - Input.mousePosition.y));
//		}

	}

	void LateUpdate(){

		MyTransform.position = new Vector3 (CameraTarget.position.x,CameraTarget.position.y + CameraHeight,CameraTarget.position.z - CameraDistance); //setting up the camera's height, and the distance from the target

		CameraTarget.eulerAngles = new Vector3(transform.rotation.x,transform.eulerAngles.y,transform.eulerAngles.z); //Will spin the object along with the camera's angle

	}
}
