using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerController : MonoBehaviour {

    private TextMesh tm;
    //private GameObject Camera;


	// Use this for initialization
	void Start () {
        tm = GetComponent<TextMesh>();
        //Camera = FindObjectOfType<Camera>().gameObject;

        tm.text = transform.parent.parent.name;
	}
	
	// Update is called once per frame
	void Update () {
        var fwd = Camera.main.transform.forward;
        ///fwd.y = 0.0f;
        //transform.LookAt();
        transform.rotation = Quaternion.LookRotation(fwd);
    }
}
