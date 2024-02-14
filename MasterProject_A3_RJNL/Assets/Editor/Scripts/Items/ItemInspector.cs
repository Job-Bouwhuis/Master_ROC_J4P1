// Creator: Job

using ShadowUprising.Items;
using System;
using System.Linq;
using UnityEditor;
using WinterRose;
using UnityEngine;

using gui = UnityEditor.EditorGUILayout;
using NUnit.Framework;
using WinterRose.Reflection;

namespace ShadowUprising.Editors.Inspectors
{
    [CustomEditor(typeof(Item))]
    public class ItemInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Item item = (Item)target;

            // get index of the current selected item function
            int index = 0;
            if (item.ItemFunction != null)
            {
                foreach(var type in ItemUtils.ItemFunctionTypes)
                {
                    if (type.Name == item.ItemFunctionProviderName)
                    {
                        break;
                    }
                    index++;
                }
            }

            int selectedIndex = gui.Popup("Item Function", index, ItemUtils.ItemFunctionTypes.Select(x => x.Name).ToArray());

            if (item.ItemFunction == null || item.ItemFunctionProviderName != ItemUtils.ItemFunctionTypes[selectedIndex].Name)
            {
                item.ItemFunctionProviderName = ItemUtils.ItemFunctionTypes[selectedIndex].Name;
                serializedObject.ApplyModifiedProperties();
            }

            if(GUI.changed)
            {
                EditorUtility.SetDirty(target);
                AssetDatabase.SaveAssets();
            }
        }
    }
}