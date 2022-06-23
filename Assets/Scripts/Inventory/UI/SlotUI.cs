﻿using System;
using FarmGame;
using FarmGame.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class SlotUI : MonoBehaviour,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
    {
        [Header("组件获取")]
        [SerializeField] private Image _slotImage;

        [SerializeField] private TextMeshProUGUI _amountText;

        [SerializeField] public Image _slotHighLight;

        [SerializeField] private Button _slotButton;

        /// <summary>
        /// 格子类型
        /// </summary>
        public SlotType SlotType;

        public bool IsSelected;

        public ItemDetails ItemDetails;

        public int ItemAmount;

        public int SlotIndex;

        public InventoryUI InventoryUI => GetComponentInParent<InventoryUI>();
        
        private void Start()
        {
            IsSelected = false;

            if (this.ItemDetails.ItemID == 0)
            {
                UpdateEmptySlot();
            }
        }
        
  

        /// <summary>
        /// 更新格子UI信息
        /// </summary>
        /// <param name="itemDetails"></param>
        /// <param name="amount"></param>
        public void UpdateSlot(ItemDetails itemDetails, int amount)
        {
            ItemDetails = itemDetails;
            _slotImage.sprite = itemDetails.ItemIcon;
            ItemAmount = amount;
            _amountText.SetText(ItemAmount.ToString());
            
            _slotImage.enabled = true;
            _slotButton.interactable = true;
        }
        
        /// <summary>
        /// 将slot 更新为空
        /// </summary>
        public void UpdateEmptySlot()
        {
            if (IsSelected)
            {
                IsSelected = false;
            }

            _slotImage.enabled = false;
            _amountText.text = String.Empty;
            _slotButton.interactable = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (ItemAmount == 0)
                return;
            IsSelected = !IsSelected;
            InventoryUI.UpdateSlotHighLight(SlotIndex);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (ItemAmount == 0)
                return;

            InventoryUI.dragItemImage.enabled = true;
            InventoryUI.dragItemImage.sprite = _slotImage.sprite;
            InventoryUI.dragItemImage.SetNativeSize();
            IsSelected = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            InventoryUI.dragItemImage.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InventoryUI.dragItemImage.enabled = false;
            //判断是不是空

            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                SlotUI targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
                if (targetSlot != null)
                {
                    int targetIndex = targetSlot.SlotIndex;

                    //背包交换
                    if (SlotType == SlotType.Bag && targetSlot.SlotType == SlotType.Bag)
                    {
                        InventoryManager.Instance.SwapItem(SlotIndex,targetIndex);
                    }
                    //商店购买
                    //仓库交换
                    
                }
            }
        }
    }
}