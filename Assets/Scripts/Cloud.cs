using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject cloudSphere;

    //+1 more than actual max 6-11
    public int numSphereMin = 6;
    public int numSphereMax = 10;

    //Max distance +- that CloudSphere could be from center of the cloud per dimension
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);

    //ranged in scales per dimension: default setting makes wider than tall
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(3, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);

    /*@ end of Start(), each CloudSphere is scaled down in Y dim based on distance from
     center in X dim, making clouds taper at L & R extents
    *ScaleMin is lowest Y scale allowable in this instance*/
    public float scaleYMin = 2f;

    //holds reference to all CloudSpheres instantiated by Cloud 
    private List<GameObject> spheres;

    void Start()
    {
        spheres = new List<GameObject>();

        //Randomly chooses how many CloudSpheres to attach to CLoud
        int num = Random.Range(numSphereMin, numSphereMax);
        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);

            //Instantiated clouds added to spheres
            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            //randomly assign a position
            Vector3 offset = Random.insideUnitSphere;

            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;

            spTrans.localPosition = offset;

            //randomly assign scale
            Vector3 scale = Vector3.one;

            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //adjust y scale by x distance from core
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);
            spTrans.localScale = scale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Restart();
        }
    }
    void Restart()
    {
        //clear old spheres
        foreach (GameObject sp in spheres){
            Destroy(sp);
        }
        Start();
    }
    
}
