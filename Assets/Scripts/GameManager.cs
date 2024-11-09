using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
    void Awake(){ if(Instance==null){Instance = this;} Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);}

    public GameObject PlayerPrefab;  // 플레이어 프리팹
    public GameObject Player;  // 플레이어 인스턴스

    [HideInInspector]
    public Vector2 checkPoint;  // 현재 체크포인트
    [HideInInspector]
    public Vector2 distantViewCheckPoint;  // 원경 체크포인트
    public GameObject[] ObstacleTriggers;  // 장애물 생성 트리거

    public float cameraLimitY = 10.3f;  // 카메라 y값 상한
    public float cameraLimitpreY = 0f;  // 카메라 y값 하한
    public float cameraLimitX = 132f;  // 카메라 x값 상한
    public float cameraLimitpreX = 0f;  // 카메라 x값 하한
    public GameObject[] memoryObjects;  // 기억 오브젝트 (UI)
    private int memoryFinded = 0;  // 찾은 기억 개수
    public bool isGameStarted = false;  // 게임 시작 여부
    public int Stage = 1; // 스테이지
    public GameObject[] DistantViews;

    public Image FadeImg;  // 페이드인아웃 이미지

    public event EventHandler OnPlayerRestarted;

    public void PlayerDie()  // 장애물 충돌 시
    {
        // 페이드인
        FadeImg.gameObject.SetActive(false);
        FadeImg.canvasRenderer.SetAlpha(0.0f);
        FadeImg.CrossFadeAlpha(1.0f, 0.6f, false);

        Invoke("reStart", 1.5f);
        foreach(GameObject trigger in ObstacleTriggers)
        {
            if(trigger.transform.position.x > checkPoint.x){
                trigger.SetActive(true);
            }
        }
    }

    void reStart(){
        Destroy(Player);
        FadeImg.CrossFadeAlpha(0f, 0.6f, false);  // 페이드아웃
        Player = Instantiate(PlayerPrefab, checkPoint, Quaternion.identity);
        DistantViews[Stage-1].transform.position = distantViewCheckPoint;
        OnPlayerRestarted?.Invoke(this, EventArgs.Empty);
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
