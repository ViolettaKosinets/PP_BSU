using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ProjectPP
{
    public class Xml
    {
        public static void read(string xmlfileName, List<string> res)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlfileName);

            XmlElement? xRoot = xDoc.DocumentElement;
            if (xRoot != null)
            {
                foreach (XmlElement xnode in xRoot)
                {
                    XmlNode? attr = xnode.Attributes.GetNamedItem("exp");
                    string expression = attr.OuterXml;
                    res.Add(expression);
                }
            }
        }

        public static void write(string filename, List<double> res)
        {
            XmlWriter xmlWriter = XmlWriter.Create(filename);

            int i = 0;
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("result");
            while (i < res.Count)
            {
                xmlWriter.WriteStartElement("res");
                xmlWriter.WriteAttributeString("answer", res[i].ToString());
                xmlWriter.WriteEndElement();
                i++;
            }

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }
}