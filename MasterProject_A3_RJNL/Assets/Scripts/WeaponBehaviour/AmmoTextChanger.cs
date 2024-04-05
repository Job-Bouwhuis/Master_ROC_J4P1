using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShadowUprising.WeaponBehaviour
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AmmoTextChanger : MonoBehaviour
    {
        TextMeshProUGUI text;

        int ammoLoaded;
        int unloadedAmmo;

        // Start is called before the first frame update
        void Start()
        {
            text = GetComponent<TextMeshProUGUI>();
            var ammoHandler = FindObjectOfType<AmmoHandler>();
            ammoHandler.onAmmoChanged += UpdateAmmo;
            ammoHandler.onUnloadedAmmoChanged += UpdateUnloadedAmmo;
        }

        void UpdateAmmo(int ammo)
        {
            ammoLoaded = ammo;
            UpdateText();
        }

        void UpdateUnloadedAmmo(int ammo)
        {
            unloadedAmmo = ammo;
            UpdateText();
        }

        void UpdateText()
        {
            text.text = $"{ammoLoaded}/{unloadedAmmo}";
        }

    }
}
