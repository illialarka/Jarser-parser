using System.Collections.Generic;
using Jarser.Parser.User;

namespace Jarser.Export
{
    public interface IExporter
    {
        ExportSettings  Settings { get; }

        void DoExport(IEnumerable<User> users);
    }
}