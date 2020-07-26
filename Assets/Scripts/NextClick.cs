using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class NextClick : MonoBehaviour
{
    public static NextClick Instance { get; private set; }
    public Transform GIftBox;
    private GameObject Gift;
    public int i, k;
    private ScaleUp[] ScaleupScript;
    private VideoPlayer MessageVideo;
    [HideInInspector]
    public bool NextStart;
    [HideInInspector]
    public bool VideoPlaced;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        i = k = 0;
        NextStart = false;
        VideoPlaced = false;
    }
    private void Update()
    {
        NextStart = true;
        if (NextStart)
        {
            if(Input.GetMouseButtonDown(0) || (Input.GetTouch(0).phase == TouchPhase.Ended && Input.touchCount == 1))
            {
                Debug.Log("Mosue clicked");
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit an obj " + hit.transform.name);
                    if (hit.transform.tag == "Gift")
                    {
                        Debug.Log("Giftbox is hit");
                        i++;
                        Gift = GameObject.FindGameObjectWithTag("Gift");
                        hit.transform.GetComponent<BoxCollider>().enabled = false;
                        //Animator actiavte.
                        hit.transform.GetComponent<Animator>().enabled = true;

                        //Timelines enbaled
                        /*int timelines = hit.transform.GetComponentsInChildren<PlayableDirector>().Length;
                        for(int k = 0; k < timelines; k++)
                        {
                            hit.transform.GetChild(k).GetComponent<PlayableDirector>().enabled = true;
                        }*/


                        if (i <= 4)
                        {
                            StartCoroutine(CreateBox());
                            Debug.Log("StartCoroutine CreateBox");
                        }
                    }
                    else if (hit.transform.tag == "Muffin")
                    {
                        GameObject gift1 = GameObject.FindGameObjectWithTag("Finish");
                        ScaleupScript = FindObjectsOfType<ScaleUp>();
                        foreach(ScaleUp scripts in ScaleupScript)
                        {
                            scripts.ScaleDec = true;
                            scripts.ScaleInc = false;
                        }
                        Instantiate(Resources.Load("Projector"), gift1.transform.position, gift1.transform.rotation);
                        VideoPlaced = true;
                    }
                }
                else if (FindObjectOfType<VideoPlayer>() != null)
                {
                    Debug.Log("video player found");
                    if (k == 0 && MessageVideo == null)
                    {
                        MessageVideo = FindObjectOfType<VideoPlayer>();
                        k++;
                        MessageVideo.Pause();
                    }
                    else if (MessageVideo.isPaused)
                    {
                        MessageVideo.Play();
                    }
                    else if (MessageVideo.isPlaying)
                    {
                        MessageVideo.Pause();
                    }
                }
            }
            
        }
    }
    IEnumerator CreateBox()
    {
        yield return new WaitForSeconds(0.25f);
        
        if (i < 4)
        {
            Gift.transform.tag = "Player";
            if (i == 1)
            {
                Gift.transform.tag = "Finish";
            }
            Transform Box = Instantiate(GIftBox, Gift.transform.position + new Vector3(0, 0.01f * i, 0), Gift.transform.rotation);
            Box.transform.parent = Gift.transform;
            Box.transform.tag = "Gift";
            Box.localScale = new Vector3(1f - i * 0.15f, 1f - i * 0.15f, 1f - i * 0.15f);

            Box.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(EnableCollider(Box));
        }
        else if (i == 4)
        {
            StartCoroutine(CakeAppear(Gift));
        }

    }
    IEnumerator CakeAppear(GameObject Gift)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject ring = Instantiate(Resources.Load("Cake"), Gift.transform.position + new Vector3(0, 0.005f, 0), Gift.transform.rotation * Quaternion.Euler(0, 180, 0)) as GameObject;
        ring.transform.parent = Gift.transform;
        ring.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    IEnumerator EnableCollider(Transform gift)
    {
        yield return new WaitForSeconds(2);
        gift.GetComponent<BoxCollider>().enabled = true;
    }
}
