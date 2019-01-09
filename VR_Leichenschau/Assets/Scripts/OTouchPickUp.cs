using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OTouchPickUp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        mask = 1 << LayerMask.NameToLayer("Interactable");
        col = GetComponent<SphereCollider>();
        myRigid = GetComponent<Rigidbody>();

        myBeam.SetActive(false);

        if (hand == Hands.LeftHand)
        {
            handTrigger = OVRInput.Axis1D.PrimaryHandTrigger;
        }
        else
        {
            handTrigger = OVRInput.Axis1D.SecondaryHandTrigger;
        }
    }

    public bool grabbing;
    public SphereCollider col;
    public Hands hand;
    OVRInput.Axis1D handTrigger;
    Rigidbody myRigid;
    GameObject grabbedObject;
    //public Transform lTouchController, rTouchController;
    public ControllerButton[] myButton;
    public GameObject myBeam;

    public bool teleGrab=false;

    // Update is called once per frame

    RaycastHit hit;
    int mask;
    void FixedUpdate()
    {
        //OVRInput.Update();
        MyButtonUpdate();
        if (OVRInput.Get(handTrigger) > 0)
        {
            grabbing = true;
            //Debug.Log(OVRInput.Get( OVRInput.Axis1D.PrimaryHandTrigger) +" ; "+OVRInput.Get( OVRInput.Axis1D.SecondaryHandTrigger));
            if (grabbedObject == null)
            {
                if (myBeam.activeSelf && teleGrab)
                {
                    GameObject hitTarget;
                    if(CastRayForGrabable(out hitTarget)){
                        GrabableObject script = hitTarget.GetComponent<GrabableObject>();
                        if (script != null)
                        {
                            script.OnGrab(myRigid);
                            grabbedObject = hitTarget;
                        }
                    }
                }
                else
                {
                    Collider[] colObjects = Physics.OverlapSphere(transform.position, col.radius, mask);
                    //Debug.Log("Amount: "+colObjects.Length);
                    GrabableObject script;
                    for (int i = 0; i < colObjects.Length; i++)
                    {
                        script = colObjects[i].attachedRigidbody.GetComponent<GrabableObject>();
                        if (script.grabJoint == null)
                        {
                            script.OnGrab(myRigid);
                            grabbedObject = colObjects[i].attachedRigidbody.gameObject;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            grabbing = false;
            if (grabbedObject != null)
            {
                grabbedObject.GetComponent<GrabableObject>().Release();
                grabbedObject = null;
            }
        }

        /*if(myButton[0].state == ButtonStates.Pressed){
            Debug.Log("Button 0 pressed on "+gameObject.name+" pressed");
            CastRayForMarkable();
        }*/

        if(myBeam.activeSelf){
            if(CastRayForMenu())
                if(myButton[0].state == ButtonStates.Pressed && hitButton!=null)
                    hitButton.GetComponent<MenuButton>().OnClick();
                
        } else {
            hitButton = null;
        }

        if(myBeam.activeSelf && myButton[0].state == ButtonStates.Pressed && hitButton == null){
            if(!CastRayForMarkable()){
                CreateMark();
            }
            
        }

        if (myButton[1].state == ButtonStates.Pressed)
        {
            //Debug.Log("Button 1 pressed on " + gameObject.name + " pressed");
            myBeam.SetActive(true);
        } else if (myButton[1].state == ButtonStates.LetGo){
            myBeam.SetActive(false);
        }

        if(myBeam.activeSelf && myButton[2].state == ButtonStates.Pressed){
            Vector3 targetPos;
            if(CastRayForGround(out targetPos)){
                VRMove.singleton.TeleportToPosition(targetPos);
            }
        }

        

        
    }

    float castRadius = 0.2f;
    RaycastHit[] hits;

    public GameObject hitButton;
    public bool CastRayForMenu(){
        //Debug.Log("casting ray");
        RaycastHit hit;
        int newMask = 1 << LayerMask.NameToLayer("OtherUI");
        if(Physics.Raycast(transform.position,transform.forward,out hit, 25,newMask)){
            hitButton = hit.collider.gameObject;
            //hitButton.Select();
            //Debug.Log(hitButton.name);
            return true;
        } else {
            hitButton = null;
            return false;
        }
    }

    public bool CastRayForGrabable(out GameObject targetHit){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100, mask))
        {
            targetHit = hit.collider.gameObject;
            //hitButton.Select();
            //Debug.Log(hitButton.name);
            return true;
        }
        else
        {
            targetHit = null;
            return false;
        }
    }
    public bool CastRayForMarkable()
    {
        bool myBool=false;
        hits = Physics.SphereCastAll(transform.position, castRadius, transform.forward, 10, mask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                /*
                MarkableObject script;
                script = hits[i].collider.attachedRigidbody.GetComponent<MarkableObject>();
                if (script != null && !script.marked)
                {
                    script.Mark();
					break;
                }*/
                MenuObject script;
                script = hits[i].collider.attachedRigidbody.GetComponent<MenuObject>();
                if(script != null && !script.marked){
                    script.Mark(hits[i].point + Vector3.up*0.75f);
                    myBool=true;
                    break;
                }
            }
        }
        return myBool;
    }

    public bool CastRayForGround(out Vector3 _position){
        //Debug.Log("casting for ground");
        RaycastHit hit;
        int groundMask = 1 << LayerMask.NameToLayer("Teleportable");
        if(Physics.Raycast(transform.position,transform.forward,out hit,100f,groundMask)){
            //Debug.Log("hit "+hit.collider.name);
            _position = hit.point;
            return true;
        }
        _position = Vector3.zero;
        return false;
    }

    public void MyButtonUpdate(){
        for(int i=0; i < myButton.Length; i++){
            myButton[i].value = OVRInput.Get(myButton[i].ovrButton);
            if(myButton[i].value){
                if(!myButton[i].lastValue){
                    myButton[i].state = ButtonStates.Pressed;
                } else {
                    myButton[i].state = ButtonStates.Held;
                }
            } else {
                if(myButton[i].lastValue){
                    myButton[i].state = ButtonStates.LetGo;
                } else {
                    myButton[i].state = ButtonStates.NotPressed;
                }
            }
            myButton[i].lastValue = myButton[i].value;
        }
    }

    public void CreateMark()
    {
        //Vector3 spawnPos = transform.position + new Vector3(Random.Range(-radius, radius), 1 * 0.5f, Random.Range(-radius, radius));
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position,transform.forward,out hit, 25)){
            Vector3 spawnPos = hit.point+Vector3.up*0.15f;
            Vector3 playerPos = ManagesScene.singleton.player.position;
            Quaternion spawnRot = Quaternion.LookRotation(spawnPos - playerPos, Vector3.up);
            spawnRot = Quaternion.Euler(0f,spawnRot.eulerAngles.y,0f);
            GameObject newMarker = Instantiate(ManagesScene.singleton.markerPrefab, spawnPos, spawnRot);
            ManagesScene.singleton.markerCount++;
            newMarker.GetComponentInChildren<UnityEngine.UI.Text>().text = ManagesScene.singleton.markerCount.ToString();
        }
    }

}



public enum Hands { LeftHand, RightHand }

public enum ButtonStates {Held,Pressed,LetGo,NotPressed}

[System.Serializable]
public class ControllerButton {
    public ButtonStates state;
    public OVRInput.RawButton ovrButton;
    public bool value;
    public bool lastValue;
}