using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    void Awake(){ if(Instance==null){Instance = this;} Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);}

    public GameObject PlayerPrefab;  // 플레이어 프리팹
    public GameObject Player;  // 플레이어 인스턴스
    [HideInInspector]
    public Vector2 checkPoint;  // 현재 체크포인트
    public GameObject[] ObstacleTriggers;  // 장애물 생성 트리거

    public float cameraLimitY = 10.3f;  // 카메라 y값 상한
    public float cameraLimitX = 132f;  // 카메라 x값 상한
    public float cameraLimitpreX = 0f;  // 카메라 x값 하한
    public GameObject[] memoryObjects;  // 기억 오브젝트 (UI)
    private int memoryFinded = 0;  // 찾은 기억 개수
    public bool isGameStarted = false;  // 게임 시작 여부
    public int Stage = 0; // 스테이지

    public event EventHandler OnPlayerRestarted;

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

        OnPlayerRestarted?.Invoke(this, EventArgs.Empty);
        // 검은화면 페이드아웃
    }

    public void memoryFind(){  // 기억 찾을 시
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
