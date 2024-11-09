using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.Stage = 3;
            Destroy(gameObject);
        }
    }
}
