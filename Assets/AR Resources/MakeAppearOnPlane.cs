using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Moves the ARSessionOrigin in such a way that it makes the given content appear to be
/// at a given location acquired via a raycast.
/// </summary>
[RequireComponent(typeof(ARSessionOrigin))]
[RequireComponent(typeof(ARRaycastManager))]
public class MakeAppearOnPlane : MonoBehaviour
{

    //[SerializeField]
    [Tooltip("A transform which should be made to appear to be at the touch point.")]
    private Transform m_Content;

    /// <summary>
    /// A transform which should be made to appear to be at the touch point.
    /// </summary>
    private Transform content
    {
        get { return m_Content; }
        set { m_Content = value; }
    }

    [SerializeField]
    [Tooltip("The rotation the content should appear to have.")]
    Quaternion m_Rotation;

    /// <summary>
    /// The rotation the content should appear to have.
    /// </summary>
    public Quaternion rotation
    {
        get { return m_Rotation; }
        set
        {
            m_Rotation = value;
            if (m_SessionOrigin != null)
            {
                m_Content = GameObject.FindGameObjectWithTag("Gift").transform;
                m_SessionOrigin.MakeContentAppearAt(content, content.transform.position, m_Rotation);
            }
                
        }
    }

    void Awake()
    {
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    /*void Update()
    {
        if (Input.touchCount == 0 || m_Content == null)
        {
            FingerPressedLong = false;
            timestarted = 0;
            i = 0;
            return;
        }
            

        
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            Ray ray = arCamera.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                display_text.text = "hit obj :" + hit.transform.tag;
                if(hit.transform.tag == "Model")
                {
                    if (i == 0)
                    {
                        timestarted = Time.time;
                        i++;
                    }
                    if (Time.time - timestarted > timeDelayThreshold)
                    {
                        display_text.text = "time diff:" + (Time.time - timestarted);
                        FingerPressedLong = true;
                        scalerot_script.rotate = false;
                    }
                    
                    if (FingerPressedLong && touch.phase == TouchPhase.Moved && m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                    {
                        // Raycast hits are sorted by distance, so the first one
                        // will be the closest hit.
                        var hitPose = s_Hits[0].pose;

                        // This does not move the content; instead, it moves and orients the ARSessionOrigin
                        // such that the content appears to be at the raycast hit position.
                        m_SessionOrigin.MakeContentAppearAt(content, hitPose.position);
                    }
                }
                else if(hit.transform.tag == "Button")
                {
                    hit.transform.GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    scalerot_script.rotate = true;
                }
            }
            
        }
    }*/

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        ARSessionOrigin m_SessionOrigin;

        ARRaycastManager m_RaycastManager;

    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;

    }
}
