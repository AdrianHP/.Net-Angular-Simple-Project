using PetHealth.Core.Interfaces.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Task : IEntity<int>
    {
        [Key] public int Id { get; set; }

        [Required] public string Title { get; set; }
        public string Description { get; set; }
        [Required] public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

    }
}
