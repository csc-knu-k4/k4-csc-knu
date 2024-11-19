using System;
using OsvitaBLL.Models;
using System.Diagnostics.CodeAnalysis;

namespace Osvita.Tests.Integration.Helpers
{
	public class TopicModelEqualityComparer : IEqualityComparer<TopicModel>
    {
        public bool Equals(TopicModel? x, TopicModel? y)
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
                & x.MaterialsIds.Count == y.MaterialsIds.Count
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] TopicModel obj)
        {
            return obj.GetHashCode();
        }
    }
}

