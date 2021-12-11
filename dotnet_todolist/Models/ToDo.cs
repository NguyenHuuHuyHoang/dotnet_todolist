using System.ComponentModel.DataAnnotations;

namespace dotnet_todolist.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public int StatusId { get; set; }
        public DateTime UpdateAt { get; set; }

        public DateTime CreateAt { get; set; }
    
        public string Name { get; set; }

        public DateTime EndDate { get; set; }

        public int GroupId { get; set; }
        public int AccountId { get; set; }



    }
}
