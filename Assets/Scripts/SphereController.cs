using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour {
    //private GameObject Camera;
    // Use this for initialization
    void Start () {
        //Camera = FindObjectOfType<Camera>().gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        var fwd = Camera.main.transform.forward;
        fwd.y = 0.0f;
        //transform.LookAt();
        transform.rotation = Quaternion.LookRotation(fwd);
    }
}
