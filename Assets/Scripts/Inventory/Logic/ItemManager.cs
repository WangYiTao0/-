using System;
using Unity.Mathematics;
using UnityEngine;

namespace FarmGame.Inventory
{
    /// <summary>
    /// 管理世界上所有的Item
    /// </summary>
    public class ItemManager : MonoBehaviour
    {
        public Item ItemPrefab;
        [SerializeField] private Transform _itemParent;

        private void Start()
        {
            _itemParent = GameObject.FindWithTag("ItemParent").transform;
        }

        private void OnEnable()
        {
            EventHandler.InstantiateItemInScene += OnInstantiateItemInScene;
        }
        private void OnDisable()
        {
            EventHandler.InstantiateItemInScene -= OnInstantiateItemInScene;
        }
        
        private void OnInstantiateItemInScene(int id, Vector3 pos)
        {
            var item = Instantiate(ItemPrefab, pos, quaternion.identity,_itemParent);
            item.ItemID = id;
        }

    }
}