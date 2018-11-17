using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton_SpawnNewMenu : MenuButton {

	public GameObject objectToEnable;
	public bool closeMyMenu=true;
	public bool disableButtonAfterAction=false;
	public override void OnClick(){
		objectToEnable.SetActive(true);
		objectToEnable.GetComponent<MyMenu>().UnParent(transform.position);
		if(closeMyMenu)closeMenu();
		if(disableButtonAfterAction)disableButtonInMenu();
	}
}
