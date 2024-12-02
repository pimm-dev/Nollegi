using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro 사용 시 필요

public class SettingsManager : MonoBehaviour
{
    // 설정 창을 참조할 변수
    public GameObject settingsPanel;

    // 설정 버튼 클릭을 위한 변수 (Inspector에서 연결)
    public Button settingsButton;

    // 전체화면/창 모드 스위치 Toggle (Inspector에서 연결)
    public Toggle fullscreenToggle;

    // 화면 밝기를 조절할 슬라이더
    public Slider brightnessSlider;

    // 밝기 값 변경을 위한 텍스트 (슬라이더 옆에 표시할 "밝기" 텍스트)
    public TextMeshProUGUI brightnessLabel;  // TextMeshProUGUI로 수정

    // 배경음 음량을 조절할 슬라이더
    public Slider volumeSlider;

    // 배경음 음량을 표시할 텍스트
    public TextMeshProUGUI volumeLabel;

    // 배경음 AudioSource
    public AudioSource backgroundAudioSource;

    void Start()
    {
        // 설정 버튼 클릭 이벤트에 함수 연결
        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(ToggleSettingsPanel);
        }

        // 전체화면/창 모드 Toggle 이벤트 연결
        if (fullscreenToggle != null)
        {
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggle);
        }

        // 화면 밝기 슬라이더 초기값 설정
        if (brightnessSlider != null)
        {
            brightnessSlider.value = 0.5f;
            brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);

            if (brightnessLabel != null)
            {
                brightnessLabel.text = "Brightness: " + (brightnessSlider.value * 100).ToString("F0") + "%";
            }
        }

        // 배경음 음량 슬라이더 초기값 설정
        if (volumeSlider != null)
        {
            volumeSlider.value = backgroundAudioSource.volume; // 초기값을 현재 오디오 소스의 볼륨으로 설정
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

            if (volumeLabel != null)
            {
                volumeLabel.text = "Volume: " + (volumeSlider.value * 100).ToString("F0") + "%"; // 텍스트에 볼륨 비율 표시
            }
        }

        // 초기 상태 설정 (전체화면 상태)
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    void Update()
    {
        // Esc 키가 눌리면 설정 창을 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsPanel();
        }
    }

    // 설정 창을 토글하는 함수
    public void ToggleSettingsPanel()
    {
        Debug.Log("ToggleSettingsPanel 호출됨");
        bool isPanelActive = settingsPanel.activeSelf;

        // 설정 창을 토글 (열려 있으면 닫고, 닫혀 있으면 열기)
        settingsPanel.SetActive(!isPanelActive);

        // 게임 일시 정지/재개
        if (settingsPanel.activeSelf)
        {
            Time.timeScale = 0;  // 게임 일시 정지
        }
        else
        {
            Time.timeScale = 1;  // 게임 재개
        }
    }

    // 전체화면/창 모드 변경 시 호출되는 함수
    public void OnFullscreenToggle(bool isFullscreen)
    {
        // 전체화면 모드를 활성화/비활성화
        Screen.fullScreen = isFullscreen;

        // 창 모드일 때, 창 모드 관련 설정을 추가할 수도 있습니다.
        if (!isFullscreen)
        {
            // 예시: 창 모드에서 화면 크기를 고정하거나 조정할 수 있습니다.
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;  // 전체화면 모드로 설정
        }
    }

    // 화면 밝기 값이 변경되었을 때 호출되는 함수
    public void OnBrightnessChanged(float value)
    {
        // 슬라이더 값에 따라 밝기 조정 (0 ~ 1 사이)
        RenderSettings.ambientLight = new Color(value, value, value);  // 전체적인 밝기 조정

        // 슬라이더 값에 따른 텍스트 업데이트
        if (brightnessLabel != null)
        {
            brightnessLabel.text = "Brightness: " + (value * 100).ToString("F0") + "%";  // 0~100%로 표시
        }
    }

    // 배경음 음량이 변경되었을 때 호출되는 함수
    public void OnVolumeChanged(float value)
    {
        // 배경음의 음량을 슬라이더 값에 맞게 조정
        if (backgroundAudioSource != null)
        {
            backgroundAudioSource.volume = value;  // 0 ~ 1 사이의 값을 사용하여 음량 조정
        }

        // 슬라이더 값에 따른 텍스트 업데이트
        if (volumeLabel != null)
        {
            volumeLabel.text = "Volume: " + (value * 100).ToString("F0") + "%";  // 0~100%로 표시
        }
    }

    // 설정 창 닫기 버튼 클릭 시 호출될 함수 (설정 창 닫을 때 사용)
    public void CloseSettingsPanel()
    {
        // 설정 창을 비활성화
        settingsPanel.SetActive(false);

        // 게임을 재개
        Time.timeScale = 1;
    }
}
