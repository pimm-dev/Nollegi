using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public HungerManager hungerManager;  // 허기 매니저 참조
    public AngerManager angerManager;    // 불쾌 지수 매니저 참조
    public ComfortManager comfortManager;  // 호감 지수 매니저 참조
    public TextMeshProUGUI gameOverText;  // 게임 오버 메시지 표시용

    [SerializeField] private AudioSource backgroundMusic;  // 배경 음악 오디오 소스

    private float hungerZeroTimer = 0.0f;  // 허기가 0인 상태에서의 경과 시간
    private bool isGameOver = false;       // 게임 오버 상태 여부

    void Start()
    {
        gameOverText.gameObject.SetActive(false);  // 게임 오버 메시지를 처음엔 비활성화
    }

    void Update()
    {
        if (!isGameOver)
        {
            CheckHungerZeroCondition();  // 허기가 0인 상태를 확인
            CheckComfortAngerCondition();  // 호감지수와 불쾌지수 상태 확인
        }
        else
        {
            HandleRestartInput();  // 게임 오버 시 리스타트 입력 확인
        }
    }

    // 허기 0인 상태에서 5초 이상이면 게임 오버
    private void CheckHungerZeroCondition()
    {
        if (hungerManager.hunger == 0)
        {
            hungerZeroTimer += Time.deltaTime;
            if (hungerZeroTimer >= 5.0f)
            {
                TriggerGameOver("Hunger reached 0 for 5 seconds!");
            }
        }
        else
        {
            hungerZeroTimer = 0.0f;  // 허기가 0이 아니면 타이머 초기화
        }
    }

    // 호감지수가 불쾌지수보다 작거나 같으면 즉시 게임 오버
    private void CheckComfortAngerCondition()
    {
        if (comfortManager.comfort <= angerManager.anger)
        {
            TriggerGameOver("Comfort <= Anger!");
        }
    }

    // 게임 오버 처리 함수
    private void TriggerGameOver(string reason)
    {
        isGameOver = true;  // 게임 오버 상태로 변경
        Debug.Log("Game Over: " + reason);
        gameOverText.gameObject.SetActive(true);  // 게임 오버 메시지 표시
        gameOverText.text = "Game Over: " + reason;

        // 폰트 크기 동적으로 조정
        gameOverText.fontSize = 50;  // 폰트 크기를 크게 설정

                // 배경 음악 정지
        if (backgroundMusic != null)
        {
            backgroundMusic.Stop();
        }
        
        // 추가적인 게임 오버 처리 (예: 씬 전환, 게임 정지 등)
        Time.timeScale = 0f;

    }

    // 게임 오버 시 리스타트 입력 확인 함수
    private void HandleRestartInput()
    {
        // 'R' 키를 눌렀을 때 게임 재시작
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // 게임 재시작 함수
    private void RestartGame()
    {
        Time.timeScale = 1f;  // 게임 시간 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 현재 씬 재로드
    }

    // 게임 오버 상태 확인 함수 
    public bool IsGameOver()
    {
        return isGameOver;
    }
}