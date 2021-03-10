using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAs4
{
    public class EmployeeDAO
    {
        string ConnectionString = @"server=DESKTOP-DLMULPB;database=BookStore;uid=sa;pwd=tam";

        public EmployeeDTO CheckLogin(string EmpID, string EmpPassword)
        {
            EmployeeDTO dto = null;

            string SQL = @"SELECT EmpID, EmpPassword, EmpRole FROM Employee WHERE EmpID = @id AND EmpPassword = @pass";
            SqlConnection connection = new SqlConnection(ConnectionString);

            try
            {
                if(connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }

                SqlCommand command = new SqlCommand(SQL, connection);

                command.Parameters.AddWithValue("id", EmpID);
                command.Parameters.AddWithValue("pass", EmpPassword);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    EmpID = reader.GetValue(0).ToString();
                    EmpPassword = reader.GetValue(1).ToString();
                    bool EmpRole = reader.GetBoolean(2);

                    dto = new EmployeeDTO(EmpID, EmpPassword, EmpRole);
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

            return dto;
        }
    }
}
