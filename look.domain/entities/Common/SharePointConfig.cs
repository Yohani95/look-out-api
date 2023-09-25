using look.domain.interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.Common
{
    public class SharePointConfig : ISharePointConfig
    {
        public string? AzureSharePointUrl { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? LibraryName { get; set; }
        public string? RelativeFilePath { get; set; }

    }
}
