
using System;
using UnityEngine;

namespace FarmGame.Inventory
{

    public class Item : MonoBehaviour
    {
        public int ItemID;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D _collider2D;
        [HideInInspector] public ItemDetails ItemDetails;
        

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _collider2D = GetComponent<BoxCollider2D>();
        }

        private void Start()
        {
            if (ItemID != 0)
                Init(ItemID);
        }

        public void Init(int itemId)
        {
            ItemID = itemId;
            ItemDetails = InventoryManager.Instance.GetItemDetails(itemId);

            if (ItemDetails != null)
            {
                _spriteRenderer.sprite = ItemDetails.ItemOnWorldSprite != null
                    ? ItemDetails.ItemOnWorldSprite
                    : ItemDetails.ItemIcon;
                //修改碰撞体尺寸
                Vector2 newSize = new Vector2(_spriteRenderer.sprite.bounds.size.x,
                    _spriteRenderer.sprite.bounds.size.y);
                _collider2D.size = newSize;
                _collider2D.offset = new Vector2(0, _spriteRenderer.sprite.bounds.center.y);
            }
        }
    }
}