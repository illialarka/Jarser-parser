using System;
using Jarser.Parser.Annotations;

namespace Jarser.Parser.Sctipts
{
    /// <summary>
    /// This is an interface for providing scripts
    /// </summary>
    public interface IScriptsProvider
    {
        /// <summary>
        /// Returns script text.
        /// </summary>
        /// <param name="path">The pathe to file with script text.</param>
        /// <returns>Text of script.</returns>
        string SelectScript([NotNull] Uri path);
    }
}
