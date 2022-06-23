using System;
using UnityEngine;

namespace FarmGame.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            Item item = col.GetComponent<Item>();
            if (item != null)
            {
                if (item.ItemDetails.CanPickedup)
                {
                    //拾取物品 添加到背包
                    InventoryManager.Instance.AddItem(item,true);
                }
            }
        }
    }
}