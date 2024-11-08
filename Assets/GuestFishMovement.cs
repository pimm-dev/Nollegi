using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestFishMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;  // 이동 속도
    public float rotationSpeed = 5.0f;  // 회전 속도
    public float minDistanceFromProtagonist = 13.0f;  // 주인공과 유지해야 하는 최소 거리
    public float inwardMoveSpeed = 2.0f;  // 안쪽으로 점점 이동하는 속도
    private bool isMoving = false;
    private bool isExiting = false;
    private float exitTimer = 0.0f;
    private Vector3 exitDirection;  // 무작위로 설정된 이동 방향
    public float exitDuration = 5.0f;  // 스테이지 종료 후 이동 시간 (초 단위)



    private Vector3[] initialPositions = new Vector3[]
    {
        new Vector3(0, -10, -30),
        new Vector3(-24, 2, -23),
        new Vector3(-49, -12, -33),
        new Vector3(-55, -11, 20),
        new Vector3(-55, -11, -32),
        new Vector3(-24, -19, -23)
    };

    void Start()
    {
        // 장애물 레이어 무시 설정
        int guestFishLayer = LayerMask.NameToLayer("FishLayer");
        int obstacleLayerIndex = LayerMask.NameToLayer("Boundary");

        if (guestFishLayer != -1 && obstacleLayerIndex != -1)
        {
            Physics.IgnoreLayerCollision(guestFishLayer, obstacleLayerIndex);
        }
        else
        {
            Debug.LogError("레이어 설정을 확인하세요: GuestFish 또는 Obstacle 레이어가 존재하지 않습니다.");
        }

        // 랜덤한 시작 위치 설정
        int randomIndex = Random.Range(0, initialPositions.Length);
        transform.position = initialPositions[randomIndex];
        Debug.Log($"시작 위치 설정: {transform.position}");
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsProtagonist();
        }
        else if (isExiting)
        {
            ExitStage();
        }
    }

    // 새로운 스테이지가 시작될 때 호출
    public void StartEnteringStage()
    {
        isMoving = true;
        Debug.Log("StartEnteringStage() 호출됨: 이동 시작");
    }

    // 스테이지가 끝날 때 호출
    public void StartExitingStage()
    {
        isMoving = false;
        isExiting = true;
        Debug.Log("StartExitingStage() 호출됨: 이동 시작");
    }


    // 주인공을 중심으로 궤도를 그리면서 이동하는 함수
    void MoveTowardsProtagonist()
    {
        Vector3 protagonistPosition = new Vector3(0, 0, 0);  // 주인공의 위치

        // 주인공과의 거리 계산
        float distanceToProtagonist = Vector3.Distance(transform.position, protagonistPosition);

        // 주인공과의 최소 거리를 유지하면서 이동
        if (distanceToProtagonist > minDistanceFromProtagonist)
        {
            // 주인공을 중심으로 궤도를 그리며 회전
            transform.RotateAround(protagonistPosition, Vector3.up, rotationSpeed * Time.deltaTime);

            // 목표를 향해 점점 안쪽으로 이동하는 로직 추가 (단, 최소 거리 유지)
            Vector3 directionToCenter = (protagonistPosition - transform.position).normalized;
            transform.position += directionToCenter * inwardMoveSpeed * Time.deltaTime;

            // 목표 Y 좌표로 점진적으로 이동 (수직 방향 이동)
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.MoveTowards(transform.position.y, protagonistPosition.y, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            // 목표 위치를 향해 회전
            Quaternion targetRotation = Quaternion.LookRotation(protagonistPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // 주인공과의 최소 거리를 깨면 멈춤
            isMoving = false;
            Debug.Log("Fish has stopped due to breaking the minimum distance from protagonist.");
        }
    }

 // 스테이지가 끝날 때 무작위 방향으로 직진하여 그라운드를 빠져나가는 함수
    void ExitStage()
    {
        // 주어진 시간 동안 무작위 방향으로 이동
        if (exitTimer < exitDuration)
        {
            // 이동할 방향을 먼저 설정하고 그 방향으로 회전
            Quaternion targetRotation = Quaternion.LookRotation(exitDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 100);

            // 회전이 목표 방향에 거의 맞춰졌을 때 X축 방향으로 이동
            if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
            {
                // X축 방향을 기준으로 이동하도록 수정
                transform.position -= transform.right * moveSpeed * Time.deltaTime;
            }

            // Y 좌표를 유지하도록 설정 (지면 위를 유지)
            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.Clamp(newPosition.y, -10.0f, 10.0f);  // Y 값의 범위를 제한하여 너무 높거나 낮지 않도록 설정
            transform.position = newPosition;

            // 이동 시간 누적
            exitTimer += Time.deltaTime;
        }
        else
        {
            // 일정 시간이 지나면 이동 중지
            isExiting = false;
            Debug.Log("ExitStage() 종료: 물고기가 그라운드를 빠져나감.");
        }
    }
}
