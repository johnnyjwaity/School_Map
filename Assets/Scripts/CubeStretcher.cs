using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStretcher : MonoBehaviour {

    public Transform startPos1;
    public Transform endPos1;

    


	// Use this for initialization
	void Start () {
        //transform.position = startPos1.position;
        //transform.position = endPos1.position;
        transform.position = new Vector3((startPos1.position.x+endPos1.position.x)/2, (startPos1.position.y + endPos1.position.y) / 2, (startPos1.position.z + endPos1.position.z) / 2);
        transform.LookAt(endPos1);
        float distance = Vector3.Distance(startPos1.position, endPos1.position);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, distance + (transform.localScale.x/2));


    }
	
	// Update is called once per frame
	void Update () {
        //Vector3 direction = endPos1.position - startPos1.position;
        //direction.Normalize();
        //transform.position += direction * 10;
	}

    

   
}
