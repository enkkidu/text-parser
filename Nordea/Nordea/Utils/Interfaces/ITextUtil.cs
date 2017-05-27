using Nordea.Models.Utils;

namespace Nordea.Utils.Interfaces
{
    public interface ITextUtil
    {
        string TextDetailsToXml(TextDetails textDetails);
        string TextDetailsToCSV(TextDetails textDetails);
        TextDetails SeparateTextDetails(string text);
    }
}