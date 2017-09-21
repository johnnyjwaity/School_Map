using System.Collections;
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

    public bool oneToTwo;
    public bool twoToOne;
    public int startFloor;

	private Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		AstarPath.active.Scan ();

        if(oneToTwo || twoToOne)
        {
            stairMap stairmap = FindObjectOfType<stairMap>();
            float shortestDistance = 999999999999999;
            GameObject closestObject = null;

            foreach(KeyValuePair<GameObject, GameObject> entry in stairmap.lowerToUpper)
            {
                float distance = Vector3.Distance(transform.position, entry.Key.transform.position);
                if(shortestDistance > distance)
                {
                    shortestDistance = distance;
                    closestObject = entry.Key;
                }
            }

            target = closestObject.transform;


        }





		Debug.Log (target.name);
        seeker = GetComponent<Seeker>();
        seeker.StartPath(transform.position, target.position, OnPathComplete);
		myRigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(pth != null && !finished)
        {
            if(currentWaypoint >= pth.vectorPath.Count)
            {
                finished = true;
                pth = null;
                myRigid.velocity = Vector3.zero;
                Debug.Log("Finished Path");
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
        else
        {
            myRigid.velocity = Vector3.zero;
        }
	}
    public void OnPathComplete(Path p)
    {
        pth = p;
        currentWaypoint = 0;
    }
}
