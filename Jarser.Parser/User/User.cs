//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Jarser Enterprises">
//     Copyright (c) All rights reserved.
// </copyright>
// <author>Larka Ilya</author>
//-----------------------------------------------------------------------

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Jarser.Parser.Annotations;
using Jarser.RegexSettings;
using Newtonsoft.Json;

namespace Jarser.Parser.User
{
    /// <summary>
    /// The class preresents the user.
    /// </summary>
    public class User : INotifyPropertyChanged
    {
        private string _biography;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty("biography")]
        public string Biography
        {
            get => _biography;
            set
            {
                var regexString = _regexGetter.GetRegexWithOr("phone_number");
                var regex = new Regex(regexString);
                var matches = regex.Matches(value);

                var phoneNumber = new StringBuilder(string.Empty);

                foreach (Match match in matches)
                {
                    phoneNumber.Append(match.Value + " ");
                }

                PhoneNumber = phoneNumber.ToString();

                _biography = value;
            }
        }

        [JsonProperty("blocked_by_viewer")]
        public bool BlockedByView { get; set; }

        [JsonProperty("country_block")]
        public bool CountryBlock { get; set; }

        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }

        [JsonProperty("external_url_linkshimmed")]
        public string ExternalUrlShimmed { get; set; }

        [JsonProperty("edge_followed_by")]
        public long Followers { get; set; }

        [JsonProperty("edge_follow")]
        public long Following { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_private")]
        public bool IsPrivate { get; set; }

        [JsonProperty("is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePictureUrl { get; set; }

        [JsonProperty("profile_pic_url_hd")]
        public string ProfilePictureHdUrl { get; set; }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("edge_owner_to_timeline_media")]
        public long Posts { get; set; }

        public string PhoneNumber { get; set; }

        private RegexGetter _regexGetter = new RegexGetter();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
