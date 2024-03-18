using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShadowUprising.WeaponBehaviour
{
    public class AmmoTextChanger : MonoBehaviour
    {
        public TMPro.TextMeshProUGUI textMeshPro;

        int ammoLoaded;
        int unloadedAmmo;

        // Start is called before the first frame update
        void Start()
        {
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
            textMeshPro.text = $"{ammoLoaded}/{unloadedAmmo}";
        }

    }
}
