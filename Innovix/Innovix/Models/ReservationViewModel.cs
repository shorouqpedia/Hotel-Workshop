using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Models
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "The Name is required")]
        [DisplayName("Name *")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The Email is required")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email *")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The Country is required")]
        [DisplayName("Country *")]
        public string Country { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Check-In Date *")]
        [Required(ErrorMessage = "The Check In Date is required")]
        public DateTime CheckIn { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Check-Out Date *")]
        [Required(ErrorMessage = "The Check Out Date is required")]
        public DateTime CheckOut { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of adults must be greater than {1}.")]
        [DisplayName("Number Of Adults *")]
        public int Adults { get; set; }
        [DisplayName("Number Of Children")]
        public int Children { get; set; }
        [Required(ErrorMessage = "The Room Type is required")]
        [DisplayName("Room Type *")]
        public string RoomTypeId { get; set; }
        public List<SelectListItem> RoomType { get; set; }
        [Required(ErrorMessage = "The Meal Plan is required")]
        [DisplayName("Meal Plan *")]
        public string MealPlanId { get; set; }
        public List<SelectListItem> MealPlan { get; set; }

    }
}
