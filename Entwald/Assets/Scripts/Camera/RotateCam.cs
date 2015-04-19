
using UnityEngine;
using System.Collections;

public class RotateCam : MonoBehaviour {
	
	
	public float rotateSpeed = 5.0f;
	private float vertical = 0;
	
	void  Start (){
		
	}
	
	void  Update (){
		
	}
	
	void  LateUpdate (){
		// Rotate cam based on mouse movement
		float horizontal;
		horizontal = transform.parent.localEulerAngles.y + Input.GetAxis("Mouse X") * rotateSpeed;
		vertical += Input.GetAxis("Mouse Y") * rotateSpeed;

		// Changes the player rotation based on camera rotation
		transform.localEulerAngles = new Vector3 (-vertical, 0, 0);
		transform.parent.localEulerAngles = new Vector3 (0, horizontal, 0);
	}

}