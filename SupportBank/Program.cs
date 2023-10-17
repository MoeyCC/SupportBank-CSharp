using System.Security.Cryptography.X509Certificates;

namespace SupportBank;

internal class Program
{
  #nullable disable

  static void Main(string[] args)
  {
    Console.WriteLine();
    Console.WriteLine("Welcome to SupportBank!");
    Console.WriteLine("=======================");
    Console.WriteLine();

    var bank = new Bank("C:\\Users\\moses\\OneDrive\\Desktop\\Corndel\\SupportBank-CSharp\\Transactions2014.csv");

    bank.ListAll();

    //bank.ListAccount("Tim L");

    //string stringPath = "C:\\Users\\moses\\OneDrive\\Desktop\\Corndel\\SupportBank-CSharp\\Transactions2014.csv";

    //var reader = new CSVFileReader();
    //var transactions = reader.ReadFile(stringPath);

    //foreach (var transaction in transactions)
    //{
    // Console.WriteLine(transaction.From);  
    //}
  }
}
