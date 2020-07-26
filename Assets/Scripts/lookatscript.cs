using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatscript : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = FindObjectOfType<Camera>().transform;
    }
    void Update()
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        rotation.eulerAngles = rotation.eulerAngles + new Vector3(0, 180, 0);
        transform.rotation = Quaternion.Euler(rotation.eulerAngles);
    }
}
