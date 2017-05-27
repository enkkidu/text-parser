using Nordea.Models.Enum;
using Nordea.Utils.Interfaces;
using Nordea.Service.Interfaces;
using System;
using System.Linq;

namespace Nordea.Service
{
    public class TextService : ITextService
    {
        private ITextUtil _textUtil;

        public TextService(ITextUtil textUtil)
        {
            _textUtil = textUtil;
        }

        public string Parse(string text, ResultType resultType)
        {
            var textDetails = _textUtil.SeparateTextDetails(text);
            if (!textDetails.Sentences.Any())
            {
                throw new Exception("No sentence found. Remember the sentence needs to end with . ! ?");
            }

            switch (resultType)
            {
                case ResultType.CSV:
                    return _textUtil.TextDetailsToCSV(textDetails);
                case ResultType.XML:
                    return _textUtil.TextDetailsToXml(textDetails);
                default:
                    return string.Empty;
            }
        }
    }
}