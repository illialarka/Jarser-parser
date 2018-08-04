namespace Jarser.Export
{
    public class ExportSettings
    {
        public ExportSettings(
            bool biography = false,
            bool blockedByView = false,
            bool countryBlock = false,
            bool externalUrl = false,
            bool externalUrlShimmed = false,
            bool followers = false,
            bool following = false,
            bool fullName = false,
            bool id = false,
            bool isPrivate = false,
            bool isVerified = false,
            bool profilePictureUrl = false,
            bool profilePictureHdUrl = false,
            bool userName = false,
            bool posts = false,
            bool phoneNumber = false)
        {
            Biography = biography;
            BlockedByView = blockedByView;
            CountryBlock = countryBlock;
            ExternalUrl = externalUrl;
            ExternalUrlShimmed = externalUrlShimmed;
            Followers = followers;
            Following = following;
            FullName = fullName;
            Id = id;
            IsPrivate = isPrivate;
            IsVerified = isVerified;
            ProfilePictureUrl = profilePictureUrl;
            ProfilePictureHdUrl = profilePictureHdUrl;
            UserName = userName;
            Posts = posts;
            PhoneNumber = phoneNumber;
        }

        public bool Biography { get; }

        public bool BlockedByView { get; }

        public bool CountryBlock { get; }

        public bool ExternalUrl { get; }

        public bool ExternalUrlShimmed { get; }

        public bool Followers { get; }

        public bool Following { get; }

        public bool FullName { get; }

        public bool Id { get; }

        public bool IsPrivate { get; }

        public bool IsVerified { get; }

        public bool ProfilePictureUrl { get; }

        public bool ProfilePictureHdUrl { get; }

        public bool UserName { get; }

        public bool Posts { get; }

        public bool PhoneNumber { get; }
    }
}
