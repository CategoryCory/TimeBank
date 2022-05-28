using System.Diagnostics.CodeAnalysis;
using TimeBank.Repository.Models;

namespace TimeBank.Services.Comparers
{
    public class SkillComparer : IEqualityComparer<UserSkill>
    {
        public bool Equals(UserSkill x, UserSkill y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x is null || y is null) return false;

            return x.UserSkillId == y.UserSkillId;
        }

        public int GetHashCode([DisallowNull] UserSkill obj)
        {
            int SkillIdHashCode = obj.UserSkillId.GetHashCode();

            int SkillNameHashCode = obj.SkillName.GetHashCode();

            return SkillIdHashCode ^ SkillNameHashCode;
        }
    }
}
