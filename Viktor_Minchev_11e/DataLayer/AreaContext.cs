using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AreaContext : IDB<Area, int>
    {
        private SocialMediaDbContext _context;

        public AreaContext(SocialMediaDbContext context)
        {
            this._context = context;
        }

        public Area Create(Area item)
        {
            try 
            { 
                _context.Areas.Add(item);
                _context.SaveChanges();

                //теоретично това ще връща направения нов запис ако имаме нужда от него x1
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
                _context.Areas.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Area Read(int key, bool noTracking = false, bool navigationProperties = false)
        {
            try
            {
                IQueryable<Area> query = _context.Areas;

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (navigationProperties)
                {
                    query = query.Include(area => area.Users);
                }

                return query.SingleOrDefault(area => area.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Area> Read(int skip, int take, bool navigationProperties = false)
        {
            try
            {
                IQueryable<Area> query = _context.Areas.AsNoTrackingWithIdentityResolution();

                if (navigationProperties)
                {
                    query = query.Include(area => area.Users);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Area> ReadAll(bool navigationProperties = false)
        {
            try
            {
                IQueryable<Area> query = _context.Areas.AsNoTracking();

                if (navigationProperties)
                {
                    query = query.Include(area => area.Users);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Area Update(Area item, bool navigationProperties = false)
        {
            try
            {
                if (Read(item.ID) == null)
                {
                    return Create(item); //ако няма този запис го създаваме, тоест няма причина да се пипат навигационните свойства x1
                }
                else
                {
                    Area areaFromDB = _context.Areas.Find(item.ID);
                    if (navigationProperties)
                    {
                        List<User> users = new List<User>();
                        foreach(User user in item.Users)
                        {
                            User userFromDB = _context.Users.Find(user.ID);
                            if (userFromDB == null)
                            {
                                users.Add(user);
                            }
                            else
                            {
                                users.Add(userFromDB);
                            }
                            users.Add(user);
                        }

                        areaFromDB.Users = users;
                    }
                    _context.Entry(areaFromDB).CurrentValues.SetValues(item);
                    _context.SaveChanges();

                    return areaFromDB;
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
