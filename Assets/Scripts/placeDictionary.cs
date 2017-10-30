using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class placeDictionary : MonoBehaviour {

    public GameObject[] firstFloorRooms;

    private Dictionary<string, GameObject> firstFloorRoomsDict;
    private List<string> firstRoomNames;

	public GameObject[] secondFloorRooms;
	private Dictionary<string, GameObject> secondFloorRoomsDict;
	private List<string> secondRoomNames;

    public GameObject[] usableFirstFloorStairs;

    public GameObject[] usableFirstFloorElevators;

    public Text inputBox, enterBox;
    private string enterPrev;
    public GameObject listView;
    public GameObject buttonPrefab;
    public Dropdown options;
    public Text dropdownLabel;
    private string previousInput = "abdjksabfck";

    public Text inputBox2;
    public Dropdown options2;
    public Text dropdownLabel2;
    private string previousInput2 = "abdjksabfck";

    public GameObject tracker;

    private CameraController mainCamera;

    private floorManager fm;
    // Use this for initialization
    void Start () {
        firstFloorRoomsDict = new Dictionary<string, GameObject>();
        firstRoomNames = new List<string>();

		secondFloorRoomsDict = new Dictionary<string, GameObject>();
		secondRoomNames = new List<string>();

        fm = GetComponent<floorManager>();


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

		for (int i =0; i<secondFloorRooms.Length; i++)
		{
			bool hasDuplicate = false;
			if(secondFloorRooms[i] != null)
			{
				foreach (string registered in secondRoomNames) {
					if (secondFloorRooms [i].name == registered) {
						hasDuplicate = true;
					}
				}
				if (!hasDuplicate) {

					secondFloorRoomsDict.Add (secondFloorRooms [i].name, secondFloorRooms [i]);
					secondRoomNames.Add (secondFloorRooms [i].name);
					Debug.Log ("Went Through");
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
        if(enterBox.text != enterPrev){
            foreach(Transform child in listView.transform)
            {
                Destroy(child.gameObject);
            }



            foreach (string room in firstRoomNames)
            {
                if (room.ToLower().Contains(enterBox.text))
                {
                    GameObject newButton = Instantiate(buttonPrefab);
                    newButton.transform.SetParent(listView.transform);
                    newButton.
                    //newButton.transform.parent = listView.transform;
                    newButton.AddComponent<ContentSizeFitter>();
                    

                }
            }

            foreach (string room2 in secondRoomNames)
            {
                if (room2.ToLower().Contains(enterBox.text))
                {
                    GameObject newButton = Instantiate(buttonPrefab);
                    newButton.transform.parent = listView.transform;
                    newButton.AddComponent<ContentSizeFitter>();

                }
            }



        }








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

			foreach(string room2 in secondRoomNames)
			{
				if (room2.ToLower().Contains(inputBox.text.ToLower()))
				{
					Dropdown.OptionData o = new Dropdown.OptionData();
					o.text = room2;
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
			foreach(string room2 in secondRoomNames)
			{
				if (room2.ToLower().Contains(inputBox2.text.ToLower()))
				{
					Dropdown.OptionData o2 = new Dropdown.OptionData();
					o2.text = room2;
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

        enterPrev = enterBox.text;
    }

    public void handleNav(){
      Navigate(null, null, false);
    }

    public void Navigate(GameObject startPos, Transform endPos, bool onFirstFloor)
    {
		Dictionary <string, GameObject> containsOption1;
		Dictionary <string, GameObject> containsOption2;
        int startFloor = 0;
        int endFloor = 0;
        if (startPos == null || endPos == null)
        {
            if (firstFloorRoomsDict.ContainsKey(options2.options[options2.value].text))
            {
                containsOption2 = firstFloorRoomsDict;
                startFloor = 1;
            }
            else
            {
                containsOption2 = secondFloorRoomsDict;
                startFloor = 2;
            }

            if (firstFloorRoomsDict.ContainsKey(options.options[options.value].text))
            {
                containsOption1 = firstFloorRoomsDict;
                endFloor = 1;
            }
            else
            {
                containsOption1 = secondFloorRoomsDict;
                endFloor = 2;
                onFirstFloor = false;
            }


            if (options2.value != 0 && options.value != 0)
            {
                GameObject tkr = Instantiate(tracker, containsOption2[options2.options[options2.value].text].transform.position, Quaternion.Euler(Vector3.zero));
                tkr.GetComponent<AIFinder>().target = containsOption1[options.options[options.value].text].transform;
                tkr.GetComponent<AIFinder>().startFloor = startFloor;
                //GameObject tkr = Instantiate(tracker, firstFloorRoomsDict[options2.options[options2.value].text].transform.position, Quaternion.Euler(Vector3.zero));
                //tkr.GetComponent<AIFinder>().target = firstFloorRoomsDict[options.options[options.value].text].transform;
                if (startFloor != endFloor)
                {
                    if (startFloor == 1)
                    {
                        tkr.GetComponent<AIFinder>().oneToTwo = true;
                    }
                    else
                    {
                        tkr.GetComponent<AIFinder>().twoToOne = true;

                    }
                }

                mainCamera.locked = true;
                mainCamera.tracker = tkr;


            }
        }
        else
        {

            if (onFirstFloor)
            {
                fm.showFloor2 = false;
            }
            else
            {
                fm.showFloor2 = true;
            }

            GameObject tkr = Instantiate(tracker, startPos.transform.position, Quaternion.Euler(Vector3.zero));
            tkr.GetComponent<AIFinder>().target = endPos.transform;
            tkr.GetComponent<AIFinder>().startFloor = startFloor;
            mainCamera.locked = true;
            mainCamera.tracker = tkr;
        }




    }




}
