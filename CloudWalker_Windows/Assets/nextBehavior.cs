using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextBehavior : MonoBehaviour
{
    RaycastHit hitInfo;
    float count;
    float timer;
    float timerNext = 0;
    bool countFlag = false;
    public float countSpeed = 3f;
    public float timeLimitNext = 0.1f;
    public float timeLimit = 0f;
    public Image circle;
    public bool nextFlag = false;
    public bool refreshed = true;

    public gameManager gm;

    public GameObject messageBoard;
    public int currentMessage = 0;
    public int day = 1;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (nextFlag && timerNext < timeLimitNext) {
            timerNext += Time.deltaTime;
        }
        else if (nextFlag) {
            timerNext = 0;
            nextFlag = false;
            nextMessage();
        }

        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {
            if (hitInfo.collider.gameObject == this.gameObject && refreshed) {
                if (count < 1) {
                    timer = 0;
                    countFlag = true;
                    count += countSpeed * Time.deltaTime;
                    circle.fillAmount = count;
                }
                else {
                    nextFlag = true;
                    refreshed = false;
                }
            }
            else {
                if (countFlag && timer < timeLimit) {
                    timer += Time.deltaTime;
                }
                else {
                    timer = 0;
                    countFlag = false;
                    count = 0;
                    circle.fillAmount = count;
                }
            }
        }
        else if(refreshed == false) {
            refreshed = true;
        }
    }

    void nextMessage() {

        if (currentMessage == 0 && day == 1) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide4"));
        }
        else if (currentMessage == 1 && day == 1) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide5"));
        }
        else if (currentMessage == 2 && day == 1) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide6"));
        }
        else if (currentMessage == 3 && day == 1) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide7"));
        }
        else if (currentMessage == 4 && day == 1) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide8"));
        }
        else if (currentMessage == 5 && day == 1) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide9"));
            gm.messageDone = true;
            gameObject.SetActive(false);
        }

        if (currentMessage == 0 && day == 2) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide11"));
        }
        else if (currentMessage == 1 && day == 2) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide12"));
        }
        else if (currentMessage == 2 && day == 2) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide13"));
            gm.messageDone = true;
            gameObject.SetActive(false);
        }

        if (currentMessage == 0 && day == 3) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide15"));
        }
        else if (currentMessage == 1 && day == 3) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide16"));
        }
        else if (currentMessage == 2 && day == 3) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide17"));
        }
        else if (currentMessage == 3 && day == 3) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide18"));
            gm.messageDone = true;
            gameObject.SetActive(false);
        }

        if (currentMessage == 0 && day == 4) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide21"));
        }
        else if (currentMessage == 1 && day == 4) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide22"));
            gm.messageDone = true;
            gameObject.SetActive(false);
        }

        currentMessage++;


    }
}
