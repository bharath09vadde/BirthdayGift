using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class TapToPlace : MonoBehaviour
{
    public GameObject objecttoplace;
    public GameObject placementindicator;
    private ARSessionOrigin arorigin;
    private Pose placementpose;
    private bool placementposeisvalid = false;
    public bool modelplaced;
    public GameObject Main_Canvas;
    MakeAppearOnPlane MakeApper_script;

    private int i;

    void Start()
    {
        arorigin = FindObjectOfType<ARSessionOrigin>();
        MakeApper_script = arorigin.GetComponent<MakeAppearOnPlane>();
        modelplaced = false;
        Main_Canvas.SetActive(false);
        i = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (modelplaced == false)
        {

            updateplacementpose();
            updatePlacementIndicator();
        }

        if (placementposeisvalid && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began && !modelplaced)
        {
            Placeobject();
        }
    }

    private void Placeobject()
    {
        objecttoplace.transform.position = placementpose.position;
        arorigin.MakeContentAppearAt(objecttoplace.transform, objecttoplace.transform.position, Quaternion.identity);
        //objecttoplace.transform.eulerAngles = placementpose.rotation.eulerAngles;
        objecttoplace.SetActive(true);
        modelplaced = true;
        placementindicator.GetComponentInChildren<MeshRenderer>().enabled = false;
        Main_Canvas.SetActive(true);
    }
    

    private void updatePlacementIndicator()
    {
        if (placementposeisvalid)
        {
            placementindicator.GetComponentInChildren<MeshRenderer>().enabled = true;
            placementindicator.transform.SetPositionAndRotation(placementpose.position, placementpose.rotation);
        }
        else
        {
            placementindicator.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    private void updateplacementpose()
    {
        var screencenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arorigin.GetComponent<ARRaycastManager>().Raycast(screencenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds);

        placementposeisvalid = hits.Count > 0;
        if (placementposeisvalid)
        {
            placementpose = hits[0].pose;

            var cameraforward = Camera.current.transform.forward;
            var camerabearing = new Vector3(cameraforward.x, 0, cameraforward.z).normalized;
            placementpose.rotation = Quaternion.LookRotation(camerabearing);
        }
    }
    public void movemodel()
    {
        Main_Canvas.SetActive(false);

        modelplaced = false;
        placementindicator.GetComponentInChildren<MeshRenderer>().enabled = true;
        objecttoplace.SetActive(false);
    }


    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }

}
