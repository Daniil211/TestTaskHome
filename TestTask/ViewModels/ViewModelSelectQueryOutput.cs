using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TestTask.ViewModels
{
    public class ViewModelSelectQueryOutput
    {
        public List<Query1Result> Query1 { get; set; }
        public List<Query2Result> Query2 { get; set; }
        public List<Query3Result> Query3 { get; set; }
        public List<Query4Result> Query4 { get; set; }
        public List<Query5Result> Query5 { get; set; }
        public List<Query6Result> Query6 { get; set; }
        public List<Query7Result> Query7 { get; set; }
        public List<Query8Result> Query8 { get; set; }

        public ViewModelSelectQueryOutput()
        {
            string connectionString = "Data Source=DESKTOP-5MDQV6C;Initial Catalog=TestTask;Integrated Security=True";

            Query1 = new List<Query1Result>();
            Query2 = new List<Query2Result>();
            Query3 = new List<Query3Result>();
            Query4 = new List<Query4Result>();
            Query5 = new List<Query5Result>();
            Query6 = new List<Query6Result>();
            Query7 = new List<Query7Result>();
            Query8 = new List<Query8Result>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                #region 1.	Вывести менеджеров у которых имеется номер телефона
                SqlCommand command1 = new SqlCommand("SELECT FIO FROM Managers WHERE Phone IS NOT NULL", connection);
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    Query1.Add(new Query1Result { FIO = reader1.GetString(0) });
                }
                reader1.Close();
                #endregion

                #region 2.	Вывести кол-во продаж за 20 июня 2021
                SqlCommand command2 = new SqlCommand("SELECT COUNT(*) FROM Sells WHERE Date = CONVERT(datetime, '2021-06-20', 120)", connection);
                Query2Result result2 = new Query2Result();
                result2.Count = (int)command2.ExecuteScalar();
                Query2.Add(result2);
                #endregion

                #region 3.	Вывести среднюю сумму продажи с товаром 'Фанера'
                SqlCommand command3 = new SqlCommand("SELECT AVG(Sum) FROM Sells WHERE EXISTS (SELECT ID FROM Products WHERE Name = 'Фанера' AND ID = Sells.ID_Products)", connection);
                Query3Result result3 = new Query3Result();
                result3.AVGSum = (decimal)command3.ExecuteScalar();
                Query3.Add(result3);
                #endregion

                #region 4.	Вывести фамилии менеджеров и общую сумму продаж для каждого с товаром 'ОСБ'
                SqlCommand command4 = new SqlCommand("SELECT Managers.FIO, SUM(Sells.Sum) FROM Sells JOIN Managers ON Sells.ID_Manager = Managers.ID WHERE Sells.ID_Products IN (SELECT ID FROM Products WHERE Name = 'ОСБ') GROUP BY Managers.FIO", connection);
                SqlDataReader reader4 = command4.ExecuteReader();
                while (reader4.Read())
                {
                    Query4.Add(new Query4Result { LastName = reader4.GetString(0), TotalSum = reader4.GetDecimal(1) });
                }
                reader4.Close();
                #endregion

                #region 5.	Вывести менеджера и товар, который продали 22 августа 2021
                SqlCommand command5 = new SqlCommand("SELECT Managers.FIO, Products.Name FROM Sells JOIN Managers ON Sells.ID_Manager = Managers.ID JOIN Products ON Sells.ID_Products = Products.ID WHERE Sells.Date = '2021-08-22'", connection);
                SqlDataReader reader5 = command5.ExecuteReader();
                while (reader5.Read())
                {
                    Query5.Add(new Query5Result { FIO = reader5.GetString(0), ProductName = reader5.GetString(1) });
                }
                reader5.Close();
                #endregion

                #region 6.	Вывести все товары, у которых в названии имеется 'Фанера' и цена не ниже 1750
                SqlCommand command6 = new SqlCommand("SELECT * FROM Products WHERE Name LIKE '%Фанера%' AND Cost >= 1750", connection);
                SqlDataReader reader6 = command6.ExecuteReader();
                while (reader6.Read())
                {
                    Query6.Add(new Query6Result { ID = reader6.GetInt32(0), Name = reader6.GetString(1), Price = reader6.GetDecimal(2) });
                }
                reader6.Close();
                #endregion

                #region 7.	Вывести историю продаж товаров, группируя по месяцу продажи и наименованию товара
                SqlCommand command7 = new SqlCommand("SELECT Products.Name, MONTH(Sells.Date) AS Month, SUM(Sells.Count) AS TotalCount, SUM(Sells.Sum) AS TotalSum FROM Sells JOIN Products ON Sells.ID_Products = Products.ID GROUP BY Products.Name, MONTH(Sells.Date)", connection);
                SqlDataReader reader7 = command7.ExecuteReader();
                while (reader7.Read())
                {
                    Query7.Add(new Query7Result { ProductName = reader7.GetString(0), Month = reader7.GetInt32(1), TotalCount = reader7.GetInt32(2), TotalSum = reader7.GetDecimal(3) });
                }
                reader7.Close();
                #endregion

                #region 8.	Вывести количество повторяющихся значений и сами значения из таблицы 'Товары', где количество повторений больше 1.
                SqlCommand command8 = new SqlCommand("SELECT Name, COUNT(*) AS Count FROM Products GROUP BY Name HAVING COUNT(*) > 1", connection);
                SqlDataReader reader8 = command8.ExecuteReader();
                while (reader8.Read())
                {
                    Query8.Add(new Query8Result { Name = reader8.GetString(0), Count = reader8.GetInt32(1) });
                }
                reader8.Close();
                #endregion
            }
        }
    }
    public class Query1Result
    {
        public string FIO { get; set; }
    }

    public class Query2Result
    {
        public int Count { get; set; }
    }

    public class Query3Result
    {
        public decimal AVGSum { get; set; }
    }

    public class Query4Result
    {
        public string LastName { get; set; }
        public decimal TotalSum { get; set; }
    }

    public class Query5Result
    {
        public string FIO { get; set; }
        public string ProductName { get; set; }
    }

    public class Query6Result
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class Query7Result
    {
        public string ProductName { get; set; }
        public int Month { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalSum { get; set; }
    }
    public class Query8Result
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}


