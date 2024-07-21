using DataLayer;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

namespace BusinessLayer
{
    public class FeeBL
    {
        private StudentTuitionFeesDL dal = new StudentTuitionFeesDL();

        public List<StudentTuitionFee> GetAllStudentTuitionFees()
        {
            return dal.GetAllStudentTuitionFees();
        }

    }
}
