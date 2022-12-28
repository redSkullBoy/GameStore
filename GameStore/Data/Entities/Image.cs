namespace GameStore.Data.Entities
{
    public class Image
    {
        public int ImageID { get; set; }
        public byte[]? Content { get; set; }

        public Product? Product { get; set; }
    }
}
