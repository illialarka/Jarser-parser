using System.Collections.Generic;
using System.IO;
using System.Text;
using Jarser.Parser.User;

namespace Jarser.Export
{
    public class CSVExporter : ISpecificExporter
    {
        private char _separator = ',';
        public void ExportTo(IEnumerable<User> users, ExportSettings exportSettings, string path)
        {
            var csvContent = new StringBuilder();

            if (exportSettings.Id)
            {
                csvContent.Append("Id" + _separator);
            }

            if (exportSettings.UserName)
            {
                csvContent.Append("User name" + _separator);
            }

            if (exportSettings.Followers)
            {
                csvContent.Append("Followers" + _separator);
            }

            if (exportSettings.Following)
            {
                csvContent.Append("Following" + _separator);
            }

            if (exportSettings.Biography)
            {
                csvContent.Append("Biography" + _separator);
            }

            if (exportSettings.FullName)
            {
                csvContent.Append("Full name" + _separator);
            }

            if (exportSettings.IsPrivate)
            {
                csvContent.Append("Is private" + _separator);
            }

            if (exportSettings.BlockedByView)
            {
                csvContent.Append("BlockedByView" + _separator);
            }

            if (exportSettings.CountryBlock)
            {
                csvContent.Append("CountryBlock" + _separator);
            }

            if (exportSettings.ExternalUrl)
            {
                csvContent.Append("ExternalUrl" + _separator);
            }

            if (exportSettings.ExternalUrlShimmed)
            {
                csvContent.Append("ExternalUrlShimmed" + _separator);
            }

            if (exportSettings.IsVerified)
            {
                csvContent.Append("Is verified" + _separator);
            }

            if (exportSettings.ProfilePictureUrl)
            {
                csvContent.Append("Link to profile picture" + _separator);
            }

            if (exportSettings.ProfilePictureHdUrl)
            {
                csvContent.Append("Link to profile HD picture" + _separator);
            }

            if (exportSettings.PhoneNumber)
            {
                csvContent.Append("Phone number" + _separator);
            }

            csvContent.AppendLine();

            foreach (var user in users)
            {
                if (exportSettings.Id)
                {
                    var formated = FormatToCSV(user.Id);
                    csvContent.Append(formated);
                }

                if (exportSettings.UserName)
                {
                    var formated = FormatToCSV(user.UserName);
                    csvContent.Append(formated);
                }

                if (exportSettings.Followers)
                {
                    var formated = FormatToCSV(user.Followers.ToString());
                    csvContent.Append(formated);
                }

                if (exportSettings.Following)
                {
                    var formated = FormatToCSV(user.Following.ToString());
                    csvContent.Append(formated);
                }

                if (exportSettings.Biography)
                {
                    var formated = FormatToCSV(user.Biography);
                    csvContent.Append(formated);
                }

                if (exportSettings.FullName)
                {
                    var formated = FormatToCSV(user.FullName);
                    csvContent.Append(formated);
                }

                if (exportSettings.IsPrivate)
                {
                    var formated = FormatToCSV(user.IsPrivate.ToString());
                    csvContent.Append(formated);
                }

                if (exportSettings.BlockedByView)
                {
                    var formated = FormatToCSV(user.BlockedByView.ToString());
                    csvContent.Append(formated);
                }

                if (exportSettings.CountryBlock)
                {
                    var formated = FormatToCSV(user.CountryBlock.ToString());
                    csvContent.Append(formated);
                }

                if (exportSettings.ExternalUrl)
                {
                    var formated = FormatToCSV(user.ExternalUrl);
                    csvContent.Append(formated);
                }

                if (exportSettings.ExternalUrlShimmed)
                {
                    var formated = FormatToCSV(user.ExternalUrlShimmed);
                    csvContent.Append(formated);
                }

                if (exportSettings.IsVerified)
                {
                    var formated = FormatToCSV(user.IsVerified.ToString());
                    csvContent.Append(formated);
                }

                if (exportSettings.ProfilePictureUrl)
                {
                    var formated = FormatToCSV(user.ProfilePictureUrl);
                    csvContent.Append(formated);
                }

                if (exportSettings.ProfilePictureHdUrl)
                {
                    var formated = FormatToCSV(user.ProfilePictureHdUrl);
                    csvContent.Append(formated);
                }

                if (exportSettings.PhoneNumber)
                {
                    var formated = FormatToCSV(user.PhoneNumber);
                    csvContent.Append(formated);
                }

                csvContent.AppendLine();
            }

            File.AppendAllText(path, csvContent.ToString(), Encoding.UTF8);
        }

        private string FormatToCSV(string input)
        {
            return input.Replace("\n", " ").Replace(",", string.Empty) + _separator;
        }
    }
}