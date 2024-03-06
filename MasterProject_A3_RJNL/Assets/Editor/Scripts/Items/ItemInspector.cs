// Creator: Job

using ShadowUprising.Items;
using System.Linq;
using UnityEditor;
using UnityEngine;

using gui = UnityEditor.EditorGUILayout;

namespace ShadowUprising.Editors.Inspectors
{
    /// <summary>
    /// An editor script to display the item function type in the inspector as a dropdown instead of a textfield. improves the developer experience.
    /// </summary>
    [CustomEditor(typeof(Item))]
    public class ItemInspector : Editor
    {
        /// <summary>
        /// Draws the inspector for the item
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); 

            if(ItemUtils.ItemFunctionTypes.Count is 0)
            {
                gui.Separator();
                gui.LabelField("No Item Function Types Found");
                return;
            }

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