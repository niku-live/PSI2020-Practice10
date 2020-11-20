using System;
using System.Data.SqlClient;

namespace DbDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection("Server=localhost;Database=demo;Trusted_Connection=True;"))
            {
                connection.Open();

                var command = new SqlCommand("SELECT TOP 1 [Name] FROM [Employees]", connection);
                var result = command.ExecuteScalar();
                Console.WriteLine($"{result}");

                command = new SqlCommand("SELECT * FROM [Employees]", connection);             
                var reader = command.ExecuteReader();
                Console.WriteLine($"Column count: {reader.FieldCount}, visible: {reader.VisibleFieldCount}");
                for (int colNo = 0; colNo < reader.FieldCount; colNo++)
                {                    
                    Console.WriteLine($"{reader.GetName(colNo)} {reader.GetFieldType(colNo)}");
                }
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader["Name"]} {reader[2]}");
                }
                reader.Close();

                string name = "A";
                string lastName = "B');DELETE FROM [Employees];--";

                //Bad example with SQL injection
                var insertSql = $"INSERT INTO [dbo].[Employees]([Name],[LastName]) VALUES ('{name}','{lastName}')";
                insertSql = "INSERT INTO [dbo].[Employees]([Name],[LastName]) VALUES ('" + name + "','" + lastName + "')";
                //INSERT INTO [dbo].[Employees]([Name],[LastName]) VALUES ('A','B');DELETE FROM [Employees];--')

                //Avoiding SQL injection
                insertSql = $"INSERT INTO [dbo].[Employees]([Name],[LastName]) VALUES (@name,@lastName)";
                command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.ExecuteNonQuery();
            }
        }
    }
}
