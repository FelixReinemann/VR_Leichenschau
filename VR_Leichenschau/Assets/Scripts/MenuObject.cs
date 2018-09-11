using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MarkableObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject myMenu;
	public override void Mark(){
		Debug.Log("Menu Appears");
		myMenu.SetActive(true);
		myMenu.GetComponent<MyMenu>().UnParent();
	}

}
