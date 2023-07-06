namespace BankTransaction.DTO.bankTranactionDTO
{
    public class UPDATEbankTransactionDTO
    {
        public int ID { get; set; }
        public string NameOfSender { get; set; }
        public string NameOfReciever { get; set; }
        public string PhoneNumber { get; set; }
        public string governorate { get; set; }
        public decimal AmountInDollar { get; set; }
        public decimal AmountInEuro { get; set; }
        public decimal DailyPrice { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public DateTime DateOfReciever { get; set; }
        public string Notes { get; set; }
    }
}
