using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreDemoApp.Data
{
    public  interface IData<T>
    {
        IEnumerable<T> GetItems(string searchTerm);
        T GetById(int id);
        T GetByCode(string code);
        T Update(T updatedItem);
        T Add(T newItem);
        T Delete(int id);
        int Commit();
    }
}
