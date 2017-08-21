using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {
    public float maxDistance;
    private MeshRenderer mr;
    //private GameObject Camera;
    // Use this for initialization
    void Start () {
        //Camera = FindObjectOfType<Camera>().gameObject;
        mr = GetComponent<MeshRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        var fwd = Camera.main.transform.forward;
        fwd.y = 0.0f;
        //transform.LookAt();
        transform.rotation = Quaternion.LookRotation(fwd);

        if(Vector3.Distance(Camera.main.transform.position, transform.position) > maxDistance){
            //MeshRenderer mr = GetComponent<MeshRenderer>();
            mr.enabled = false;
        }
        else
        {
            mr.enabled = true;
        }
    }
}
