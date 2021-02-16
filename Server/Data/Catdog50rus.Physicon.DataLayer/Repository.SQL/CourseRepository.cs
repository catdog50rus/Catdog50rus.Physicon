using Catdog50rus.Physicon.Core.Interfaces;
using Catdog50rus.Physicon.Core.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Catdog50rus.Physicon.DataLayer.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly string _connectionString;
       
        public CourseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<CoursesTree>> GetDataAsync()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var request = @"SELECT c.Title As Course, c.Id, c.[Subject], c.Grade, c.Genre, M.CourseId, M.Id As ModuleId, M.ParentId, M.Title, M.Num, M.[Order] 
                            FROM dbo.Courses C LEFT JOIN Modules M On C.ID = M.CourseId
                            ORDER BY c.Title ASC, M.[Order] ASC";

            var res = await db.QueryAsync<CoursesTree>(request);

            return res;
        }
    }
}
