using InsertTry;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

Console.WriteLine("Press 1 for delete, 2 for normal insert, " +
    "3 for prepared insert");
Console.WriteLine("4 for Entity, 5 for bulk insert");
string action = Console.ReadLine();


DateTime before = DateTime.Now;
List<Title> titles = new List<Title>();
foreach (string line in System.IO.File.ReadLines(@"C:\Users\sabin\Downloads\data.tsv")
    .Skip(1))
{
    string[] values = line.Split('\t');
    if (values.Length == 9)
    {
        titles.Add(new Title()
        {
            tconst = values[0],
            titleType= values[1],
            primaryTitle = values[2].Replace("\"", ""),
            originalTitle = values[3].Replace("\"", ""),
            isAdult = CheckBoolColumn(values[4]),
            startYear = CheckIntColumn(values[5]),
            endYear = CheckIntColumn(values[6]),
            runtimeMinute = CheckIntColumn(values[7]),
            genre = values[8]
        });
    }
}
DateTime after = DateTime.Now;

Console.WriteLine("Read lines in : " + (after - before));

Console.WriteLine("Amount of titles: " + titles.Count());

before = DateTime.Now;

using (SqlConnection sqlConn = new SqlConnection("Data Source=ZORO;Initial Catalog=TitleDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
{
    sqlConn.Open();

    switch (action)
    {
        case "1":
            DeleteTitles(sqlConn);
            break;
        //case "2":
        //    IInserter inserter = new NormalInserter();
        //    inserter.InsertTitles(sqlConn, titles);
        //    break;
        case "3":
            IInserter inserter2 = new PreparedInserter();
            inserter2.InsertTitles(sqlConn, titles);
            break;
        //case "4":
        //    IInserter inserter3 = new EFInserter();
        //    inserter3.InsertTitles(sqlConn, titles);
        //    break;
        //case "5":
        //    IInserter inserter4 = new BulkInserter();
        //    inserter4.InsertTitles(sqlConn, titles);
        //    break;
    }

}
after = DateTime.Now;

Console.WriteLine("Inserted in : " + (after - before));

void DeleteTitles(SqlConnection sqlConn)
{
    SqlCommand sqlComm = new SqlCommand("DELETE FROM Titles", sqlConn);
    sqlComm.ExecuteNonQuery();
}

int? CheckIntColumn(string value)
{
    int beforeParse = 0;
    bool canParse = int.TryParse(value, out beforeParse);
    int? result = null;
    if (beforeParse != 0)
    {
        result = beforeParse;
    }
    return result;
}

bool CheckBoolColumn (string value)
{
    if (value == "1")
    {
        return true;
    }else
    {
        return false;
    }
}