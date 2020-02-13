using lib.common.interfaces;

namespace application.cqrs._base
{
    public abstract class CreditableBase : ITakeCredit
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
