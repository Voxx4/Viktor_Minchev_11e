using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserContext : IDB<User, int>
    {
        private SocialMediaDbContext _context;

        public UserContext(SocialMediaDbContext context)
        {
            this._context = context;
        }

        public User Create(User item)
        {
            try
            {
                _context.Users.Add(item);
                _context.SaveChanges();

                //теоретично това ще връща направения нов запис ако имаме нужда от него x2
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
                _context.Users.Remove(Read(key));
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User Read(int key, bool noTracking = false, bool navigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users;

                if (noTracking)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                if (navigationProperties)
                {
                    query = query.Include(user => user.Friends).Include(user => user.Interests);
                }

                return query.SingleOrDefault(area => area.ID == key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<User> Read(int skip, int take, bool navigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users.AsNoTrackingWithIdentityResolution();

                if (navigationProperties)
                {
                    query = query.Include(user => user.Friends).Include(user => user.Interests);
                }

                return query.Skip(skip).Take(take).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<User> ReadAll(bool navigationProperties = false)
        {
            try
            {
                IQueryable<User> query = _context.Users.AsNoTracking();

                if (navigationProperties)
                {
                    query = query.Include(user => user.Friends).Include(user => user.Interests);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User Update(User item, bool navigationProperties = false)
        {
            try
            {
                if (Read(item.ID) == null)
                {
                    return Create(item); //ако няма този запис го създаваме, тоест няма причина да се пипат навигационните свойства x2
                }
                else
                {
                    User userFromDB = _context.Users.Find(item.ID);
                    if (navigationProperties)
                    {
                        // приятелите
                        List<User> friends = new List<User>();
                        foreach (User friend in item.Friends)
                        {
                            User friendFromDB = _context.Users.Find(friend.ID);
                            if (friendFromDB == null)
                            {
                                friends.Add(friend);
                            }
                            else
                            {
                                friends.Add(userFromDB);
                            }
                            friends.Add(friend);
                        }

                        userFromDB.Friends = friends;

                        //интересите
                        List<Interest> interests = new List<Interest>();
                        foreach (Interest interest in item.Interests)
                        {
                            Interest interestFromDB = _context.Interests.Find(interest.ID);
                            if (interestFromDB == null)
                            {
                                interests.Add(interest);
                            }
                            else
                            {
                                interests.Add(interestFromDB);
                            }
                            interests.Add(interest);
                        }

                        userFromDB.Interests = interests;
                    }
                    _context.Entry(userFromDB).CurrentValues.SetValues(item);
                    _context.SaveChanges();

                    return userFromDB;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
