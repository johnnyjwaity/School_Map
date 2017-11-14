using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floorManager : MonoBehaviour {

	public bool showFloor2;
    private bool pastFloorWas1;
	private floor2[] floor2Obj;
	private floor1[] floor1Obj;

	public Dropdown floorDrop;


	// Use this for initialization
	void Start () {
		floor2Obj = FindObjectsOfType<floor2> ();
		floor1Obj = FindObjectsOfType<floor1> ();
        pastFloorWas1 = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(pastFloorWas1 != !showFloor2)
        {
            changeDropdown();
            pastFloorWas1 = !pastFloorWas1;
        }

		if (!showFloor2) {
			foreach (floor2 obj in floor2Obj) {
				obj.gameObject.SetActive (false);
			}
			foreach (floor1 obj in floor1Obj) {
				obj.gameObject.SetActive (true);
			}
		} else {
			foreach (floor2 obj in floor2Obj) {
				obj.gameObject.SetActive (true);
			}
			foreach (floor1 obj in floor1Obj) {
				obj.gameObject.SetActive (false);
			}
		}

		
	}

	public void changeFloors(){
		if (floorDrop.value == 0) {
			showFloor2 = false;
		} else {
			showFloor2 = true;
		}

	}
    private void changeDropdown()
    {
        if (showFloor2)
        {
            floorDrop.value = 1;
        }
        else
        {
            floorDrop.value = 0;
        }
    }
}
