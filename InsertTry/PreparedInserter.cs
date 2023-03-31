using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace InsertTry
{
    public class PreparedInserter : IInserter
    {
        public void InsertTitles(SqlConnection sqlConn, List<Title> allTitles)
        {
            //SqlCommand sqlComm = new SqlCommand(
            //             "INSERT INTO [dbo].[Titles]([tconst],[primaryTitle]," +
            //             "[originalTitle],[startYear],[endYear])" +
            //             " VALUES (" +
            //             "@tconst," +
            //             "@primaryTitle," +
            //             "@originalTitle," +
            //             "@startYear," +
            //             "@endYear);", sqlConn);
            SqlCommand sqlComm = new SqlCommand(
                         "EXEC SpInsertTitleBasicIMDB " +
                         "@tconst," +
                         "@titleType," +
                         "@primaryTitle," +
                         "@originalTitle," +
                         "@isAdult," +
                         "@startYear," +
                         "@endYear,"+
                         "@runtimeMinute," +
                         "@genre"
                         , sqlConn);

            SqlParameter tconstPar = new SqlParameter("@tconst", SqlDbType.Text, 50);
            sqlComm.Parameters.Add(tconstPar);

            SqlParameter titleTypePar = new SqlParameter("@titleType", SqlDbType.Text, 50);
            sqlComm.Parameters.Add(titleTypePar);

            SqlParameter primaryTitlePar = new SqlParameter("@primaryTitle", SqlDbType.Text, 250);
            sqlComm.Parameters.Add(primaryTitlePar);

            SqlParameter originalTitlePar = new SqlParameter("@originalTitle", SqlDbType.Text, 250);
            sqlComm.Parameters.Add(originalTitlePar);

            SqlParameter isAdultPar = new SqlParameter("@isAdult", SqlDbType.Bit);
            sqlComm.Parameters.Add(isAdultPar);

            SqlParameter startYearPar = new SqlParameter("@startYear", SqlDbType.Int);
            sqlComm.Parameters.Add(startYearPar);

            SqlParameter endYearPar = new SqlParameter("@endYear", SqlDbType.Int);
            sqlComm.Parameters.Add(endYearPar);

            SqlParameter runtimeMinutePar = new SqlParameter("@runtimeMinute", SqlDbType.Int);
            sqlComm.Parameters.Add(runtimeMinutePar);

            SqlParameter genrePar = new SqlParameter("@genre", SqlDbType.Text, -1);
            sqlComm.Parameters.Add(genrePar);

            sqlComm.Prepare();

            foreach (Title title in allTitles)
            {
                tconstPar.Value = title.tconst;
                titleTypePar.Value = title.titleType;
                primaryTitlePar.Value = title.primaryTitle;
                originalTitlePar.Value = title.originalTitle;
                isAdultPar.Value = title.isAdult;
                if (title.startYear == null)
                {
                  
                    startYearPar.Value = DBNull.Value;
                }
                else
                {
                    startYearPar.Value = title.startYear;
                }
                if (title.endYear == null)
                {
                    endYearPar.Value = DBNull.Value;
                }
                else
                {
                    endYearPar.Value = title.endYear;
                }
                if (title.runtimeMinute == null)
                {
                    runtimeMinutePar.Value = DBNull.Value;
                }
                else
                {
                     runtimeMinutePar.Value = title.runtimeMinute;
                }
                genrePar.Value = title.genre;
                //Console.WriteLine(sqlComm.CommandText);
               sqlComm.ExecuteNonQuery();
            }
        }
    }
}
