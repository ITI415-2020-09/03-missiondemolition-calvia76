using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SetActive method on GameObj tells game whether or not to ignore them

public class Slingshot : MonoBehaviour
{
    public GameObject launchPoint;

    private void Awake()
    {   //transform.Find searches for a child of Slingshot named LP and returns
        //its transform. 
        Transform launchPointTrans = transform.Find("LaunchPoint");
        //gets gameobj associated with Transform and assigns to game obj field 'launchPoint'
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);

    }
    private void OnMouseEnter()
    {

        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    private void OnMouseExit()
    {
        print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
