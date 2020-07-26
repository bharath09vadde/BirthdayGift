using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSelect : MonoBehaviour
{
    private ARtaptoplaceobj artap_script;
    private int count;
    private Color grey_, green_;

    // Start is called before the first frame update
    void Start()
    {
        artap_script = GetComponent<ARtaptoplaceobj>();
        grey_ = new Color(0.4470588f, 0.4470588f, 0.4470588f, 1);
        green_ = new Color(0, 0.6627451f, 0.3098039f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(artap_script.modelplaced == true && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag == "Info")
                {
                    hit.transform.GetComponent<Image>().color = green_;
                    hit.transform.GetChild(0).gameObject.SetActive(true);
                }
                else if(hit.transform.tag == "Close")
                {
                    hit.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Image>().color = grey_;
                    hit.transform.parent.gameObject.SetActive(false);
                }
                else if(hit.transform.tag == "Weblink")
				{
					Application.OpenURL("https://www.pilkington.com/de-de/de/produkte/produktkategorien/spezialglaeser/pilkington-mirropane-chrome");
				}
            }
        }
    }
}
