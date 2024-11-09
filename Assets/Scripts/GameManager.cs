using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    void Awake(){ if(Instance==null){Instance = this;} Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);}

    public GameObject PlayerPrefab; // 플레이어 프리팹
    public GameObject Player;  // 플레이어 인스턴스
    [HideInInspector]
    public Vector2 checkPoint;
    public GameObject[] ObstacleTriggers;

    public float cameraLimitY = 10.3f;
    public float cameraLimitX = 132f;
    public GameObject[] memoryObjects;
    private int memoryFinded = 0;
    public bool isGameStarted = false;

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

    public void memoryFind(){
        memoryFinded++;
        if(memoryFinded == 1) {
            cameraLimitX = 1010;  // Todo
            memoryObjects[0].SetActive(true);
        }
        else if(memoryFinded == 2) {
            cameraLimitX = 1010;  // Todo
            memoryObjects[1].SetActive(true);
        }
        else if(memoryFinded == 3) {
            memoryObjects[2].SetActive(true);
        }
    }

    
}
