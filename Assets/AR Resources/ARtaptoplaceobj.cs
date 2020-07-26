using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;

public class ARtaptoplaceobj : MonoBehaviour
{
    public Transform objecttoplace;
    public GameObject placementindicator;
    public GameObject lights;
    private ARSessionOrigin arorigin;
    private Pose placementpose;
    private bool placementposeisvalid = false;
    public bool modelplaced, firstplace;
    public GameObject Main_Canvas;

    public ARPlaneManager myplanemanager;

    private GameObject Projector, Root;

    void Start()
    {
        arorigin = FindObjectOfType<ARSessionOrigin>();
        modelplaced = false;
        Main_Canvas.SetActive(false);
        firstplace = true;
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
            if (!IsPointerOverUIObject())
            {
                Placeobject();
            }
        }
    }

    private void Placeobject()
    {
        TogglePlaneActive(false);
        placementindicator.GetComponentInChildren<MeshRenderer>().enabled = false;
        Main_Canvas.SetActive(true);

        if (firstplace)
        {
            Instantiate(objecttoplace, placementpose.position, placementpose.rotation);
            lights.transform.position = placementpose.position;
            firstplace = false;
            StartCoroutine(Enablestart());
            modelplaced = true;
        }
        else if(modelplaced == false)
        {
            modelplaced = true;
            if (Projector != null)
            {
                Projector.transform.position = placementpose.position;
                Projector.SetActive(true);
            }
            else if(Root != null)
            {
                var cameraforward = Camera.current.transform.forward;
                var camerabearing = new Vector3(cameraforward.x, 0, cameraforward.z).normalized;
                Root.transform.rotation = Quaternion.LookRotation(camerabearing);
                Root.transform.position = placementpose.position;
                Root.SetActive(true);
            }
        }
    }
    

    private void updatePlacementIndicator()
    {
        if (placementposeisvalid)
        {
            placementindicator.GetComponentInChildren<MeshRenderer>().enabled = true;
            placementindicator.transform.position = placementpose.position;
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
        }
    }
    public void movemodel()
    {
        TogglePlaneActive(true);
        Main_Canvas.SetActive(false);
        NextClick.Instance.NextStart = false;

        modelplaced = false;
        placementindicator.GetComponentInChildren<MeshRenderer>().enabled = true;
        //objecttoplace.SetActive(false);
        if(GameObject.FindGameObjectWithTag("Finish") != null)
        {
            Root = GameObject.FindGameObjectWithTag("Finish");
            Root.SetActive(false);
        }
        else if(GameObject.FindGameObjectWithTag("Projector") != null)
        {
            Projector = GameObject.FindGameObjectWithTag("Projector");
            Projector.SetActive(false);
        }
    }


    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }

    IEnumerator Enablestart()
    {
        yield return new WaitForSeconds(1);
        NextClick.Instance.NextStart = true;
    }

    public void TogglePlaneActive(bool bActive)
    {
        //disable the detected plane to avoid the collision
        foreach (var plane in myplanemanager.trackables)
        {
            plane.gameObject.SetActive(bActive);
        }
    }

}
