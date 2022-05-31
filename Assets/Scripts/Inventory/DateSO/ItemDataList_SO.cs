using System.Collections.Generic;
using FarmGame;
using UnityEngine;

namespace FarmGame
{
    [CreateAssetMenu(fileName = "ItemDataList_SO", menuName = "Inventory/ItemDataList", order = 0)]
    public class ItemDataList_SO : ScriptableObject
    {
        public List<ItemDetails> ItemDetailsList;
    }
}