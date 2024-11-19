using System;
using OsvitaDAL.Entities;
using System.Diagnostics.CodeAnalysis;
using OsvitaBLL.Models;

namespace Osvita.Tests.Integration.Helpers
{
	public class ContentBlockModelEqualityComparer : IEqualityComparer<ContentBlockModel>
    {
        public bool Equals(ContentBlockModel? x, ContentBlockModel? y)
        {
            if (x is null && y is null)
            {
                return true;
            }
            if (x is null || y is null)
            {
                return false;
            }
            return x.Id == y.Id
                && x.Title == y.Title
                && x.MaterialId == y.MaterialId
                && x.Title == y.Title
                && x.ContentBlockModelType == y.ContentBlockModelType
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] ContentBlockModel obj)
        {
            return obj.GetHashCode();
        }
    }
}

