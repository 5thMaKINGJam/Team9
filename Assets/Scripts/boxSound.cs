using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxSound : MonoBehaviour
{
    AudioSource sound;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player")
        {
            sound.Play();
        }
    }
    private void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag == "Player")
        {
            sound.Stop();
        }
    }
}
