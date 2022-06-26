using System;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FarmGame.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public ItemToolTip _ItemToolTip;
        
        [Header("拖拽图片")] public Image dragItemImage;
        
        [Header("玩家背包UI")]
        [SerializeField] private GameObject _bagUI;

        private bool _isBagOpened;
        [SerializeField] private SlotUI[] _playSlotArray;

        private void Start()
        {
            for (int i = 0; i < _playSlotArray.Length; i++)
            {
                _playSlotArray[i].SlotIndex = i;
            }

            _isBagOpened = _bagUI.activeInHierarchy;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OpenBagUI();
            }
        }

        private void OnEnable()
        {
            
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        }
        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        }
        
        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < _playSlotArray.Length; i++)
                    {
                        if (list[i].ItemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].ItemID);
                            _playSlotArray[i].UpdateSlot(item,list[i].ItemAmount);
                        }
                        else
                        {
                            _playSlotArray[i].UpdateEmptySlot();
                        }
                    }

                    break;
                case InventoryLocation.Box:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 打开背包
        /// </summary>
        public void OpenBagUI()
        {
            _isBagOpened = !_isBagOpened;
            
            _bagUI.SetActive(_isBagOpened);
        }

        /// <summary>
        /// 更新高亮显示
        /// </summary>
        /// <param name="index"></param>
        public void UpdateSlotHighLight(int index)
        {
            foreach (var slotUI in _playSlotArray)
            {
                if (slotUI.IsSelected && slotUI.SlotIndex == index)
                {
                    slotUI._slotHighLight.gameObject.SetActive(true);
                }
                else
                {
                    slotUI.IsSelected = false;
                    slotUI._slotHighLight.gameObject.SetActive(false);
                }
            }
        }
    }
}