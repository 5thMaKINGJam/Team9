using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Rigidbody2D body;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Vector2 movement;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        // Rigidbody2D에 속도를 설정하여 움직이게 합니다.
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
        // 상향 힘을 가해 점프
        body.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false; // 점프 후에는 바닥에서 떨어진 상태로 변경

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥과 충돌 감지
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥을 떠날 때 isGrounded를 false로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


}

