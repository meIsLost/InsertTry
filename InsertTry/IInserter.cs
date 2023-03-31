using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace InsertTry
{
    public interface IInserter
    {
        void InsertTitles(SqlConnection sqlConn, List<Title> allTitles);
    }
}
