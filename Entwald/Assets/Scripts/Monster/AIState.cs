using UnityEngine;
using System.Collections;

public class AIState : MonoBehaviour {

	//State Machine
	public bool followState;
	public bool pursueState;
	public bool wanderState;

	// Wander Variables
	public bool walking;
	private float stopTime;
	public float maxStopTime;
	public float maxWalkDistance;

	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		//Pursue ();
		agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.pursueState) {
			Pursue ();
		} else if (this.followState) {
			Follow ();
		} else if (this.wanderState) {
			Wander ();
		} else
			Idle ();
	
	}

	public void Pursue() {
		this.agent.speed = 10;
		this.agent.stoppingDistance = 2;

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		this.agent.SetDestination (player.transform.position);

		pursueOn ();
		followOff ();
		wanderOff ();
		}

	public void Follow(){
		this.agent.speed = 7;
		this.agent.stoppingDistance = 20;

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		this.agent.SetDestination (player.transform.position);

		followOn ();
		pursueOff ();
		wanderOff ();
	}

	public void Idle(){
		this.agent.Stop (true);
		followOff ();
		pursueOff ();
	}

	public void Wander()
	{
		this.agent.speed = 7;
		this.agent.stoppingDistance = 3;
		if (!walking) {
			Vector3 direction = Random.insideUnitSphere * maxWalkDistance;
			direction += transform.position;
			NavMeshHit hit;
			NavMesh.SamplePosition (direction, out hit, Random.Range (0f, maxWalkDistance), 1);
			
			Vector3 destination = hit.position;
			this.agent.SetDestination (destination);
			this.walking = true;
		}

		if (agent.remainingDistance < agent.stoppingDistance) {
			if(stopTime >= maxStopTime){
				this.walking = false;
				this.stopTime = 0;
			}
			this.stopTime += Time.deltaTime;
		}
		wanderOn ();
		followOff ();
		pursueOff ();
		//Debug.Log (stopTime);
	}

	public void pursueOn() {
		this.pursueState = true;
	}

	public void pursueOff() {
		this.pursueState = false;
	}

	public void wanderOn() {
		this.wanderState = true;
	}
	
	public void wanderOff() {
		this.wanderState = false;
	}

	public void followOn(){
		this.followState = true;
	}

	public void followOff(){
		this.followState = false;
	}
	
}
