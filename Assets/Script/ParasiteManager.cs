using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasiteManager : MonoBehaviour
{
    public GameObject parasitePrefab; // 기생충 프리팹
    public int minParasiteCount = 3;  // 최소 기생충 수
    public int maxParasiteCount = 8;  // 최대 기생충 수

    private List<GameObject> parasites = new List<GameObject>(); // 기생충 리스트
    private Mesh fishMesh; // 물고기 Mesh 데이터

    void InitializeFishMesh()
    {
        // 물고기 Mesh 가져오기
        MeshFilter fishMeshFilter = GetComponent<MeshFilter>();
        if (fishMeshFilter != null)
        {
            fishMesh = fishMeshFilter.mesh;

            if (fishMesh == null || fishMesh.triangles.Length == 0 || fishMesh.vertices.Length == 0)
            {
                Debug.LogError($"{gameObject.name} 물고기 Mesh에 삼각형 또는 정점 데이터가 없습니다.");
                return;
            }
        }
        else
        {
            Debug.LogError($"{gameObject.name} 물고기 MeshFilter가 없습니다.");
            return;
        }
    }

    // 기생충 생성 및 물고기 표면에 부착
    public void SpawnParasites()
    {

        InitializeFishMesh(); 

        ClearParasites();

        if (fishMesh == null)
        {
            Debug.LogError($"{gameObject.name} 물고기 Mesh가 없습니다.");
            return;
        }

        int parasiteCount = Random.Range(minParasiteCount, maxParasiteCount);

        for (int i = 0; i < parasiteCount; i++)
        {
            Vector3 spawnPosition = GetRandomSurfacePointOnFish();

            if (spawnPosition != Vector3.zero)
            {
                GameObject parasite = Instantiate(parasitePrefab, spawnPosition, Quaternion.identity);
                parasite.transform.SetParent(transform); // 물고기의 자식으로 설정
                parasites.Add(parasite);
            }
        }

        Debug.Log($"{gameObject.name}에 {parasites.Count}개의 기생충이 생성되었습니다.");
    }

    // 기존 기생충 삭제
    public void ClearParasites()
    {
        foreach (GameObject parasite in parasites)
        {
            Destroy(parasite);
        }
        parasites.Clear();
    }

    // 물고기 Mesh에서 임의의 표면 위치 찾기
    Vector3 GetRandomSurfacePointOnFish()
    {
        if (fishMesh == null) return Vector3.zero;

        int[] triangles = fishMesh.triangles;
        Vector3[] vertices = fishMesh.vertices;

        if (triangles.Length == 0 || vertices.Length == 0)
        {
            Debug.LogError("삼각형 또는 정점 데이터가 없습니다.");
            return Vector3.zero;
        }

        int triangleIndex = Random.Range(0, triangles.Length / 3) * 3;
        Vector3 vertex1 = vertices[triangles[triangleIndex]];
        Vector3 vertex2 = vertices[triangles[triangleIndex + 1]];
        Vector3 vertex3 = vertices[triangles[triangleIndex + 2]];

        Vector3 randomPointInTriangle = GetRandomPointInTriangle(vertex1, vertex2, vertex3);
        return transform.TransformPoint(randomPointInTriangle);
    }

    // 삼각형 내부에서 임의의 점을 선택
    Vector3 GetRandomPointInTriangle(Vector3 v1, Vector3 v2, Vector3 v3)
    {
        float u = Random.value;
        float v = Random.value;

        if (u + v > 1)
        {
            u = 1 - u;
            v = 1 - v;
        }

        return v1 + u * (v2 - v1) + v * (v3 - v1);
    }
}