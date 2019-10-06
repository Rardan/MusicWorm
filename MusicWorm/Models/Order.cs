using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public DateTime OrderDate { get; set; }


        public StoreUser User { get; set; }

        [Required(ErrorMessage = "Enter your first name")]
        [Display(Name = "First name")]
        [StringLength(50, ErrorMessage = "Too long")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter your last name")]
        [Display(Name = "Last name")]
        [StringLength(50, ErrorMessage = "Too long")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter your address")]
        [StringLength(100, ErrorMessage = "Too long")]
        [Display(Name = "Address Line 1")]
        public string AdressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AdressLine2 { get; set; }

        [Required(ErrorMessage = "Enter your zip code")]
        [Display(Name = "Zip code")]
        [StringLength(5, MinimumLength = 5)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Enter your city")]
        [StringLength(50, ErrorMessage = "Too long")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter your country")]
        [StringLength(50)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Enter your phone number")]
        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter your email address")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public decimal OrderTotal { get; set; }

        public string Condidtion { get; set; }
    }
}
