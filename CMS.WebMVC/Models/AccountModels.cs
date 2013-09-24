
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CMS.WebMVC
{

    public class ChangePasswordModel
    {
        private string oldPasswordValue;
        private string newPasswordValue;

        private string confirmPasswordValue;
        [Required()]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword
        {
            get { return oldPasswordValue; }
            set { oldPasswordValue = value; }
        }

        [Required()]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword
        {
            get { return newPasswordValue; }
            set { newPasswordValue = value; }
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get { return confirmPasswordValue; }
            set { confirmPasswordValue = value; }
        }
    }

    public class LogOnModel
    {
        private string userNameValue;
        private string passwordValue;

        private bool rememberMeValue;
        [Required()]
        [Display(Name = "Alias")]
        public string UserName
        {
            get { return userNameValue; }
            set { userNameValue = value; }
        }

        [Required()]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get { return passwordValue; }
            set { passwordValue = value; }
        }

        [Display(Name = "Remember me?")]
        public bool RememberMe
        {
            get { return rememberMeValue; }
            set { rememberMeValue = value; }
        }
    }

    public class RegisterModel
    {
        private string userNameValue;
        private string passwordValue;
        private string confirmPasswordValue;

        private string emailValue;
        [Required()]
        [Display(Name = "Alias")]
        public string UserName
        {
            get { return userNameValue; }
            set { userNameValue = value; }
        }

        //[Required()]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Email address")]
        //public string Email
        //{
        //    get { return emailValue; }
        //    set { emailValue = value; }
        //}

        [Required()]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password
        {
            get { return passwordValue; }
            set { passwordValue = value; }
        }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get { return confirmPasswordValue; }
            set { confirmPasswordValue = value; }
        }
    }
}