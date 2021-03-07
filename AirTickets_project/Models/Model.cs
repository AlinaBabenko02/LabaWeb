using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirTickets_project
{
    public partial class Model
    {
        public Model()
        {
            Planes = new HashSet<Plane>();
        }

        public int ModelsId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Фирма")]
        public string Firm { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Количество мест")]
        public int? NumberOfSeats { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Название модели")]
        public string ModelName { get; set; }

        public virtual ICollection<Plane> Planes { get; set; }
    }
}
