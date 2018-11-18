using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeAllChildComponents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UpdateMyRigidsArray();
		UpdateMyRigidsData();
	}
	
	Rigidbody[] myRigids;
	public float mass=1f, drag, dragTorque;

	void Update () {
		if(myRigids.Length==0)return;
		if(mass!=myRigids[0].mass || drag != myRigids[0].drag || dragTorque != myRigids[0].angularDrag){
			UpdateMyRigidsData();
		}
	}

	void UpdateMyRigidsArray(){
		myRigids = GetComponentsInChildren<Rigidbody>();
	}

	void UpdateMyRigidsData(){
		foreach(Rigidbody rig in myRigids){
			rig.mass = mass;
			rig.drag = drag;
			rig.angularDrag = dragTorque;
		}
	}
}
