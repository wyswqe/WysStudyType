using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;
using NHibernate;
using NHibernate.Criterion;

namespace MyGameServer.Manager
{
    class UserManager : IUserManager
    {
        public void Add(User user)
        {
            //ISession session = NHibernateHelper.OpenSession();
            //session.Save(user);
            //session.Close();

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transation = session.BeginTransaction())
                {
                    session.Save(user);
                    transation.Commit();
                }
            }
        }
        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transation = session.BeginTransaction())
                {
                    session.Update(user);
                    transation.Commit();
                }
            }
        }
        public void Remove(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transation = session.BeginTransaction())
                {
                    session.Delete(user);
                    transation.Commit();
                }
            }
        }
        public User GetById(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transation = session.BeginTransaction())
                {
                    User user = session.Get<User>(id);
                    transation.Commit();
                    return user;
                }
            }
        }

        public User GetByUsername(string username)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //ICriteria criteria = session.CreateCriteria(typeof(User));
                //criteria.Add(Restrictions.Eq("UserName", username));
                //User user = criteria.UniqueResult<User>();
                User user = session.CreateCriteria(typeof(User)).Add(Restrictions.Eq("Username", username)).UniqueResult<User>();
                return user;
            }
        }
        public ICollection<User> GetAllUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //ICriteria criteria = session.CreateCriteria(typeof(User));
                //criteria.Add(Restrictions.Eq("UserName", username));
                //User user = criteria.UniqueResult<User>();
                IList<User> user = session.CreateCriteria(typeof(User)).List<User>();
                return user;
            }
        }

        public bool VerifyUser(string username, string password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //ICriteria criteria = session.CreateCriteria(typeof(User));
                //criteria.Add(Restrictions.Eq("UserName", username));
                //User user = criteria.UniqueResult<User>();
                User user = session
                    .CreateCriteria(typeof(User))
                    .Add(Restrictions.Eq("Username", username))
                    .Add(Restrictions.Eq("Password", password))
                    .UniqueResult<User>();
                if (user == null) return false;
                return true;
            }
        }
    }
}
