using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    //public AudioSource audioSource;
    //public AudioClip footsteps; // 
    //public AudioClip jump;
    //public float stepDelay = 0.5f;
    //public float groundCheckDistance = 0.2f;

    //private bool isWalking = false;
    //private bool isJumping = false;
    //private float stepTimer = 0f;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //// 이동 중인지 감지 (수평 및 수직 입력 모두 체크)
        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    isWalking = true;
        //}
        //else
        //{
        //    isWalking = false;
        //}

        // 걷는 상태일 때 일정 간격으로 발소리 재생
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            if (!transform.GetChild(0).GetComponent<AudioSource>().isPlaying)
                transform.GetChild(0).GetComponent<AudioSource>().Play();
        }


        // 점프 입력 감지 및 점프 소리 재생
        //if (Input.GetButtonDown("Jump") && !isJumping) // 점프 키를 누른 순간 감지
        //{
        //    isJumping = true;
        //    transform.GetChild(0).GetComponent<AudioSource>().Play(); // 점프 소리 재생
        //}

        //// 착지 감지 (점프 후 다시 발이 닿았을 때)
        //if (isJumping && isGrounded())
        //{
        //    isJumping = false;
        //}
    }

    //bool isGrounded()
    //{
    //    // 바닥과의 거리 설정하여 바닥에 닿았는지 체크
    //    return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    //}
}

