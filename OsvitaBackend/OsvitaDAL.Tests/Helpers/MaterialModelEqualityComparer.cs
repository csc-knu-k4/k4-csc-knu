using System;
using OsvitaDAL.Entities;
using System.Diagnostics.CodeAnalysis;
using OsvitaBLL.Models;

namespace Osvita.Tests.Integration.Helpers
{
	public class MaterialModelEqualityComparer : IEqualityComparer<MaterialModel>
    {
        public bool Equals(MaterialModel? x, MaterialModel? y)
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
                && x.ContentBlocksIds.Count == x.ContentBlocksIds.Count
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] MaterialModel obj)
        {
            return obj.GetHashCode();
        }
    }
}

