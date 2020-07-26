using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    public bool ShouldGoUp;
    public bool ScaleInc, ScaleDec;

    void Start()
    {
        ScaleInc = true;
        ScaleDec = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(ScaleInc)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(0.75f, 0.75f, 0.75f), 0.1f);
            if (ShouldGoUp)
            {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, new Vector3(0, 1.5f, 0), 0.1f);
            }
        }
        else if(ScaleDec)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(0, 0, 0), 0.1f);
            if(this.transform.localScale == Vector3.zero)
            {
                Destroy(GameObject.FindGameObjectWithTag("Finish"));
            }
        }
        
        
    }
}
