namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations.Sofa
{
    public class SofaReadDto : FurnitureReadDto
    {
        public bool HasSleepMode { get; set; }
        public int Pillows { get; set; }
    }
}