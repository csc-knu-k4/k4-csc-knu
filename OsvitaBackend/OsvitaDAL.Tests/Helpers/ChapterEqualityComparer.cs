using System;
using System.Diagnostics.CodeAnalysis;
using OsvitaDAL.Entities;

namespace Osvita.Tests.Helpers
{
	public class ChapterEqualityComparer : IEqualityComparer<Chapter>
    {
        public bool Equals(Chapter? x, Chapter? y)
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
                && x.SubjectId == y.SubjectId
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] Chapter obj)
        {
            return obj.GetHashCode();
        }
    }
}

