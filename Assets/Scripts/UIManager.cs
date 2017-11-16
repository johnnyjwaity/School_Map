using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject buttonHolder;
    public Animator NavPanel;
    public GameObject controlPanel;
    public GameObject dropFloor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openNavPanel()
    {
        NavPanel.SetBool("NavOpen", true);
        buttonHolder.SetActive(false);
    }

    public void closeNavPanel()
    {
        NavPanel.SetBool("NavOpen", false);
        //buttonHolder.SetActive(true);
        
    }
    public void playerCloseNavPanel()
    {
        NavPanel.SetBool("NavOpen", false);
        buttonHolder.SetActive(true);

    }
    public void startCorse()
    {
        if (NavPanel.GetBool("NavOpen"))
        {
            closeNavPanel();
        }
        controlPanel.SetActive(true);
        dropFloor.SetActive(false);
    }
    public void endCorse()
    {
        buttonHolder.SetActive(true);
        controlPanel.SetActive(false);
        dropFloor.SetActive(true);
    }
}
