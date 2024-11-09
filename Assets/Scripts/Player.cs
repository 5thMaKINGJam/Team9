using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator animator;
    public SpriteRenderer bodySpriteRenderer;
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘
    private Vector2 movement; // 이동 벡터
    private bool isGrounded; // 땅에 닿았는지 여부
    public LayerMask objectLayer;
    public float pushStrength = 5f; // 물체 밀기 힘
    private Rigidbody2D currentObject;
    private bool isColliding;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bodySpriteRenderer = transform.Find("body").GetComponent<SpriteRenderer>();
        animator = transform.Find("body").GetComponent<Animator>();
    }

    void Update()
    {
        movement = Vector2.zero;

        // 이동 방향 설정
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
            bodySpriteRenderer.flipX = false;
            animator.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
            bodySpriteRenderer.flipX = true;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            animator.SetBool("isGrounded", false);
        }

        // 물체 밀기/당기기
        if (isColliding && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                PushOrPullObject(-1); // 왼쪽(-1)으로 당기기
                animator.SetBool("isPulling", true);
                animator.SetBool("isPushing", false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                PushOrPullObject(1); // 오른쪽(+1)으로 밀기
                animator.SetBool("isPushing", true);
                animator.SetBool("isPulling", false);
            }
            else
            {
                // 아무 키도 누르지 않을 때 애니메이션 초기화
                animator.SetBool("isPushing", false);
                animator.SetBool("isPulling", false);
            }
        }
        else
        {
            // Shift 키를 떼면 애니메이션 초기화
            animator.SetBool("isPushing", false);
            animator.SetBool("isPulling", false);
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
            animator.SetBool("isGrounded", true);
        }
        if (((1 << collision.gameObject.layer) & objectLayer) != 0) // objectLayer에 포함된 레이어인지 확인
        {
            isColliding = true;
            currentObject = collision.rigidbody;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
        if (((1 << collision.gameObject.layer) & objectLayer) != 0)
        {
            isColliding = false;
            currentObject = null;
        }
    }

    void PushOrPullObject(int direction)
    {
        if (currentObject != null)
        {
            Vector2 force = new Vector2(direction * pushStrength, 0);
            currentObject.AddForce(force, ForceMode2D.Impulse);
        }
    }
}





