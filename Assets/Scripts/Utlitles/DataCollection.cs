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
}