using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaMove : MonoBehaviour
{

    public GameObject target;
    public float CamreaMoveSpeed = 10.0f;
    Vector3 TargetPos;
    void FixedUpdate() {
        TargetPos = new Vector3 (target.transform.position.x, 
                                 target.transform.position.y, 
                                 -10f);
        transform.position = Vector3.Lerp (transform.position, TargetPos, Time.deltaTime * CamreaMoveSpeed);
    }
}
