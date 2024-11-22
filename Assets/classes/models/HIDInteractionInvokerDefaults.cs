using UnityEngine;

/// <summary>
/// HIDInteractionInvokerConfig의 기본값을 정의합니다.
/// </summary>
public static class HIDInteractionInvokerDefaults {
    public static readonly KeyCode[] DEFAULT_KEY_TOGGLE_DISABLE_CURSOR_FOCUS_MODE = { KeyCode.Escape };
    public static readonly KeyCode[] DEFAULT_KEY_TOGGLE_ENABLE_CURSOR_FOCUS_MODE = { KeyCode.Mouse0 };

    public static readonly KeyCode[] DEFAULT_KEY_HOLD_DISABLE_CURSOR_FOCUS_MODE = { KeyCode.LeftAlt };
}
