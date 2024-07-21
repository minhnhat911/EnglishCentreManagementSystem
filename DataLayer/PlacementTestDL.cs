using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace DataLayer
{
    public class PlacementTestDL : DataProvider
    {
        public List<PlacementTest> GetAllEntryTest()
        {
            List<PlacementTest> list = new List<PlacementTest>();
            string sql = @"
            SELECT pt.TestID, s.StudentID, u.FirstName + ' ' + u.LastName AS FullName,
                   pt.TestDate, pt.Score, pt.LevelName
                    FROM PlacementTests pt
                    JOIN Students s ON pt.StudentID = s.StudentID
                    JOIN Users u ON s.UserID = u.UserID
                    JOIN ProficiencyLevels p ON pt.LevelName = p.LevelName
        ";
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    list.Add(new PlacementTest
                    {
                        TestID = reader["TestID"].ToString(),
                        StudentID = reader["StudentID"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        TestDate = Convert.ToDateTime(reader["TestDate"]),
                        Score = float.Parse(reader["Score"].ToString()),
                        LevelName = reader["LevelName"].ToString()
                    });
                }
            }
            finally { Disconnect(); }
            return list;
        }
        public List<(float Score, int Frequency)> GetScoreFrequencyByYear(int year)
        {
            List<(float, int)> result = new List<(float, int)>();
            string sql = "sp_GetScoreFrequencyByYear";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Year", year)
            };
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.StoredProcedure, parameters );

                while (reader.Read())
                {
                    float score = float.Parse(reader["Score"].ToString());
                    int frequency = int.Parse(reader["Frequency"].ToString());
                    result.Add((score, frequency));
                }
            }
            finally { Disconnect(); }

            return result;
        }       

        public int AddPlacementTest(PlacementTest test)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@TestID", test.TestID),
                new SqlParameter("@StudentID", test.StudentID),
                new SqlParameter("@TestDate", test.TestDate),
                new SqlParameter("@Score", test.Score)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery("sp_AddPlacementTest", CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }
        public int UpdatePlacementTest(PlacementTest test)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@TestID", test.TestID),
                new SqlParameter("@TestDate", test.TestDate),
                new SqlParameter("@Score", test.Score)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery("sp_UpdatePlacementTest", CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }
        public int DeletePlacementTest(string testID)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@TestID", testID)
            };

            try
            {
                Connect();
                return MyExcuteNonQuery("sp_DeletePlacementTest", CommandType.StoredProcedure, parameters);
            }
            finally
            {
                Disconnect();
            }
        }
        //sinh tự động PT ID
        public string GenerateTestID()
        {
            string sql = "SELECT TOP 1 TestID FROM PlacementTests ORDER BY TestID DESC";
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
                if (reader.Read())
                {
                    string lastID = reader["TestID"].ToString(); 
                    if (lastID.Length > 2)
                    {
                    
                        int num = int.Parse(lastID.Substring(2));
                        return "PT" + (num + 1).ToString("D3"); 
                    }
                    else
                    {
                        throw new Exception("TestID không đúng định dạng.");
                    }
                }
                else
                {
                    return "PT001"; 
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                Console.WriteLine("Lỗi khi tạo TestID: " + ex.Message);
                return null; 
            }
            finally
            {
                Disconnect(); 
            }
        }

    }
}
