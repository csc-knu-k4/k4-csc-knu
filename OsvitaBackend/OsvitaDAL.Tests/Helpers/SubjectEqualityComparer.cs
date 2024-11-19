using System;
using System.Diagnostics.CodeAnalysis;
using OsvitaDAL.Entities;

namespace Osvita.Tests.Integration.Helpers
{
	public class SubjectEqualityComparer : IEqualityComparer<Subject>
    {
        public bool Equals(Subject? x, Subject? y)
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
                && x.Title == y.Title;
        }

        public int GetHashCode([DisallowNull] Subject obj)
        {
            return obj.GetHashCode();
        }
    }
}

