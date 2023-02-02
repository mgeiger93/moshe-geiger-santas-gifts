using GiftManagement.DAL.DataModels;

namespace GiftManagement.DAL.Repositories
{
    public interface IGiftRequestorsRepository
    {
        IDictionary<string, float> Catalog { get; }
        IList<Requestor> Requestors { get; }
    }
}