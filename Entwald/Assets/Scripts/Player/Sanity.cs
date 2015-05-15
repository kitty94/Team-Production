using UnityEngine;
using System.Collections;

public class Sanity : MonoBehaviour {

	// Sanity Meter/ Fear Meter
	//public float sanityRate;
	// Use this for initialization
//	public float currentSanity;
//	public float maxSanity;
//	public float minSanity;
	public float sanityRate;
	private float enemyDistance;
	private float dogDistance;

	GameObject playerObj,enemyObj,dogObj;

	Player player;

	void Start () {
		playerObj = GameObject.FindGameObjectWithTag ("Player");
		enemyObj = GameObject.FindGameObjectWithTag ("Enemy");
		dogObj = GameObject.FindGameObjectWithTag ("Dog");
		player = Player.Instance;
	}
	
	// Update is called once per frame
	void Update () {

		if(player.isDetected && !player.isHiding)
		{
			increaseSanity();
		}
		else
		{
			depleteSanity();
		}
	
	}

	void OnGUI (){
		GUI.Box(new Rect(5, 5, 55, 25), "Sanity");
		GUI.Box(new Rect(65, 5, 55, 25), player.currentSanity.ToString("0") + "/" + player.maxSanity);
	}

	void increaseSanity(){
		if (this.sanityRate < player.maxSanity && player.isDetected && !player.gameOver && !player.isHiding) {
			player.currentSanity += Time.deltaTime * this.sanityRate;
		} 
	}

	void depleteSanity()
	{
		if (player.currentSanity >= 0) 
		{
			player.currentSanity -= Time.deltaTime * this.sanityRate;
		}

	}

	public void calculateSanityRate () {
		
		enemyDistance = Vector3.Distance (playerObj.transform.position, enemyObj.transform.position);
		dogDistance = Vector3.Distance (playerObj.transform.position, dogObj.transform.position);

		if(player.isDetected){
			if (enemyDistance <= 4 ) {
				sanityRate = 5;
			} else if (enemyDistance <= 7 && enemyDistance > 4) {
				sanityRate = 3;
			} else if (enemyDistance <= 10 && enemyDistance > 7) {
				sanityRate = 1;
			} else
				sanityRate = 0;
		} else {
			if (dogDistance <= 4 ) {
				sanityRate = 5;
			} else if (dogDistance <= 7 && dogDistance > 4) {
				sanityRate = 3;
			} else if (dogDistance <= 10 && dogDistance > 7) {
				sanityRate = 1;
			} else
				sanityRate = 0;
		}
		
		//Debug.Log (Vector3.Distance( player.transform.position,this.agent.transform.position));
	}

}
