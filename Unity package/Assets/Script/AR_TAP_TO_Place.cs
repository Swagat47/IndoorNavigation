using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class AR_TAP_TO_Place : MonoBehaviour
{
    public GameObject gameobjectToInstantiate;   // declare gameobject that you want to instantiate 
   
    //camera offSet coordinates 
    Vector3 CamOffSet = new Vector3(0f, -0.3f, 1f);  // distance w.r.t screen or device 


    private GameObject spawnObject;   // object that spawn on scene 
   // private ARRaycastManager _arRayCastManager;     // Raycast declaration 
    
   // private Vector2 touchPosition;  // vector2 declare for determine mobile position where place  


    /*
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Awake()
    {
        _arRayCastManager = GetComponent<ARRaycastManager>();
    }
    // Start is called before the first frame update

    bool tryGetTouchPOsition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;

    }
    */


    [SerializeField]
    GameObject ARCam;

    [SerializeField]
    ARSessionOrigin m_SessionOrigin;


    /*
    // Update is called once per frame
    void Update()
    {
        if (!tryGetTouchPOsition(out Vector2 touchPosition))
            return;
        if (_arRayCastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(spawnObject == null)
            {
                spawnObject = Instantiate(gameobjectToInstantiate, hitPose.position, hitPose.rotation);   
            }
            else
            {
                spawnObject.transform.position = hitPose.position;
            }
        }
    }
    */
    private Vector3 ballPos = new Vector3();
    private Vector3 lastPos = new Vector3();
    private Vector3 currentPos = new Vector3();

    void Start()
    {
        m_SessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;
        transform.parent = ARCam.transform;

        spawnObject = Instantiate(gameobjectToInstantiate);

        ballPos = ARCam.transform.position + ARCam.transform.forward * CamOffSet.z
            + ARCam.transform.up * CamOffSet.y;
        spawnObject.transform.position = ballPos;
        lastPos = ballPos;
    }

    private void Update()
    {
        if (!spawnObject.activeInHierarchy)
        {
            spawnObject = Instantiate(gameobjectToInstantiate);

            ballPos = ARCam.transform.position + ARCam.transform.forward * CamOffSet.z
                + ARCam.transform.up * CamOffSet.y;
            spawnObject.transform.position = ballPos;
            lastPos = ballPos;
        }
        currentPos = ARCam.transform.position + ARCam.transform.forward * CamOffSet.z
            + ARCam.transform.up * CamOffSet.y;



        if (currentPos.x - lastPos.x > 0.5f || currentPos.y - lastPos.y > 0.5f || currentPos.z - lastPos.z > 0.5f)
        {
            lastPos = currentPos;
            spawnObject = Instantiate(gameobjectToInstantiate);

            ballPos = ARCam.transform.position + ARCam.transform.forward * CamOffSet.z
                + ARCam.transform.up * CamOffSet.y;
            spawnObject.transform.position = ballPos;
            lastPos = ballPos;
        }

    }

    public void PutObject()
    {
        spawnObject = Instantiate(gameobjectToInstantiate);

        ballPos = ARCam.transform.position + ARCam.transform.forward * CamOffSet.z
            + ARCam.transform.up * CamOffSet.y;
        spawnObject.transform.position = ballPos;

    }


}


