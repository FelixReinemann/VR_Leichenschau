using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopySkeletonPositions : MonoBehaviour {

	public Transform targetRoot;
	public Transform myRoot;
	public bool useOnlyRigidbodies;

	void Update(){
		if(Input.GetKeyDown(KeyCode.A)){
			TransportBonePositions();
		}
	}
	public void TransportBonePositions(){
		Debug.Log("changingPositions");
		Transform[] targetBones = new Transform[targetRoot.childCount];
		targetBones = targetRoot.GetComponentsInChildren<Transform>();
		Transform[] myBones = new Transform[myRoot.childCount];
		myBones = myRoot.GetComponentsInChildren<Transform>();

		for(int i=0; i < targetBones.Length; i++){
			myBones[i].position = targetBones[i].position;
			myBones[i].rotation = targetBones[i].rotation;
		}
	}
}
