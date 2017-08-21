using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class placeDictionary : MonoBehaviour {

    public GameObject[] firstFloorRooms;
    
    private Dictionary<string, GameObject> firstFloorRoomsDict;
    private List<string> firstRoomNames;

    public GameObject[] usableFirstFloorStairs;

    public GameObject[] usableFirstFloorElevators;

    public Text inputBox;
    public Dropdown options;
    public Text dropdownLabel;
    private string previousInput = "abdjksabfck";

    public Text inputBox2;
    public Dropdown options2;
    public Text dropdownLabel2;
    private string previousInput2 = "abdjksabfck";

    public GameObject tracker;

    private CameraController mainCamera;
    // Use this for initialization
    void Start () {
        firstFloorRoomsDict = new Dictionary<string, GameObject>();
        firstRoomNames = new List<string>();
        mainCamera = FindObjectOfType<CameraController>();

        for (int i =0; i<firstFloorRooms.Length; i++)
        {
            if(firstFloorRooms[i] != null)
            {
                firstFloorRoomsDict.Add(firstFloorRooms[i].name, firstFloorRooms[i]);
                firstRoomNames.Add(firstFloorRooms[i].name);
                Debug.Log("Went Through");
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(inputBox.text != "")
        {
            options.ClearOptions();
            Dropdown.OptionData l = new Dropdown.OptionData();
            l.text = "...";
            options.options.Add(l);
            //Dropdown.OptionDataList od = new Dropdown.OptionDataList();
            foreach(string room in firstRoomNames)
            {
                if (room.ToLower().Contains(inputBox.text.ToLower()))
                {
                    Dropdown.OptionData o = new Dropdown.OptionData();
                    o.text = room;
                    options.options.Add(o);
                }
            }
            //options.ClearOptions(); 
        }
        dropdownLabel.text = options.options[options.value].text;

        if(inputBox.text != previousInput)
        {
            options.value = 0;
        }

        previousInput = inputBox.text;

        if (inputBox2.text != "")
        {
            options2.ClearOptions();
            Dropdown.OptionData l2 = new Dropdown.OptionData();
            l2.text = "...";
            options2.options.Add(l2);
            //Dropdown.OptionDataList od = new Dropdown.OptionDataList();
            foreach (string room in firstRoomNames)
            {
                if (room.ToLower().Contains(inputBox2.text.ToLower()))
                {
                    Dropdown.OptionData o2 = new Dropdown.OptionData();
                    o2.text = room;
                    options2.options.Add(o2);
                }
            }
            //options.ClearOptions(); 
        }
        dropdownLabel2.text = options2.options[options2.value].text;

        if (inputBox2.text != previousInput2)
        {
            options2.value = 0;
        }

        previousInput2 = inputBox2.text;
    }

    public void Navigate()
    {
        if(options2.value !=0 && options.value != 0)
        {
            GameObject tkr = Instantiate(tracker, firstFloorRoomsDict[options2.options[options2.value].text].transform.position, Quaternion.Euler(Vector3.zero));
            tkr.GetComponent<AIFinder>().target = firstFloorRoomsDict[options.options[options.value].text].transform;
            mainCamera.locked = true;
            mainCamera.tracker = tkr;


        }
    }




}



