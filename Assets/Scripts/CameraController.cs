using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool locked;
    private bool playerExisted;
    private Quaternion defaultRotation;

    public GameObject tracker;

    public float moveSpeed;

    public float verticalOffset;
	public float Offset;


    private float PanSpeed = 20f;
    private float ZoomSpeedTouch = 0.1f;
    private float ZoomSpeedMouse = 0.5f;

    public float[] BoundsX = new float[] { -10f, 5f };
    public float[] BoundsZ = new float[] { -18f, -4f };
    public float[] ZoomBounds = new float[] { 10f, 85f };

    private Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; // Touch mode only

    private bool wasZoomingLastFrame; // Touch mode only
    private Vector2[] lastZoomPositions; // Touch mode only

    private floorManager fm;
                                         
    void Start () {
        defaultRotation = transform.rotation;
        cam = GetComponent<Camera>();
        fm = FindObjectOfType<floorManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (tracker == null)
        {
            if (playerExisted)
            {
                playerExisted = false;
                locked = false;
                FindObjectOfType<UIManager>().endCorse();
                //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-10);
            }
        }

        if (locked)
        {
            playerExisted = true;
            Vector3 tagetPos = tracker.transform.position;
            tagetPos.y += verticalOffset;

			//if(Vector3.Distance(tracker.transform.position, transform.position) > verticalOffset+Offset){
            //    transform.position = Vector3.Lerp(transform.position, tagetPos, moveSpeed * Time.deltaTime);
                //Vector3 direction = Vector3.MoveTowards(transform.position, tagetPos, moveSpeed * Time.deltaTime);
                //transform.position = direction;
				//transform.LookAt(tracker.transform.position);

            //    Quaternion lookRot = Quaternion.LookRotation(tracker.transform.position - transform.position);
                //transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * moveSpeed);
			//}
            transform.position = Vector3.Lerp(transform.position, tagetPos, moveSpeed * Time.deltaTime);
            transform.LookAt(tracker.transform.position);
        }
        else
        {
            if (!fm.showFloor2){
                transform.position = new Vector3(transform.position.x, 16,transform.position.z);
            }
            else{
                transform.position = new Vector3(transform.position.x, 26, transform.position.z);
            }

            //transform.rotation = defaultRotation;
        }

        HandleTouch();
        //if (Input.touchSupported && Application.platform != RuntimePlatform.WebGLPlayer)
        //{
        //    HandleTouch();
        //}
        //else
        //{
        //    HandleMouse();
        //}
    }

    void HandleTouch()
    {
        switch (Input.touchCount)
        {

            case 1: // Panning
                locked = false;
                wasZoomingLastFrame = false;
                Debug.Log("Panning");

                // If the touch began, capture its position and its finger ID.
                // Otherwise, if the finger ID of the touch doesn't match, skip it.
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastPanPosition = touch.position;
                    panFingerId = touch.fingerId;
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                {
                    PanCamera(touch.position);
                }
                break;

            case 2: // Zooming
                Debug.Log("Zooming");
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    // Zoom based on the distance between the new positions compared to the 
                    // distance between the previous positions.
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, ZoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }
                break;

            default:
                wasZoomingLastFrame = false;
                break;
        }
    }

    void HandleMouse()
    {
        // On mouse down, capture it's position.
        // Otherwise, if the mouse is still down, pan the camera.
        if (Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }

        // Check for scrolling to zoom the camera
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, ZoomSpeedMouse);
    }

    void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * PanSpeed, 0, offset.y * PanSpeed);

        // Perform the movement
        transform.Translate(move, Space.World);

        // Ensure the camera remains within bounds.
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
        transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }
}
