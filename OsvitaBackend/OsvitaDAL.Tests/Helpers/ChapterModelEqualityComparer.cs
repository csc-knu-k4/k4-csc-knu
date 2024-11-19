using System;
using OsvitaDAL.Entities;
using System.Diagnostics.CodeAnalysis;
using OsvitaBLL.Models;

namespace Osvita.Tests.Integration.Helpers
{
	public class ChapterModelEqualityComparer : IEqualityComparer<ChapterModel>
    {
        public bool Equals(ChapterModel? x, ChapterModel? y)
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
                & x.TopicsIds.Count == y.TopicsIds.Count
                && x.OrderPosition == y.OrderPosition;
        }

        public int GetHashCode([DisallowNull] ChapterModel obj)
        {
            return obj.GetHashCode();
        }
    }
}

