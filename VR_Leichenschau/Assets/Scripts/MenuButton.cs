using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

	public virtual void OnClick(){
		Debug.Log(gameObject.name +" was clicked");
	}
}
