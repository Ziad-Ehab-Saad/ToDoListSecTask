using System.ComponentModel.DataAnnotations;

namespace ToDoListSecTask.Models
{
    public class ToDoList
    {
        public int id { get; set; }

        [Required]
        public string Name{ get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DeadLine { get; set; }
        
        public string? File { get; set; }


    }
}
