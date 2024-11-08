using UnityEngine;

public class HIDExpressionManagement
{
    private HIDExpressionManagement() {}
    public static HIDExpressionManagement instance = new HIDExpressionManagement();
    public bool isCursorFocusMode { get; private set; } = true;

    public void SetCursorFocusMode(bool isCursorFocusMode)
    {
        this.isCursorFocusMode = isCursorFocusMode;
        ExecuteEachFrame();
    }

    public void ExecuteEachFrame()
    {
        if (Cursor.visible == isCursorFocusMode) {
            Cursor.visible = !isCursorFocusMode;
            Cursor.lockState = isCursorFocusMode ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
