using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension {
    public static T GetOrAddComponent<T>(this GameObject obj) where T : Component {
        return Utilities.GetOrAddComponent<T>(obj);
    }

    public static void BindEvent(this GameObject go, Action action = null, Action<BaseEventData> dragAction = null, UIEvent type = UIEvent.Click) {
        UI_Base.BindEvent(go, action, dragAction, type);
    }
}