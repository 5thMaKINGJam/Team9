using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject.GetComponent<Player>());
            Destroy(other.gameObject.transform.GetChild(0).GetComponent<Animator>());
            
            GameManager.Instance.PlayerDie();
            if(gameObject.name == "Bridge") Destroy(gameObject);
        }
    }
}
