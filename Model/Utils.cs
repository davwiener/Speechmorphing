using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechmorphingHomeAssignment.Model
{
    public static class Utils
    {
        private static bool Isfloat(Type t)
        {
           return t == typeof(float) || t == typeof(double);
        }
        private static bool IsInt(Type t)
        {
            return t == typeof(int) || t == typeof(Int32) || t == typeof(Int16) || t == typeof(Int64);
        }
        //check if a object is a number
        public static bool IsNumber(object value)
        {
            try
            {
                JValue val = value as JValue;
                if (val == null||val.Value == null)
                {
                    return false;
                }
                Type t = val.Value.GetType();
                return IsInt(t) || Isfloat(t);
            }
            catch (Exception)//somthing went wrong
            {
                //return false;
                return false;
            }
            
        }
        //check if tow numbers are equal
        public static bool IsNumbersAreEquals(object value, object otherValue)
        {
            try
            {
                JValue val = value as JValue;
                Type t = val.Value.GetType();
                JValue otherVal = otherValue as JValue;
                Type otherT = val.Value.GetType();
                if (IsInt(t) && IsInt(otherT))
                {
                    int x = int.Parse(val.Value.ToString());
                    int y = int.Parse(otherVal.Value.ToString());
                    return x == y;
                }
                if (Isfloat(t) && Isfloat(otherT))
                {
                    float x = float.Parse(val.Value.ToString());
                    float y = float.Parse(otherVal.Value.ToString());
                    return x == y;
                }
                if (Isfloat(t) && IsInt(otherT))
                {
                    float v = float.Parse(val.Value.ToString());
                    int otherV = int.Parse(otherVal.Value.ToString());
                    return Math.Floor(v) == (float)val.Value && (int)v == otherV;
                }
                float otherVal2 = float.Parse(otherVal.Value.ToString());
                int val2 = int.Parse(val.Value.ToString());
                return Math.Floor(otherVal2) == otherVal2 && (int)otherVal2 == val2;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public static bool IsNullOrZero(object value)
        {
            JValue val = value as JValue;
            return val.Value == null || val.ToString() == "0";
        }
    }
}
