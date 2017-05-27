using Nordea.Models.Enum;

namespace Nordea.Service.Interfaces
{
    public interface ITextService
    {
        string Parse(string text, ResultType resultType);
    }
}