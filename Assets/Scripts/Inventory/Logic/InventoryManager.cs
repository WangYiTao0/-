
using System;
using System.Linq;
using UnityEngine;

namespace  FarmGame.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("物品数据")]
        public ItemDataList_SO ItemDataListSo;

        /// <summary>
        /// 玩家背包
        /// </summary>
        [Header("背包数据")]
        public InventoryBag_SO PlayerBag;
        /// <summary>
        /// 箱子
        /// </summary>
        public InventoryBag_SO BoxBag;


        private void Start()
        {
            //更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,PlayerBag.ItemList);
        }

        /// <summary>
        /// 返回 Item信息
        /// </summary>
        /// <param name="id">item id</param>
        /// <returns></returns>
        public ItemDetails GetItemDetails(int id)
        {
            return ItemDataListSo.ItemDetailsList.Find(item => item.ItemID == id);
        }

        /// <summary>
        /// 添加物品到Player背包里
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toDestory">是否要销毁物品</param>
        public void AddItem(Item item, bool toDestory)
        {
            int index = GetItemIndexInBag(item.ItemID);

            //TODO 应该有添加失败的情况
            AddItemAtIndex(item.ItemID,index,1);
            
            // 是否已经有该物体
            //InventoryItem newItem = new InventoryItem();

            //Debug.Log(item.ItemDetails.ItemID);
          
            if (toDestory)
            {
                Destroy(item.gameObject);
            }
            
            //更新UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,PlayerBag.ItemList);
        }
        /// <summary>
        /// 背包是否有空位
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < PlayerBag.ItemList.Count; i++)
            {
                if (PlayerBag.ItemList[i].ItemID == 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// 通过物品ID找到背包已有的物品位置
        /// </summary>
        /// <param name="ID">物品ID</param>
        /// <returns>-1则没有这个物品</returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < PlayerBag.ItemList.Count; i++)
            {
                if (PlayerBag.ItemList[i].ItemID == ID)
                    return i;
            }
            return -1;
        }


        private void AddItemAtIndex(int id, int index, int amount)
        {
            //如果背包没有这个 且 背包有空位
            if (index == -1 && CheckBagCapacity())
            {
                var item = new InventoryItem(){ItemID = id, ItemAmount = amount};
                //找空位
                for (int i = 0; i < PlayerBag.ItemList.Count; i++)
                {
                    if (PlayerBag.ItemList[i].ItemID == 0)
                    {
                        PlayerBag.ItemList[i] = item;
                        break;
                    }
                }
            }
            else //背包有这个
            {
                int currentAmount = PlayerBag.ItemList[index].ItemAmount + amount;
                var item = new InventoryItem() { ItemID = id, ItemAmount = currentAmount };
                PlayerBag.ItemList[index] = item;
            }
        }

        /// <summary>
        /// 交换物品
        /// </summary>
        /// <param name="fromIndex">起始Index </param>
        /// <param name="targetIndex">目标 Index</param>
        public void SwapItem(int fromIndex, int targetIndex)
        {
            InventoryItem currentItem = PlayerBag.ItemList[fromIndex];
            InventoryItem targetItem = PlayerBag.ItemList[targetIndex];

            //目标地点有物品 有东西交换
            if (targetItem.ItemID != 0)
            {
                PlayerBag.ItemList[fromIndex] = targetItem;
                PlayerBag.ItemList[targetIndex] = currentItem;
            }
            else //目标地点没有物品
            {
                PlayerBag.ItemList[fromIndex] = new InventoryItem();
                PlayerBag.ItemList[targetIndex] = currentItem;
            }
            
            //更新背包
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player,PlayerBag.ItemList);
        }
    }
}

