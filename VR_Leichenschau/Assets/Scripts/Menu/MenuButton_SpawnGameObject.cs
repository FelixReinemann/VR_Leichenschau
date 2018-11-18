using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton_SpawnGameObject : MenuButton {

	public GameObject prefab;
	public Vector3 spawnPosition;
	public Quaternion spawnRotation;

	public override void OnClick(){
		Instantiate(prefab,spawnPosition,spawnRotation);
		disableButtonInMenu();
	}
}
