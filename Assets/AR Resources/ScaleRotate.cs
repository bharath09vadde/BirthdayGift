using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ScaleRotate : MonoBehaviour
{

    MakeAppearOnPlane m_MakeAppearOnPlane;
    ARSessionOrigin m_SessionOrigin;
    ARRaycastManager m_RaycastManager;

    [HideInInspector]
    public float velocityX = 0.0f;

    private Camera arCamera;


    private bool touchended;
    private bool videopalced;


    // Start is called before the first frame update
    void Start()
    {
        touchended = true;
        arCamera = GetComponentInChildren<Camera>();
        m_SessionOrigin = GetComponent<ARSessionOrigin>();
        m_MakeAppearOnPlane = GetComponent<MakeAppearOnPlane>();
        videopalced = NextClick.Instance.VideoPlaced;
    }
    private void OnEnable()
    {
        velocityX = 60f;
        m_MakeAppearOnPlane.rotation = Quaternion.AngleAxis(velocityX, Vector3.up);
    }

    private void LateUpdate()
    {
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;


            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if (m_SessionOrigin.transform.localScale.x >= 0.3f && m_SessionOrigin.transform.localScale.x <= 7.5f)
            {
                m_SessionOrigin.transform.localScale += Vector3.one * (deltaMagnitudeDiff * 0.001f);
                m_SessionOrigin.transform.localScale = new Vector3(Mathf.Clamp(m_SessionOrigin.transform.localScale.x, 0.3f, 7.5f),
                                                                   Mathf.Clamp(m_SessionOrigin.transform.localScale.y, 0.3f, 7.5f),
                                                                   Mathf.Clamp(m_SessionOrigin.transform.localScale.z, 0.3f, 7.5f));
            }

        }
        
        //Rotation of the objects script.
        if (Input.touchCount == 1 && !videopalced)
        {
            
            var touch = Input.GetTouch(0);

            velocityX -=  touch.deltaPosition.x * (Time.deltaTime / (touch.deltaTime + 0.001f)) * 0.1f;
            m_MakeAppearOnPlane.rotation = Quaternion.AngleAxis(velocityX , Vector3.up);
        }
        
    }
}
