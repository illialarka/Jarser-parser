using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Jarser.RegexSettings
{
    public class RegexGetter : IRegexGetter
    {
        private readonly string _documentPath;

        private Dictionary<string, IEnumerable<string>> _cache;

        public RegexGetter()
        {
            _documentPath = @".\settings\regex_set.xml";
            _cache = new Dictionary<string, IEnumerable<string>>();
        }

        public IEnumerable<string> GetRegexesByName(string nameOfRegex)
        {
            if (string.IsNullOrEmpty(nameOfRegex))
            {
                throw new ArgumentNullException(nameof(nameOfRegex));
            }

            IEnumerable<string> outputRegexes;

            if (_cache.ContainsKey(nameOfRegex))
            {
                _cache.TryGetValue(nameOfRegex, out outputRegexes);
                return outputRegexes;
            }

            var regexesElement = XElement.Load(_documentPath);

            outputRegexes = regexesElement.Elements("regex")
                .Where(elem => elem.Attributes()
                                   .FirstOrDefault(atr => atr.Name == "name" && atr.Value == nameOfRegex) != null)
                .Select(elem => elem.Value)
                .ToList();

            _cache.Add(nameOfRegex, outputRegexes);

            return outputRegexes;
        }

        public string GetRegexWithAnd(string nameOfRegex, bool withCache = true)
        {
            if (string.IsNullOrEmpty(nameOfRegex))
            {
                throw new ArgumentNullException(nameof(nameOfRegex));
            }

            var regexes = GetRegexesByName(nameOfRegex);
            var regex = CompositeRegexes(regexes, RegexConditional.And);

            return regex;
        }

        public string GetRegexWithOr(string nameOfRegex, bool withCache = true)
        {
            if (string.IsNullOrEmpty(nameOfRegex))
            {
                throw new ArgumentNullException(nameof(nameOfRegex));
            }

            var regexes = GetRegexesByName(nameOfRegex);
            var regex = CompositeRegexes(regexes, RegexConditional.Or);

            return regex;
        }

        private string CompositeRegexes(IEnumerable<string> regexes, RegexConditional conditional)
        {
            if (regexes == null)
            {
                throw new ArgumentNullException(nameof(regexes));
            }

            var compositeSymbol = conditional == RegexConditional.Or ? '|' : '&';
            var outputRegex = new StringBuilder();

            foreach (var regex in regexes)
            {
                var regexString = new StringBuilder();
                regexString.Append("(");
                regexString.Append(regex);
                regexString.Append(")");
                regexString.Append(compositeSymbol);
                outputRegex.Append(regexString);
            }

            outputRegex.Remove(outputRegex.Length - 1, 1);

            return outputRegex.ToString();
        }
    }
}
