using System.Collections.Generic;
using FarmGame;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class ItemEditor : EditorWindow
{
    private ItemDataList_SO _dataBase;
    private List<ItemDetails> _itemDetailsList = new List<ItemDetails>();

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
        
        //Load DataBase
        LoadDataBase();
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

        _itemDetailsList = _dataBase.ItemDetailsList;
        //标记 不标记则无法保存数据
        EditorUtility.SetDirty(_dataBase);

        // #if UNITY_EDITOR
        // foreach (var itemDetails in _itemDetailsList)
        // {
        //     Debug.Log(itemDetails.ItemID);
        // }
        // #endif
    }
}