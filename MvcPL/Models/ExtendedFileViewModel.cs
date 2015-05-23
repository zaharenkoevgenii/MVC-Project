using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPL.Models
{
    public class ExtendedFileViewModel : FileViewModel
    {
        public string OwnerName { get; set; }
    }
}
