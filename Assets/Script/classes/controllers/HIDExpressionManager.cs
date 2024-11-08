using UnityEngine;

public class HIDExpressionManager : MonoBehaviour
{
    HIDExpressionManagement hidExpressionManagement = HIDExpressionManagement.instance;
    HIDInteractionInvokerConfig keyConfig = HIDInteractionInvokerConfigHolder.instance.config;

    void Update()
    {
        bool isActiveFocusModeInThisFrame = hidExpressionManagement.isCursorFocusMode;
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
