﻿namespace MagicTown_TownAPI.Models.Functionalities
{
    public class PaginationParams
    {
        private const int MaxPageSize = 100;
        public int PageNumber {get; set;} = 1;

        public int _pageSize = 6;

        public int PageSize 
        {
            get {return _pageSize;}
            set {_pageSize = value > MaxPageSize ? MaxPageSize : value;}
        }


    }
}
