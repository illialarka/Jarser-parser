using System.Collections.Generic;
using Jarser.Parser.User;

namespace Jarser.Export
{
    public interface ISpecificExporter
    {
        void ExportTo(IEnumerable<User> users, ExportSettings exportSettings, string path);
    }
}