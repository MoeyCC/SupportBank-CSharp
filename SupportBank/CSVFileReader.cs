using NLog;

namespace SupportBank;

public class CSVFileReader
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

#nullable disable
    public List<Transaction> ReadFile(string path)
    {

        var result = new List<Transaction>();

        using (var sr = new StreamReader(path))
        {
            int lineNumber = 1;
            sr.ReadLine();
            while (!sr.EndOfStream)

            {
                string line = sr.ReadLine();
                string[] cols = line.Split(",");
                lineNumber ++;
                try
                {
                    decimal amount = Decimal.Parse(cols[4]);
                }
                catch (FormatException e)
                {
                    Logger.Error($"File: {path} -- Line: {lineNumber} -- Value in amount field is not valid: {cols[4]}");
                    continue;
                }

                try
                {
                    DateOnly date = DateOnly.Parse(cols[0]);
                }
                catch (FormatException e)
                {
                    Logger.Error($"Value is not a date: {cols[0]}");
                    continue;
                }

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