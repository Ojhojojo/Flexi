namespace TourFlexSystem.Domain.Models;

//TODO: Optimize this table
public class JournalledTransaction
{
    public Guid Id { get; set; }

    public string ContractReference { get; set; }

    public string TransactionId { get; set; }

    public string PostilionReference { get; set; }

    public string CustomerId { get; set; }

    public string AccountNumber { get; set; }

    public string TransactionType { get; set; }

    public string AdditionalText { get; set; }

    public string TransactionDate { get; set; }

    public string ValueDate { get; set; }

    public decimal Amount { get; set; }

    public string Direction { get; set; }

    public decimal AccountBalance { get; set; }

    public string FromAccountNumber { get; set; }

    public string FromAccountName { get; set; }

    public string ToAccountNumber { get; set; }

    public string ToAccountName { get; set; }

    public string Particulars { get; set; }

    public string Code { get; set; }

    public string Reference { get; set; }

    public DateTime LastUpdatedDate { get; set; }

    public bool Processed { get; set; }

    public string MessageId { get; set; }

    public string Source { get; set; }
}
