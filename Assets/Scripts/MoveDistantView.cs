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
    Rigidbody2D me;
    void Start ()
    {
        GameManager.Instance.OnPlayerRestarted+=(object sender, EventArgs e)=>{target=GameManager.Instance.Player;};
        target = GameManager.Instance.Player;
        me = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate() 
    {
        if(!GameManager.Instance.isGameStarted  || target==null || GameManager.Instance.Stage!=myStage) 
            return;
        if(target.transform.position.x <= LimitXpre || target.transform.position.x >= LimitX)
            me.velocity = new Vector2(0f,0f);
        else me.velocity = new Vector2(target.GetComponent<Rigidbody2D>().velocity.x/1.5f, 0);
    }
}
