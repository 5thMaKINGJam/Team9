using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float pushStrength = 5f;         // 밀 때의 힘
    public float interactDistance = 2f;     // 플레이어와 물체 간 상호작용 거리
    public GameObject objectToPush;         // 밀고자 하는 물체

    private bool isPushing = false; 
    void Update()
    {
        // 플레이어와 물체의 거리가 상호작용 거리 내에 있는지 확인
        if (objectToPush != null && Vector3.Distance(transform.position, objectToPush.transform.position) <= interactDistance)
        {
            // Shift 키를 눌렀을 때만 밀기 시작
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isPushing = true; // 밀기 활성화
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                isPushing = false; // 밀기 비활성화
            }

            // 밀기 활성화 상태일 때만 물체를 밀도록 설정
            if (isPushing)
            {
                Vector3 direction = (objectToPush.transform.position - transform.position).normalized;

                // 물체의 y 값은 고정하고 x와 z 값만 이동
                objectToPush.transform.position += new Vector3(direction.x, 0, direction.z) * pushStrength * Time.deltaTime;
            }
        }
        else
        {
            isPushing = false; // 거리가 멀어지면 밀기 비활성화
        }
    }
}


