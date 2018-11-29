using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using SikiNHibernate.Model;
using SikiNHibernate.Manager;

namespace SikiNHibernate
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 原始
            //var configuation = new Configuration();
            //configuation.Configure();//解析nhibernate.cfg.xml
            //configuation.AddAssembly("SikiNHibernate");//解析映射文件 User.hbm.xml....

            //ISessionFactory sessionFactory = configuation.BuildSessionFactory();
            //ISession session = null;
            //ITransaction transaction = null;
            //try
            //{
            //    sessionFactory = configuation.BuildSessionFactory();

            //    session = sessionFactory.OpenSession();//打开一个跟数据库的会话


            //    //事务
            //    transaction = session.BeginTransaction();//开启事务
            //    //进行操作
            //    User user1 = new User() { Username = "444555avc", Password = "23414" };
            //    User user2 = new User() { Username = "444555avc", Password = "23414" };
            //    session.Save(user1);
            //    session.Save(user2);
            //    transaction.Commit();//提交

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //}
            //finally
            //{
            //    if (transaction != null)
            //    {
            //        transaction.Dispose();
            //    }
            //    if (session != null)
            //    {
            //        session.Close();
            //    }
            //    if (sessionFactory != null)
            //    {
            //        sessionFactory.Close();
            //    }
            //}
            //User user = new User() { Id = 10 }; 
            #endregion
            IUserManager userManager = new UserManager();
            //userManager.Add(user);
            //userManager.Update(user);
            //userManager.Remove(user);
            //User user = userManager.GetById(19);
            //User user = userManager.GetByUsername("wer");
            //Console.WriteLine(user.Username);
            //Console.WriteLine(user.Password);
            //ICollection<User> users = userManager.GetAllUsers();
            //foreach (var u in users)
            //{
            //    Console.WriteLine(u.Username + " " + u.Password);
            //}
            #region 验证用户
            //Console.WriteLine(userManager.VerifyUser("wer", "wer"));
            //Console.WriteLine(userManager.VerifyUser("wer2", "wer"));
            #endregion

            Console.ReadLine();
        }
    }
}
