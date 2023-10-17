namespace SupportBank;

public class CSVFileReader
{
    #nullable disable
    public List<Transaction> ReadFile(string path)
    {
        var result = new List<Transaction>();

        using (var sr = new StreamReader(path))
        {
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] cols = line.Split(",");
                Transaction transaction = new Transaction(
                    DateOnly.Parse(cols[0]),
                    cols[1],
                    cols[2],
                    cols[3],
                    Decimal.Parse(cols[4]) 
                );
                result.Add(transaction);
            }
        } 
        return result;
    }
     
}