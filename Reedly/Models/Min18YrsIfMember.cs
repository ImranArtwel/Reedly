using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reedly.Models
{
    public class Min18YrsIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //check membership type
            var customer = (Customer)validationContext.ObjectInstance;
            if(customer.MembershipTypeId == MembershipType.Unknown || customer.MembershipTypeId == MembershipType.PayAsYouGo)
            {
                return ValidationResult.Success;
            }

            //check if customer birthdate is null
            if (customer.Birthday == null)
                return new ValidationResult("Birthdate is required");

            var age = DateTime.Today.Year - customer.Birthday.Value.Year;
            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("Customer must be above 18 years old");
        }
    }
}