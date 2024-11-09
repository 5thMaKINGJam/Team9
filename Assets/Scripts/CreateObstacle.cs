using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObstacle : MonoBehaviour
{
    public GameObject obstacle; // 프리팹
    public Vector2 pos;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            Instantiate(obstacle, pos, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
