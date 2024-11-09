using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 5f;             
    private GameObject objectToMove;
    public float pushStrength = 5f; // 물체 밀기 힘
    private Vector2 move;

    void Update()
    { 
    if (Input.GetKey(KeyCode.LeftShift))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, move, 1f);

            if (hit.collider != null && hit.collider.CompareTag("Moveable"))
            {
                Moveable moveableObject = hit.collider.GetComponent<Moveable>();
                if (moveableObject != null)
                {
                    // 물체를 밀거나 끌 수 있도록 물체를 이동
                    Vector3 targetPos = new Vector3(hit.collider.transform.position.x, transform.position.y, transform.position.z);
    hit.collider.transform.position = Vector3.Lerp(hit.collider.transform.position, targetPos, Time.deltaTime* moveableObject.moveSpeed);
                }
            }
        }
        else
{
    // Shift 키를 떼면 플레이어가 자유롭게 이동
    transform.Translate(move * moveSpeed * Time.deltaTime);
}
    }
}



