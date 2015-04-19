using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour {

	public Vector3 checkpoint;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	

	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ();
			//checkpoint.transform.position = this.transform.position;
			checkpoint.x = this.transform.position.x;
			checkpoint.y = this.transform.position.y;
			checkpoint.z = this.transform.position.z;

			player.savePoint = checkpoint;
			Debug.Log ("Collided with: " + col.gameObject.name);
		}
	}
}
