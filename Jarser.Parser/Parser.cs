//-----------------------------------------------------------------------
// <copyright file="Parser.cs" company="Jarser Enterprises">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Larka Ilya</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Jarser.Logger;
using Jarser.WebCommunication;

namespace Jarser.Parser
{
    public class Parser : IParser
    {
        private readonly string _htmlString;
        private readonly ILogger _logger = new Logger.Logger(typeof(Parser));
        private bool _cancel;

        public Parser(string htmlString = null)
        {
            _htmlString = htmlString;
        }

        public EventHandler<ParserProcessEventArgs> ProcessParse { get; set; }
        public int Delay { get; set; }

        public IEnumerable<User.User> ParseFollowers(IEnumerable<string> nicknames = null)
        {
            var listOut = new List<User.User>();
            try
            {
                if (nicknames == null)
                {
                    var listNickNames = Regex
                        .Matches(_htmlString, "(?<=title=\")\\w+(?=\" class=\"FPmhX notranslate zsYNt \")")
                        .Cast<Match>()
                        .Select(m => m.Value)
                        .ToList();

                    _logger.Info($"Find {listNickNames.Count} nicknmes.");

                    foreach (var nickName in listNickNames)
                    {
                        Thread.Sleep(Delay);
                        using (var htmlSelector = new HtmlSelector(new Uri("https://www.instagram.com/" + nickName)))
                        {
                            if (_cancel)
                            {
                                break;
                            }

                            var htmlString = htmlSelector.Run();
                            var user = new ProfileParser().ParseObject<User.User>(htmlString);

                            listOut.Add(user);
                            OnProcessParser(new ParserProcessEventArgs(listNickNames.Count, listOut.Count));
                        }
                    }

                    _logger.Info($"Result list of users is {listOut.Count}.");
                }
                else
                {
                    var listNickNames = nicknames.ToList();

                    foreach (var nickname in listNickNames)
                    {
                        Thread.Sleep(Delay);
                        using (var htmlSelector = new HtmlSelector(new Uri("https://www.instagram.com/" + nickname)))
                        {
                            if (_cancel)
                            {
                                break;
                            }

                            var htmlString = htmlSelector.Run();
                            var user = new ProfileParser().ParseObject<User.User>(htmlString);

                            listOut.Add(user);

                            OnProcessParser(new ParserProcessEventArgs(listNickNames.Count(), listOut.Count));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Exception(e);
            }

            return listOut;
        }

        public Task<IEnumerable<User.User>> ParseFollowersAsync(IEnumerable<string> nicknames = null)
        {
            var task = Task.Run(() => ParseFollowers(nicknames));
            return task;
        }

        public void Cancel()
        {
            _cancel = true;
        }

        public void OnProcessParser(ParserProcessEventArgs args)
        {
            ProcessParse?.Invoke(this, args);
        }
    }
}
