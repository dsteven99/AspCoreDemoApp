using AspCoreDemoApp.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreDemoApp.Data
{
    public  class SqlVideoData :IData<Video>
    {
        private readonly VideoDbContext db;

        public SqlVideoData(VideoDbContext db)
        {
            this.db = db;
        }
        public Video Add(Video newItem)
        {
            db.Videos.Add(newItem);
            return newItem;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Video Delete(int id)
        {
            var item = GetById(id);
            if (item != null)
            {
                db.Videos.Remove(item);
            }

            return item;
        }

        public Video GetById(int id)
        {
            return db.Videos.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Video> GetItems(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return db.Videos.ToList();
            }
            else
            {
                return db.Videos
                    .Where(c => c.Title.Contains(searchTerm) || c.Description.Contains(searchTerm)).ToList();
            }
        }

        public Video Update(Video updatedItem)
        {
            var entity = db.Videos.Attach(updatedItem);
            entity.State = EntityState.Modified;
            return updatedItem;
        }
    }
}
