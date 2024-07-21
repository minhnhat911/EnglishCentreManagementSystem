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
    public class ProficiencyLevelDL:DataProvider
    {
        public List<ProficiencyLevel> GetAllProficiencyLevels()
        {
            List<ProficiencyLevel> list = new List<ProficiencyLevel>();
            string sql = "sp_GetAllProficiencyLevels";

            Connect();
            SqlDataReader reader = MyExcuteReader(sql, CommandType.StoredProcedure);
            while (reader.Read())
            {
                ProficiencyLevel level = new ProficiencyLevel
                {
                    LevelName = reader["LevelName"].ToString()
                };
                list.Add(level);
            }
            reader.Close();
            Disconnect();

            return list;
        }
    }
}
