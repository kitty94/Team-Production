using UnityEngine;
using System.Collections;

public class Zone : MonoBehaviour {
	//private float minimumTime;
	private float maxZoneTime = 10.0f; // Maximum Limit to stay in the Area
	private float zoneTime = 0; // Current Time you've been inside the area.
	public string area; // Name of the Zone/Area
	public bool enemyMoved = false;

	Player player;
	EnemyDetection enemy;

	void Start(){
		player = Player.Instance;
		enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyDetection>();
	}

	void Update(){
		Check ();
	}

	void calculateTime(){
		if(zoneTime < maxZoneTime){
				zoneTime += Time.deltaTime;
				Debug.Log(area +": " +zoneTime);
		}
	}

	void Check(){
		if ( zoneTime >= maxZoneTime && !enemyMoved){
			enemy.MoveToPlayer();
			enemyMoved = true;
		}
	}
	
	// Calculate the time
	void OnTriggerStay(Collider col ){
		if(col.transform.tag == "Player"){
			calculateTime();
		}
	}

	// Checks and change's Player's current Area name upon Enter.
	void OnTriggerEnter(Collider col){
		if(col.transform.tag == "Player"){
			Player.Instance.currentArea = area;
		}
	}


	// This is optional. Determined if Player can backtrack or not.
	void OnTriggerExit(Collider col){
		if(col.transform.tag == "Player"){
			zoneTime = 0.0f;
			enemyMoved = false;
		}
	}
}
