using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

    public floor1[] floor1Objects;
    public floor2[] floor2Objects;


    // Use this for initialization
    void Start () {
        floor1Objects = FindObjectsOfType<floor1>();
        floor2Objects = FindObjectsOfType<floor2>();
        foreach(floor1 f in floor1Objects)
        {
            f.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
