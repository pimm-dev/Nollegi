using UnityEngine;
using UnityEngine.EventSystems; // UI 상호작용 감지를 위한 네임스페이스

/// <summary>
/// HID 장치와의 상호작용, 입출력 상황을 화면에 표현하기 위해 필요한 기능을 제공합니다.
/// </summary>
public class HIDExpressionManagement
{
    private HIDExpressionManagement() {}
    public static HIDExpressionManagement instance = new HIDExpressionManagement();
    public bool isCursorFocusMode { get; private set; } = true;

    /// <summary>
    /// 커서 포커스 모드를 활성화하거나 비활성화합니다.
    /// </summary>
    /// <param name="isCursorFocusMode">커서 포커스 모드 여부</param>
    public void SetCursorFocusMode(bool isCursorFocusMode)
    {
        this.isCursorFocusMode = isCursorFocusMode;
        ExecuteEachFrame();
    }

    /// <summary>
    /// 매 프레임마다 <c>HIDExpressionManagement</c>가 관리하는 HID 표현 상태를 설정에 따라 조정합니다. <br />
    /// <c>HIDExpressionManagement</c>를 사용하고자 한다면, 이 메서드는 반드시 매 프레임마다 호출해야 합니다. <br />
    /// </summary>
    /// <example>
    /// <code>
    /// void Update() {
    ///     HIDExpressionManagement.instance.ExecuteEachFrame();
    /// }
    /// </code>
    /// </example>
    public void ExecuteEachFrame()
    {
        // UI 클릭 중 상태를 감지
        if (EventSystem.current != null && Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject())
        {
            return; // UI 클릭 중일 경우 상태를 유지
        }

        // 커서 상태를 포커스 모드 상태에 따라 업데이트
        if (Cursor.visible != !isCursorFocusMode || Cursor.lockState != (isCursorFocusMode ? CursorLockMode.Locked : CursorLockMode.None))
        {
            Cursor.visible = !isCursorFocusMode;
            Cursor.lockState = isCursorFocusMode ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
