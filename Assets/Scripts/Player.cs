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
    public LayerMask objectLayer; // 상호작용 가능한 물체 레이어
    public float pushStrength = 5f; // 물체 밀기 힘
    public float interactionRadius = 3f; // 상호작용 거리 (3미터)
    private Rigidbody2D currentObject; // 현재 상호작용하는 물체
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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            DetectAndInteractWithObject();
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
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Moveable"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Moveable"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }

    // 3미터 이내에 물체가 있으면 상호작용하도록 하는 메서드
    void DetectAndInteractWithObject()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, interactionRadius, objectLayer);

        if (hitObjects.Length > 0)
        {
            foreach (var hit in hitObjects)
            {
                Rigidbody2D hitRigidbody = hit.GetComponent<Rigidbody2D>();

                if (hitRigidbody != null)
                {
                    currentObject = hitRigidbody;

                    if (Input.GetKey(KeyCode.A))
                    {
                        // 왼쪽(-1)으로 당기기
                        animator.SetBool("isPulling", true);
                        animator.SetBool("isPushing", false);
                        PullOrPushObject(-1);
                    }
                    else if (Input.GetKey(KeyCode.D))
                    {
                        // 오른쪽(+1)으로 밀기
                        animator.SetBool("isPushing", true);
                        animator.SetBool("isPulling", false);
                        PullOrPushObject(1);
                    }
                    else
                    {
                        animator.SetBool("isPushing", false);
                        animator.SetBool("isPulling", false);
                    }
                }
            }
        }
        else
        {
            // 상호작용할 물체가 없을 때 애니메이션 초기화
            animator.SetBool("isPushing", false);
            animator.SetBool("isPulling", false);
        }
    }

    // 물체를 밀거나 당기는 함수
    void PullOrPushObject(int direction)
    {
        if (currentObject != null)
        {
            Vector2 force = new Vector2(direction * pushStrength, 0);
            currentObject.AddForce(force, ForceMode2D.Impulse);
        }
    }

    // 디버깅용: 플레이어의 상호작용 범위 표시
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}








