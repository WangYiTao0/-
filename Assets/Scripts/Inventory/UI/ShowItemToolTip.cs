using System;
using Inventory.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmGame.Inventory
{
    [RequireComponent(typeof(SlotUI))]
    public class ShowItemToolTip : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        private SlotUI _slotUI;

        private InventoryUI _inventory => GetComponentInParent<InventoryUI>();
        private void Awake()
        {
            _slotUI = GetComponent<SlotUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_slotUI.ItemAmount == 0)
            {
                _inventory._ItemToolTip.gameObject.SetActive(false);
                return;
            }
            _inventory._ItemToolTip.gameObject.SetActive(true);
            _inventory._ItemToolTip.SetupToolTip(_slotUI.ItemDetails,_slotUI.SlotType);
            _inventory._ItemToolTip.GetComponent<RectTransform>().pivot = new Vector2(0.5f,0);
            _inventory._ItemToolTip.transform.position = transform.position + Vector3.up * 60;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventory._ItemToolTip.gameObject.SetActive(false);
        }
    }
}