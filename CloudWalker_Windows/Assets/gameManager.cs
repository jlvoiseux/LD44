using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject fader;
    public GameObject blood;
    public GameObject messageBoard;
    public nextBehavior nextButton;
    public MapGenerator mapGen;

    public Image energy;

    public int scoreDay1 = 10;
    public int scoreDay2 = 10;
    public int scoreDay3 = 10;
    public int scoreDay4 = 10;

    bool b1f1 = false;
    bool b1f2 = false;
    bool b2f1 = false;
    bool b3f1 = false;

    public bool fallOn = false;

    public float fallSpeed = 5f;
    public Transform player;

    public float spawnFreq1 = 5f;
    public float spawnFreq2 = 5f;
    public float spawnFreq3 = 5f;
    public float spawnFreq4 = 5f;

    public AudioSource atm;
    public AudioSource h1;
    public AudioSource h2;
    public AudioSource h3;
    public AudioSource fall;
    public AudioSource music;

    public bool messageDone = false;

    public int state = 0;
    float timer = 0f;
    int day = 1;

    bool fadeInFlag = false;
    bool fadeOutFlag = false;
    bool startFlag = false;
    bool day1Flag = false;

    public enemyGenerator gen;

    // Start is called before the first frame update
    void Start()
    {
        energy.fillAmount = 1;
        blood.SetActive(false);
        nextButton.gameObject.SetActive(false);
        mapGen.GenerateMap();
        fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day1"));
        messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide3"));
        if (state == 0)
            fader.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
        else
            fader.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(1, 1, 1, 0));
    }

    // Update is called once per frame
    void Update() {

        if (fallOn) {
            player.Translate(Vector3.down* fallSpeed*Time.deltaTime);
        }

        if (state == 0 && !startFlag) {
            if (fadeIn(1, 0.5f, true) == 1) {
                startFlag = true;
                timer = 2f;
            }
        }

        if (state == 0 && startFlag) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                music.Play();
                atm.Play();
                timer = 0f;
                state = 1;
            }
        }

        if (state == 1 && !day1Flag) {
            if (fadeOut(1, 0.5f) == 1) {
                day1Flag = true;
                nextButton.gameObject.SetActive(true);
            }
        }

        if (state == 1 && day1Flag && messageDone) {
            messageDone = false;
            timer = 1;
            state = 2;
        }

        if (state == 2 && gen.score < scoreDay1) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                gen.generateFlag = true;
                timer = spawnFreq1;
            }
        }

        if(state == 2 && gen.score == 1) {
            energy.fillAmount = 0.92f;
        }

        if (state == 2 && gen.score == 2) {
            energy.fillAmount = 0.84f;
        }

        if (state == 2 && gen.score == 3) {
            energy.fillAmount = 0.76f;
        }

        if (state == 2 && gen.score == 4) {
            energy.fillAmount = 0.68f;
        }

        if (state == 2 && gen.score == 5) {
            energy.fillAmount = 0.6f;
        }


        if (state == 2 && gen.score >= scoreDay1) {
            gen.killAll = true;
            timer = 3;
            state = 3;
            fader.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0, 0, 0, 0));
        }

        if (state == 3) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                state = 4;
                
            }
        }

        if (state == 4) {
            if (fadeToBlack(0.5f) == 1) {
                state = 5;
                atm.Stop();
            }
        }

        if(state == 5) {
            if (fadeIn(2, 0.5f, true) == 1) {
               state = 6;
                atm.Play();
            }
        }

        if(state == 6) {
            if (fadeOut(2, 0.5f) == 1) {
                state = 7;
                switchDay();
            }
        }

        if (state == 7 && messageDone) {
            messageDone = false;
            timer = 1;
            state = 8;
        }

        if (state == 8 && gen.score < scoreDay2) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                gen.generateFlag = true;
                timer = spawnFreq2;
            }
        }

        if (state == 8 && gen.score >= scoreDay2) {
            gen.killAll = true;
            timer = 3;
            state = 9;
            fader.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0, 0, 0, 0));
        }

        if (state == 8 && gen.score == 1) {
            energy.fillAmount = 0.52f;
        }

        if (state == 8 && gen.score == 2) {
            energy.fillAmount = 0.44f;
        }

        if (state == 8 && gen.score == 3) {
            energy.fillAmount = 0.36f;
        }

        if (state == 8 && gen.score == 4) {
            energy.fillAmount = 0.28f;
        }

        if (state == 8 && gen.score == 5) {
            energy.fillAmount = 0.2f;
        }

        if (state == 9) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                state = 10;
            }
        }

        if (state == 10) {
            if (fadeToBlack(0.5f) == 1) {
                state = 11;
                atm.Stop();
            }
        }

        if (state == 11) {
            if (fadeIn(3, 0.5f, true) == 1) {
                state = 12;
                atm.Play();
            }
        }

        if (state == 12) {
            if (fadeOut(3, 0.5f) == 1) {
                state = 13;
                switchDay();
            }
        }

        if (state == 13 && messageDone) {
            messageDone = false;
            timer = 1;
            state = 14;
        }

        if (state == 14 && gen.score < scoreDay3) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                gen.generateFlag = true;
                timer = spawnFreq3;
            }
        }

        if (state == 14 && gen.score >= scoreDay3/2) {
            if (!b1f1) {
                h1.Play();
                blood.SetActive(true);
                blood.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Blood/blood1"));
                b1f1 = true;
            }
        }

        if (state == 14 && gen.score >= scoreDay3) {
            gen.killAll = true;
            timer = 3;
            state = 15;
            fader.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0, 0, 0, 0));
        }

        if (state == 14 && gen.score == 1) {
            energy.fillAmount = 0.12f;
        }

        if (state == 14 && gen.score == 2) {
            energy.fillAmount = 0.04f;
        }

        if (state == 14 && gen.score == 3) {
            energy.color = Color.red;
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide19"));
            energy.fillAmount = 1f;
        }

        if (state == 14 && gen.score == 4) {
            energy.fillAmount = 0.92f;
        }

        if (state == 14 && gen.score == 5) {
            energy.fillAmount = 0.84f;
        }

        if (state == 15) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                state = 16;
            }
        }

        if (state == 16) {
            if (fadeToBlack(0.5f) == 1) {
                energy.fillAmount = 1f;
                state = 17;
                blood.SetActive(false);
                h1.Stop();
                atm.Stop();

            }
        }

        if (state == 17) {
            if (fadeIn(4, 0.5f, true) == 1) {
                state = 18;
                atm.Play();
            }
        }

        if (state == 18) {
            if (fadeOut(4, 0.5f) == 1) {
                state = 19;
                switchDay();
            }
        }

        if (state == 19 && messageDone) {
            messageDone = false;
            timer = 1;
            state = 20;
        }

        if (state == 20 && gen.score < scoreDay4) {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                gen.generateFlag = true;
                if (!b1f2) {
                    h1.Play();
                    blood.SetActive(true);
                    b1f2 = true;
                }
                timer = spawnFreq4;
            }
        }

        if(state == 20 && gen.score >= scoreDay4 / 2) {
            if (!b3f1) {
                b3f1 = true;
                h2.Stop();
                h3.Play();
                blood.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Blood/blood3"));
            }
        }
        else if (state == 20 && gen.score >= 2) {
            if (!b2f1) {
                b2f1 = true;
                h1.Stop();
                h2.Play();
                blood.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Blood/blood2"));
            }
        }
        

        if (state == 20 && gen.score >= scoreDay4) {
            gen.killAll = true;
            timer = 3;
            state = 21;
            fader.GetComponent<Renderer>().material.SetColor("_Color", new Vector4(0, 0, 0, 0));
        }

        if (state == 20 && gen.score == 1) {
            energy.fillAmount = 0.9f;
        }
        if (state == 20 && gen.score == 2) {
            energy.fillAmount = 0.8f;
        }
        if (state == 20 && gen.score == 3) {
            energy.fillAmount = 0.7f;
        }
        if (state == 20 && gen.score == 4) {
            energy.fillAmount = 0.6f;
        }
        if (state == 20 && gen.score == 5) {
            energy.fillAmount = 0.5f;
        }
        if (state == 20 && gen.score == 6) {
            energy.fillAmount = 0.4f;
        }
        if (state == 20 && gen.score == 7) {
            energy.fillAmount = 0.3f;
        }
        if (state == 20 && gen.score == 8) {
            energy.fillAmount = 0.2f;
        }
        if (state == 20 && gen.score == 9) {
            energy.fillAmount = 0.1f;
        }
        if (state == 20 && gen.score == 10) {
            energy.fillAmount = 0f;
        }

        if (state == 21) {
            timer -= Time.deltaTime;
            if(timer < 1 && !fallOn) {
                fallOn = true;
            }
            if (timer <= 0) {
                state = 22;
                
            }
        }

        if (state == 22) {
            if (fadeToBlack(0.5f) == 1) {
                blood.SetActive(false);
                state = 23;
                fall.Play();
                atm.Stop();
                timer = 2.5f;
                h3.Stop();
                music.Stop();

            }            
        }

        if (state == 23) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                switchDay();
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

    int fadeIn(int day, float speed, bool startFromBlack) {
        if (day == 1 && !fadeInFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day1"));
            fadeInFlag = true;
        }
        else if (day == 2 && !fadeInFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day2"));
            fadeInFlag = true;
        }
        else if (day == 3 && !fadeInFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day3"));
            fadeInFlag = true;
        }
        else if (day == 4 && !fadeInFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day4"));
            fadeInFlag = true;
        }
        else if (day == 5 && !fadeInFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day5"));
            fadeInFlag = true;
        }
        else if (day == 6 && !fadeInFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day6"));
            fadeInFlag = true;
        }
        Color previousColor = fader.GetComponent<Renderer>().material.GetColor("_Color");
        if (startFromBlack == true) {
            Color newColor = new Vector4(previousColor.r + speed * Time.deltaTime, previousColor.g + speed * Time.deltaTime, previousColor.b + speed * Time.deltaTime, previousColor.a);
            fader.GetComponent<Renderer>().material.SetColor("_Color", newColor);
            if (newColor.r >= 1 && newColor.g >= 1 && newColor.b >= 1) {
                fadeInFlag = false;
                return 1;
            }
            else return 0;
        }
        else {
            Color newColor = new Vector4(previousColor.r, previousColor.g, previousColor.b, previousColor.a + speed * Time.deltaTime);
            if (newColor.a >= 1) {
                fadeInFlag = false;
                return 1;
            }
            else return 0;
        }    
    }

    int fadeOut(int day, float speed) {
        if (day == 1 && !fadeOutFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day1"));
            fadeOutFlag = true;
        }
        else if(day == 2 && !fadeOutFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day2"));
            fadeOutFlag = true;
        }
        else if (day == 3 && !fadeOutFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day3"));
            fadeOutFlag = true;
        }
        else if (day == 4 && !fadeOutFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day4"));
            fadeOutFlag = true;
        }
        else if (day == 5 && !fadeOutFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day5"));
            fadeOutFlag = true;
        }
        else if (day == 6 && !fadeOutFlag) {
            fader.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/day6"));
            fadeOutFlag = true;
        }
        Color previousColor = fader.GetComponent<Renderer>().material.GetColor("_Color");
        Color newColor = new Vector4(previousColor.r, previousColor.g, previousColor.b, previousColor.a - speed * Time.deltaTime);
        fader.GetComponent<Renderer>().material.SetColor("_Color", newColor);
        if (newColor.a <= 0) {
            fadeOutFlag = false;
            return 1;
        }
        else return 0;
    }

    void switchDay() {
        if(day == 1) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide10"));
        }
        else if(day == 2) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide14"));
        }
        else if (day == 3) {
            messageBoard.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>("Slides/Slide20"));
        }
        else if(day == 4) {
            SceneManager.LoadScene("finalScene");
        }
        gen.score = 0;
        gen.killAll = false; ;
        nextButton.gameObject.SetActive(true);
        day++;
        nextButton.day = day;
        nextButton.currentMessage = 0;
    }
}
