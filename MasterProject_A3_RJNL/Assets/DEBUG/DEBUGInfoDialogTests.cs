using ShadowUprising.UI.InfoDialogs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.DEBUG
{
    public class DEBUGInfoDialogTests : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                InfoDialogManager.Instance.ShowInfoDialog("Test", "This is a test", 5f);
                InfoDialogData data = new InfoDialogData("Test", "This is a long long long long long long long" +
                    "long long long long long long long long long long long long" +
                    "long long long long long long long long long long test", 5f);
                InfoDialogManager.Instance.ShowInfoDialog(data);
            }
        }
    }
}