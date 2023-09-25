using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.Common
{
    public interface ISharePointConfig
    {
        string? AzureSharePointUrl { get; }
        string? ClientId { get; }
        string? ClientSecret { get; }
        string? LibraryName { get; }
        string? RelativeFilePath { get; }
    }
}
