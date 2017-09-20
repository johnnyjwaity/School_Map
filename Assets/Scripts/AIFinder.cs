﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIFinder : MonoBehaviour {
    private Seeker seeker;
    public Transform target;
    private Path pth;
    private int currentWaypoint;
    private bool finished;
    public float speed;

	private Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		AstarPath.active.Scan ();
		Debug.Log (target.name);
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
		myRigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(pth != null)
        {
            if(currentWaypoint > pth.vectorPath.Count)
            {
                finished = true;
                return;
            }

            Vector3 dir = (pth.vectorPath[currentWaypoint] - transform.position).normalized;

            //transform.position += dir*speed*Time.deltaTime;
			myRigid.velocity = dir*speed*Time.deltaTime;

            if (Vector3.Distance(pth.vectorPath[currentWaypoint], transform.position) < 0.5)
            {
                currentWaypoint++;
            }
        }
	}
    public void OnPathComplete(Path p)
    {
        pth = p;
        currentWaypoint = 0;
    }
}
