using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListingAPI.Models
{
    public class RequestParams
    {
        const int _maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10; // default
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
                // if pagesize requested > max then
                //if (value > maxPageSize)
                //{
                //    _pageSize = maxPageSize;
                //}
                //else
                //{
                //    _pageSize = value;    
                //}
            }
        }
    }
}
