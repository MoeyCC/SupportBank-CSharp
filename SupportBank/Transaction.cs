namespace SupportBank;

public class Transaction
{
    public DateOnly Date { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Narrative { get; set; }
    public decimal Amount { get; set; }  

    public Transaction(DateOnly date, string from, string to, string narrative, decimal amount)
    {
        Date = date;
        From = from;
        To = to;
        Narrative = narrative;
        Amount = amount;
    }

    public override string ToString()
    {
        return $"{Date.ToShortDateString()}: {From} bought \"{Narrative}\" for {To}, for the sum og £{Amount}.";
    } 
}
