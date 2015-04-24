using UnityEngine;
using System.Collections;

public class stickTest : MonoBehaviour {

	public float stickdurability = 100;
	private float currentDurability;
	private float durabilityDamage = 10;

	public KeyCode stickAttack;

	public GameObject TestStick;

	public GameObject OwlTest;
	

	// Use this for initialization
	void Start () {
		//OwlTest.gameObject = new durabilityDamage;

		currentDurability = stickdurability;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (stickAttack)) {
						TestStick.transform.Rotate (0, 90, 0);
				}


		if (currentDurability == 0) {
			
			Destroy(this.gameObject);
			
		}
		
	}
	
	//void OnTriggerEnter(Collision stickHit){
	void OnTriggerEnter(Collider stickHit){
		if (stickHit.gameObject.tag == "Owl") {
						//currentDurability = stickdurability - durabilityDamage;
			currentDurability -= durabilityDamage;
				}
		Debug.Log (currentDurability);
		
	}
}
