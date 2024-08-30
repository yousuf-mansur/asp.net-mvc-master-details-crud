using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArtistManagement.Models.ViewModels
{
    public class ArtistViewModel
    {
        public ArtistViewModel()
        {
            RoleList = new List<int>();
        }

        public int ArtistID { get; set; }

        [Required, Display(Name = "Artist Name")]
        public string ArtistName { get; set; }

        [Required, Display(Name = "Birth Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        
        public int Age => (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);

        [Required, Display(Name = "Is Married?")]
        public bool MaritalStatus { get; set; }

        [Required(ErrorMessage = "Email address is required")]        
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]        
        public string MobileNo { get; set; }  

        public string Picture { get; set; }

        [Required, Display(Name = "Picture")]
        public HttpPostedFileBase PictureFile { get; set; }

        public List<int> RoleList { get; set; }
    }

}