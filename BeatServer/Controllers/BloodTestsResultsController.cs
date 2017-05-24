using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using BeatServer.Models;
using System.Web.Http;

namespace BeatServer.Controllers
{
    [RoutePrefix("api/BloodTestsResults")]
    public class BloodTestsResultsController : ApiController
    {
        List<BloodTest> contents = new List<BloodTest>();
        public List<BloodTest> getBloodTestsResults(User u)
        {
            var path = HttpContext.Current.Server.MapPath(@"~/xml/test.xml");
            XmlTextReader reader = new XmlTextReader(path);
            try
            {
                BloodTest bt = new BloodTest();
                while (reader.Read())
                {
                    string temp = "";

                    reader.MoveToContent();
                    if (reader.NodeType == XmlNodeType.Element)
                        temp = reader.Name;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (temp == "VALUE")
                            bt.value = reader.ReadString();
                        else if (temp == "COMPONENT_ENGLISH_NAME")
                            bt.component_name_english = reader.ReadString();
                        else if (temp == "MIN_VAL")
                            bt.min_val = int.Parse(reader.ReadString());
                        else if (temp == "MAX_VAL")
                        {
                            bt.max_val = int.Parse(reader.ReadString());
                            contents.Add(bt);
                            bt = new BloodTest();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //add an exeption to the result
            }
            return contents;
        }

    }
}