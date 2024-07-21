using System.Collections.Generic;
using DataLayer;
using TransferObject;

namespace BusinessLayer
{
    public class ProficiencyLevelBL
    {
        private ProficiencyLevelDL levelDL;

        public ProficiencyLevelBL()
        {
            levelDL = new ProficiencyLevelDL();
        }

        public List<ProficiencyLevel> GetAllLevels()
        {
            return levelDL.GetAllProficiencyLevels();
        }
    }
}
