using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton_Undress : MenuButton
{
	public GameObject dressed, undressed;
    public bool applyAllJointData=false;
    public override void OnClick()
    {
        dressed.SetActive(false);
		undressed.SetActive(true);

        if(applyAllJointData )ApplyAllJointData();

		undressed.GetComponent<CopySkeletonPositions>().TransportBonePositions();
        disableButtonInMenu();
        closeMenu();
    }

    void ApplyAllJointData(){
        Joint[] joints = undressed.GetComponentsInChildren<Joint>();
        Joint[] targetJoints = dressed.GetComponentsInChildren<Joint>();
        for(int i =0; i < joints.Length; i++){
            joints[i] = targetJoints[i];
        }
    }

}
