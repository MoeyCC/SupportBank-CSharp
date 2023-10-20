using System.Transactions;
using Newtonsoft.Json;
using NLog;

namespace SupportBank;

public class JSONFileReader
{
    private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

    public List<Transaction> ReadFile(string path){

        var transactions = new List<Transaction>();

        string json = System.IO.File.ReadAllText(path);
        var jsonTransactions = JsonConvert.DeserializeObject<List<JSONTransaction>>(json);
        var lineNumber = 1;
        
        foreach (var jsonTransaction in jsonTransactions)
        {
            lineNumber++;
            try
            {
                DateTime date = DateTime.Parse(jsonTransaction.Date);
            }
            catch (FormatException e)
            {
                Logger.Error($"File: {path} -- Line: {lineNumber} -- Value in date field is not valid: {jsonTransaction.Date}");
                continue;
            }
            try
            {
                decimal amount = decimal.Parse(jsonTransaction.Amount);
            }
            catch (FormatException e)
            {
                Logger.Error($"File: {path} -- Line: {lineNumber} -- Value in amount field is not valid: {jsonTransaction.Amount}");
                continue;
            }
            transactions.Add(new Transaction(
                DateOnly.Parse(jsonTransaction.Date), 
                jsonTransaction.FromAccount,
                jsonTransaction.ToAccount,
                jsonTransaction.Narrative,
                Decimal.Parse(jsonTransaction.Amount)
            ));
        } 
        return transactions;
    }
}