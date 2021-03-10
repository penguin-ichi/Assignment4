using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAs4
{
    public class BookDAO
    {
        string ConnectionString = @"server=DESKTOP-DLMULPB;database=BookStore;uid=sa;pwd=tam";
        public List<BookDTO> GetListBook(string BookName)
        {
            List<BookDTO> listBook = null;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                if(connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string SQL = @"SELECT BookID, BookName, BookPrice FROM Books WHERE BookName LIKE @name";
                SqlCommand command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("name", "%" + BookName + "%");

                SqlDataReader reader = command.ExecuteReader();
                listBook = new List<BookDTO>();

                while (reader.Read())
                {
                    float price = Convert.ToSingle(reader.GetDouble(2));
                    listBook.Add(new BookDTO(reader.GetInt32(0), reader.GetString(1), price));
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                connection.Close();
            }

            return listBook;
        }

        public void InsertBook(BookDTO dto)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();

                string SQL = @"INSERT INTO Books VALUES(@id, @name, @price)";
                SqlCommand command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@id", dto.BookID);
                command.Parameters.AddWithValue("@name", dto.BookName);
                command.Parameters.AddWithValue("@price", dto.BookPrice);

                command.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool DeleteBook(int BookID)
        {
            bool check;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string SQL = @"DELETE Books WHERE BookID = @id";
                SqlCommand command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@id", BookID);

                check = command.ExecuteNonQuery() > 0;
            }
            catch (SqlException se)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return check;
        }

        public bool UpdateBook(BookDTO dto)
        {
            bool check;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string SQL = @"UPDATE Books SET BookName=@name,BookPrice=@price WHERE BookID=@id";
                SqlCommand command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("@id", dto.BookID);
                command.Parameters.AddWithValue("@name", dto.BookName);
                command.Parameters.AddWithValue("@price", dto.BookPrice);

                check = command.ExecuteNonQuery() > 0;
            }
            catch (SqlException se)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
            return check;
        }

        public List<BookDTO> GetListBookDesc()
        {
            List<BookDTO> listBook = null;
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string SQL = @"SELECT BookID, BookName, BookPrice FROM Books ORDER BY BookPrice DESC";
                SqlCommand command = new SqlCommand(SQL, connection);

                SqlDataReader reader = command.ExecuteReader();
                listBook = new List<BookDTO>();

                while (reader.Read())
                {
                    float price = Convert.ToSingle(reader.GetDouble(2));
                    listBook.Add(new BookDTO(reader.GetInt32(0), reader.GetString(1), price));
                }
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            finally
            {
                connection.Close();
            }

            return listBook;
        }
    }
}
