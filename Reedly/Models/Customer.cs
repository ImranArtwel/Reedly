using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reedly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Min18YrsIfMember]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage ="Please enter customer name.")] //means Name column is no longer nullable
        [StringLength(255)] //specify number of characters
        public string Name { get; set; }

        [Display(Name ="Subscribe To Newsletter")]
        public bool isSubscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; } //navigation property

         [Display(Name ="Membership Type")]
        public byte MembershipTypeId { get; set; } //foreign key

       
    }
}