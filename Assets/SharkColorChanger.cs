using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkColorChanger : MonoBehaviour
{
    public Renderer sharkRenderer; // 상어의 Renderer를 연결

    // Start 메서드에서 색상을 변경
    void Start()
    {
        ChangeSharkColorForTesting();
    }

    // 상어의 색상을 약간 밝게 변경
    void ChangeSharkColorForTesting()
    {
        if (sharkRenderer != null)
        {
            // 기존 색상에서 약간 밝게 조정
            Color currentColor = sharkRenderer.material.color;
            Color testColor = currentColor * 1.1f; // 색을 약간 밝게
            testColor.a = currentColor.a; // 알파(투명도) 값 유지
            sharkRenderer.material.color = testColor;

            Debug.Log($"Shark color changed for testing: {testColor}");
        }
        else
        {
            Debug.LogWarning("Shark renderer is not assigned!");
        }
    }
}