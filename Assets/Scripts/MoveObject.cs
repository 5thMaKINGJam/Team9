using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public LayerMask objectLayer;
    public float pushStrength = 5f;  // 물체 밀기 힘
    private Rigidbody2D currentObject;
    private bool isColliding;

    private Animator animator;  // 캐릭터 애니메이터

    void Start()
    {
        // 캐릭터의 Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Shift 키가 눌린 상태에서 오브젝트와 충돌 중일 때만 밀기/당기기 시작
        if (isColliding && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                // A 키를 눌렀을 때 왼쪽(-1)으로 당기기
                PushOrPullObject(-1);
                animator.SetBool("isPulling", true);   // Pull 애니메이션 실행
                animator.SetBool("isPushing", false);  // Push 애니메이션 중지
            }
            else if (Input.GetKey(KeyCode.D))
            {
                // D 키를 눌렀을 때 오른쪽(+1)으로 밀기
                PushOrPullObject(1);
                animator.SetBool("isPushing", true);   // Push 애니메이션 실행
                animator.SetBool("isPulling", false);  // Pull 애니메이션 중지
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

    // 오브젝트를 밀거나 당기는 로직
    void PushOrPullObject(int direction)
    {
        if (currentObject != null)
        {
            Vector2 force = new Vector2(direction * pushStrength, 0);
            currentObject.AddForce(force, ForceMode2D.Impulse);
        }
    }

    // 충돌한 오브젝트 감지
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & objectLayer) != 0) // objectLayer에 포함된 레이어인지 확인
        {
            isColliding = true;
            currentObject = collision.rigidbody;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & objectLayer) != 0)
        {
            isColliding = false;
            currentObject = null;
        }
    }
}




