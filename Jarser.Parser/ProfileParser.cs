//-----------------------------------------------------------------------
// <copyright file="ProfileParser.cs" company="Jarser Enterprises">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Larka Ilya</author>
//-----------------------------------------------------------------------

using System;
using System.Text;
using System.Text.RegularExpressions;
using Jarser.Logger;
using Jarser.Parser.Annotations;
using Newtonsoft.Json;

namespace Jarser.Parser
{
    /// <summary>
    /// This class responsible of parsing input html string and selection information by settings.
    /// </summary>
    public sealed class ProfileParser : IProfileParser
    {
        private readonly ILogger _logger = new Logger.Logger(typeof(ProfileParser)); 

        /// <summary>
        /// Parse object from html string to instance type of <see cref="T"/> class.
        /// </summary>
        /// <typeparam name="T">Type of result object.</typeparam>
        /// <param name="htmlString">Input html string which consists informationa about user.</param>
        /// <returns>Instance of <see cref="T"/> class. It consists information.</returns>
        public T ParseObject<T>(string htmlString)
        {
            T parsedObject = default(T);
            try
            {
                if (string.IsNullOrEmpty(htmlString))
                {
                    return default(T);
                }

                var userJsonString = GetUserJsonString(htmlString);
                parsedObject = JsonConvert.DeserializeObject<T>(userJsonString.ToString());
            }
            catch (Exception e)
            {
                _logger.Exception(e);
            }

            return parsedObject;
        }

        /// <summary>
        /// Selects the json string with information about user.
        /// </summary>
        /// <param name="htmlString">The html string of page.</param>
        /// <returns>Json string with user.</returns>
        private StringBuilder GetUserJsonString([NotNull] string htmlString)
        {
            if (string.IsNullOrEmpty(htmlString))
            {
                return null;
            }

            StringBuilder userJsonString = new StringBuilder(string.Empty);
            try
            {
                var regexUserString = new Regex("(?<=\"user\":){.+(?=,\"connected_fb_page)");

                var userStringMatch = regexUserString.Match(htmlString);
                userJsonString = new StringBuilder(userStringMatch.Value);

                userJsonString.Replace("{\"count\":", string.Empty);
                userJsonString.Replace("}", string.Empty);
                userJsonString.Append("}");

                _logger.Info("Select and compose json string from html string.");

                return userJsonString;
            }
            catch (Exception e)
            {
                _logger.Exception(e);
            }

            return userJsonString;
        }
    }
}
