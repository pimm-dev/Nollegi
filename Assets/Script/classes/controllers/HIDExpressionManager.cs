using UnityEngine;
using UnityEngine.EventSystems; // UI 상호작용 감지를 위한 네임스페이스

/// <summary>
/// HID 장치와의 상호작용, 입출력 상황을 화면에 표현하기 위해 필요한 기능을 제공합니다.
/// HID 장치와의 상호작용이 발생했을 때, 상호작용 이벤트를 핸들링하고 <c>HIDExpressionManagement</c>에게 이벤트 처리 결과를 전달합니다.
/// </summary>
public class HIDExpressionManager : MonoBehaviour
{
    HIDExpressionManagement hidExpressionManagement = HIDExpressionManagement.instance;
    HIDInteractionInvokerConfig keyConfig = HIDInteractionInvokerConfigHolder.instance.config;

    void Update()
    {
        bool isActiveFocusModeInThisFrame = hidExpressionManagement.isCursorFocusMode;

        // 잠재적인 성능 이슈 우려: 포커스모드가 활성화되어있지 않으면 비활성화 키를 평가하지 않음
        if (hidExpressionManagement.isCursorFocusMode)
        {
            foreach (KeyCode each in keyConfig.keyHoldDisableCursorFocusMode)
            {
                if (Input.GetKey(each))
                {
                    isActiveFocusModeInThisFrame = false;
                    break;
                }
            }

            foreach (KeyCode each in keyConfig.keyToggleDisableCursorFocusMode)
            {
                if (Input.GetKeyDown(each))
                {
                    isActiveFocusModeInThisFrame = false;
                    break;
                }
            }
        }

        foreach (KeyCode each in keyConfig.keyToggleEnableCursorFocusMode)
        {
            if (Input.GetKeyDown(each))
            {
                isActiveFocusModeInThisFrame = true;
                break;
            }
        }
        
        hidExpressionManagement.SetCursorFocusMode(isActiveFocusModeInThisFrame);
        hidExpressionManagement.ExecuteEachFrame();
    }
}
