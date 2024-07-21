using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace BusinessLayer
{
    public class PlacementTestBL
    {
        private PlacementTestDL placementTestDL;

        public PlacementTestBL()
        {
            placementTestDL = new PlacementTestDL();
        }

        public List<PlacementTest> GetAllEntryTest()
        {
            try
            {
                return placementTestDL.GetAllEntryTest();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<(float Score, int Frequency)> GetScoreFrequencyByYear(int year)
        {
            try
            {
                return placementTestDL.GetScoreFrequencyByYear( year);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public int AddPlacementTest(PlacementTest test)
        {
            try
            {
                return placementTestDL.AddPlacementTest(test);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi thêm bài kiểm tra.", ex);
            }
        }
        public int UpdatePlacementTest(PlacementTest test)
        {
            try
            {
                return placementTestDL.UpdatePlacementTest(test);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi cập nhật bài kiểm tra.", ex);
            }
        }
        public int DeletePlacementTest(string testID)
        {
            try
            {
                return placementTestDL.DeletePlacementTest(testID);
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi xóa bài kiểm tra.", ex);
            }
        }
        public string GenerateTestID()
        {
            try
            {
                return placementTestDL.GenerateTestID();
            }
            catch (SqlException ex)
            {
                throw new Exception("Lỗi khi tạo mã học viên mới.", ex);
            }
        }
    }
}
