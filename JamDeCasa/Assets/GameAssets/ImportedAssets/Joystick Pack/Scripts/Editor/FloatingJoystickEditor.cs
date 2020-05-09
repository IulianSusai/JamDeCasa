﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FloatingJoystick))]
public class FloatingJoystickEditor : JoystickEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (background != null)
        {
            RectTransform backgroundRect = (RectTransform)background.objectReferenceValue;
			backgroundRect.anchorMax = Vector2.one * 0.5f;
			backgroundRect.anchorMin = Vector2.one * 0.5f;
			backgroundRect.pivot = center;
        }
    }
}