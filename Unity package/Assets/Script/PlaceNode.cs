using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceNode : MonoBehaviour
{
    [SerializeField]
    GameObject ARCam;

    [SerializeField]
    ARSessionOrigin m_SessionOrigin;

    Vector3 m_BallCameraOffset = new Vector3(0f, -0.4f, 1f);


    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject m_PlacedPrefab;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    public GameObject placedPrefab
    {
        get { return m_PlacedPrefab; }
        set { m_PlacedPrefab = value; }
    }

    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        m_SessionOrigin = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
        ARCam = m_SessionOrigin.transform.Find("AR Camera").gameObject;
        //transform.parent = ARCam.transform;

        Vector3 lastP = new Vector3();
        Vector3 currentP = new Vector3();
        currentP = ARCam.transform.position + ARCam.transform.forward * m_BallCameraOffset.z
            + ARCam.transform.up * m_BallCameraOffset.y;

        if (!spawnedObject.activeInHierarchy)
        {
            lastP = ARCam.transform.position + ARCam.transform.forward * m_BallCameraOffset.z
            + ARCam.transform.up * m_BallCameraOffset.y;
            
            //spawnedObject.transform.position = lastP;
            spawnedObject = Instantiate(m_PlacedPrefab, lastP, ARCam.transform.rotation);
        }

        if(currentP.x - lastP.x > 0.5f || currentP.y - lastP.y > 0.5f || currentP.z - lastP.z > 0.5f)
        {
            spawnedObject = Instantiate(m_PlacedPrefab, lastP, ARCam.transform.rotation);
            lastP = currentP;
        }

        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = s_Hits[0].pose;

                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);

                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
        }
        */
    }
}
