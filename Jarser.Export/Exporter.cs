using System;
using System.Collections.Generic;
using Jarser.Parser.Annotations;
using Jarser.Parser.User;

namespace Jarser.Export
{
    public class Exporter : IExporter
    {
        private readonly string _exportPath;

        private readonly ISpecificExporter _specificExporter;

        public Exporter([NotNull] string exportPath, [NotNull] ExportSettings exportSettings, [NotNull] ISpecificExporter specificExporter)
        {
            Settings = exportSettings;
            _exportPath = exportPath;
            _specificExporter = specificExporter;
        }

        public ExportSettings Settings { get; }

        public void DoExport(IEnumerable<User> users)
        {
            _specificExporter.ExportTo(users, Settings, _exportPath);
        }
    }
}
