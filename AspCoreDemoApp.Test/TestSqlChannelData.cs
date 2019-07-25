using AspCoreDemoApp.Core;
using AspCoreDemoApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreDemoApp.Test
{
    public class TestSqlChannelData : IData<Channel>
    {
        public Channel Add(Channel newItem)
        {
            throw new NotImplementedException();
        }

        public int Commit()
        {
            throw new NotImplementedException();
        }

        public Channel Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Channel GetById(int id)
        {
            return new Channel() { Title = "This is a Channel" };
        }

        public IEnumerable<Channel> GetItems(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Channel Update(Channel updatedItem)
        {
            throw new NotImplementedException();
        }
    }
}
