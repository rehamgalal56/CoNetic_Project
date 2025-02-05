using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Core.ReposInterfaces
{
    public interface IGenericRepo<T>
    {
        T Get(string UserId);
        void insert(T item);
        void update(T item);
        void save();
    }
}
