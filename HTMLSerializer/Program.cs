
using HTMLSerializer;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Reflection;

//using System.Text.Json;


var html = await Load("https://learn.malkabruk.co.il/");

//Build the tree:
var htmlTree = Serialize.ParseHtml(html);

//Serialize.PrintTree(htmlTree);
//queryStrings:
string queryString0 = "i.fa";//45 results
string queryString1 = "nav#menu.slideout-menu";//1 results
string queryString2 = "div .category";//only 1 result

//selector:


//var elementsList = htmlTree.FindBySelector(selector);
//Console.WriteLine(elementsList.Count());
//PrintElementsAsHtml(elementsList);

IEnumerable<HtmlElement> elements = htmlTree.Descendants();

string selectorex = "div .home-container header.home-navbar-interactive  #profile-menu a";
//string selector = "div#profile-menu";
//string selector = ".home-hero-heading";
//string selector1 = " div .home-hero-heading.heading1";
//PrintTree(rootElement, 0);

var selector = Selector.ParseQuery("div.home-container");
List<HtmlElement> nodes = HtmlElement.Search(htmlTree, selector, new List<HtmlElement>());
PrintElementsAsHtml(nodes);
Console.ReadLine();
async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

static void PrintElementsAsHtml(IEnumerable<HtmlElement> elements)
{
    foreach (var element in elements)
    {
        Console.WriteLine($"<{element.Name} id=\"{element.Id}\" class=\"{string.Join(" ", element.Classes)}\">");
        Console.WriteLine(element.InnerHtml);
        Console.WriteLine($"</{element.Name}>");
    }
}


////var htmlClear = new Regex("//s").Replace(html,"");

//var cleanHtml = new Regex("\\s+").Replace(html, " ");
//var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0).ToList();

//htmlLines.RemoveAll(h => h == "\n");
//var htmlElement = "<div id=\"my-id\" class=\"my-class1 my-class2\" width=\"100%\">text</div>";
//var attribute = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlElement);


