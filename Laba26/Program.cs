
using System.Xml;

var result = Calculation();
Console.WriteLine($"Результат: {result}");

SaveToXml(result);

double Calculation()
{
    (int x, int y) = LoadFromXml();
    return 3 * Math.Sin(x) + 2 * Math.Cos(y);
}

(int, int) LoadFromXml()
{
    XmlDocument doc = new XmlDocument();
    doc.Load("../../../parameters.xml");

    var xNode = doc.SelectSingleNode("/Parameters/X");
    int x = 0, y = 0;
    if (xNode != null)
        x = int.Parse(xNode.InnerText);

    var yNode = doc.SelectSingleNode("/Parameters/Y");
    if (yNode != null)
        y = int.Parse(yNode.InnerText);

    return (x, y);
}

void SaveToXml(double result)
{
    XmlDocument doc = new XmlDocument();

    XmlElement parametersNode = doc.CreateElement("Results");
    doc.AppendChild(parametersNode);

    XmlElement xNode = doc.CreateElement("R");
    xNode.InnerText = result.ToString();
    parametersNode.AppendChild(xNode);

    doc.Save("../../../result.xml");
}