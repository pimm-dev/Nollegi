using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseCursorManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;  // 마우스 커서를 보이게 설정
        Cursor.lockState = CursorLockMode.None;  // 마우스 커서가 자유롭게 움직일 수 있도록 설정
    }

    void Update()
    {
    Cursor.visible = true;  // 마우스 커서를 항상 보이게 설정
    Cursor.lockState = CursorLockMode.None;  // 마우스를 자유롭게
    }
}