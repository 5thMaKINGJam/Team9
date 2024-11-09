using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public float moveSpeed;
    Vector3 TargetPos;
    float width;
    Rigidbody2D me;

    void Start (){
        width = GetComponent<BoxCollider2D>().size.x;
        me = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate() 
    {
        if(GameManager.Instance.Player == null) return;

        if(GameManager.Instance.Player.transform.position.x < 
        transform.position.x + width/2 + GameManager.Instance.Player.GetComponent<BoxCollider2D>().size.x/1.5
        && 
        GameManager.Instance.Player.transform.position.x > 
        transform.position.x - width/2 - GameManager.Instance.Player.GetComponent<BoxCollider2D>().size.x/1.5)
        {
            if(Input.GetKey(KeyCode.LeftShift)){
                
                if(GameManager.Instance.Player.transform.position.x < transform.position.x 
                && GameManager.Instance.Player.GetComponent<Player>().bodySpriteRenderer.flipX==false
                ||
                GameManager.Instance.Player.transform.position.x > transform.position.x 
                && GameManager.Instance.Player.GetComponent<Player>().bodySpriteRenderer.flipX==true)
                    me.velocity = GameManager.Instance.Player.GetComponent<Player>().body.velocity;
            }
        }
    }
}
