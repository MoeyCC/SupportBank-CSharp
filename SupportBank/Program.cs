namespace SupportBank;
using NLog;
using NLog.Config;
using NLog.Targets;

internal class Program
{
  #nullable disable  

  static void Main(string[] args)
  {
    var config = new LoggingConfiguration();
    var target = new FileTarget { FileName = @"C:\Users\Default.DESKTOP-4G06BME\OneDrive\Desktop\Corndel\SupportBank-CSharp\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
    config.AddTarget("File Logger", target);
    config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
    LogManager.Configuration = config;

    Console.WriteLine();
    Console.WriteLine("Welcome to SupportBank!");
    Console.WriteLine("=======================");
    Console.WriteLine();

    var path = "C:\\Users\\Default.DESKTOP-4G06BME\\OneDrive\\Desktop\\Corndel\\SupportBank-CSharp\\";
    var fileName = "Transactions2013.json";
    var bank = new Bank(path, fileName);

    //bank.ListAll();

    bank.ListAccount("Tim L");
  }
}




