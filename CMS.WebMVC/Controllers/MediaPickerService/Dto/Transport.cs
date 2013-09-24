using System.Collections.Generic;

namespace CMS.WebMVC.Controllers.Dto
{
    public class Transport
    {
        public IEnumerable<Breadcrumb> Breadcrumbs { get; set; }
        public IEnumerable<Media> Mediae { get; set; }
    }
}
