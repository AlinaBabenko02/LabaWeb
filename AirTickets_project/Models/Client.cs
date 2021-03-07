using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AirTickets_project
{
    public partial class Client
    {
        public Client()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int ClientsId { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name="Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Номер телефона")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Паспорт")]
        public string Passport { get; set; }
        [Required(ErrorMessage = "Поле не может быть пустым")]
        [Display(Name = "Дата рождения")]
        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
