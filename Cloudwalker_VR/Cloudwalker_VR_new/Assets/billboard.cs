using UnityEngine;


public class billboard : MonoBehaviour {
    void Update() {
        transform.LookAt(Camera.main.transform.position, -Vector3.up);
    }
}