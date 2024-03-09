using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Task : IEntity<int>
    {
        [Key][Column("TaskId")] public int Id { get; set; }

        [Required]public string AssignedUserId { get; set; }
        [Required] public string Title { get; set; }
        public string Description { get; set; }
        [Required] public DateTime DueDate { get; set; }
        [Required]public bool IsCompleted { get; set; }
        public User AssignedUser { get; set; }
    }
}
