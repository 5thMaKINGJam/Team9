using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public float moveSpeed;
    Vector3 TargetPos;
    private void OnCollisionEnter2D(Collision2D other) {
        if(Input.GetKey(KeyCode.LeftShift)){
            TargetPos = new Vector3 (other.transform.position.x, 
                                 transform.position.y, 
                                 transform.position.z);
            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * moveSpeed);
        }
    }
}
