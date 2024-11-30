using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HID와의 상호작용 발생에 의존하는 이벤트에 대해서, 이벤트를 발생시키는 HID와의 상호작용 유형을 정의합니다.
/// 이 구조체는 추후에 직렬화할 수 있도록 고려되었습니다.
/// </summary>
[Serializable]
public struct HIDInteractionInvokerConfig {
    public List<KeyCode> keyToggleDisableCursorFocusMode { get; set; }
    public List<KeyCode> keyToggleEnableCursorFocusMode { get; set; }
    public List<KeyCode> keyHoldDisableCursorFocusMode { get; set; }

    /// <summary>
    /// HIDInteractionInvokerConfig의 인스턴스를 생성합니다.
    /// 최초 의도는 자동으로 기본값을 불러오도록 하여 매개 변수 없는 생성자를 하는 것이었으나,
    /// 유니티의 C# 버전이 낮아서 구현할 수 없어 매개 변수를 추가한 생성자를 사용하도록 변경되었습니다.
    /// 따라서 의도대로 작동하려면 생성자를 호출할 때 기본값 데이터를 매개변수로 전달해야합니다.
    /// 혹은 매개변수 필드를 전부 null로 설정하도록 작성해도 됩니다.
    /// </summary>
    /// <param name="keyToggleDisableCursorFocusMode"></param>
    /// <param name="keyToggleEnableCursorFocusMode"></param>
    /// <param name="keyHoldDisableCursorFocusMode"></param>
    public HIDInteractionInvokerConfig(
        KeyCode[] keyToggleDisableCursorFocusMode,
        KeyCode[] keyToggleEnableCursorFocusMode,
        KeyCode[] keyHoldDisableCursorFocusMode
    ) {
        this.keyToggleDisableCursorFocusMode = keyToggleDisableCursorFocusMode != null ? new List<KeyCode>(keyToggleDisableCursorFocusMode) : new List<KeyCode>(HIDInteractionInvokerDefaults.DEFAULT_KEY_TOGGLE_DISABLE_CURSOR_FOCUS_MODE);
        this.keyToggleEnableCursorFocusMode = keyToggleEnableCursorFocusMode != null ? new List<KeyCode>(keyToggleEnableCursorFocusMode) : new List<KeyCode>(HIDInteractionInvokerDefaults.DEFAULT_KEY_TOGGLE_ENABLE_CURSOR_FOCUS_MODE);
        this.keyHoldDisableCursorFocusMode = keyHoldDisableCursorFocusMode != null ? new List<KeyCode>(keyHoldDisableCursorFocusMode) : new List<KeyCode>(HIDInteractionInvokerDefaults.DEFAULT_KEY_HOLD_DISABLE_CURSOR_FOCUS_MODE);
    }
}

/// <summary>
/// HIDInteractionInvokerConfig를 생성하고 관리하는 클래스입니다.
/// HIDInteractionInvokerConfig를 사용하고자 한다면, 이 클래스의 인스턴스를 사용하세요.
/// </summary>
public class HIDInteractionInvokerConfigHolder {
    // Todo: Setter must be implemented later.
    // HIDInteractionInvokerConfig의 세터가 private로 지정되어있습니다.
    // 이후에 세터를 추가할 수 있도록 의도된 것이나, 상황에 따라 public으로 변경하는 것도 고려하세요.
    public HIDInteractionInvokerConfig config { get; private set; }

    private HIDInteractionInvokerConfigHolder() {
        this.config = new HIDInteractionInvokerConfig(
            HIDInteractionInvokerDefaults.DEFAULT_KEY_TOGGLE_DISABLE_CURSOR_FOCUS_MODE,
            HIDInteractionInvokerDefaults.DEFAULT_KEY_TOGGLE_ENABLE_CURSOR_FOCUS_MODE,
            HIDInteractionInvokerDefaults.DEFAULT_KEY_HOLD_DISABLE_CURSOR_FOCUS_MODE
        );
    }

    public static HIDInteractionInvokerConfigHolder instance = new HIDInteractionInvokerConfigHolder();
}
