using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public Animator bodyAnimator;// 리지드바디 (물리)
    public SpriteRenderer bodySpriteRenderer;   // Body의 SpriteRenderer
    public SpriteRenderer handSpriteRenderer;   // 스프라이트 렌더러
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘
    private Vector2 movement;    // 이동 벡터
    private bool isGrounded;     // 땅에 닿았는지 여부

    // 애니메이션 상태를 제어할 변수
    private bool isMoving;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        bodySpriteRenderer = transform.Find("body").GetComponent<SpriteRenderer>(); 
        handSpriteRenderer = transform.Find("playerHand").GetComponent<SpriteRenderer>();
        bodyAnimator = transform.Find("body").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;

        // 이동 방향을 설정
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
            bodyAnimator.SetBool("isMoving", true);
            bodySpriteRenderer.flipX = false; 
            handSpriteRenderer.flipX = false;  
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
            bodyAnimator.SetBool("isMoving", true);
            bodySpriteRenderer.flipX =  true;  
            handSpriteRenderer.flipX = true;   
        }
        else
        {
            bodyAnimator.SetBool("isMoving", false); 
        }

        // 점프 입력 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            handSpriteRenderer.enabled = true;  // Shift 키가 눌리면 Hand 보이게
        }
        else
        {
            handSpriteRenderer.enabled = false; // Shift 키를 떼면 Hand 숨기기
        }
    }

    void FixedUpdate()
    {
        // 물리 엔진에서 이동을 처리
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
        // 점프 처리
        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;  // 점프 후에는 공중에 있는 상태
    }
    void Push()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // 바닥에 닿으면 isGrounded를 true로 설정
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false; 
        }
    }
}

