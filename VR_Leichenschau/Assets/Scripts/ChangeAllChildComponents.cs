using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeAllChildComponents : MonoBehaviour {

	// Use this for initialization
	public bool changeAllRigidBodies=true;
	public bool changeAllGrabableObjects=false;
	public bool grabableScriptsAreBodyParts=true;
	void Start () {
		UpdateMyRigidsArray();
		if(changeAllRigidBodies)
			UpdateMyRigidsData();
	}
	
	Rigidbody[] myRigids;
	public float mass=1f, drag, dragTorque;

	void Update () {
		if(myRigids.Length>0 && changeAllRigidBodies)
			if(mass!=myRigids[0].mass || drag != myRigids[0].drag || dragTorque != myRigids[0].angularDrag){
				UpdateMyRigidsData();
			}
		if(changeAllGrabableObjects){
			foreach(GrabableObject script in GetComponentsInChildren<GrabableObject>()){
				script.isBodyPart=grabableScriptsAreBodyParts;
			}
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
