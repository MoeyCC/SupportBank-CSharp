namespace SupportBank;

public class JSONTransaction
{
    public string Date { get; set; }
    public string FromAccount { get; set; }
    public string ToAccount { get; set; } 
    public string Narrative { get; set;}
    public string Amount { get; set; } 
}