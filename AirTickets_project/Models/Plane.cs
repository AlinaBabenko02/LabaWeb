using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirTickets_project
{
    public partial class Plane
    {
        public Plane()
        {
            Flights = new HashSet<Flight>();
        }

        public int PlanesId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Модель самолета")]
        public int? ModelsId { get; set; }

        public virtual Model Models { get; set; }
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
