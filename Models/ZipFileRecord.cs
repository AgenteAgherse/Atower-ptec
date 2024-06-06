namespace PruebaTecnica.Models
{
    public class ZipFileRecord
    {
        public int Id { get; set; }
        public string Archivo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
