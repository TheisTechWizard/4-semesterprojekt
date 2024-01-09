namespace DataLayer
{
    public interface IDataContext<TConnetion>
    {
        TConnetion Open();
    }
}
