using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairMap : MonoBehaviour {

    public Dictionary<GameObject, GameObject> lowerToUpper;
    public Dictionary<GameObject, GameObject> upperToLower;

    public Dictionary<GameObject, GameObject> elevatorLowerToUpper;
    public Dictionary<GameObject, GameObject> elevatorUpperToLower;



    

    // Use this for initialization
    void Start () {
        lowerToUpper = new Dictionary<GameObject, GameObject>();
        upperToLower = new Dictionary<GameObject, GameObject>();
        elevatorLowerToUpper = new Dictionary<GameObject, GameObject>();
        elevatorUpperToLower = new Dictionary<GameObject, GameObject>();


        floorTransport[] allTransportMethods = FindObjectsOfType<floorTransport>();

        List<floorTransport> elevators = new List<floorTransport>();
        List<floorTransport> stairs = new List<floorTransport>();
        foreach (floorTransport currentObject in allTransportMethods)
        {
            if (currentObject.elevator)
            {
                elevators.Add(currentObject);
            }
            else
            {
                stairs.Add(currentObject);
            }
        }


        foreach(floorTransport currentStair in stairs)
        {
            if (currentStair.onLowerFloor)
            {
                floorTransport partner = null;
                foreach(floorTransport potentialPartner in stairs)
                {
                    if(potentialPartner.id == currentStair.id && !potentialPartner.onLowerFloor)
                    {
                        partner = potentialPartner;
                    }
                }
                lowerToUpper.Add(currentStair.gameObject, partner.gameObject);
            }

        }

        foreach (floorTransport currentEle in elevators)
        {
            if (currentEle.onLowerFloor)
            {
                floorTransport partner = null;
                foreach (floorTransport potentialPartner in elevators)
                {
                    if (potentialPartner.id == currentEle.id && !potentialPartner.onLowerFloor)
                    {
                        partner = potentialPartner;
                    }
                }
                elevatorLowerToUpper.Add(currentEle.gameObject, partner.gameObject);
            }

        }

        foreach(KeyValuePair<GameObject, GameObject> entry in lowerToUpper)
        {
            upperToLower.Add(entry.Value, entry.Key);
        }

        foreach (KeyValuePair<GameObject, GameObject> entry in elevatorLowerToUpper)
        {
            elevatorUpperToLower.Add(entry.Value, entry.Key);
        }




    }

    // Update is called once per frame
    void Update () {
		
	}
}
