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
        // 발자국 소리를 루프 설정
        audioSource.clip = footsteps;
        audioSource.loop = true;
    }

    void Update()
    {
        // 이동 중인지 감지
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // 걷는 상태에서 발자국 소리를 루프 재생
        if (isWalking && isGrounded && !isJumping)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // 발자국 소리 루프 재생 시작
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause(); // 걷지 않을 때 소리 일시 중지
            }
        }

        // 점프 입력 감지 및 점프 소리 재생
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            isJumping = true;
            isGrounded = false;
            audioSource.Pause(); // 점프 시 발자국 소리 일시 중지
            audioSource.PlayOneShot(jump); // 점프 소리 재생
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
            if (isWalking)
            {
                audioSource.UnPause(); // 착지 시 발자국 소리 재개
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            audioSource.Pause(); // 공중에 있을 때 발자국 소리 일시 중지
        }
    }
}







