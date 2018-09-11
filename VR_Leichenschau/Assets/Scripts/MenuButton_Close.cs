using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton_Close : MenuButton{
	public override void OnClick(){
		transform.parent.parent.GetComponent<MyMenu>().ReParent();
		transform.parent.parent.gameObject.SetActive(false);
	}
}
