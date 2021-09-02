using HRProgram.DLL.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProgram.DLL.Repostitory
{
    public class StatusRepo : GenericRepo<Statuses>
    {
        public StatusRepo(DbContext context):base(context)
        {

        }
    }
}
