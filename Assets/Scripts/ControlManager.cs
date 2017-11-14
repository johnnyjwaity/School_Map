﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour {
    private AIFinder player;
    public Slider speedSlider;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        try
        {
            player = FindObjectOfType<AIFinder>();
        }
        catch
        {
            return;
        }


        if(player != null)
        {
            changeSpeed(speedSlider.value);
        }
        
	}
    
    public void pause()
    {
        player.pause();
    }

    public void changeSpeed(float speed)
    {
        player.changeSpeed(speed);
    }
    public void lockCam()
    {
        Camera.main.GetComponent<CameraController>().locked = true;
    }
}
