using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SanityVision : MonoBehaviour {

	MotionBlur visionBlur;
	Color sanityView;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Sanity player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Sanity> ();
		visionBlur = GameObject.Find("Main Camera").GetComponent<MotionBlur>();
		visionBlur.blurAmount =  0.01f * player.currentSanity;
		sanityView = new Color (1, 0, 0, 0.01f * player.currentSanity);
		GameObject.Find ("SanityCanvas").transform.GetChild (0).GetComponent<Image> ().color = sanityView;
	}
}
