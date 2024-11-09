using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckPoint : MonoBehaviour
{
    public float groundPosY;  // 플레이어가 땅을 디뎠을 때 위치
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            GameManager.Instance.checkPoint = new Vector2(gameObject.transform.position.x, groundPosY);
            Destroy(gameObject);
        }
    }
}
