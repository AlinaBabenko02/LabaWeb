using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirTickets_project
{
    public partial class Airport
    {
        public Airport()
        {
            FlightPlaceOfArrivalNavigations = new HashSet<Flight>();
            FlightPlaceOfDepartureNavigations = new HashSet<Flight>();
        }

        public int AirportsId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Город")]
        public string City { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Адресс")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Название аэропорта")]
        public string NameOfAirport { get; set; }

        public virtual ICollection<Flight> FlightPlaceOfArrivalNavigations { get; set; }
        public virtual ICollection<Flight> FlightPlaceOfDepartureNavigations { get; set; }
    }
}
