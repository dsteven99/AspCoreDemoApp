using AspCoreDemoApp.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreDemoApp.Data
{
    public class SqlChannelData : IData<Channel>
    {
        private readonly VideoDbContext db;

        public SqlChannelData(VideoDbContext db)
        {
            this.db = db;
        }
        public Channel Add(Channel newItem)
        {
            db.Channels.Add(newItem);
            return newItem;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Channel Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                db.Channels.Remove(item);
            }

            return item;
        }

        public Channel GetById(int id)
        {
            return db.Channels.Include(c => c.Videos).SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Channel> GetItems(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return  db.Channels.Include(c => c.Videos).ToList();
            }
            else
            {
                return db.Channels.Include(c => c.Videos)
                    .Where(c => c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm)).ToList();
            }
        }

        public Channel Update(Channel updatedItem)
        {
            var entity = db.Channels.Attach(updatedItem);
            entity.State = EntityState.Modified;
            return updatedItem;
        }
    }
}
