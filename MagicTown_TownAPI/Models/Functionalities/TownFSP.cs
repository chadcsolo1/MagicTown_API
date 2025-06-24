using System.Linq.Expressions;

namespace MagicTown_TownAPI.Models.Functionalities
{
    public class TownFSP<T>  where T : class
    {
        public Expression<Func<T, bool>> Filter
        {
            get;
            set;
        }

        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public int PageNumber
        {
            get;
            set;
        }   
    }
}
