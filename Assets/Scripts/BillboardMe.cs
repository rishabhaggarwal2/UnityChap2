using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (GameObject.Find ("Player").transform.position);
//		transform.Rotate (0, 100, 0);
	}
}
