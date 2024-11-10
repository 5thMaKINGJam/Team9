using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffects : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footsteps;
    public AudioClip jump;
    public float stepDelay = 0.5f;

    private bool isWalking = false;
    private bool isJumping = false;
    private bool isGrounded = false;
    private float stepTimer = 0f;

    void Start()
    {
        // 발자국 소리 루프 설정
        audioSource.clip = footsteps;
        audioSource.loop = true;
    }

    void Update()
    {
        // 이동 중인지 감지 (수평 및 수직 입력 모두 체크)
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // 걷는 상태이고 바닥에 있을 때만 발자국 소리 재생
        if (isWalking && isGrounded && !isJumping)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // 걷기 소리 재생 시작
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // 걷기 소리 멈춤
            }
        }

        // 점프 입력 감지 및 점프 소리 재생
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            StartCoroutine(PlayJumpSound()); // 점프 소리 코루틴 호출
        }
    }

    // 바닥에 닿았는지 확인
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

    // 점프 소리를 끝까지 재생하는 코루틴
    private IEnumerator PlayJumpSound()
    {
        isJumping = true;
        audioSource.Stop(); // 기존 소리 멈추기
        audioSource.clip = jump;
        audioSource.Play(); // 점프 소리 재생
        yield return new WaitForSeconds(jump.length); // 점프 소리가 끝날 때까지 대기
        isJumping = false;
        audioSource.clip = footsteps; // 발자국 소리 클립으로 복원
    }
}




