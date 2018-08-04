using System.Collections.Generic;

namespace Jarser.RegexSettings
{
    public interface IRegexGetter
    {
        IEnumerable<string> GetRegexesByName(string nameOfRegex);

        string GetRegexWithAnd(string nameOfRegex, bool withCache = true);

        string GetRegexWithOr(string nameOfRegex, bool withCache = true);
    }
}
