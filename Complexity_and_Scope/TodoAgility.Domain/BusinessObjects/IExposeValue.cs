namespace TodoAgility.Domain.BusinessObjects
{
    public interface IExposeValue<TValue>
    {
        TValue GetValue();
    }
}