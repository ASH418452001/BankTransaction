namespace BankTransaction.DTO
{
    public class ApiRespons<T>
    {
        public float StatusCode { get; set; }
        public int Count { get; set; }
        public T Result { get; set; }
    }
}
