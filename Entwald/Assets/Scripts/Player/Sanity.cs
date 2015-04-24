using UnityEngine;
using System.Collections;

public class Sanity : MonoBehaviour {

	// Sanity Meter/ Fear Meter
	//public float sanityRate;
	// Use this for initialization
	public float currentSanity;
	public float maxSanity;
	public float minSanity;
	public float sanityRate;
	private float enemyDistance;
	private float dogDistance;

	GameObject player,enemy,dog;
	Sanity sanity;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
		dog = GameObject.FindGameObjectWithTag ("Dog");
		sanity = GameObject.FindGameObjectWithTag ("Player").GetComponent<Sanity> ();
	}
	
	// Update is called once per frame
	void Update () {
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();

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
		GUI.Box(new Rect(65, 5, 55, 25), this.currentSanity.ToString("0") + "/" + this.maxSanity);
	}

	void increaseSanity(){
		Player player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		if (this.sanityRate < maxSanity && player.isDetected && !player.gameOver && !player.isHiding) {
			this.currentSanity += Time.deltaTime * this.sanityRate;
		} 
	}

	void depleteSanity()
	{
		if (currentSanity >= 0) 
		{
			currentSanity -= Time.deltaTime * this.sanityRate;
		}

	}

	public void calculateSanityRate () {
		
		enemyDistance = Vector3.Distance (player.transform.position, enemy.transform.position);
		dogDistance = Vector3.Distance (player.transform.position, dog.transform.position);

		if(player.GetComponent<Player>().isDetected){
			if (enemyDistance <= 4 ) {
				sanity.sanityRate = 5;
			} else if (enemyDistance <= 7 && enemyDistance > 4) {
				sanity.sanityRate = 3;
			} else if (enemyDistance <= 10 && enemyDistance > 7) {
				sanity.sanityRate = 1;
			} else
				sanity.sanityRate = 0;
		} else {
			if (dogDistance <= 4 ) {
				sanity.sanityRate = 5;
			} else if (dogDistance <= 7 && dogDistance > 4) {
				sanity.sanityRate = 3;
			} else if (dogDistance <= 10 && dogDistance > 7) {
				sanity.sanityRate = 1;
			} else
				sanity.sanityRate = 0;
		}
		
		//Debug.Log (Vector3.Distance( player.transform.position,this.agent.transform.position));
	}

}
