using System;
using System.Collections.Generic;
using System.Text;

namespace CityComparison.Domain.Entites
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}
