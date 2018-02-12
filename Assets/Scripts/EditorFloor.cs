using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class EditorFloor : MonoBehaviour {
    public bool showFloor2 = true;
    public floor1[] floor1Objects;
    public floor2[] floor2Objects;

    void Awake()
    {
        floor1Objects = FindObjectsOfType<floor1>();
        floor2Objects = FindObjectsOfType<floor2>();
        foreach (floor1 f in floor1Objects)
        {
            f.gameObject.SetActive(false);
        }
    }
    void Update () {
        if (showFloor2)
        {
            foreach (floor1 f in floor1Objects)
            {
                f.gameObject.SetActive(false);
            }
            foreach (floor2 f in floor2Objects)
            {
                f.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (floor1 f in floor1Objects)
            {
                f.gameObject.SetActive(true);
            }
            foreach (floor2 f in floor2Objects)
            {
                f.gameObject.SetActive(false);
            }
        }
	}
}
