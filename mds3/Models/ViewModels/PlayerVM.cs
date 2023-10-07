using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace mds3.Models.ViewModels
{
    public class PlayerVM
    {
        public PlayerVM()
        {
            this.FormatList = new List<int>();
        }
        public int PlayerId { get; set; }
        [Required(ErrorMessage = "Enter Player Name"), StringLength(200), Display(Name = "Player Name")]

        public string PlayerName { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Birth Of Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public System.DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Picture")]

        public HttpPostedFileBase PicturePath { get; set; }
        [Display(Name = "Maritual Status")]

        public bool MaritualStatus { get; set; }
        public List<int> FormatList { get; set; }
    }
}