using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMove : MonoBehaviour
{

    public float moveSpeed;
    public float rotationSpeed;

    public Vector2 moveStick;
    public Vector2 rotateStick;
    Vector3 moveVector;
    public Transform head;
    public Transform toRotate;
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
        rotateStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        //---------movement---------------//
        //myRotation = Quaternion.LookRotation(new Vector3(head.forward.x, 0, head.forward.z).normalized, Vector3.up);
        //moveVector = myRotation * (transform.forward * moveStick.y + transform.right * moveStick.x);
        moveVector = (transform.forward * moveStick.y + transform.right * moveStick.x);
        moveVector = new Vector3(moveVector.x,0,moveVector.z);


        //transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        //transform.Translate(moveVector * moveSpeed * Time.deltaTime,Space.Self);
        transform.position = transform.position + moveVector* moveSpeed * Time.deltaTime;
        //--------------rotation--------------//
        toRotate.RotateAround(head.position, Vector3.up, Time.deltaTime * rotationSpeed * rotateStick.x);
    }
}
