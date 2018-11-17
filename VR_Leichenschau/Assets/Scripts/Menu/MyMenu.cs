using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMenu : MonoBehaviour
{

    [System.Serializable]
    public class MenuItem
    {
        public enum ButtonState { Active, Hidden, Inactive }
        public GameObject theButton;
        public ButtonState buttonState = ButtonState.Active;

    }

    public Transform myOriginalParent;
    public Vector3 originalPosition;
    public Quaternion originalRotation;

    public MenuItem[] buttonObjects;
    public Transform spawnButtonsIn;

	public GameObject gotCalledFrom;

    void Update()
    {
		ShowButtons();
    }

    public void ShowButtons()
    {
        foreach (MenuItem butt in buttonObjects)
        {
            if (butt.buttonState == MenuItem.ButtonState.Active)
            {
                butt.theButton.SetActive(true);
            }
            else
            {
                butt.theButton.SetActive(false);
            }
        }
    }

    public void UnParent()
    {
        myOriginalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        transform.SetParent(null);
        //transform.position = _position;
        transform.LookAt(ManagesScene.singleton.player, Vector3.up);

    }
    public void UnParent(Vector3 _position)
    {
        myOriginalParent = transform.parent;
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        transform.SetParent(null);
        transform.position = _position;
        transform.LookAt(ManagesScene.singleton.player, Vector3.up);

    }

    public void ReParent()
    {
        if (myOriginalParent != null)
        {
            transform.parent = myOriginalParent;
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
        }
    }

	public void CloseMenu(){
        ReParent();
        gameObject.SetActive(false);
	}

}
