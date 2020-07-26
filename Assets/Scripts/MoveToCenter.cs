using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MoveToCenter : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, Vector3.zero, 0.1f);
        if(this.transform.localPosition == Vector3.zero)
        {
            //GetComponentInChildren<VideoPlayer>().enabled = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
            
            Destroy(this);
        }
    }
}
