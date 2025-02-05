using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Core.Models
{
    public class Skill
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("User")]
        public string UserId {  get; set; }
       
        public string? Name { get; set; }

    }
}
