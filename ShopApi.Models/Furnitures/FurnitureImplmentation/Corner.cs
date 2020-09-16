namespace ShopApi.Models.Furnitures.FurnitureImplmentation
{
    public class Corner : Furniture
    {
        public override string Type { get; } = "Corner";
        public bool HaveSleepMode { get; set; }
        public bool HaveHeadrests { get; set; }
    }
}