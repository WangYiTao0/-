using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FarmGame.Inventory
{
    public class ItemToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;       
        [SerializeField] private TextMeshProUGUI _typeText;       
        [SerializeField] private TextMeshProUGUI _descriptionText;  
        [SerializeField] private Text _itemValueText;       
        [SerializeField] private GameObject _buttomPart;

        private ItemDetails _itemDetails;
        
        public void SetupToolTip(ItemDetails itemDetails, SlotType slotType)
        {
            _itemDetails = itemDetails;
            _nameText.SetText(itemDetails.ItemName);
            _typeText.SetText(GetItemType(itemDetails.ItemType));
            _descriptionText.SetText(itemDetails.ItemDescription);

            if (itemDetails.ItemType == ItemType.Commodity ||
                itemDetails.ItemType == ItemType.Seed ||
                itemDetails.ItemType == ItemType.Furniture)
            {
                _buttomPart.SetActive(true);
                var price = itemDetails.ItemPrice;
                //商店slot 里 显示原本价格
                //包里显示卖出价格
                if (slotType == SlotType.Bag || slotType == SlotType.Box)
                { 
                    price = (int)(itemDetails.ItemPrice * itemDetails.SellPercentage);
                } 
                _itemValueText.text = price.ToString();
            }
            else
            {
                _buttomPart.SetActive(false);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }

        private string GetItemType(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Seed => "種子",
                ItemType.Commodity => "商品",
                ItemType.Furniture => "家具",
                ItemType.BreakTool => "ツール",
                ItemType.ChopTool => "ツール",
                ItemType.HoeTool => "ツール",
                ItemType.ReapTool => "ツール",
                ItemType.WaterTool => "ツール",
                ItemType.CollectTool => "ツール",
                _ => "null"
            };
        }
    }
}