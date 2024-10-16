namespace TestApplication.DTOs
{
    public class CreateNoteDTO
    {
        public required string text { get; set; }
        public int? status { get; set; }
    }
}
