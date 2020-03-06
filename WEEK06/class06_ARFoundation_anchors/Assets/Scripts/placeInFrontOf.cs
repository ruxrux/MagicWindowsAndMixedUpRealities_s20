using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARAnchorManager))]


public class placeInFrontOf : MonoBehaviour
{
    private ARAnchorManager arAnchorManager;

    private List<ARAnchor> anchors = new List<ARAnchor>();

    [SerializeField]
    private Camera _camera;


    void Awake()
    {
        arAnchorManager = GetComponent<ARAnchorManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Began)
            return;

        Vector3 pos = _camera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, .3f));
        ARAnchor newAnchor = arAnchorManager.AddAnchor(new Pose(pos, Quaternion.identity));
    }
}
