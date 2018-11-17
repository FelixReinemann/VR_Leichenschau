using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton_Undress : MenuButton
{
	public GameObject dressed, undressed;
    public override void OnClick()
    {
        dressed.SetActive(false);
		undressed.SetActive(true);
		undressed.GetComponent<CopySkeletonPositions>().TransportBonePositions();
        disableButtonInMenu();
        closeMenu();
    }

}
