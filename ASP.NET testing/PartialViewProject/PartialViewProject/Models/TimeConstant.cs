using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PartialViewProject.Models
{
    public class TimeConstant
    {
        [Required(ErrorMessage = "The value needs to be entered")]
        [Range(1, int.MaxValue)]
        public int T_response { get; set; }

        [Range(1, int.MaxValue)]
        public int T_onscene { get; set; }

        [Range(1, int.MaxValue)]
        public int T_DTN_CSC { get; set; }

        [Required(ErrorMessage = "The value needs to be entered")]
        [Range(1, int.MaxValue)]
        public int T_DTN_PSC { get; set; }

        [Range(1, int.MaxValue)]
        public int T_NTDO { get; set; }

        [Range(1, int.MaxValue)]
        public int T_PSC_CSC { get; set; }

        [Required(ErrorMessage = "The value needs to be entered")]
        [Range(1, int.MaxValue)]
        public int T_DTP_MS { get; set; }

        [Range(1, int.MaxValue)]
        public int T_DTP_DnS { get; set; }
    }
}
