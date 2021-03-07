using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirTickets_project
{
    public partial class Flight
    {
        public Flight()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int? PlanesId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Аэропорт отправления")]
        public int? PlaceOfDeparture { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Время отправления")]
        public DateTime? DepartureTime { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Аэропорт прибытия")]
        public int? PlaceOfArrival { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Время прибытия")]
        public DateTime? ArrivalTime { get; set; }
        public int FlightsId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Время полета")]
        public TimeSpan? FlightTime { get; set; }
        [Display(Name = "Аэропорт отправления")]
        public virtual Airport PlaceOfDepartureNavigation { get; set; }
        [Display(Name = "Аэропорт прибытия")]
        public virtual Airport PlaceOfArrivalNavigation { get; set; }
        [Display(Name = "Самолет")]
        public virtual Plane Planes { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
