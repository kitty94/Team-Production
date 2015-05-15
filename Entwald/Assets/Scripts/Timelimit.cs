using UnityEngine;
using System.Collections;

public class Timelimit : MonoBehaviour {
	//private float minimumTime;
	private float maxZoneTime = 60.0f;
	private float zoneTime = 0;

	// Use this for initialization
	void Start () {
		zoneTime += Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		calculateTime();
		Debug.Log (zoneTime);
	
	}

	void calculateTime(){
		do{
			zoneTime += Time.deltaTime;
		}while(zoneTime < maxZoneTime);
	}
}
