using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace CSharp直接连接MySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            //Read();
            //Insert();
            //Update();
            //Delete();
            //ReadUsersCount();
            //ExcuteScalar();
            Console.WriteLine(VerifyUser("sikiedu2", "123"));
            Console.ReadKey();
        }

        static bool VerifyUser(string username, string password)
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有链接数据库
            try
            {
                conn.Open();//建立连接
                //string sql = "select * from users where username = '" + username + "' and password = '" + password + "'";
                string sql = "select * from users where username = @username and password = @password";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
                Console.WriteLine("已经建立连接");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();//关闭连接
            }
            return false;
        }

        static void Read()
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有链接数据库
            try
            {
                conn.Open();//建立连接
                string sql = "select * from users";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.ExecuteReader();//执行一些查询
                //cmd.ExecuteNonQuery();//插入 删除
                //cmd.ExecuteScalar();//执行一些查询，返回一个单个的值
                MySqlDataReader reader = cmd.ExecuteReader();
                //reader.Read();
                //Console.WriteLine(reader[0].ToString() + reader[1].ToString() + reader[2].ToString());
                while (reader.Read())
                {
                    //Console.WriteLine(reader[0].ToString() + reader[1] + reader[2]);
                    //Console.WriteLine(reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
                    Console.WriteLine(reader.GetInt32("id") + " " + reader.GetString("username") + " " + reader.GetString("password"));
                }

                Console.WriteLine("已经建立连接");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();//关闭连接
            }
            Console.ReadKey();
        }

        static void Insert()
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有链接数据库
            try
            {
                conn.Open();//建立连接
                //string sql = "insert into users(username,password) values('awdasd','123')";
                //string sql = "insert into users(username,password,registerdate) values('wwws','123','2014-01-09')";
                string sql = "insert into users(username,password,registerdate) values('csdFu','234','" + DateTime.Now + "')";
                Console.WriteLine(DateTime.Now);

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int resule = cmd.ExecuteNonQuery();//返回值是数据库中受影响的行数

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();//关闭连接
            }
        }

        static void Update()
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有链接数据库
            try
            {
                conn.Open();//建立连接
                //string sql = "insert into users(username,password) values('awdasd','123')";
                //string sql = "insert into users(username,password,registerdate) values('wwws','123','2014-01-09')";
                string sql = "update users set username='sdfeswes',password='22222' where id = 4;";
                Console.WriteLine(sql);

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int resule = cmd.ExecuteNonQuery();//返回值是数据库中受影响的行数

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();//关闭连接
            }
        }

        static void Delete()
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有链接数据库
            try
            {
                conn.Open();//建立连接
                //string sql = "insert into users(username,password) values('awdasd','123')";
                //string sql = "insert into users(username,password,registerdate) values('wwws','123','2014-01-09')";
                string sql = "delete from users where id = 4;";
                Console.WriteLine(sql);

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int resule = cmd.ExecuteNonQuery();//返回值是数据库中受影响的行数

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();//关闭连接
            }
        }

        static void ReadUsersCount()
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有链接数据库
            try
            {
                conn.Open();//建立连接
                //string sql = "insert into users(username,password) values('awdasd','123')";
                //string sql = "insert into users(username,password,registerdate) values('wwws','123','2014-01-09')";
                string sql = "select count(*) from users";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                int count = Convert.ToInt32(reader[0].ToString());
                Console.WriteLine(count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();//关闭连接
            }
        }

        static void ExcuteScalar()
        {
            string connectStr = "server=127.0.0.1;port=3306;database=mygamedb;user=root;password=root;";
            MySqlConnection conn = new MySqlConnection(connectStr);//并没有链接数据库
            try
            {
                conn.Open();//建立连接
                //string sql = "insert into users(username,password) values('awdasd','123')";
                //string sql = "insert into users(username,password,registerdate) values('wwws','123','2014-01-09')";
                string sql = "select count(*) from users";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                object o = cmd.ExecuteScalar();
                int count = Convert.ToInt32(o.ToString());
                Console.WriteLine(count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conn.Close();//关闭连接
            }
        }
    }
}
