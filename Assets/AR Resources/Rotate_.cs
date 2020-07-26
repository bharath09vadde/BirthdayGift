using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_ : MonoBehaviour
{
    private bool StartRotate;

    private void OnEnable()
    {
        StartRotate = true;
    }
    private void OnDisable()
    {
        StartRotate = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(StartRotate)
        {
            transform.Rotate(0, 0, 50 * Time.deltaTime);
        }
    }
}
