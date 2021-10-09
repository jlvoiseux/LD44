using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startBehavior : MonoBehaviour
{
    public GameObject fader;

    RaycastHit hitInfo;
    float count;
    float timer;
    float timerNext = 0;
    bool countFlag = false;
    public float countSpeed = 3f;
    public float timeLimitNext = 0.1f;
    public float timeLimit = 0f;
    public Image circle;
    public bool startFlag = false;
    public bool refreshed = true;

    bool goBlack = false;
    // Start is called before the first frame update
    void Start()
    {        
        fader.SetActive(false);
        circle.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
                    startFlag = true;
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
        else if (refreshed == false) {
            refreshed = true;
        }

        if (startFlag) {
            if (!goBlack) {
                fader.SetActive(true);
                fader.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0, 0, 0, 0));
                goBlack = true;
            }
            if (fadeToBlack(0.5f) == 1) {
                SceneManager.LoadScene("mainScene");
            }
        }

        int fadeToBlack(float speed) {
            Color previousColor = fader.GetComponent<Renderer>().material.GetColor("_Color");
            Color newColor = new Vector4(previousColor.r - speed * Time.deltaTime, previousColor.g - speed * Time.deltaTime, previousColor.b - speed * Time.deltaTime, previousColor.a + speed * Time.deltaTime);
            fader.GetComponent<Renderer>().material.SetColor("_Color", newColor);
            if (newColor.r <= 0 && newColor.g <= 0 && newColor.b <= 0 && newColor.a >= 1) {
                return 1;
            }
            else return 0;
        }

    }
}
