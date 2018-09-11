using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMenu : MonoBehaviour {

	public Transform myOriginalParent;
	public Vector3 originalPosition;
	public Quaternion originalRotation;


    public void UnParent()
    {
        myOriginalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        transform.SetParent(null);
        //transform.position = _position;
        transform.LookAt(ManagesScene.singleton.player, Vector3.up);

    }
public void UnParent(Vector3 _position){
	myOriginalParent = transform.parent;
	originalPosition = transform.localPosition;
	originalRotation = transform.localRotation;
	transform.SetParent(null);
	transform.position = _position;
    transform.LookAt(ManagesScene.singleton.player, Vector3.up);
	
}

public void ReParent(){
	if(myOriginalParent!=null){
		transform.parent = myOriginalParent;
		transform.localPosition = originalPosition;
		transform.localRotation = originalRotation;
	}
}

}
