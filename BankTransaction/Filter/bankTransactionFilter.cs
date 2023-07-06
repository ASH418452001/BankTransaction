using BankTransaction.shared.filters;

namespace BankTransaction.Filter
{
    public class bankTransactionFilter : Pagination
    {
      

        public string NameOfSender { get; set; }
        public string NameOfReciever { get; set; }
        public string governorate { get; set; }
  

     
        public string Notes { get; set; }
        public int fromAmountdolar { get; set; }
        public int fromAmountEuro { get; set; }
        public int toAmountdolar { get; set; }
        public int toAmountEuro { get; set; }
        public int fromdailyprice { get; set; }
        public int todailyprice { get; set; }


        public int fromdateOfTransaction { get; set; }
        public int fromdateOfRecieve { get; set; }

        public int todateOfTransaction { get; set; }
        public int todateOfRecieve { get; set; }


    }
}
