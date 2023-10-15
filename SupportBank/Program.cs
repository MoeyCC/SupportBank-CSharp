using System.Security.Cryptography.X509Certificates;

namespace SupportBank;

internal class Program
{
#nullable disable

  static void Main(string[] args)
  {

    List<Transaction> transactionsList = new List<Transaction>();
    List<Account> accountsList = new List<Account>();
    List<string> names = new List<string>();
    LongestWords longestWords = new LongestWords
    {
      LongestFrom = 0,
      LongestTo = 0,
      LongestNarrative = 0
    };

    Console.WriteLine();
    Console.WriteLine("Welcome to SupportBank!");
    Console.WriteLine("=======================");
    Console.WriteLine();

    //Process CSV file
    string filePath = "C:\\Users\\moses\\OneDrive\\Desktop\\Corndel\\SupportBank-CSharp\\Transactions2014.csv";
    using (var reader = new StreamReader(filePath))
    {
      reader.ReadLine();
      while (!reader.EndOfStream)
      {
        var line = reader.ReadLine();
        string[] transaction = line.Split(",");

        names.Add(transaction[1]);
        names.Add(transaction[2]);

        longestWords.UpdateLongestWords(transaction[1], transaction[2], transaction[3]);

        AddTransactionToList(transaction, transactionsList);
      }
    }

    //Create accounts 
    List<string> uniqueNames = names.Distinct().ToList();
    foreach (var name in uniqueNames)
    {
      accountsList.Add(new Account(name, transactionsList));
    }

    //
    while (true)
    {
      Console.WriteLine();
      Console.WriteLine("Please enter command :");
      Console.WriteLine("List All - Display all accounts balance.");
      Console.WriteLine("List [Account Name] - Display account transactions.");
      Console.WriteLine();
      string userInput = Console.ReadLine();
      if (userInput == "X") Environment.Exit(0);
      if (userInput == "List All")
      {
        Console.WriteLine("{0} {1}", "ACCOUNT".PadRight(14), "BALANCE");
        Console.Write("=========".PadRight(15));
        Console.WriteLine("=======");
        foreach (var account in accountsList)
        {
          Console.WriteLine("{0} {1, 7:C}", account.Name.PadRight(14), account.Balance);
        }
      }
      else if (userInput.Length >= 6)
      {
        if (userInput.Substring(0, 4) == "List")
        {
          int indexOfSpace = userInput.IndexOf(" ");
          string userInputAccount = userInput.Substring(indexOfSpace,
          (userInput.Length - indexOfSpace)).Trim();
          foreach (var account in accountsList)
          {
            if (account.Name == userInputAccount)
            {
              Console.WriteLine("{0} {1} {2} {3} {4}", "DATE".PadRight(13), "FROM".PadRight(13), "TO".PadRight(13), "NARRATIVE".PadRight(40), "AMOUNT");
              Console.WriteLine("{0} {1} {2} {3} {4}", "==========".PadRight(13), "=========".PadRight(13), "=========".PadRight(13), "================================".PadRight(40), "======");
              foreach (var transaction in account.Transactions)
              {
                Console.WriteLine("{0} {1} {2} {3} {4, 6:C}", transaction.Date.ToShortDateString().PadRight(13), transaction.From.PadRight(13), transaction.To.PadRight(13), transaction.Narrative.PadRight(40), transaction.Amount);
              }
            }
          }
        }
      }
    }
  }

  public static void AddTransactionToList(string[] transaction, List<Transaction> transactionsList)
  {
    transactionsList.Add(new Transaction
    (
      DateTime.Parse(transaction[0]),
      transaction[1],
      transaction[2],
      transaction[3],
      decimal.Parse(transaction[4])
    ));
  }
}

class Transaction
{
  public DateTime Date { get; set; }
  public string From { get; set; }
  public string To { get; set; }
  public string Narrative { get; set; }
  public decimal Amount { get; set; }
  public Transaction(DateTime date, string from, string to, string narrative, decimal amount)
  {
    Date = date;
    From = from;
    To = to;
    Narrative = narrative;
    Amount = amount;
  }
}

class Account
{
  public string Name { get; set; }
  public decimal Balance { get; set; }
  public List<Transaction> Transactions { get; set; }
  public Account(string name, List<Transaction> transactionsList)
  {
    Name = name;
    Balance = 0;
    Transactions = new List<Transaction>();
    foreach (var transaction in transactionsList)
    {
      if (transaction.To == Name || transaction.From == Name)
      {
        Transactions.Add(new Transaction(
          transaction.Date,
          transaction.From,
          transaction.To,
          transaction.Narrative,
          transaction.Amount
        ));
      }
      if (transaction.To == Name) Balance += transaction.Amount;
      if (transaction.From == Name) Balance -= transaction.Amount;
    }
  }
}

class LongestWords
{
  public int LongestFrom { get; set; }
  public int LongestTo { get; set; }
  public int LongestNarrative { get; set; }
  public void UpdateLongestWords(string from, string to, string narrative)
  {
    if (from.Length > LongestFrom) LongestFrom = from.Length;
    if (to.Length > LongestTo) LongestTo = to.Length;
    if (narrative.Length > LongestNarrative) LongestNarrative = narrative.Length;
  }
}

/* static void Main(string[] args)
{
  string[] outputs = {
                        "this is output",
                        "this is also output",
                        "output",
                        "my output"
                    };

  // order outputs in descending order by length
  var orderedOutputs = outputs.OrderByDescending(s => s.Length);

  // get longest output and add 5 chars
  var padWidth = orderedOutputs.First().Length + 5;

  foreach (string str in outputs)
  {
    // this will pad the right side of the string with whitespace when needed
    string paddedString = str.PadRight(padWidth);
    Console.WriteLine("{0}{1}", paddedString, "text");
  }
} */
/* Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("{0} {1} {2} {3} {4}", 
      "DATE".PadRight(12),
      "FROM".PadRight(padWidthFromName), 
      "TO".PadRight(padWidthToName),
      "NARRATIVE".PadRight(padWidthNarrative),
      "AMOUNT" 
    );

    //Console.WriteLine("=========================================================================================");
    Console.Write("==========".PadRight(13));
    Console.Write("=========".PadRight(padWidthFromName + 1));
    Console.Write("=========".PadRight(padWidthToName + 1));
    Console.Write("==================================".PadRight(padWidthNarrative + 1));
    Console.WriteLine("======");

    Console.ResetColor();
 */


