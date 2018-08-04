namespace Jarser.Parser
{

    public interface IProfileParser
    {
        T ParseObject<T>(string htmlString);
    }
}
