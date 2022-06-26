namespace FarmGame
{
    public enum ItemType
    {

        /// <summary>
        /// 种子
        /// </summary>
        Seed,

        /// <summary>
        /// 商品
        /// </summary>
        Commodity,

        /// <summary>
        /// 家具
        /// </summary>
        Furniture,

        /// <summary>
        /// 锄地的
        /// </summary>
        HoeTool,

        /// <summary>
        /// 砍树的
        /// </summary>
        ChopTool,

        /// <summary>
        /// 砸石头
        /// </summary>
        BreakTool,

        /// <summary>
        /// 除草
        /// </summary>
        ReapTool,

        /// <summary>
        /// 浇水的
        /// </summary>
        WaterTool,

        /// <summary>
        /// 菜篮子
        /// </summary>
        CollectTool,

        /// <summary>
        /// 杂草
        /// </summary>
        ReapableScenery,
    }

    public enum SlotType
    {
        Bag,
        Box,
        Shop,
    }

    public enum InventoryLocation
    {
        Player,Box
    }

    public enum PartType
    {
        None,
        Carry,
        Hoe,
        Break,
    }

    public enum PartName
    {
        Body,Hair,Arm,Tool
    }

    public enum Season
    {
        Spring,
        Summer,
        Autumn,
        Winter,
    }
}