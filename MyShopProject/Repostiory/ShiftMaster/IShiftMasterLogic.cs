using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Repostiory.ShiftMaster
{
    public interface IShiftMasterLogic
    {
        void ShiftMasterSubmit(ShiftMasterEngine model, ref int strIsComplete, ref string strMessage);

        ShiftMasterEngine GetShiftById(String ShiftId, ref int strIsComplete, ref string strMessage);

        string Delete(string ActionType, string id, ref int strIsComplete);
    }
}