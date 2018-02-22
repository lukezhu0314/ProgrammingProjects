using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ViewComponentPractice.Models
{
    public class Parameters
    {
        public string SelectedScreeningTool { get; set; }
        public List<SelectListItem> ScreeningTools { get; set; }
        public int DoorInDoorOut { get; set; }
        public int DoortoNeedle { get; set; }
        public int DoortoPuncture { get; set; }
    }

    
}