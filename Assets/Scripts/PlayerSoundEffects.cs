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
        // ���ڱ� �Ҹ� ���� ����
        audioSource.clip = footsteps;
        audioSource.loop = true;
    }

    void Update()
    {
        //// �̵� ������ ���� (���� �� ���� �Է� ��� üũ)
        //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        //{
        //    isWalking = true;
        //}
        //else
        //{
        //    isWalking = false;
        //}

        // �ȴ� ������ �� ���� �������� �߼Ҹ� ���
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            if (!GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

        // �ȴ� �����̰� �ٴڿ� ���� ���� ���ڱ� �Ҹ� ���
        if (isWalking && isGrounded && !isJumping)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // �ȱ� �Ҹ� ��� ����
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // �ȱ� �Ҹ� ����
            }
        }


        // ���� �Է� ���� �� ���� �Ҹ� ���
        if (Input.GetButtonDown("Jump") && isGrounded && !isJumping)
        {
            StartCoroutine(PlayJumpSound()); // ���� �Ҹ� �ڷ�ƾ ȣ��
        }
    }

    // �ٴڿ� ��Ҵ��� Ȯ��
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
    private IEnumerator PlayJumpSound()
    {
        isJumping = true;
        audioSource.Stop(); // ���� �Ҹ� ���߱�
        audioSource.clip = jump;
        audioSource.Play(); // ���� �Ҹ� ���
        yield return new WaitForSeconds(jump.length); // ���� �Ҹ��� ���� ������ ���
        isJumping = false;
        audioSource.clip = footsteps; // ���ڱ� �Ҹ� Ŭ������ ����
    }
}


