namespace dotnet_todolist.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public Boolean isCompleted { get; set; }
        public DateTime UpdateAt { get; set; }

        public DateTime CreateAt { get; set; }

        public Boolean SoftDelete { get; set; }

        public String Content { get; set; }


    }
}
