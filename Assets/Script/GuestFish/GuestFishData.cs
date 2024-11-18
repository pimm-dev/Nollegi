using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestFishData : MonoBehaviour
{

    public string fishName;          // 물고기 이름
    public float comfortValue = 0.0f; // Comfort 값
    public float angerValue = 0.0f;   // Anger 값

    public GuestFishData(string name, float comfort, float anger)
    {
        fishName = name;
        comfortValue = comfort;
        angerValue = anger;
    }

    void Start()
    {
        comfortValue = Random.Range(40.0f, 60.0f);  // Comfort 랜덤 값
        angerValue = Random.Range(0.0f, 20.0f);     // Anger 랜덤 값

        Debug.Log($"{fishName}: 랜덤 값 초기화 -> Comfort={comfortValue}, Anger={angerValue}");
    }

    // 데이터를 업데이트
    public void UpdateValues(float comfort, float anger)
    {
        comfortValue = comfort;
        angerValue = anger;
    }
}