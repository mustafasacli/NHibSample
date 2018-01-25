using NHib.Core.Entity;

namespace NHib.Repository.Interfaces
{
    public interface INRepository
    {
        object Save<T>(T t);
    }
}
