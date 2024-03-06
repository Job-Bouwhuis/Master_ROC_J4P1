// Creator: Job
using ShadowUprising.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class CreateTextButtonMenuItem : MonoBehaviour
{
    [MenuItem("GameObject/UI/Text Button", false, 10)]
    static void CreateTextButton(MenuCommand command)
    {
        GameObject result = new("New Text Button");

        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(result, command.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(result, "Create " + result.name);
        // Select the object for the user
        Selection.activeObject = result;

        result.AddComponent<RectTransform>();
        result.AddComponent<TextMeshProUGUI>();
        var button = result.AddComponent<TextButton>();

        button.text = "Button";
        button.normalColor = new Color(1, 1, 1);
        button.hoverColor = new Color(.5f, .5f, .5f);
        button.pressedColor = new Color(1, .5f, .5f);
        button.disabledColor = new Color(.5f, .5f, .5f, .33f);
    }
}