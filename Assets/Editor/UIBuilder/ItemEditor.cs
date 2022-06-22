using System;
using System.Collections.Generic;
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
        
        //Find ListView Container -> ItemList -> 1、label 2、ListView
        _itemListView = root.Q<VisualElement>("ItemList").Q<ListView>("ListView");
        //Load DataBase
        LoadDataBase();
        
        GenerateListView();
    }

    private void LoadDataBase()
    {
        //固定写法 t: 表示Type
        var dataArray =   AssetDatabase.FindAssets("ItemDataList_SO");
        if (dataArray.Length > 1)
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
                if (_itemList[id].ItemIcon.texture != null)
                {
                    element.Q<VisualElement>("Icon").style.backgroundImage = _itemList[id].ItemIcon.texture;
                }
                element.Q<Label>("Name").text = _itemList[id].ItemName == null ? "NO ITEM" : _itemList[id].ItemName;
            }
        };

        _itemListView.fixedItemHeight = 60;
        _itemListView.itemsSource = _itemList;
        _itemListView.makeItem = makeItem;
        _itemListView.bindItem = bindItem;
    }
    
}