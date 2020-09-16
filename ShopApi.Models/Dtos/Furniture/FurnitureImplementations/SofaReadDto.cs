namespace ShopApi.Models.Dtos.Furniture.FurnitureImplementations
{
    public class SofaReadDto : FurnitureReadDto
    {
        public bool HasSleepMode { get; set; }
        public int Pillows { get; set; }
    }
}