using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMove : MonoBehaviour
{

    public float moveSpeed;
    public float rotationSpeed;

    public Vector2 moveStick;
    Vector3 moveVector;
    public Transform head;
    Quaternion myRotation;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OVRInput.Update();
        moveStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        myRotation = Quaternion.LookRotation(new Vector3(head.forward.x, 0, head.forward.z).normalized, Vector3.up);
        moveVector = myRotation * (transform.forward * moveStick.y + transform.right * moveStick.x);
        transform.Translate(moveVector * moveSpeed * Time.deltaTime);


    }
}
