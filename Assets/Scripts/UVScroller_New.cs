using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroller_New : MonoBehaviour
{
    [ExecuteInEditMode]
    public float scrollSpeed, MainoffsetX, MainoffsetY;

    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float offest = Time.time * scrollSpeed;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(MainoffsetX * offest, MainoffsetY * offest);
    }
}
