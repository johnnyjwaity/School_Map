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
    public bool elevator;

    private Transform secondaryLocation;

    private GameObject couterpart;
	private Rigidbody myRigid;
	// Use this for initialization
	void Start () {
		//AstarPath.active.Scan ();

        if(oneToTwo || twoToOne)
        {
            stairMap stairmap = FindObjectOfType<stairMap>();
            float shortestDistance = 999999999999999;
            GameObject closestObject = null;
            secondaryLocation = target;
            if(oneToTwo)
            {
                foreach (KeyValuePair<GameObject, GameObject> entry in stairmap.lowerToUpper)
                {
                    float distance = Vector3.Distance(transform.position, entry.Key.transform.position);
                    if (shortestDistance > distance)
                    {
                        shortestDistance = distance;
                        closestObject = entry.Key;
                        couterpart = entry.Value;
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<GameObject, GameObject> entry in stairmap.upperToLower)
                {
                    Debug.Log("", entry.Key);
                    float distance = Vector3.Distance(transform.position, entry.Key.transform.position);
                    if (shortestDistance > distance)
                    {
                        shortestDistance = distance;
                        closestObject = entry.Key;
                        couterpart = entry.Value;
                    }
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
                //if(oneToTwo || twoToOne)
                //{
                //    Debug.Log("Ran Create New Tracker");
                //    createNewTracker();
                //}
                //Destroy(gameObject);
                return;
            }
            transform.LookAt(pth.vectorPath[currentWaypoint]);
            Vector3 dir = (pth.vectorPath[currentWaypoint] - transform.position).normalized;

            //transform.position += dir*speed*Time.deltaTime;
            myRigid.velocity = dir * speed * Time.deltaTime;

            if (Vector3.Distance(pth.vectorPath[currentWaypoint], transform.position) < 0.5)
            {
                currentWaypoint++;
            }
        }
        else
        {
            Debug.Log("Got Here");
            myRigid.velocity = Vector3.zero;
            if (oneToTwo || twoToOne)
            {
                FindObjectOfType<placeDictionary>().Navigate(couterpart, secondaryLocation, twoToOne);
            }
            Destroy(gameObject);
        }
	}
    public void OnPathComplete(Path p)
    {
        pth = p;
        currentWaypoint = 0;
    }

    /*
    private void createNewTracker()
    {
        CameraController mainCam = FindObjectOfType<CameraController>();
        mainCam.locked = false;
        Dictionary<GameObject, GameObject> correctDict;
        if (oneToTwo)
        {
            correctDict = FindObjectOfType<stairMap>().lowerToUpper;
            floorManager floors= FindObjectOfType<floorManager>();
            floors.showFloor2 = true;
        }
        else
        {
            correctDict = FindObjectOfType<stairMap>().upperToLower;
            floorManager floors = FindObjectOfType<floorManager>();
            floors.showFloor2 = false;
        }
        AstarPath.active.Scan();
        //yield return new WaitForSeconds(2);
        GameObject correctObject = null;
        foreach(KeyValuePair<GameObject, GameObject> currentObject in correctDict)
        {
            if(currentObject.Key.transform.Equals(target))
            {
                correctObject = currentObject.Value;
            }
        }
        Debug.Log("Correct Obj:", correctObject);
        GameObject newTracker = Instantiate(FindObjectOfType<placeDictionary>().tracker, correctObject.transform.position, Quaternion.Euler(Vector3.zero));
        newTracker.GetComponent<AIFinder>().target = secondaryLocation;
        //mainCam = FindObjectOfType<CameraController>();
        mainCam.tracker = newTracker;
        mainCam.locked = true;
        Destroy(gameObject);
    }
    */
}
