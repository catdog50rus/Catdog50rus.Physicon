using Catdog50rus.Physicon.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catdog50rus.Physicon.Core.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CoursesTree>> GetDataAsync();
    }
}
