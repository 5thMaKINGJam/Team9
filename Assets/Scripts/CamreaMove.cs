using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CamreaMove : MonoBehaviour
{

    public GameObject target;
    public float CamreaMoveSpeed = 5.0f;
    public float offsetY;
    Vector3 TargetPos;

    void Start (){GameManager.Instance.OnPlayerRestarted+=(object sender, EventArgs e)=>{target=GameManager.Instance.Player;};}
    void FixedUpdate() {

        if(!GameManager.Instance.isGameStarted  || target==null) return;

        float targetX;
        float targetY;

        if(GameManager.Instance.cameraLimitY < target.transform.position.y + offsetY) targetY = GameManager.Instance.cameraLimitY;
        else if(GameManager.Instance.cameraLimitpreY > target.transform.position.y) targetY = GameManager.Instance.cameraLimitpreY;
        else targetY = target.transform.position.y;
        if(GameManager.Instance.cameraLimitX < target.transform.position.x) targetX = GameManager.Instance.cameraLimitX;
        else if(GameManager.Instance.cameraLimitpreX > target.transform.position.x) targetX = GameManager.Instance.cameraLimitpreX;
        else targetX = target.transform.position.x;

        TargetPos = new Vector3 (targetX, 
                                 targetY, 
                                 -10f);
        transform.position = Vector3.Lerp (transform.position, TargetPos, Time.deltaTime * CamreaMoveSpeed);
    }
}
