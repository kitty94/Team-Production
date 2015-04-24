
using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour {

	private float rotateClamp = 30f; //setting how far the camera can rotate on the Y axis
	public Camera cam;


	void Start(){


		Screen.lockCursor = true; //Locking the cursor at the center point of the screen
		Screen.showCursor = false; //Hiding the cursor
	}

	public Texture2D Cross; //setting the picture for the cross arrow

	public float rotateSpeed = 5.0f;
	private float vertical = 0;

	void Update(){

		RaycastHit hit; //used to get information back from a raycast
		Ray ray = cam.ScreenPointToRay(Input.mousePosition); //setting up the ray line to be at the camera's location
		Debug.DrawRay(ray.origin, ray.direction, Color.red); //drawing the rayline at the origin and direction of the camera

	if(Input.GetMouseButton(0))
		{
			if (Physics.Raycast (ray, out hit, 1000)) 
			{
				Debug.Log ("Hit");
			} 
			else 
			{
				Debug.Log ("Miss");
			}

			Screen.lockCursor = true; //Locking the cursor at the center point of the screen
		}
	}
	

	void LateUpdate (){
		float horizontal;
		horizontal = transform.parent.localEulerAngles.y + Input.GetAxis("Mouse X") * rotateSpeed;
		
		vertical += Input.GetAxis("Mouse Y") * rotateSpeed;

		vertical = Mathf.Clamp (vertical, -rotateClamp, rotateClamp);
		transform.localEulerAngles = new Vector3 (-vertical, 0, 0);
		transform.parent.localEulerAngles = new Vector3 (0, horizontal, 0);
}

	void OnGUI()
	{
		float xMin = (Screen.width / 2) - (Cross.width / 2); //setting xMin(width of the screen) to be at the center
		float yMin = (Screen.height / 2) - (Cross.height / 2); //setting yMin to be at the center also
		GUI.DrawTexture(new Rect(xMin, yMin, Cross.width, Cross.height), Cross); //creating a GUI texture that will be at the center
	}
}