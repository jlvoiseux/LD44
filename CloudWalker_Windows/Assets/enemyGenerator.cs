using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
    public List<Transform> generators;
    public bool generateFlag = false;
    public GameObject enemy;
    GameObject enemyTemp;
    public Transform target;
    public int score = 0;
    public bool killAll = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generateFlag) {
            generateFlag = false;
            generateEnemy();
        }
    }

    public void generateEnemy() {
        int index = Random.Range(0, 15);
        enemyTemp = Instantiate(enemy, generators[index].position, Quaternion.identity);
        enemyTemp.GetComponent<enemyBehavior>().target = target;
        enemyTemp.GetComponent<enemyBehavior>().gen = this;
        Debug.Log("Enemy Generated");
    }
}
