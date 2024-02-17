using ShadowUprising.Items;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable

namespace ShadowUprising.Inventorye
{
    public class Slot : MonoBehaviour
    {
        public Item? item;

        [SerializeField] Image graphic;
        [SerializeField] TMP_Text countText;

        private void Awake()
        {
            graphic = GetComponentInChildren<Image>();
            countText = GetComponentInChildren<TMP_Text>();
        }

        public void SetGraphic(Item item)
        {
            graphic.sprite = item.icon;
            countText.text = "";
            graphic.color = new Color(255, 255, 255, 255);
        }

        public void Clear()
        {
            graphic.sprite = null;
            graphic.color = new Color(0, 0, 0, 255);
            countText.text = "";
        }
    }
}