using System.Collections.Generic;

namespace Rent_Management_System.Models.TenantModels
{
    public class TenantListModel
    {
        public IEnumerable<TenantItemModel> Tenants { get; set; }
    }
}
