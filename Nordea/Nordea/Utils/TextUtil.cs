using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Nordea.Models.Utils;
using Nordea.Utils.Interfaces;

namespace Nordea.Utils
{
    public class TextUtil : ITextUtil
    {
        private const string SentenceRegexPattern = @"[a-zA-Z\-,'\s]+[.?!]";
        private const string WordRegexPattern = @"[^\W\d](\w|[-']{1}(?=\w))*";
        private const string SpecialCharactersRegexPattern = "\t|\n|\r|\"";

        //Normally I would use XmlSerializer, but to have same result I use this
        public string TextDetailsToXml(TextDetails textDetails)
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, settings))
                {
                    writer.WriteProcessingInstruction("xml", "\"version=1.0\" encoding=\"UTF-8\" standalone=\"yes\"");
                    writer.WriteStartElement("text");
                    foreach (var sentence in textDetails.Sentences)
                    {
                        writer.WriteStartElement("sentence");
                        foreach (var word in sentence.Words)
                        {
                            writer.WriteStartElement("word");
                            writer.WriteValue(word);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                }
                return stringWriter.ToString();
            }
        }

        public string TextDetailsToCSV(TextDetails textDetails)
        {
            var stringBuilder = new StringBuilder();
            if (textDetails.Sentences.Any())
            {
                var maxWordCount = textDetails.Sentences.Max(x => x.Words.Count);

                for (var i = 1; i <= maxWordCount; i++)
                {
                    stringBuilder.Append(",Word ");
                    stringBuilder.Append(i);
                }
                stringBuilder.AppendLine();

                for (var i = 0; i < textDetails.Sentences.Count; i++)
                {
                    stringBuilder.Append("Sentence ");
                    stringBuilder.Append(i + 1);
                    foreach (var word in textDetails.Sentences[i].Words)
                    {
                        stringBuilder.Append(", ");
                        stringBuilder.Append(word);
                    }
                    stringBuilder.AppendLine();
                }
            }
            return stringBuilder.ToString();
        }

        public TextDetails SeparateTextDetails(string text)
        {
            var result = new TextDetails();
            text = Regex.Replace(text, SpecialCharactersRegexPattern, string.Empty);

            var sentenceRegex = new Regex(SentenceRegexPattern);
            var wordRegex = new Regex(WordRegexPattern);

            result.Sentences = new List<Sentence>();
            List<string> words;
            foreach (Match sentenceMatch in sentenceRegex.Matches(text))
            {
                words = new List<string>();
                foreach (Match wordMatch in wordRegex.Matches(sentenceMatch.Value))
                {
                    words.Add(wordMatch.Value);
                }
                words.Sort();
                result.Sentences.Add(new Sentence { Words = words });
            }

            return result;
        }
    }
}