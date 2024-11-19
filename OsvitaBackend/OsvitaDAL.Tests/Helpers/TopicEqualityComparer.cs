using System;
using System.Diagnostics.CodeAnalysis;
using OsvitaDAL.Entities;

namespace Osvita.Tests.Integration.Helpers
{
	public class TopicEqualityComparer : IEqualityComparer<Topic>
    {
        public bool Equals(Topic? x, Topic? y)
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
                && x.ChapterId == y.ChapterId
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] Topic obj)
        {
            return obj.GetHashCode();
        }
    }
}

