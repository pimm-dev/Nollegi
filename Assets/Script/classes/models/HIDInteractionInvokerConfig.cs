using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HIDInteractionInvokerConfig {
    public List<KeyCode> keyToggleDisableCursorFocusMode { get; set; }
    public List<KeyCode> keyToggleEnableCursorFocusMode { get; set; }
    public List<KeyCode> keyHoldDisableCursorFocusMode { get; set; }
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

public class HIDInteractionInvokerConfigHolder {
    // Todo: Setter must be implemented later.
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
