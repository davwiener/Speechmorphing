using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
namespace SpeechmorphingHomeAssignment.Model
{

    public class JsonCompare 
    {
        //will hold the values and keys in a JSON
        public Dictionary<string, object> JTokenDic { get; set; }
        //string error will be presented i view
        public string StringError { get; set; }
        public List<int> IDList { get; set; } = new List<int>();
        bool notIdDiff = false;
        public JsonCompare(string path) : base()
        {
            if (File.Exists(path))
            {
                JTokenDic = new Dictionary<string, object>();
                using (StreamReader r = new StreamReader(path))
                {
                    var json = r.ReadToEnd();
                    var jobj = JObject.Parse(json);
                    BuildJsonDic("", jobj);
                }
            }
            else
            {
                StringError = "File" + path + "don't Exsits";
            }
        }
        //Recursive function. Wiil bult enter all the keys and valus in the JSON file to JTokenDic.
        void BuildJsonDic(string currName, JObject jobj)
        {
            try
            {
            //Check all proprties of te current jobj
            foreach (var jtoken in jobj.Properties())
            {
                string name = currName + jtoken.Name;
                //We dont want to parse null values but null value will be added to JTokenDic.
                if (jtoken.Value != null)
                {
                    JTokenType type = ((JToken)jtoken.Value).Type;
                    //check the type 
                    switch (type)
                    {
                        case JTokenType.Object:
                            int i = 0;
                            foreach (var token in jtoken.Value.Children())
                            {
                                BuildJsonDic(name + "[" + i.ToString() + "].", token as JObject);
                                i++;
                            }
                            break;
                        case JTokenType.Array:
                            int j = 0;
                            //if its array Will need to check all its children
                            foreach (var token in jtoken.Value.Children())
                            {
                                if (token is JObject)
                                {
                                    BuildJsonDic(name + "[" + j.ToString() + "].", token as JObject);
                                }
                                else
                                {
                                    JTokenDic.Add(name + "[" + j.ToString() +"]", token);
                                    if (jtoken.Name.ToLower() == "id")
                                    {
                                        try
                                        {
                                            int id;
                                            int.TryParse(jtoken.Value.ToString(), out id);
                                            IDList.Add(id);
                                        }
                                        catch (Exception e)
                                        {
                                            StringError = "sotmtjing went wrong parsing id " + e.Message;
                                        }

                                    }
                                }
                                j++;
                            }
                            break;
                        default:
                            JTokenDic.Add(name, jtoken.Value);
                            if(jtoken.Name.ToLower()=="id")
                            {
                                try
                                {
                                    int id;
                                    int.TryParse(jtoken.Value.ToString(), out id);
                                    IDList.Add(id);
                                }
                                catch (Exception e)
                                {
                                    StringError = "sotmtjing went wrong parsing id " + e.Message;
                                }
                                
                            }
                            break;
                    }
                }
                else
                {
                    JTokenDic.Add(name, jtoken);
                }

            }
            }
            catch (Exception e)
            {
                StringError = "somthing went wrong: " + e.Message;
            }
        }
    
        public string CompareTo(JsonCompare other,bool firstJson)
        {
            try
            {
                string ret = string.Empty;
                foreach (string key in JTokenDic.Keys)
                {
                    object value = JTokenDic[key];
                    if (other.JTokenDic.ContainsKey(key))
                    {
                        object otherValue = other.JTokenDic[key];
                        //only in the first json will be checking value diffrences
                        if (firstJson)
                        {
                            //checking if numbers are equal
                            if (Utils.IsNumber(value) && Utils.IsNumber(otherValue))
                            {
                                if (!Utils.IsNumbersAreEquals(value, otherValue))
                                {
                                    if (key.Split('.')[key.Split('.').Length - 1].ToLower() != "id")//if not diffreance may be offset diffrence
                                    {
                                        notIdDiff = true;
                                    }//else id diff will be check later
                                    ret += "differns valuse in key :" + key + " the first json have a value: " + (value.ToString().Length == 0 ? "null" : value.ToString()) + " while the second json have a value of: " + otherValue.ToString() + "\r\n";
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            //checking if nulls ans zeros
                            if (Utils.IsNullOrZero(value) && Utils.IsNullOrZero(otherValue))
                            {
                                continue;
                            }
                            if (!value.Equals(otherValue))
                            {
                                if (key.Split('.')[key.Split('.').Length - 1].ToLower() != "id")//if not diffreance may be offset diffrence
                                {
                                    notIdDiff = true;
                                }
                                //else id diff will be check later
                                ret += "differns valuse in key :" + key + " the first json have a value: " + (value.ToString().Length == 0? "null" : value.ToString())+ " while the second json have a value of: " + otherValue.ToString() + "\r\n";

                            }
                        }
                    }
                    else
                    {
                        notIdDiff = true;
                        ret += (firstJson ? "first" : "second") + "json have a key :" + key + " and a value of: " + value + " while " + (firstJson ? "second" : "first") + "json dosen't contain the key and value \r\n";
                    }
                }
                if (firstJson && (!notIdDiff) && ret != string.Empty)//id's are diffrent
                {
                    //check if the tow id's list have the same length and are not empty
                    if (IDList.Count == other.IDList.Count && IDList.Count > 0)
                    {
                        int offset = IDList[0] - other.IDList[0];
                        //check if all the id's have the same offset
                        for (int i = 1; i < IDList.Count; i++)
                        {
                            if (IDList[i] - other.IDList[i] != offset)
                            {
                                return (ret + "id's of the tow Json's dosen't have the same offset");
                            }
                        }
                        ret = string.Empty; // all the id's have the same offset
                    }
                    else
                    {
                        ret += "id's of the tow Json's dosen't have the same numbers of id's";
                    }

                }
                return ret;
            }
            catch(Exception e)
            {
                StringError = "somthing went wrong: " + e.Message;
                return StringError;
            }

        }

    }

}