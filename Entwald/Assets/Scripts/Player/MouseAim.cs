using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseAim : MonoBehaviour {
	
	public Transform target;
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;
	
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;
	
	public float distanceMin = 1.0f;
	public float distanceMax = 20f;

	public float rotateSpeed = 5;

	
	float x = 0.0f;
	float y = 0.0f;

	//CursorLockMode wantedMode;
	
	// Apply requested cursor state
	void SetCursorState ()
	{
//		Cursor.lockState = wantedMode;
//		// Hide cursor when locking
//		Cursor.visible = (CursorLockMode.Locked != wantedMode);
	}
	
	// Use this for initialization
	void Start () {

//		Cursor.lockState = wantedMode;
//		wantedMode = CursorLockMode.Locked;
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

	}

	void LateUpdate () {
		if (target) {
			x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
			
			y = ClampAngle(y, yMinLimit, yMaxLimit);
			
			Quaternion rotation = Quaternion.Euler(y, x, 0);
			
			distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);
			
			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) {
				distance -=  hit.distance;
				//target.LookAt (hit.transform.position);
			}
			Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;
			
			transform.rotation = rotation;
			transform.position = position;

			float horizontal;

			horizontal = transform.parent.localEulerAngles.y + Input.GetAxis("Mouse X") * rotateSpeed;
		
			transform.parent.localEulerAngles = new Vector3 (0, horizontal, 0);


			
		}
		
	}
	void OnGUI ()
	{
//		GUILayout.BeginVertical ();
//		// Release cursor on escape keypress
//		if (Input.GetKeyDown (KeyCode.Escape))
//			Cursor.lockState = wantedMode = CursorLockMode.None;
//		
//		switch (Cursor.lockState)
//		{
//		case CursorLockMode.None:
//			GUILayout.Label ("Cursor is normal");
//			if (GUILayout.Button ("Lock cursor"))
//				wantedMode = CursorLockMode.Locked;
//			if (GUILayout.Button ("Confine cursor"))
//				wantedMode = CursorLockMode.Confined;
//			break;
//		case CursorLockMode.Confined:
//			GUILayout.Label ("Cursor is confined");
//			if (GUILayout.Button ("Lock cursor"))
//				wantedMode = CursorLockMode.Locked;
//			if (GUILayout.Button ("Release cursor"))
//				wantedMode = CursorLockMode.None;
//			break;
//		case CursorLockMode.Locked:
//			GUILayout.Label ("Cursor is locked");
//			if (GUILayout.Button ("Unlock cursor"))
//				wantedMode = CursorLockMode.None;
//			if (GUILayout.Button ("Confine cursor"))
//				wantedMode = CursorLockMode.Confined;
//			break;
//		}
//		
//		GUILayout.EndVertical ();
//		
//		SetCursorState ();
	}

	
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
	
	
}