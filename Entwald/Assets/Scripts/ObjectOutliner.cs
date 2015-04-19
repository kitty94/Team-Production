using UnityEngine;
using System.Collections;

public class ObjectOutliner : MonoBehaviour {

	public Material outlinerMat;
	public Material normalMat;
	public string objectTag;
	public GameObject[] objects;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == objectTag) {

			for(int i=0; i < objects.Length; i++){
				objects[i].renderer.material = outlinerMat;
			}
		}
	}

	void OnTriggerExit(Collider col) {
		if (col.gameObject.tag == objectTag) {
			for(int i=0; i < objects.Length; i++){
				objects[i].renderer.material = normalMat;
			}
		}
	}
}
