using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OpenInv : MonoBehaviour {
	public Animator InventoryAnim;
	public GameObject Slots;

	// Use this for initialization
	void Start () {
		Slots.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			Slots.SetActive(true);
		}
		else if(Input.GetKeyUp(KeyCode.LeftShift))
		{
			Slots.SetActive(false);
		}
	}
}
