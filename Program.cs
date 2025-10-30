using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "Hotels.xml";
        public static string xmlErrorURL = "HotelsErrors.xml";
        public static string xsdURL = "Hotels.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {


            //return "No Error" if XML is valid. Otherwise, return the desired exception message.

            try
            {
                // 1️⃣ Configure the validation settings
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(null, xsdUrl);
                settings.ValidationType = ValidationType.Schema;

                string errorMsg = "No Error";  // default

                // 2️⃣ Handle validation errors through an event
                settings.ValidationEventHandler += (sender, e) =>
                {
                    errorMsg = $"Validation Error: {e.Message}";
                };

                // 3️⃣ Create an XML reader that uses the validation settings
                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    // Read through the XML document to trigger validation
                    while (reader.Read()) { }
                }

                // 4️⃣ Return results
                return errorMsg;
            }
            catch (Exception ex)
            {
                // Return any unexpected exceptions (like file not found, parsing error, etc.)
                return $"Exception: {ex.Message}";
            }



        }

        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                // 1️⃣ Load the XML document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlUrl);

                // 2️⃣ Convert XML → JSON
                // This automatically converts elements and nested nodes into JSON
                string jsonText = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented, true);

                // 3️⃣ Return JSON text
                return jsonText;
            }
            catch (Exception ex)
            {
                // Handle any file or parsing exceptions
                return $"Exception: {ex.Message}";
            }

        }

    }
}