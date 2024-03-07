using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetHealth.Core.Interfaces.CoreInterfaces
{
    public interface IDTO<TKey> : IBaseDTO where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
