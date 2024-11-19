using System;
using System.Diagnostics.CodeAnalysis;
using OsvitaDAL.Entities;

namespace Osvita.Tests.Integration.Helpers
{
	public class ContentBlockEqualityComparer : IEqualityComparer<ContentBlock>
    {
        public bool Equals(ContentBlock? x, ContentBlock? y)
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
                && x.ContentType == y.ContentType
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] ContentBlock obj)
        {
            return obj.GetHashCode();
        }
    }
}

