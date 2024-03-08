using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IDTO<TKey> : IBaseDTO where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
