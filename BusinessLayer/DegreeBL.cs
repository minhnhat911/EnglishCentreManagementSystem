using System.Collections.Generic;
using TransferObject;
using DataLayer;

namespace BusinessLayer
{
    public class DegreeBL
    {
        private DegreeDL degreeDL;

        public DegreeBL()
        {
            degreeDL = new DegreeDL();
        }

        public List<Degree> GetAllDegrees()
        {
            return degreeDL.GetAllDegrees();
        }
    }
}
