using System;

namespace Jarser.Parser
{
    public class ParserProcessEventArgs : EventArgs
    {
        public ParserProcessEventArgs(int totalItem, int processedItem)
        {
            TotalItem = totalItem;
            ProcessedItem = processedItem;
        }

        public int TotalItem { get; }

        public int ProcessedItem { get; }
    }
}
