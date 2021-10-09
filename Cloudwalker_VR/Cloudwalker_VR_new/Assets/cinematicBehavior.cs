using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cinematicBehavior : MonoBehaviour {
    public GameObject fader;

    public float speed1 = 5f;
    public float speed2 = 5f;
    public float speed3 = 5f;
    public float speed4 = 5f;

    Vector3 initt = new Vector3(-20.2f, 11.09f, -14);
    Vector3 goal1t = new Vector3(-20.02f, 14.02f, 1.06f);
    Vector3 goal2t = new Vector3(-5.84f, 7.02f, 0.22f);
    Vector3 goal3t = new Vector3(-6.519f, 4.988f, 0.358f);

    Quaternion initr = Quaternion.Euler(19.056f, 45.367f, 1.193f);
    Quaternion goal1r = Quaternion.Euler(30.633f, 90f, 0f);
    Quaternion goal2r = Quaternion.Euler(90f, 0f, -90f);
    Quaternion goal3r = Quaternion.Euler(90f, 0f, -90f);

    float timer = 30f;
    int state = 0;

    // Start is called before the first frame update
    void Start() {
        fader.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        transform.position = initt;
        transform.rotation = initr;
    }

    // Update is called once per frame
    void Update() {
        if (state == 0) {
            if (fadeIn(0.5f, true) == 1) {
                state = 1;
            }
        }

        if (state == 1) {
            if (fadeOut(0.5f) == 1) {
                state = 2;
            }
        }

        if (state == 2) {
            transform.position = Vector3.Lerp(transform.position, goal1t, Time.deltaTime * speed1);
            transform.rotation = Quaternion.Lerp(transform.rotation, goal1r, Time.deltaTime * speed1);
            if (Vector3.Distance(transform.position, goal1t) < 0.5f && Quaternion.Angle(transform.rotation, goal1r) < 0.5f) {
                state = 3;
            }
        }
        if (state == 3) {
            transform.position = Vector3.Lerp(transform.position, goal2t, Time.deltaTime * speed2);
            transform.rotation = Quaternion.Lerp(transform.rotation, goal2r, Time.deltaTime * speed2);
            if (Vector3.Distance(transform.position, goal2t) < 0.5f && Quaternion.Angle(transform.rotation, goal2r) < 0.5f) {
                state = 4;
            }
        }
        if (state == 4) {
            timer -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, goal3t, Time.deltaTime * speed3);
            transform.rotation = Quaternion.Lerp(transform.rotation, goal3r, Time.deltaTime * speed3);
            if (timer <= 0) {
                fader.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0, 0, 0, 0));
                state = 5;
                timer = 3;
            }
        }
        if (state == 5) {
            if (fadeToBlack(0.5f) == 1) {

            }
            timer -= Time.deltaTime;
            if (timer <= 0) {
                SceneManager.LoadScene("menuScene");
            }
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

    int fadeIn(float speed, bool startFromBlack) {
        fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day5"));
        Color previousColor = fader.GetComponent<Renderer>().material.GetColor("_Color");
        if (startFromBlack == true) {
            Color newColor = new Vector4(previousColor.r + speed * Time.deltaTime, previousColor.g + speed * Time.deltaTime, previousColor.b + speed * Time.deltaTime, previousColor.a);
            fader.GetComponent<Renderer>().material.SetColor("_Color", newColor);
            if (newColor.r >= 1 && newColor.g >= 1 && newColor.b >= 1) {
                return 1;
            }
            else return 0;
        }
        else {
            Color newColor = new Vector4(previousColor.r, previousColor.g, previousColor.b, previousColor.a + speed * Time.deltaTime);
            if (newColor.a >= 1) {
                return 1;
            }
            else return 0;
        }
    }


    int fadeOut(float speed) {
        Color previousColor = fader.GetComponent<Renderer>().material.GetColor("_Color");
        Color newColor = new Vector4(previousColor.r, previousColor.g, previousColor.b, previousColor.a - speed * Time.deltaTime);
        fader.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        if (newColor.a <= 0) {
            return 1;
        }
        else return 0;
    }
}