using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterBody : MonoBehaviour {

	public Renderer getCenterFrom;
	public Transform getDirectionFrom;

	// Use this for initialization
	void Start () {
		ManagesScene.currentActiveBody = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
