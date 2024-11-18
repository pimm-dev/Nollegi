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
    

    private Vector3[] initialPositions;

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

        // (0, 0, 0) 기준으로 거리 20에 있는 랜덤 좌표 설정 (Y 좌표는 0)
        GenerateInitialPositions();

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
            transform.RotateAround(protagonistPosition, Vector3.up, rotationSpeed * Time.deltaTime);

            Vector3 directionToCenter = (protagonistPosition - transform.position).normalized;
            transform.position += directionToCenter * inwardMoveSpeed * Time.deltaTime;

            Vector3 newPosition = transform.position;
            newPosition.y = Mathf.MoveTowards(transform.position.y, protagonistPosition.y, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            Quaternion targetRotation = Quaternion.LookRotation(protagonistPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
            Debug.Log("Fish has stopped due to breaking the minimum distance from protagonist.");
        }
    }

    // 스테이지가 끝날 때 무작위 방향으로 직진하여 그라운드를 빠져나가는 함수
   void ExitStage()
    {
    // (0, 0, 0) 기준으로 이동 범위를 설정
    Vector3 protagonistPosition = new Vector3(0, 0, 0);
    float minDistance = 50.0f;
    float maxDistance = 60.0f;

    if (exitTimer < exitDuration)
    {
        Quaternion targetRotation = Quaternion.LookRotation(exitDirection, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 100);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 1.0f)
        {
            transform.position -= transform.right * moveSpeed * Time.deltaTime;
        }

        // 현재 위치에서 Y 좌표를 유지하며 업데이트
        Vector3 newPosition = transform.position;
        newPosition.y = 0.0f;
        transform.position = newPosition;

        exitTimer += Time.deltaTime;
    }
    else
    {
        // ExitStage 종료 후, 랜덤한 위치로 배치
        isExiting = false;

        // (0, 0, 0)을 중심으로 45~60 거리 내의 랜덤 위치 계산
        float randomDistance = Random.Range(minDistance, maxDistance);
        float randomAngle = Random.Range(0, Mathf.PI * 2);

        float x = randomDistance * Mathf.Cos(randomAngle);
        float z = randomDistance * Mathf.Sin(randomAngle);

        transform.position = new Vector3(x, 0, z);

        Debug.Log($"ExitStage 종료: 물고기가 새로운 위치로 이동: {transform.position}");
    }
    }

    // (0, 0, 0) 기준으로 거리 20에 있는 랜덤 좌표 생성 (Y 좌표는 항상 0)
    void GenerateInitialPositions()
    {
        float minDistance = 50.0f;
        float maxDistance = 60.0f;
        initialPositions = new Vector3[6];
        for (int i = 0; i < initialPositions.Length; i++)
        {
            float angle = Random.Range(0, Mathf.PI * 2); // 랜덤한 각도 (라디안 단위)
            float randomDistance = Random.Range(minDistance, maxDistance);

            // X와 Z 좌표는 원 위의 랜덤 위치를 계산
            float x = randomDistance * Mathf.Cos(angle);
            float z = randomDistance * Mathf.Sin(angle);

            initialPositions[i] = new Vector3(x, 0, z); // Y 좌표는 0으로 고정
        }
    }
}
