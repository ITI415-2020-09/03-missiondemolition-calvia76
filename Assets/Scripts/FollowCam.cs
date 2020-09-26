using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;
using UnityEngine.UIElements.Experimental;

public class FollowCam : MonoBehaviour

{   /*Point Of Interest; same value shared by
    all instances of FollowCam class, POI accessible anywhere w/ FollowCam.Poi*/
    static public GameObject POI; 

    [Header("Set In Inspector")]
    public float easing = 0.05f;
    //default value Vector2(0,0)
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;

    void Awake()
    {
        //holds initial z position of camera
        camZ = this.transform.position.z; 
    }
    void FixedUpdate() //fixedupdate used bc PhysX engine is used, syncs with this function
    {
        Vector3 destination;

        if (POI == null) 
        {
            destination = Vector3.zero;
        }else
        {
            destination = POI.transform.position;

            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }
    
        
        
        //camera is moved to pos of POI, except z position, set to camZ*/
        

        //limit x y to min values
        //MathF.Max ensures cam never moves in negative direction
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        /*Method interpolates between 2 pts, returns weighted average of the two
         * if easing=0, returns 1st pt (transform.position) 
         * if easing=1, returns 2nd pt (destination) 
         if easing 0-1, returns pt between 0-1 (about 50% if .5)*/
        destination = Vector3.Lerp(transform.position, destination, easing);

        //1. Forces destination.z to be camZ to keep camera far enough away
        destination.z = camZ; 

        //set camera to destination
        transform.position = destination;

        //Set orthographicSize of cam to keep ground in view
        Camera.main.orthographicSize = destination.y + 10; 
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
