using System.Transactions;

namespace SupportBank;

public class Bank 
{
    private List<Transaction> Transactions { get; set; }

    public Bank(string path, string filename){
        if(IsFileType(filename) == "csv"){
            var reader = new CSVFileReader();
            var filePath = $"{path}{filename}";
            Transactions = reader.ReadFile(filePath);
        }   

        if(IsFileType(filename) == "json"){
            var reader = new JSONFileReader();
            var filePath = $"{path}{filename}";
            Transactions = reader.ReadFile(filePath);
            foreach (var transaction in Transactions)
            {
                Console.WriteLine($"{transaction.Date}: {transaction.From} bought {transaction.Narrative} for {transaction.To} for the sum of {transaction.Amount}");                
            }
        }      
    }

    public void ListAll(){
        var names = GetUniqueNames();
        foreach (var name in names)
        {
            decimal balance = 0;
            
            var transactions = GetTransactionsForAccount(name);
            foreach (var transaction in transactions)
            {
                if(transaction.From == name)
                {
                    balance -= transaction.Amount;
                } else {
                    balance += transaction.Amount;
                }
            }   
            Console.WriteLine($"{name} {balance}");                     
        }
    }   

    public void ListAccount(string name){
        var transactions = GetTransactionsForAccount(name);
        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction.ToString());
        }
    } 

    private List<Transaction> GetTransactionsForAccount(string name)
    {
        var result = new List<Transaction>();
        foreach (var transaction in Transactions)
        {
            if(transaction.From == name || transaction.To == name)
            {
                result.Add(transaction);
            }            
        }
        return result;
    }

    private List<string> GetUniqueNames(){
        var names = new List<string>();
        foreach (var transaction in Transactions)
        {
            var from = transaction.From;
            if(!names.Contains(from)){
                names.Add(from);
            }           
            var to = transaction.To;
            if(!names.Contains(to)){
                names.Add(to);
            }
        }
        return names;
    }

    public string IsFileType(string path){
        var fullStop = path.IndexOf(".") + 1;
        return path.Substring(fullStop, path.Length - fullStop); 
    }    
}