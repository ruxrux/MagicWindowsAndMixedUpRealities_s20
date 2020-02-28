using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


[RequireComponent(typeof(ARRaycastManager))]

public class placeStuff : MonoBehaviour
{
    [SerializeField]
    GameObject m_PlacedPrefab;


    ARRaycastManager m_RaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();


    private GameObject spawnedObject;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Vector2 touchPosition = new Vector2();
            touchPosition = Input.GetTouch(0).position;

            //if (!TryGetTouchPosition(out Vector2 touchPosition))
            //    return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))  // what other types of TRACKABLES :)
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(m_PlacedPrefab, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }
    }

}
