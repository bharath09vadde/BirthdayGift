using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public MeshRenderer[] BoxSides;
    public Material[] Wrappers;


    // Start is called before the first frame update
    void Start()
    {
        int i = NextClick.Instance.i;
        if(i > 0)
        {
            foreach (MeshRenderer Sides in BoxSides)
            {
                Sides.material = Wrappers[i];
            }
        }
        else if(i == 0)
        {
            foreach (MeshRenderer Sides in BoxSides)
            {
                Sides.material = Wrappers[0];
            }
        }

    }
}
