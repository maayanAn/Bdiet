using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using BeatServer.Models;
using System.Web.Http;
using BeatServer.Managers;
using System.Web.Http.Description;

namespace BeatServer.Controllers
{
    [RoutePrefix("api/BloodTestsResults")]
    public class BloodTestsResultsController : ApiController
    {
        public List<BloodTest> contents = new List<BloodTest>();

        // Read blood test results from the xml file and send it to the client
        public List<BloodTest> getBloodTestsResults([FromUri] int UserId)
        {
            var path = HttpContext.Current.Server.MapPath(@"~/xml/test.xml");
            XmlTextReader reader = new XmlTextReader(path);
            try
            {
                BloodTest bt = new BloodTest();

                List<int> lacksList = new List<int>();

                // Parsing the xml 
                while (reader.Read())
                {
                    string temp = "";

                    reader.MoveToContent();
                    if (reader.NodeType == XmlNodeType.Element)
                        temp = reader.Name;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        // Read the blood test results 
                        if (temp == "VALUE")
                            bt.value = int.Parse(reader.ReadString());
                        else if (temp == "NAME")
                            bt.name = reader.ReadString();
                        else if (temp == "MIN_VAL")
                            bt.min_val = int.Parse(reader.ReadString());
                        else if (temp == "MAX_VAL")
                        {
                            bt.max_val = int.Parse(reader.ReadString());
                            contents.Add(bt);

                            // Check for lacks in the blood test results
                            if (bt.value < bt.min_val)
                            {
                                int NutrientId = EntitiesManager.getInstance().GetNutrientIdByName(bt.name);

                                if (NutrientId != -1)
                                {
                                    // Add the lack to the list
                                    lacksList.Add(NutrientId);
                                }
                                
                            }
                            else if(bt.name == "total cholesterol" && bt.value > bt.max_val)
                            {
                                int NutrientId = EntitiesManager.getInstance().GetNutrientIdByName(bt.name);

                                if (NutrientId != -1)
                                {
                                    lacksList.Add(NutrientId);
                                }
                            }

                            bt = new BloodTest();
                        }
                    }

                }

                // Save the user lacks in the user table
                EntitiesManager.getInstance().SaveBloodResultsLacks(lacksList, UserId);
            }
            catch (Exception ex) {}
            
            // Return the blood test to the user
            return contents;
        }

    }
}