using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTMLSerializer
{
    public class Serialize
    {
        public static HtmlElement ParseHtml(string html)
        {
            
            var clearHtml = new Regex(@"[\r\n]+").Replace(html, "");
            var htmlLines = new Regex("<(.*?)>").Split(clearHtml).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            int keywordIndex = htmlLines.FindIndex(s => s.StartsWith("html"));
            htmlLines.RemoveRange(0, keywordIndex + 1);

            HtmlElement rootElement = new HtmlElement { Name = "html", Children = new List<HtmlElement>(), Parent = null };
            HtmlElement currentElement = rootElement;
            int currentIndex = 0;

            while (currentIndex < htmlLines.Count)
            {
                string line = htmlLines[currentIndex];
                var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(line).Cast<Match>()
                    .Select(match => match.Value).ToList();
                if (line.StartsWith("!--"))
                {
                    while (!line.Contains("-->"))
                    {
                        currentIndex++;
                        line = htmlLines[currentIndex];
                    }
                    continue;
                }

                int allLine = line.IndexOf(' ');
                string name, id = null, clas = null;
                List<string> classes = null;

                if (allLine == -1)
                {
                    name = line;
                }
                else
                    name = line.Substring(0, line.IndexOf(" "));

                if (attributes.Count > 0)
                {
                    id = attributes.FirstOrDefault(attr => attr.StartsWith("id"));
                    if (id != null)
                    {
                        attributes.RemoveAll(attr => attr == id);
                        id = id.Substring(4);
                        id = id.Replace("\"", string.Empty);
                    }

                    clas = attributes.FirstOrDefault(attr => attr.StartsWith("class"));
                    if (clas != null)
                    {
                        attributes.RemoveAll(attr => attr == clas);
                        clas = clas.Substring(7);
                        clas = clas.Replace("\"", string.Empty);
                        classes = clas.Split(" ").ToList();
                    }
                }

                if (line == "/html")
                {
                    break;
                }
                else if (line.StartsWith("/"))
                {
                    if(currentElement != null) {
                    }
                    
                }

                else if (!HtmlHelper.Instance.IsHtmlTag(name))
                {
                    currentElement.InnerHtml = (string)line;
                }
                else if (HtmlHelper.Instance.IsSelfClosingTag(name))
                {
                    HtmlElement child = new HtmlElement
                    {
                        Name = name,
                        Id = id,
                        Classes = classes,
                        Attributes = attributes,
                        Parent = currentElement
                    };
                    currentElement.Children.Add(child);
                }
                else
                {
                    HtmlElement child = new HtmlElement
                    {
                        Name = name,
                        Id = id,
                        Classes = classes,
                        Attributes = attributes,
                        Parent = currentElement,
                        Children = new List<HtmlElement>()
                    };


                    currentElement.Children.Add(child);
                    currentElement = child;
                }

                currentIndex++;
            }
            return rootElement;
        }



        //public static void PrintTree(HtmlElement element, int depth = 0)
        //{
        //    if (element == null)
        //        return;

        //    Console.Write(new string(' ', depth * 2));
        //    Console.Write($"<{element.Name}");

        //    // Print attributes
        //    foreach (var attribute in element.Attributes)
        //    {
        //        Console.Write($" {attribute.Name}=\"{attribute.Value}\"");
        //    }

        //    Console.Write(">");

        //    // Print inner text
        //    if (!string.IsNullOrEmpty(element.InnerHtml))
        //    {
        //        Console.Write(element.InnerHtml.Trim());
        //    }

        //    Console.WriteLine();

        //    // Recursively print children
        //    foreach (var child in element.Children)
        //    {
        //        PrintTree(child, depth + 1);
        //    }

        //    Console.Write(new string(' ', depth * 2));
        //    Console.WriteLine($"</{element.Name}>");
        //}

    }

}

