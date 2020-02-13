namespace lib.common.interfaces
{
    public interface IHaveID<T> where T : struct
    {
        T ID { get; set; }
    }
}
