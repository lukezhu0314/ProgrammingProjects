using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Data_Validation.Models
{
    public class StudentModel
    {
        [Required(ErrorMessage = "type something")]
        public Guid StudentId
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public string FirstName
        {
            get;
            set;
        }

        [Required(ErrorMessage = "type something")]
        public string LastName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public DateTime DateOfBirth
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public string Address
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public string ContactNo
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public string EmailId
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public string ConfirmEmail
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public string UserName
        {
            get;
            set;
        }
        [Required(ErrorMessage = "type something")]
        public string Password
        {
            get;
            set;
        }
    }
}
