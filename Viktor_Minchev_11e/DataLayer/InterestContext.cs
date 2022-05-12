using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class InterestContext : IDB<Interest, int>
    {
        private SocialMediaDbContext _context;

        public InterestContext(SocialMediaDbContext context)
        {
            this._context = context;
        }

        public Interest Create(Interest item)
        {
            try
            {
                _context.Interests.Add(item);
                _context.SaveChanges();

                //теоретично това ще връща направения нов запис ако имаме нужда от него 
                return Read(item.ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int key)
        {
            try
            {
                _context.Interests.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // пише само 2 така че правя другите две
        public Interest Read(int key, bool noTracking = false, bool navigationProperties = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Interest> Read(int skip, int take, bool navigationProperties = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Interest> ReadAll(bool navigationProperties = false)
        {
            throw new NotImplementedException();
        }

        public Interest Update(Interest item, bool navigationProperties = false)
        {
            throw new NotImplementedException();
        }
    }
}
