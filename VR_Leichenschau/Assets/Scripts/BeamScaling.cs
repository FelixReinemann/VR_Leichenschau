using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScaling : MonoBehaviour {

	RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(transform.parent.position,transform.parent.forward,out hit)){
			transform.localScale = new Vector3(transform.localScale.x,(transform.parent.position-hit.point).magnitude/1.8f,transform.localScale.z);
			transform.localPosition = new Vector3(0,0,(transform.parent.position - hit.point).magnitude/1.8f);
		}
	}
}
