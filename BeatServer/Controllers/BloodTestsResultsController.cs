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


        public List<BloodTest> getBloodTestsResults([FromUri] int userId)
        {
            var path = HttpContext.Current.Server.MapPath(@"~/xml/test.xml");
            XmlTextReader reader = new XmlTextReader(path);
            try
            {
                BloodTest bt = new BloodTest();
                List<int> lacksList = new List<int>();
                while (reader.Read())
                {
                    string temp = "";

                    reader.MoveToContent();
                    if (reader.NodeType == XmlNodeType.Element)
                        temp = reader.Name;
                    if (reader.NodeType == XmlNodeType.Element)
                    {
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

                            if (bt.value < bt.min_val)
                            {
                                int NutrientId = EntitiesManager.getInstance().GetNutrientIdByName(bt.name);

                                if (NutrientId != -1)
                                {
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

                EntitiesManager.getInstance().SaveBloodResultsLacks(lacksList, userId);
            }
            catch (Exception ex)
            {
                //add an exeption to the result
            }
            
            return contents;
        }

    }
}