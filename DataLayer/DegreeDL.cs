using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
    public class DegreeDL:DataProvider
    {
        public List<Degree> GetAllDegrees()
        {
            List<Degree> degrees = new List<Degree>();

            List<Degree> list = new List<Degree>();
            string sql = "SELECT DegreeID, DegreeName FROM Degrees";

            Connect();
            SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
            while (reader.Read())
            {
                Degree degree = new Degree
                {
                    DegreeID = reader["DegreeID"].ToString()
                };
                list.Add(degree);
            }
            reader.Close();
            Disconnect();

            return list;           
        }

    }
}
