namespace TodoApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Judul { get; set; } // Tambahkan '?'
        public string? Description { get; set; } // Tambahkan '?'
    }
}
