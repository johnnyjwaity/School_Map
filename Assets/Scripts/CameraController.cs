using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool locked;

    public GameObject tracker;

    public float moveSpeed;

    public float verticalOffset;
	public float Offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (locked)
        {
            Vector3 tagetPos = tracker.transform.position;
            tagetPos.y += verticalOffset;

			if(Vector3.Distance(tracker.transform.position, transform.position) > verticalOffset+Offset){
				transform.position = Vector3.Lerp(transform.position, tagetPos, moveSpeed * Time.deltaTime);

				transform.LookAt(tracker.transform.position);
			}
            //transform.position = Vector3.Lerp(transform.position, tagetPos, moveSpeed * Time.deltaTime);
            //transform.LookAt(tracker.transform.position);
        }
	}
}
