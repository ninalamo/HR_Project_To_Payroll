namespace domain
{
    public class Shift : BaseAudit<long>
    {
        public int Start { get; set; }
        public int End { get; set; } 
    }

}
