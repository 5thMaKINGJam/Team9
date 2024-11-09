using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    
    public AudioSource audioSource;
    public AudioClip footstepsGrass;
    public AudioClip jumpGrass;
    public float stepDelay = 0.5f;
    public float groundCheckDistance = 0.2f;

    private bool isWalking = false;
    private bool isJumping = false;
    private float stepTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        // 이동 중인지 감지
        if (Input.GetAxis("Horizontal") != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // 걷는 상태일 때 일정 간격으로 발소리 재생
        if (isWalking && !isJumping)
        {
            stepTimer += Time.deltaTime;
            if (stepTimer >= stepDelay)
            {
                audioSource.PlayOneShot(footstepsGrass);
                stepTimer = 0f;
            }
        }
        if (Input.GetButtonDown("Jump") && !isJumping) // 점프 키를 누른 순간 감지
        {
            isJumping = true;
            audioSource.PlayOneShot(jumpGrass); // 점프 소리 재생
        }

        // 착지 감지 (점프 후 다시 발이 닿았을 때)
        if (isJumping && isGrounded()) 
        {
            isJumping = false;
        }
        else
        {
            stepTimer = 0f;  // Idle 상태일 때 stepTimer 초기화 (소리 멈추기)
        }
    }


    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);        
        
    }
}

