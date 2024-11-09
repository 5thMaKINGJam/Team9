using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaMove : MonoBehaviour
{

    public GameObject target;
    public float CamreaMoveSpeed = 5.0f;
    public float offsetY;
    Vector3 TargetPos;
    void FixedUpdate() {

        if(!GameManager.Instance.isGameStarted) return;

        float targetX;
        float targetY;

        if(GameManager.Instance.cameraLimitY < target.transform.position.y + offsetY) targetY = GameManager.Instance.cameraLimitY;
        else targetY = target.transform.position.y + offsetY;
        if(GameManager.Instance.cameraLimitX < target.transform.position.x) targetX = GameManager.Instance.cameraLimitX;
        else if(0 > target.transform.position.x) targetX = 0f;
        else targetX = target.transform.position.x;

        TargetPos = new Vector3 (targetX, 
                                 targetY, 
                                 -10f);
        transform.position = Vector3.Lerp (transform.position, TargetPos, Time.deltaTime * CamreaMoveSpeed);
    }
}
