using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Core.Models
{
        public class Experience
        {
        public string Id { get; set; } = Guid.NewGuid().ToString();


        [ForeignKey("User")]
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string?Title { get; set; }
        public DateOnly ?StartDate { get; set; } 
        public DateOnly? EndDate { get; set; }



        }
}
