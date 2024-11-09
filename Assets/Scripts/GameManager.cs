using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    void Awake(){ if(Instance==null){Instance = this;} }

    public GameObject PlayerPrefab; // 플레이어 프리팹
    public GameObject Player;  // 플레이어 인스턴스
    [HideInInspector]
    public Vector2 checkPoint;
    public GameObject[] ObstacleTriggers;

    public void PlayerDie()  // 장애물 충돌 시
    {
        // 검은화면 페이드인
        
        Destroy(Player);
        Player = Instantiate(PlayerPrefab, checkPoint, Quaternion.identity);
        
        foreach(GameObject trigger in ObstacleTriggers)
        {
            if(trigger.transform.position.x > checkPoint.x){
                trigger.SetActive(true);
            }
        }

        // 검은화면 페이드아웃
    }
}
