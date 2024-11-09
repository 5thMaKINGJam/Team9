using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaMove : MonoBehaviour
{

    public GameObject target;
    
    public float offSetX = 0.0f;
    public float CamreaMoveSpeed = 10.0f;
    Vector2 TargetPos;
    void FixedUpdate() {
        TargetPos = new Vector2 (TargetPos.x, transform.position.y);
        transform.position = Vector3.Lerp (transform.position, TargetPos, Time.deltaTime);
    }
}
