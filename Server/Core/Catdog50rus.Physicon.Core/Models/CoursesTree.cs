using System;
using System.Collections.Generic;

namespace Catdog50rus.Physicon.Core.Models
{
    public class CoursesTree : IComparable
    {
        //c.Title As Course, M.CourseId, M.Id, M.ParentId, M.Title, M.Num, M.[Order]

        public string Course { get; set; }
        public int CourseId { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public string Genre { get; set; }
        public int ModuleId { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Num { get; set; }
        public int Order { get; set; }

        public int CompareTo(object obj)
        {
            if (obj is CoursesTree item)
            {
               
                return ModuleId.CompareTo(item.ParentId);
            }
            else
            {
                throw new ArgumentException("Несовпадение типов");
            }
        }
    }
}
