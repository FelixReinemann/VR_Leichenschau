using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

	public virtual void OnClick(){
		Debug.Log(gameObject.name +" was clicked");
	}


    public void disableButtonInMenu()
    {
        MyMenu menu = GetComponentInParent<MyMenu>();
        for (int i = 0; i < menu.buttonObjects.Length; i++)
        {
            if (menu.buttonObjects[i].theButton == gameObject)
            {
                menu.buttonObjects[i].buttonState = MyMenu.MenuItem.ButtonState.Inactive;
            }
        }
    }

	public void closeMenu(){
        GetComponentInParent<MyMenu>().CloseMenu();
	}
}
