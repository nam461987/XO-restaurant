using XO.Common.Enums;
using XO.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XO.Business.Filter
{
    public static class FilterExtension
    {
        public static IQueryable<T> ToFilterByRole<T>(this IQueryable<T> source, Func<T, int> getRestaurantIdProperty,
            Func<T, int> getBranchIdProperty, int? restaurantId, int? branchId)
        {
            if ((restaurantId == 0 && branchId == 0) || (restaurantId == null && branchId == null))
            {
                return source;
            }
            else if ((restaurantId > 0 && branchId == 0) || (restaurantId > 0 && branchId == null))
            {
                source = source.Where(c => getRestaurantIdProperty(c) == restaurantId);
            }
            else if (restaurantId > 0 && branchId > 0)
            {
                source = source.Where(c => getRestaurantIdProperty(c) == restaurantId && getBranchIdProperty(c) == branchId);
            }
            return source;
        }
        public static IQueryable<T> ToFilterByAccountType<T>(this IQueryable<T> source,
            Func<T, int> getParentIdStatusProperty,
            Func<T, int> getCurrentStatusProperty,
            int? accountTypeId)
        {
            if (accountTypeId != (int)EAccountType.WebUser)
            {
                return source;
            }
            else if (accountTypeId == (int)EAccountType.WebUser && getParentIdStatusProperty != null)
            {
                source = source.Where(c => getParentIdStatusProperty(c) == (int)EStatus.Using &&
                getCurrentStatusProperty(c) == (int)EStatus.Using);
            }
            else
            {
                source = source.Where(c => getCurrentStatusProperty(c) == (int)EStatus.Using);
            }
            return source;
        }
        public static IQueryable<StaffPortfolio> ToFilterByAccountType(this IQueryable<StaffPortfolio> source,
            int? accountTypeId)
        {
            if (accountTypeId != (int)EAccountType.WebUser)
            {
                return source;
            }
            else
            {
                source = source.Where(c => c.Status == (int)EStatus.Using);
            }
            return source;
        }
        public static IQueryable<BlogType> ToFilterByAccountType(this IQueryable<BlogType> source,
            int? accountTypeId)
        {
            if (accountTypeId != (int)EAccountType.WebUser)
            {
                return source;
            }
            else
            {
                source = source.Where(c => c.Status == (int)EStatus.Using);
            }
            return source;
        }
    }
}
