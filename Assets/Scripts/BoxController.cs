using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{

	Vector3 otherBox;
	private Vector3 position;
	private bool distanceChanged = false;
	private int getDis = 0;
	public float moveRate = 0.005f;
	public int limitDistance = 10;
	// Use this for initialization
	void Start ()
	{
		// position = this.transform.position;	
	}
	
	// Update is called once per frame
	void Update ()
	{
        
		if (Input.GetKey (KeyCode.RightArrow)) {
			this.transform.Translate (Vector3.right * moveRate * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			this.transform.Translate (Vector3.left * moveRate * Time.deltaTime);
		}


		//this.transform.Translate(Vector3.right * Vector3.Distance(this.transform.position, otherBox) * Time.deltaTime);
		// getDis = Ardunity.HCSR.measuredDistance;
		//if (getDis > limitDistance) return;
		/*
		Debug.Log (Ardunity.HCSR.measuredDistance.ToString ());
		if (Ardunity.HCSR.measuredDistance > 0 && Ardunity.HCSR.measuredDistance < 10) {
			this.transform.position = Vector3.Lerp (this.transform.position, Vector3.left * (getDis / 5), Time.deltaTime * 1f);
		} else if (Ardunity.HCSR.measuredDistance > 10 && Ardunity.HCSR.measuredDistance < 20) {
			this.transform.position = Vector3.Lerp (this.transform.position, Vector3.right * (getDis / 5), Time.deltaTime * 1f);
		} else
			return;
        */
	}
}
