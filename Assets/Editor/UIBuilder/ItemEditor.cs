using System;
using System.Collections.Generic;
using System.Linq;
using FarmGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class ItemEditor : EditorWindow
{
    private ItemDataList_SO _dataBase;
    private List<ItemDetails> _itemList = new List<ItemDetails>();
    private VisualTreeAsset _itemRawTemplate;

    private ListView _itemListView;
    private ScrollView _itemDetailsSection;
    private ItemDetails _activeItem;

    private VisualElement _iconPreview;
    
    
    
    //Default Icon
    private Sprite _defaultIcon;

    [MenuItem("FarmGame/ItemEditor")]
    public static void ShowExample()
    {
        ItemEditor wnd = GetWindow<ItemEditor>();
        wnd.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/ItemEditor.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
        
        //Get itemRawTemplate
        _itemRawTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/UIBuilder/ItemRawTemplate.uxml");
        
        //
        _defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/GameResource/Art/Faces/Boy01-1.png");
        
        //Find ListView Container -> ItemList -> 1、label 2、ListView
        _itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        //ind ScrollView  Container -> ScrollView
        _itemDetailsSection = root.Q<ScrollView>("ItemDetails");
        
        _iconPreview = _itemDetailsSection.Q<VisualElement>("Icon");

        root.Q<Button>("AddButton").clicked += OnAddItemButtonClicked;
        root.Q<Button>("DeleteButton").clicked += OnDeleteItemButtonClicked;

        
        //Load DataBase
        LoadDataBase();
        
        GenerateListView();
    }

    #region 按键事件
    private void OnAddItemButtonClicked()
    {
        ItemDetails newitem = new ItemDetails();

        newitem.ItemName = "New Item";
        newitem.ItemID = 1000 + _itemList.Count;
        _itemList.Add(newitem);
        _itemListView.Rebuild();
    }

    private void OnDeleteItemButtonClicked()
    {
        _itemList.Remove(_activeItem);
        _activeItem = null;
        //没选中前 不显示信息
        _itemDetailsSection.visible = false;
        _itemListView.Rebuild();
    }


    #endregion
    
    private void LoadDataBase()
    {
        //固定写法 t: 表示Type
        var dataArray =   AssetDatabase.FindAssets("t: ItemDataList_SO");
        if (dataArray.Length >= 1)
        {
            var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
            _dataBase = AssetDatabase.LoadAssetAtPath(path,typeof(ItemDataList_SO)) as ItemDataList_SO;
        }

        _itemList = _dataBase.ItemDetailsList;
        //标记 不标记则无法保存数据
        EditorUtility.SetDirty(_dataBase);

        // #if UNITY_EDITOR
        // foreach (var itemDetails in _itemDetailsList)
        // {
        //     Debug.Log(itemDetails.ItemID);
        // }
        // #endif
    }

    private void GenerateListView()
    {
        //get 模板
        Func<VisualElement> makeItem = () => _itemRawTemplate.CloneTree();
        
        //E ELEMENT I iD
        Action<VisualElement, int> bindItem = (element, id) =>
        {
            if (id < _itemList.Count)
            {
                element.Q<VisualElement>("Icon").style.backgroundImage　
                        = _itemList[id].ItemIcon == null ? _defaultIcon.texture : _itemList[id].ItemIcon.texture;
                element.Q<Label>("Name").text = _itemList[id].ItemName == null ? "NO ITEM" : _itemList[id].ItemName;
        
            }
        };

        _itemListView.fixedItemHeight = 60;
        _itemListView.itemsSource = _itemList;
        _itemListView.makeItem = makeItem;
        _itemListView.bindItem = bindItem;

        _itemListView.onSelectionChange += OnListSelectionChange;

        //没选中前 不显示信息
        _itemDetailsSection.visible = false;
    }

    private void OnListSelectionChange(IEnumerable<object> selectItem)
    {
        _activeItem = (ItemDetails) selectItem.First();
        GetItemDetails();
        _itemDetailsSection.visible = true;
    }

    private void GetItemDetails()
    {
        _itemDetailsSection.MarkDirtyRepaint();
        
        _itemDetailsSection.Q<IntegerField>("ItemID").value = _activeItem.ItemID;
        //必须要写回调 这样才会在编辑器改变后 更新信息
        _itemDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemID = evt.newValue;
        });
        _itemDetailsSection.Q<TextField>("ItemName").value = _activeItem.ItemName;
        _itemDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemName = evt.newValue;
            _itemListView.Rebuild();
        });

        _iconPreview.style.backgroundImage = 
            _activeItem.ItemIcon == null ? _defaultIcon.texture : _activeItem.ItemIcon.texture;
        _itemDetailsSection.Q<ObjectField>("ItemIcon").value = _activeItem.ItemIcon;
        _itemDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
        {
            Sprite newIcon = evt.newValue as Sprite;
            _activeItem.ItemIcon = newIcon;
       
            _iconPreview.style.backgroundImage = newIcon == null? _defaultIcon.texture : newIcon.texture;
            _itemListView.Rebuild();
        });
        
         //其他所有变量的绑定
        _itemDetailsSection.Q<ObjectField>("ItemSprite").value = _activeItem.ItemOnWorldSprite;
        _itemDetailsSection.Q<ObjectField>("ItemSprite").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemOnWorldSprite = (Sprite)evt.newValue;
        });

        _itemDetailsSection.Q<EnumField>("ItemType").Init(_activeItem.ItemType);
        _itemDetailsSection.Q<EnumField>("ItemType").value = _activeItem.ItemType;
        _itemDetailsSection.Q<EnumField>("ItemType").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemType = (ItemType)evt.newValue;
        });

        _itemDetailsSection.Q<TextField>("Description").value = _activeItem.ItemDescription;
        _itemDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemDescription = evt.newValue;
        });

        _itemDetailsSection.Q<IntegerField>("ItemUseRadius").value = _activeItem.ItemUseRadius;
        _itemDetailsSection.Q<IntegerField>("ItemUseRadius").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemUseRadius = evt.newValue;
        });

        _itemDetailsSection.Q<Toggle>("CanPickedup").value = _activeItem.CanPickedup;
        _itemDetailsSection.Q<Toggle>("CanPickedup").RegisterValueChangedCallback(evt =>
        {
            _activeItem.CanPickedup = evt.newValue;
        });

        _itemDetailsSection.Q<Toggle>("CanDropped").value = _activeItem.CanDropped;
        _itemDetailsSection.Q<Toggle>("CanDropped").RegisterValueChangedCallback(evt =>
        {
            _activeItem.CanDropped = evt.newValue;
        });

        _itemDetailsSection.Q<Toggle>("CanCarried").value = _activeItem.CanCarried;
        _itemDetailsSection.Q<Toggle>("CanCarried").RegisterValueChangedCallback(evt =>
        {
            _activeItem.CanCarried = evt.newValue;
        });

        _itemDetailsSection.Q<IntegerField>("Price").value = _activeItem.ItemPrice;
        _itemDetailsSection.Q<IntegerField>("Price").RegisterValueChangedCallback(evt =>
        {
            _activeItem.ItemPrice = evt.newValue;
        });

        _itemDetailsSection.Q<Slider>("SellPercentage").value = _activeItem.SellPercentage;
        _itemDetailsSection.Q<Slider>("SellPercentage").RegisterValueChangedCallback(evt =>
        {
            _activeItem.SellPercentage = evt.newValue;
        });
    }
}