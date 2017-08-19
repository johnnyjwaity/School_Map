using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIFinder : MonoBehaviour {
    private Seeker seeker;
    public Transform target;
    private Path pth;
	// Use this for initialization
	void Start () {
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPathComplete(Path p)
    {
        pth = p;
    }
}
