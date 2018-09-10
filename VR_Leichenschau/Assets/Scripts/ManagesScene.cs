using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagesScene : MonoBehaviour {
	public static ManagesScene singleton;
	void Awake(){
		if(ManagesScene.singleton==null){
			ManagesScene.singleton = this;
		}else{
			Destroy(this);
		}
	}

public GameObject markerPrefab;
public Transform player;
public int markerCount=0;
}
