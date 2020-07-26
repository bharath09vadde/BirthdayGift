using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledIn : MonoBehaviour
{
    private Vector3 ActualScale;

    private void Awake()
    {
        ActualScale = this.gameObject.transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        //StartCoroutine(Scaletozero());
    }
    IEnumerator Scaletozero()
    {
        yield return new WaitForSeconds(0.001f);
        transform.localScale = Vector3.zero;
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.localScale.z != 1)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, ActualScale, 0.05f);
        }
    }
}
