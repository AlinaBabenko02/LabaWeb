using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirTickets_project
{
    public partial class Type
    {
        public Type()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int TypesId { get; set; }
        [Required(ErrorMessage ="Поле не может быть пустым")]
        [Display(Name ="Тип билета")]
        public string TypeName { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
