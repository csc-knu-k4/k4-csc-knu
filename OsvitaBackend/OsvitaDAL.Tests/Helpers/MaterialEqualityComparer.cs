using System;
using OsvitaDAL.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Osvita.Tests.Integration.Helpers
{
	public class MaterialEqualityComparer : IEqualityComparer<Material>
    {
        public bool Equals(Material? x, Material? y)
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
                && x.TopicId == y.TopicId
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] Material obj)
        {
            return obj.GetHashCode();
        }
    }
}

