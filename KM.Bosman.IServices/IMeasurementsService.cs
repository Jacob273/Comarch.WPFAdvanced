using System.Collections.Generic;
using System.Threading.Tasks;

namespace KM.Bosman.IServices
{
    public interface IMeasurementsService<T>
    {
        IEnumerable<T> Get();
        Task<IEnumerable<T>> GetAsync();
    }
}
