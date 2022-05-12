using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDB<T, K>
    {
        T Create(T item);

        T Read(K key, bool noTracking = false, bool navigationProperties = false);

        IEnumerable<T> Read(int skip, int take, bool navigationProperties = false);

        IEnumerable<T> ReadAll(bool navigationProperties = false);

        T Update(T item, bool navigationProperties = false);

        void Delete(K key);
    }
}
