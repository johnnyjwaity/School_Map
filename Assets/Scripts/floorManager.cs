using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floorManager : MonoBehaviour {

	public bool showFloor2;
	private floor2[] floor2Obj;
	private floor1[] floor1Obj;

	public Dropdown floorDrop;


	// Use this for initialization
	void Start () {
		floor2Obj = FindObjectsOfType<floor2> ();
		floor1Obj = FindObjectsOfType<floor1> ();
	}
	
	// Update is called once per frame
	void Update () {
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
}
