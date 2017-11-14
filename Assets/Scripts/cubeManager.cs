using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeManager : MonoBehaviour {

    public GameObject cube;
    public GameObject cube2;

    private List<GameObject> lineObjects;

	// Use this for initialization
	void Start () {
        lineObjects = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void displayCubes(List<Vector3> path)
    {
        foreach(GameObject currentObj in lineObjects)
        {
            Destroy(currentObj);
        }
        lineObjects.Clear();
        int index = 0;
        foreach(Vector3 currentPoint in path)
        {
            if(index == path.Count - 1)
            {
                break;
            }
            GameObject start = Instantiate(cube2, currentPoint, Quaternion.Euler(Vector3.zero));
            GameObject end = Instantiate(cube2, path[index + 1], Quaternion.Euler(Vector3.zero));
            GameObject newCube = Instantiate(cube);
            newCube.GetComponent<CubeStretcher>().startPos1 = start.transform;
            newCube.GetComponent<CubeStretcher>().endPos1 = end.transform;
            lineObjects.Add(newCube);
            Destroy(start);
            Destroy(end);
            index++;
        }
    }
}
