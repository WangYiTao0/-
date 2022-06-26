using System;
using UnityEngine;

namespace FarmGame
{
    [Serializable]
    public class ItemDetails
    {
        public int ItemID;
        
        public string ItemName;

        public ItemType ItemType;

        public Sprite ItemIcon;

        public Sprite ItemOnWorldSprite;

        public string ItemDescription;

        public int ItemUseRadius;

        public bool CanPickedup;
        
        public bool CanDropped;
        
        public bool CanCarried;

        public int ItemPrice;

        [Range(0,1)]
        public float SellPercentage;
    }

    /// <summary>
    /// Struct 默认值
    /// </summary>
    [System.Serializable]
    public struct InventoryItem
    {
        public int ItemID;
        public int ItemAmount;
    }

    [Serializable]
    public class AnimatorType
    {
        public PartType _PartType;
        public PartName _PartName;

        public AnimatorOverrideController _OverrideController;
    }
}