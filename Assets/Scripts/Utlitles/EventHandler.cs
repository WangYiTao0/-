using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public static class EventHandler
    {
        public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;

        public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> itemList)
        {
            UpdateInventoryUI?.Invoke(location,itemList);
        }
    }
}