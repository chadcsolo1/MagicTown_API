using System.Linq.Expressions;

namespace MagicTown_TownAPI.Models.Functionalities
{
    public class TownFSP
    {

        public string? Filter
        {
            get;
            set;
        }

        public string? OrderBy
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
        //public Expression<Func<Town, bool>> Filter
        //{
        //    get;
        //    set;
        //}

        //public Func<IQueryable<Town>, IOrderedQueryable<Town>> OrderBy
        //{
        //    get;
        //    set;
        //}

        //public int PageSize
        //{
        //    get;
        //    set;
        //}

        //public int PageNumber
        //{
        //    get;
        //    set;
        //}   
    }
}
