using HRProgram.DLL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.DLL.Repostitory
{
    public class SummaryRepo : GenericRepo<Summary>
    {
        public SummaryRepo(DbContext context):base(context)
        {

        }
    }
}
