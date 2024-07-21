using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class PlacementTest
    {
        public string TestID { get; set; }
        public string StudentID { get; set; }
        public string FullName { get; set; } // Từ bảng Users
        public DateTime TestDate { get; set; }
        public float Score { get; set; }
        public string LevelName { get; set; }
        public PlacementTest() { }

        public PlacementTest(string testID, string studentID, DateTime testDate, float score, string levelName)
        {
            TestID = testID;
            StudentID = studentID;
            TestDate = testDate;
            Score = score;
            LevelName = levelName;
        }
    }

}
