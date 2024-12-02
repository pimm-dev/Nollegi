using UnityEngine;
using TMPro;  // TextMeshPro 관련 기능을 사용하기 위해 추가

public class ComfortManager : MonoBehaviour
{
    public float comfort = 0.0f;  // 현재 Comfort 지수
    public float maxComfort = 100.0f;  // 최대 Comfort 지수
    public TextMeshProUGUI comfortText;  // TextMeshProUGUI 컴포넌트 참조

    void Start()
    {
        UpdateComfortUI();    // 게임 시작 시 UI 업데이트
    }

    // Comfort 지수를 증가시키는 함수
    public void IncreaseComfort(float amount)
    {
        comfort += amount;
        if (comfort > maxComfort)
        {
            comfort = maxComfort;  // Comfort 지수는 최대값을 넘지 않도록 설정
        }

        UpdateComfortUI();  // Comfort 지수가 변경될 때마다 UI 업데이트
    }

    // Comfort 지수를 감소시키는 함수
    public void DecreaseComfort(float amount)
    {
        comfort -= amount;
        if (comfort < 0)
        {
            comfort = 0;  // Comfort 지수는 0 이하로 떨어지지 않도록 설정
        }

        UpdateComfortUI();  // Comfort 지수가 변경될 때마다 UI 업데이트
    }

    // Comfort 지수를 UI에 업데이트하는 함수
    void UpdateComfortUI()
    {
        if (comfortText != null)
        {
            comfortText.text = "Comfort: " + comfort.ToString("F1");  // 소수점 1자리까지 표시
        }
    }

    // 스테이지가 시작될 때 호출되는 함수
    public void StartNewStage()
    {
        UpdateComfortUI();    // UI 업데이트
    }
}