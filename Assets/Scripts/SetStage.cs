using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetStage : MonoBehaviour
{
    public AudioSource m3;
    public AudioSource m1;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.Stage = 3;
            StartCoroutine(GameManager.Instance.AudioFade(m1, m3));
            Destroy(gameObject, 2f);
        }
    }
}
