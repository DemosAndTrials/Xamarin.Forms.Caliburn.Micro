using System.Collections.Generic;

namespace Shared.Model
{
    public class ListResponse<T>
    {
        public ICollection<T> Value { get; set; }
    }
}
