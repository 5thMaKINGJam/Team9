using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    public AudioSource footsteps; // 걷는 소리
    public AudioSource jump; // 점프 소리
    public float stepDelay = 0.5f; // 걷는 소리 간격

    private bool isWalking = false;
    private bool isJumping = false;
    private bool isGrounded = false;

    private float stepTimer = 0f; // 걷는 소리 타이머

    void Update()
    {
        // 이동 입력 체크
        isWalking = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        // 걷고 있고 땅에 있을 때
        if (isWalking && isGrounded)
        {
            stepTimer += Time.deltaTime;

            // 걷는 소리 재생
            if (stepTimer >= stepDelay)
            {
                footsteps.Play();
                stepTimer = 0f; // 타이머 초기화
            }

            // 점프 입력 체크
            if (Input.GetButtonDown("Jump") && !isJumping)
            {
                isJumping = true;
                footsteps.Stop(); // 걷는 소리 중지
                jump.Play(); // 점프 소리 재생
            }
        }
        else
        {
            footsteps.Stop(); // 걷는 소리 중지
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}





