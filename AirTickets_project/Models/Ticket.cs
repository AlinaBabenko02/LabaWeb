using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirTickets_project
{
    public partial class Ticket
    {
        public int TicketsId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Рейс")]
        public int? FlightsId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Тип билета")]
        public int? TypesId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Стоимость")]
        public decimal? Cost { get; set; }
        [Display(Name = "Покупатель")]
        public int? ClientsId { get; set; }

        public virtual Client Clients { get; set; }
        public virtual Flight Flights { get; set; }
        public virtual Type Types { get; set; }
    }
}
