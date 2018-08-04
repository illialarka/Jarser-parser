using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jarser.Parser
{
    /// <summary>
    /// This interface represents the parser of profile for selecting.
    /// </summary>
    public interface IParser
    {
        EventHandler<ParserProcessEventArgs> ProcessParse { get; set; }

        int Delay { set; }

        IEnumerable<User.User> ParseFollowers(IEnumerable<string> nicknames = null);

        Task<IEnumerable<User.User>> ParseFollowersAsync(IEnumerable<string> nicknames = null);

        void Cancel();
    }
}