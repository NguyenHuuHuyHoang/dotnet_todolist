namespace dotnet_todolist.Models
{
    public class ThongTinThemMoiTodo
    {
        public int StatusId { get; set; }
     
        public string Name { get; set; }

        public DateTime EndDate { get; set; }

        public int GroupId { get; set; }
        
    }
}
