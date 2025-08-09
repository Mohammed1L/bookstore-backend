namespace BookStoreBackend.Dtos
{
    public class BookDto
    {

        public long Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? ISBN { get; set; }
        public int Inventory { get; set; }
        
        
        
    }
}