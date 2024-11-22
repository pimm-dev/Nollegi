using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // UI 기능을 사용하기 위해 추가

public class Stage : MonoBehaviour
{
    public ComfortManager comfortManager;  // ComfortManager 참조
    public TrustManager trustManager;  // TrustManager 참조
    public AngerManager angerManager;  
    public GuestFishMovement guestFishMovement;  
    public Button endStageButton;  // 스테이지 종료 버튼 참조

    public GuestFishMovement[] guestFishes; // 손님 물고기 배열
    private Dictionary<GuestFishMovement, GuestFishData> fishDataMap = new Dictionary<GuestFishMovement, GuestFishData>();
    private GuestFishMovement currentGuestFish; // 현재 선택된 물고기
    private GuestFishMovement previousGuestFish; // 이전에 선택된 물고기
    


    void Start()
    {
        foreach (var fish in guestFishes)
        {
            if (!fishDataMap.ContainsKey(fish))
            {
                var data = new GuestFishData(
                    fish.gameObject.name,                  // 물고기 이름
                    Random.Range(40.0f, 60.0f),           // Comfort 값
                    Random.Range(0.0f, 20.0f)             // Anger 값
                );

                fishDataMap.Add(fish, data);
                Debug.Log($"Initialized data for {fish.gameObject.name}: Comfort={data.comfortValue}, Anger={data.angerValue}");
            }
        }

       if (endStageButton != null)
        {
            endStageButton.onClick.AddListener(EndCurrentStage);  // 버튼 클릭 시 EndCurrentStage() 호출
        }

        StartNewStage();
    }

        // 새로운 스테이지를 시작하는 함수
    public void StartNewStage()
    {
        List<GuestFishMovement> availableFishes = new List<GuestFishMovement>(guestFishes);

        if (previousGuestFish != null)
        {
            availableFishes.Remove(previousGuestFish);
        }

        int randomIndex = Random.Range(0, availableFishes.Count);
        currentGuestFish = availableFishes[randomIndex];
        previousGuestFish = currentGuestFish;

        // 선택된 물고기의 ParasiteManager 관리
        ParasiteManager parasiteManager = currentGuestFish.GetComponent<ParasiteManager>();
        parasiteManager.ClearParasites(); // 기존 기생충 삭제
        parasiteManager.SpawnParasites(); // 새로 생성

         // 선택된 물고기의 데이터를 ComfortManager 등에 전달
        if (fishDataMap.TryGetValue(currentGuestFish, out GuestFishData data))
        {
            comfortManager.comfort = data.comfortValue;
            angerManager.anger = data.angerValue;
        }

        currentGuestFish.StartEnteringStage();
        comfortManager.StartNewStage();
        angerManager.StartNewStage();
        trustManager.StartNewStage();
        Debug.Log($"New stage started with guest fish: {currentGuestFish.gameObject.name}");
    }

    // 현재 스테이지를 종료하는 함수
    void EndCurrentStage()
    {
       if (currentGuestFish != null && fishDataMap.TryGetValue(currentGuestFish, out GuestFishData data))
        {
            // Manager의 값을 데이터에 저장
            data.UpdateValues(comfortManager.comfort, angerManager.anger);
            Debug.Log($"Current stage ended for guest fish: {currentGuestFish.gameObject.name}");
        }
        currentGuestFish.StartExitingStage();

        StartNewStage();
    }
}