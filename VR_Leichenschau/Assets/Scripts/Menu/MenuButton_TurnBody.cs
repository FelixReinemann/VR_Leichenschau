using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton_TurnBody : MenuButton {

	public override void OnClick(){
		ManagesScene.currentActiveBody.transform.Translate(Vector3.up*0.3f,Space.World);
		ManagesScene.currentActiveBody.transform.RotateAround(	ManagesScene.currentActiveBody.GetComponent<RegisterBody>().getCenterFrom.bounds.center,
																ManagesScene.currentActiveBody.transform.up,
																180f);
	}

	public Vector3 GetCenterPoint(Transform target){
		Vector3 myVector = Vector3.zero;
		foreach(Transform trans in target.GetComponentsInChildren<Transform>()){
			myVector+=trans.position;
		}
		myVector.x=myVector.x / target.childCount;
		myVector.y = myVector.y /target.childCount;
		myVector.y = myVector.z / target.childCount;
		return myVector;
	}

	void Update(){
		Debug.DrawRay(ManagesScene.currentActiveBody.GetComponent<RegisterBody>().getCenterFrom.bounds.center,Vector3.up*3f,Color.blue);
	}
}
