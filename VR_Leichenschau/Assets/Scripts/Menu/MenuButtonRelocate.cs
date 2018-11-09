﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonRelocate : MenuButton {

	public GameObject body;
	public Transform targetRoot;
	public override void OnClick(){
		body.GetComponent<CopySkeletonPositions>().targetRoot = targetRoot;
		body.GetComponent<CopySkeletonPositions>().TransportBonePositions();
	}
}