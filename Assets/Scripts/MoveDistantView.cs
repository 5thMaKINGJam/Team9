using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveDistantView : MonoBehaviour
{
    public int myStage;
    public float LimitX;
    public float LimitXpre;

    GameObject target;
    Vector3 TargetPos;
    Rigidbody2D me;
    void Start (){GameManager.Instance.OnPlayerRestarted+=(object sender, EventArgs e)=>{target=GameManager.Instance.Player;};
    me = GetComponent<Rigidbody2D>();}
    void FixedUpdate() 
    {
        if(!GameManager.Instance.isGameStarted  || target==null || GameManager.Instance.Stage!=myStage) 
            return;

        me.velocity = new Vector2(target.GetComponent<Player>().body.velocity.x, 0);
    }
}
