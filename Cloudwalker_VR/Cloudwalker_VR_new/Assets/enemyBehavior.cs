using UnityEngine;
using UnityEngine.UI;

public class enemyBehavior: MonoBehaviour {

    public Camera camera;
    public float speed = 5f;
    public float threshold = 0.5f;
    public Transform target;
    public enemyGenerator gen;
    Vector3 tempPos;

    RaycastHit hitInfo;
    bool countFlag = false;
    float count = 0f;
    public float countSpeed = 2f;
    float timer = 0f;
    float timerWin = 0f;
    public float timeLimit = 1f;
    public float timeLimitWin = 0.5f;
    public Image circle;
    bool hasWon = false;

    private void Update() {

        if (gen.killAll) {
            Destroy(gameObject);
        }      

        

        tempPos = new Vector3(target.position.x, transform.position.y, target.position.z);
        float step = speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, tempPos) > threshold) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            Vector3 relativePos = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
        else {
            Destroy(gameObject);
        }

    
        if(transform.position.y < -200) {
            gen.generateEnemy();
            Destroy(gameObject);
        }

        if (hasWon && timerWin < timeLimitWin) {
            timerWin += Time.deltaTime;
        }
        else if (hasWon) {
            gen.score++;
            Destroy(gameObject);
        }

        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo)) {            
            if (hitInfo.collider.gameObject == this.transform.GetChild(0).gameObject) {
                if (count < 1) {
                    timer = 0;
                    countFlag = true;
                    count += countSpeed * Time.deltaTime;
                    circle.fillAmount = count;
                }
                else {
                    hasWon = true;
                    circle.color = Color.green;
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
        

    }
}