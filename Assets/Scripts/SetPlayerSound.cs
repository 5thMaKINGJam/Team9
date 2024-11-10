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
            other.GetComponent<PlayerSoundEffects>().footsteps = changeMoveAudio;
            other.GetComponent<PlayerSoundEffects>().jump = changeJumpAudio;
        }
    }
}
