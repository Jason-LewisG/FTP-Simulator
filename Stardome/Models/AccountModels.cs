﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
  
namespace Stardome.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel 
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Middle Name")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string MiddleName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Prefix")]
        [StringLength(10, ErrorMessage = "The {0} cannot be empty.")]
        public string Prefix { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Suffix")]
        [StringLength(10, ErrorMessage = "The {0} cannot be empty.")]
        public string Suffix { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string EmailAddress { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "AccountCreatedOn")]
        public DateTime AccountCreatedOn { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "UserId")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public Int32 UserId { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "RoleId")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public Int32 RoleId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Security Answer1")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string SecurityAnswer1 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Security Answer2")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string SecurityAnswer2 { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Security Question1")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string SecurityQuestion1 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Security Question2")]
        [StringLength(100, ErrorMessage = "The {0} cannot be empty.")]
        public string SecurityQuestion2 { get; set; }


    }


}
