﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkableObject : MonoBehaviour {


	float radius = 0.4f;
	float markerSize = 0.1f;
	RaycastHit hit;
	int myMask;
	void Start(){
		myMask= 1 << LayerMask.NameToLayer("Interactable");
		markerSize = ManagesScene.singleton.markerPrefab.GetComponentInChildren<BoxCollider>().size.x;
	}
	public void Mark(){
		Vector3 spawnPos = transform.position + new Vector3(Random.Range(-radius,radius),1*0.5f,Random.Range(-radius,radius));
		int repeats = 0;
		while(true){
			if(repeats >= 15){
				Debug.Log("couldn't find good place to spawn marker");
				break;
			}
			if(Physics.SphereCast(spawnPos,markerSize,Vector3.down, out hit, 2, myMask)){
                spawnPos = transform.position + new Vector3(Random.Range(-radius, radius), 1 * 0.5f, Random.Range(-radius, radius));
				repeats++;
			} else {
				break;
			}
		}
		Vector3 playerPos = ManagesScene.singleton.player.position;
		Quaternion spawnRot = Quaternion.LookRotation(playerPos - spawnPos, Vector3.up);
		Instantiate(ManagesScene.singleton.markerPrefab,spawnPos, spawnRot);
	}

void FixedUpdate(){
	/*
	OVRInput.Update();
	if(OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three) ){
		Debug.Log("marked");
		Mark();
	}*/
}

}