using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmGame
{
    public class AnimatorOverride : MonoBehaviour
    {
        private Animator[] _animators;
        [SerializeField] private SpriteRenderer _holdSpriteRenderer;

        [Header("各部分动画列表")]
        [SerializeField] private List<AnimatorType> _animatorTypeList;

        private Dictionary<string, Animator> _animatorNameDictionary = new Dictionary<string, Animator>();
        private void Awake()
        {
            _animators = GetComponentsInChildren<Animator>();

            foreach (var animator in _animators)
            {
                _animatorNameDictionary.Add(animator.name,animator);
            }

            if (_holdSpriteRenderer == null)
            {
                _holdSpriteRenderer = transform.Find("HoldItem").GetComponent<SpriteRenderer>();
            }

            _holdSpriteRenderer.enabled = false;
        }
        //物品点击时拾取

        private void OnEnable()
        {
            EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        }
        private void OnDisable()
        {
            EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        }


        private void OnItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
        {
            //WORKFLOW: 不同工具返回不同动画
            PartType currentType = itemDetails.ItemType switch
            {
                ItemType.Seed => PartType.Carry,
                ItemType.Commodity => PartType.Carry,
                _ => PartType.None,
            };

            if (isSelected == false)
            {
                currentType = PartType.None;
                _holdSpriteRenderer.enabled = false;
            }
            else
            {
                if (currentType == PartType.Carry)
                {
                    _holdSpriteRenderer.sprite = itemDetails.ItemOnWorldSprite;
                    _holdSpriteRenderer.enabled = true;
                }
            }
            //改变动画
            SwitchAnimator(currentType);
        }

        void SwitchAnimator(PartType partType)
        {
            foreach (var animatorType in _animatorTypeList)
            {
                if (animatorType._PartType == partType)
                {
                    _animatorNameDictionary[animatorType._PartName.ToString()].runtimeAnimatorController =
                        animatorType._OverrideController;
                }
            }
        }
    }
}