using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Input;

namespace TestTask.ViewModels
{
    public class ViewModelSelectQueryOutput : INotifyPropertyChanged
    {
        private List<Query1Result> _query1;
        private List<Query2Result> _query2;
        private List<Query3Result> _query3;
        private List<Query4Result> _query4;
        private List<Query5Result> _query5;
        private List<Query6Result> _query6;
        private List<Query7Result> _query7;
        private List<Query8Result> _query8;

        public List<Query1Result> Query1
        {
            get { return _query1; }
            set
            {
                _query1 = value;
                OnPropertyChanged("Query1");
            }
        }

        public List<Query2Result> Query2
        {
            get { return _query2; }
            set
            {
                _query2 = value;
                OnPropertyChanged("Query2");
            }
        }

        public List<Query3Result> Query3
        {
            get { return _query3; }
            set
            {
                _query3 = value;
                OnPropertyChanged("Query3");
            }
        }

        public List<Query4Result> Query4
        {
            get { return _query4; }
            set
            {
                _query4 = value;
                OnPropertyChanged("Query4");
            }
        }

        public List<Query5Result> Query5
        {
            get { return _query5; }
            set
            {
                _query5 = value;
                OnPropertyChanged("Query5");
            }
        }

        public List<Query6Result> Query6
        {
            get { return _query6; }
            set
            {
                _query6 = value;
                OnPropertyChanged("Query6");
            }
        }

        public List<Query7Result> Query7
        {
            get { return _query7; }
            set
            {
                _query7 = value;
                OnPropertyChanged("Query7");
            }
        }

        public List<Query8Result> Query8
        {
            get { return _query8; }
            set
            {
                _query8 = value;
                OnPropertyChanged("Query8");
            }
        }

        public ICommand NavigationCommand { get; set; }

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
                    Query4.Add(new Query4Result { FIO = reader4.GetString(0), TotalSum = reader4.GetDecimal(1) });
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
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Navigate(object parameter)
        {
            // логика навигации
        }
    }
}


