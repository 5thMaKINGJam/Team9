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
    public float cameraLimitX = 0f;  // 카메라 x값 상한
    public float cameraLimitpreX = 0f;  // 카메라 x값 하한
    public GameObject[] memoryObjects;  // 기억 오브젝트 (UI)
    private int memoryFinded = 0;  // 찾은 기억 개수
    public bool isGameStarted = false;  // 게임 시작 여부
    public int Stage = 0; // 스테이지
    public GameObject[] DistantViews;

    public Image FadeImg;  // 페이드인아웃 이미지
    public Image[] MemoryImg;  // 기억 돌아오는 이미지
    public Image Logo;  // 로고
    public GameObject WakeUp;
    public Image[] endings;
    public event EventHandler OnPlayerRestarted;

    void Start(){
        Player.SetActive(false);
    }

    public void PlayerDie()  // 장애물 충돌 시
    {
        GetComponent<AudioSource>().Play();
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        FadeIn(FadeImg);
        
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
            FadeIn(FadeImg);
            StartCoroutine(nextFadeIn(MemoryImg[0]));
            StartCoroutine(FadeOut(MemoryImg[0], 132f));

            memoryObjects[0].SetActive(true);
            Stage = 1;
        }
        else if(memoryFinded == 2) {
            FadeIn(FadeImg);
            StartCoroutine(nextFadeIn(MemoryImg[1]));
            StartCoroutine(FadeOut(MemoryImg[1], 285.7f));
            StartCoroutine(nextFadeIn2(MemoryImg[2], 285.7f));
            StartCoroutine(nextFadeIn3(MemoryImg[3], 285.7f));

            memoryObjects[1].SetActive(true);
        }
        else if(memoryFinded == 3) {
            FadeIn(FadeImg);
            StartCoroutine(nextFadeIn(MemoryImg[4]));
            StartCoroutine(FadeOut(MemoryImg[4], 285.7f));
            StartCoroutine(nextFadeIn2(MemoryImg[5], 285.7f));

            memoryObjects[2].SetActive(true);
        }
    }

    public void FadeIn(Image img){  // 페이드 인
        img.gameObject.SetActive(true);
        img.canvasRenderer.SetAlpha(0.0f);
        img.CrossFadeAlpha(1.0f, 0.6f, false);
    }
    IEnumerator nextFadeIn(Image img){
        yield return new WaitForSeconds(0.6f);
        FadeIn(img);
    }
    IEnumerator nextFadeIn2(Image img, float c){
        yield return new WaitForSeconds(4f);
        FadeIn(img);
        StartCoroutine(FadeOut(img, c));
    }
    IEnumerator nextFadeIn3(Image img, float c){
        yield return new WaitForSeconds(8f);
        FadeIn(img);
        StartCoroutine(FadeOut(img, c));
    }
    IEnumerator FadeOut(Image img, float cameraLimit){
        yield return new WaitForSeconds(5f);
        FadeImg.CrossFadeAlpha(0f, 0.6f, false);
        img.CrossFadeAlpha(0f, 0.6f, false);
        img.gameObject.SetActive(false);
        cameraLimitX = cameraLimit;

        if(img==MemoryImg[5]) Invoke("Ending", 2.1f);
    }

    public void BreakPlease(GameObject Bridge){
        StartCoroutine(LetsBreak(Bridge));
    }

    IEnumerator LetsBreak(GameObject Bridge){
        for(int i = Bridge.transform.childCount-3; i > -1 ; i--){
            yield return new WaitForSeconds(0.5f);
            if(Bridge == null) break;
            Bridge.transform.GetChild(i).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Destroy(Bridge.transform.GetChild(i).gameObject, 1f);
        }
    }

    public void GameStart(){
        StartCoroutine(AudioFade(
        GameObject.Find("Sun").GetComponent<AudioSource>(),
        GameObject.Find("Main Camera").GetComponent<AudioSource>()));
        Logo.CrossFadeAlpha(0f, 2f, false);
    }

    public IEnumerator AudioFade(AudioSource Out, AudioSource In){
        yield return new WaitForSeconds(1f);
        if(WakeUp != null) WakeUp.GetComponent<Animator>().Play("WakeUp");
        In.Play();
        for(int i = 0; i<20; i++){
            yield return new WaitForSeconds(0.1f);
            In.volume += 0.05f;
            Out.volume -= 0.05f;
            if(i == 13){
                if(WakeUp!=null) Destroy(WakeUp);
                Player.SetActive(true);
            }
        }
        Out.Stop();
        Logo.gameObject.SetActive(false);
    }

    public void Ending()
    {
        FadeIn(FadeImg);
        nextFadeIn(endings[0]);
        FadeOut(endings[0], cameraLimitX);

        endings[1].gameObject.SetActive(true);
        endings[2].gameObject.SetActive(true);
        endings[3].gameObject.SetActive(true);
        endings[4].gameObject.SetActive(true);

        StartCoroutine(EndingFade(1));
    }
    IEnumerator EndingFade(int i){
        yield return new WaitForSeconds(1f);
        endings[i].CrossFadeAlpha(0f, 0.5f, false);
        if(endings.Length - 2 >= i + 1)
            StartCoroutine(EndingFade(i+1));
    }
}