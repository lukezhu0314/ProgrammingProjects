using System.Collections.Generic;
using QuICR_WebApp.Models.GraphicsModel;

namespace QuICR_WebApp.Models
{
    public class ViewModel
    {
        public List<PolygonGeometry> PolygonGeometries { get; set; }
        public List<Color> FillColors { get; set; }
    }
}