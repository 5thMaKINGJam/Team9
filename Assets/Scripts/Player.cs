using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator bodyAnimator;
    public Animator handAnimator;
    public SpriteRenderer bodySpriteRenderer;
    public SpriteRenderer handSpriteRenderer;
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘
    private Vector2 movement;    // 이동 벡터
    private bool isGrounded;     // 땅에 닿았는지 여부

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bodySpriteRenderer = transform.Find("body").GetComponent<SpriteRenderer>();
        handSpriteRenderer = transform.Find("bodyHand").GetComponent<SpriteRenderer>();
        bodyAnimator = transform.Find("body").GetComponent<Animator>();
        handAnimator = transform.Find("bodyHand").GetComponent<Animator>();
        handSpriteRenderer.enabled = false; // 시작 시 Hand를 숨김
    }

    void Update()
    {
        movement = Vector2.zero;

        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Shift 키가 눌리면 핸드만 보이게 하고, 애니메이션 상태 변경
        if (isShiftPressed)
        {
            bodyAnimator.SetBool("isShiftPressed", true);
            handAnimator.SetBool("isShiftPressed", true);
            handSpriteRenderer.enabled = true;
            bodySpriteRenderer.enabled = false;
        }
        else
        {
            bodyAnimator.SetBool("isShiftPressed", false);
            handAnimator.SetBool("isShiftPressed", false);
            handSpriteRenderer.enabled = false;
            bodySpriteRenderer.enabled = true;
        }

        // 이동 방향 설정
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
            bodySpriteRenderer.flipX = false;
            handSpriteRenderer.flipX = false;

            if (isShiftPressed)
            {
                handAnimator.SetBool("isMoving", true);
            }
            else
            {
                bodyAnimator.SetBool("isMoving", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
            bodySpriteRenderer.flipX = true;
            handSpriteRenderer.flipX = true;

            if (isShiftPressed)
            {
                handAnimator.SetBool("isMoving", true);
            }
            else
            {
                bodyAnimator.SetBool("isMoving", true);
            }
        }
        else
        {
            bodyAnimator.SetBool("isMoving", false);
            handAnimator.SetBool("isMoving", false);
        }

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            bodyAnimator.SetBool("isGrounded", false);
            handAnimator.SetBool("isGrounded", false);
        }
    }

    void FixedUpdate()
    {
        if (movement == Vector2.zero)
        {
            body.velocity = new Vector2(0, body.velocity.y); // 수평 이동만 멈추기
        }
        else
        {
            body.velocity = new Vector2(movement.x * moveSpeed, body.velocity.y);
        }
    }

    void Jump()
    {
        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            bodyAnimator.SetBool("isGrounded", true);
            handAnimator.SetBool("isGrounded", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            bodyAnimator.SetBool("isGrounded", false);
            handAnimator.SetBool("isGrounded", false);
        }
    }
}


