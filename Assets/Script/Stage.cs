using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI 기능을 사용하기 위해 추가

public class StageManager : MonoBehaviour
{
    public ComfortManager comfortManager;  // ComfortManager 참조
    public TrustManager trustManager;  // TrustManager 참조
    public AngerManager angerManager;  
    public GuestFishMovement guestFishMovement;  
    public Button nextStageButton;  // UI Button 참조
    public Button endStageButton;  // 스테이지 종료 버튼 참조

    void Start()
    {
        // 버튼에 클릭 이벤트 리스너 추가
        if (nextStageButton != null)
        {
            nextStageButton.onClick.AddListener(StartNewStage);  // 버튼 클릭 시 StartNewStage() 호출
        }

       if (endStageButton != null)
        {
            endStageButton.onClick.AddListener(EndCurrentStage);  // 버튼 클릭 시 EndCurrentStage() 호출
        }
    }

    // 새로운 스테이지를 시작하는 함수
    void StartNewStage()
    {
        if (comfortManager != null)
        {
            comfortManager.StartNewStage();  // ComfortManager의 StartNewStage() 호출
        }

        if (angerManager != null)
        {
            angerManager.StartNewStage(); 
        }

        if (trustManager != null)
        {
            trustManager.StartNewStage();  // TrustManager의 StartNewStage() 호출
        }

        if (guestFishMovement != null)
        {
            guestFishMovement.StartEnteringStage();  
        }


        Debug.Log("New stage started!");
    }

    // 현재 스테이지를 종료하는 함수
    void EndCurrentStage()
    {
        if (guestFishMovement != null)
        {
            guestFishMovement.StartExitingStage();  // 
        }

        Debug.Log("Current stage ended!");
    }
}