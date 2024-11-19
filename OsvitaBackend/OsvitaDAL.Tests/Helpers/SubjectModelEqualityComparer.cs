using System.Diagnostics.CodeAnalysis;
using OsvitaBLL.Models;

namespace Osvita.Tests.Integration.Helpers
{
	public class SubjectModelEqualityComparer : IEqualityComparer<SubjectModel>
    {
        public bool Equals(SubjectModel? x, SubjectModel? y)
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
                && x.ChaptersIds.Count == y.ChaptersIds.Count;
        }

        public int GetHashCode([DisallowNull] SubjectModel obj)
        {
            return obj.GetHashCode();
        }
    }
}

