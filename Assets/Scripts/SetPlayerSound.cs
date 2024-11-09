using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerSound : MonoBehaviour
{
    public AudioClip changeJumpAudio;
    public AudioClip changeMoveAudio;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            AudioClip temp = changeMoveAudio;
            changeMoveAudio = other.GetComponent<AudioSource>().clip;
            other.GetComponent<AudioSource>().clip = temp;

            temp = changeJumpAudio;
            changeJumpAudio = other.transform.GetChild(0).GetComponent<AudioSource>().clip;
            other.transform.GetChild(0).GetComponent<AudioSource>().clip = temp;
        }
    }
}
