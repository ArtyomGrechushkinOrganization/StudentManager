namespace Database.Models
{
    public class FindWord
    {
        public string Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
