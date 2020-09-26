using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SetActive method on GameObj tells game whether or not to ignore them

public class Slingshot : MonoBehaviour
    

{   //Creates header in the inspector view of script
    [Header("Set In Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set Dynamically")]
    public GameObject   launchPoint;
    public Vector3      launchPos;
    public GameObject   projectile;
    public bool         aimingMode;
    private Rigidbody   projectileRigidbody;

    private void Awake()
    {   //transform.Find searches for a child of Slingshot named LP and returns
        //its transform. 
        Transform launchPointTrans = transform.Find("LaunchPoint");
        //gets gameobj associated with Transform and assigns to game obj field 'launchPoint'
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }
    private void OnMouseEnter()
    {

        print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    private void OnMouseDown()
    {   //player presssed the mouse button while over Slingshot obj
        aimingMode = true;
        //instantiates projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        //starts projectile at launchpoint
        projectile.transform.position = launchPos;
        //
        projectile.GetComponent<Rigidbody>().isKinematic = true;

        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }
    private void Update()
    {
        //If Slingshot is not in aimingMody, don't run
        if (!aimingMode) return;

        //Get current mouse pos in 2D screen coordinate
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Find the delta from the LaunchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;

        //Limit mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if(mouseDelta.magnitude> maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            //mouse has been released
            aimingMode = false;

            /*setting projectile to kinematic gives projectile physics properties,
            allowing it to move with respect to velocity and gravity*/
            projectileRigidbody.isKinematic = false;

            //rigidbody of projectile given velocity proportional to distance from launchPos
            projectileRigidbody.velocity = -mouseDelta * velocityMult;

            /* sets static public field FollowCam.POI to be newly fired projectile*/
            FollowCam.POI = projectile;

            /*sets to null to allow for another instance of projectile to be created
            once the slingshot is fired, allowing another shot*/
            projectile = null;
        }
        
    }

}
