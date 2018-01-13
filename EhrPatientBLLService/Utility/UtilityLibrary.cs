// Created by       : Buddhima Kudagama
// Created on       : 10th january 2018
// Type             : common class
// Description      : contains common properties and methods

using System;
using System.IO;
using System.Xml;
using System.Globalization;
using System.Threading;
using System.Configuration;

namespace Utility
{
    public static class UtilityLibrary
    {
        #region Enums

        /// <summary>
        /// Account types for send orders
        /// </summary>
        public enum AccountType
        {
            TEST,
            LIVE
        }

        /// <summary>
        /// Order by options of Address
        /// </summary>
        public enum AddressOrderBY
        {
            Address1,
            Address2,
            City,
            Def,
            Name,
            PostCode
        }

        public enum ApiResponseType
        {
            XML,
            JSON
        }

        /// <summary>
        /// CRUD mode Insert/Update/Delete
        /// </summary>
        public enum CRUDMode
        {
            INSERT,
            UPDATE,
            DELETE
        }

        #endregion

        #region Get value string

        /// <summary>
        /// Remove special symbols from provided string ex : '(',')'
        /// </summary>
        /// <param name="stringWithSymbols"></param>
        /// <returns></returns>
        public static string GetStringWithoutBrackets(string stringWithSymbols)
        {
            if (!string.IsNullOrWhiteSpace(stringWithSymbols))
            {
                return stringWithSymbols.Replace("(", "").Replace(")", "").Trim();
            }
            else { return string.Empty; }
        }

        /// <summary>
        /// convert object value to a string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetValueString(object obj)
        {
            string ret = string.Empty;
            if (obj != null)
            {
                ret = GetValueString2(obj);
            }
            return ret;
        }

        /// <summary>
        /// Convert object value to a string comming from GetValueString(object obj)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetValueString2(object value)
        {
            string retValue = "";
            if (!DBNull.Value.Equals(value) && value != null)
            {
                retValue = value.ToString();
                retValue = retValue.Trim();
            }
            return retValue;
        }

        /// <summary>
        /// get value string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetValueString(object value, string defaultValue)
        {
            string retValue = defaultValue;
            if (!DBNull.Value.Equals(value) && value != null)
            {
                retValue = value.ToString();
            }
            if (string.IsNullOrEmpty(retValue))
            {
                retValue = defaultValue;
            }
            return retValue.Trim();
        }

        /// <summary>
        /// Get xml documet as a string  : by Buddhima
        /// </summary>
        /// <param name="xmlCoc"></param>
        /// <returns></returns>
        public static string GetXMLAsString(XmlDocument xmlCoc)
        {
            string msg = string.Empty;
            try
            {
                StringWriter sw = new StringWriter();
                XmlTextWriter tx = new XmlTextWriter(sw);
                xmlCoc.WriteTo(tx);

                msg = sw.ToString(); ;
            }
            catch (Exception)
            {
                msg = string.Empty;
            }
            return msg;
        }

        /// <summary>
        /// Get date as string yyyy-mm-dd
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateToSqlString(DateTime dt)
        {
            string result = string.Empty;
            result = string.Format("{0}-{1}-{2}", dt.Year.ToString().PadLeft(4, '0'), dt.Month.ToString().PadLeft(2, '0'), dt.Day.ToString().PadLeft(2, '0'));
            return result;
        }

        /// <summary>
        /// Get time as hh:mm:ss.fff
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string TimeToSqlString(DateTime dt)
        {
            string result = string.Empty;
            result = string.Format("{0}:{1}:{2}.{3}",
                dt.Hour.ToString().PadLeft(2, '0'),
                dt.Minute.ToString().PadLeft(2, '0'),
                dt.Second.ToString().PadLeft(2, '0'),
                dt.Millisecond.ToString().PadLeft(3, '0'));
            return result;
        }

        /// <summary>
        /// Get date and time as string yyy-mm-dd hh:mm:ss.fff
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DatatimeToSqlString(DateTime dt)
        {
            return string.Format("{0} {1}", DateToSqlString(dt), TimeToSqlString(dt));
        }
        /// <summary>
        /// replace " ' " by "  ''  "
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string DoQuotes(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return "";
            else
            {
                return sql.Trim().Replace("'", "''");
            }
        }

        #endregion

        /// <summary>
        /// check is the string is numeric
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string s)
        {
            foreach (char c in s)
            {
                if (!char.IsDigit(c) && c != '.')
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Get appSetting value by providing key name
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingValue(string key)
        {
            string retString = "";
            try
            {
                retString = (ConfigurationManager.AppSettings[key] != null) ? ConfigurationManager.AppSettings[key].ToString() : string.Empty;
            }
            catch (Exception)
            {
                //  throw;
            }
            return retString;
        }


        /// <summary>
        /// Convert a decimal to currency string accurding to the currency
        /// if coddev='USD' then CurrencyString := '$';
        /// if coddev='EUR' then CurrencyString := '€';
        /// if coddev='BEF' then CurrencyString := 'BEF';
        /// if coddev='CAD' then CurrencyString := 'CAD';
        /// if coddev='GBP' then CurrencyString := '£';
        /// </summary>
        /// <param name="value"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string GetCurrencyStringNew(decimal value, string currency)
        {
            try
            {
                if (currency == "EUR")
                {
                    return value.ToString("€ 0.00");
                }
                else if (currency == "USD")
                {
                    return value.ToString("$ 0.00");
                }
                else if (currency == "BEF")
                {
                    return value.ToString("BEF 0.00");
                }
                else if (currency == "CAD")
                {
                    return value.ToString("CAD 0.00");
                }
                else if (currency == "GBP")
                {
                    return value.ToString("£ 0.00");
                }
                else
                {
                    return value.ToString("0.00");
                }
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        /// <summary>
        /// get currency with symbol
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetValueCurrencyWithSymbol(CultureInfo culture, object value)
        {
            string retValue = "0.00";
            if (!DBNull.Value.Equals(value) && value != null)
            {
                decimal val = GetValueDecimal(value);
                retValue = String.Format(culture, "{0:C}", val);
            }
            return retValue.Trim();
        }

        /// <summary>
        /// convert object value to double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double GetValueDouble(object value)
        {
            double retValue = 0f;
            string tempString = string.Empty;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = 2;
            nfi.NumberDecimalSeparator = ".";

            if (!DBNull.Value.Equals(value))
            {
                tempString = RemoveThousandSeperator(value.ToString().Trim());
                tempString = tempString.Replace(",", ".");
                // buddhima 2016/09/13 : remove currency symbols
                tempString = tempString.Replace("€", "").Trim();
                tempString = tempString.Replace("$", "").Trim();
                tempString = tempString.Replace("BEF", "").Trim();
                tempString = tempString.Replace("CAD", "").Trim();
                tempString = tempString.Replace("£", "").Trim();
            }

            if (!string.IsNullOrWhiteSpace(tempString))
            {
                double val = 0;
                double.TryParse(tempString, NumberStyles.Any, nfi, out val);
                retValue = val;
            }
            return retValue;
        }

        /// <summary>
        /// get decimal value : replace currency symbols from string and get as "0.00" format
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal GetValueDecimal(object value)
        {
            decimal retValue = 0M;
            string tempString = string.Empty;
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = 2;
            nfi.NumberDecimalSeparator = ".";

            if (!DBNull.Value.Equals(value))
            {
                tempString = RemoveThousandSeperator(value.ToString().Trim());
                tempString = tempString.Replace(",", ".");

                // tempString = tempString.Replace("€", "").Trim();
                // buddhima 2016/09/13 : remove currency symbols
                tempString = tempString.Replace("€", "").Trim();
                tempString = tempString.Replace("$", "").Trim();
                tempString = tempString.Replace("BEF", "").Trim();
                tempString = tempString.Replace("CAD", "").Trim();
                tempString = tempString.Replace("£", "").Trim();
            }

            if (!string.IsNullOrEmpty(tempString))
            {
                decimal val = 0M;
                decimal.TryParse(tempString, NumberStyles.Any, nfi, out val);
                retValue = val;
            }
            nfi = null;
            return retValue;
        }

        /// <summary>
        /// get decimal value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defalutValue"></param>
        /// <returns></returns>
        public static decimal GetValueDecimal(object value, decimal defalutValue)
        {
            decimal retValue = defalutValue;
            try
            {
                string tempString = string.Empty;
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalDigits = 2;
                nfi.NumberDecimalSeparator = ".";


                if (!DBNull.Value.Equals(value))
                {
                    tempString = RemoveThousandSeperator(value.ToString().Trim());
                    tempString = tempString.Replace(",", ".");
                }

                if (!string.IsNullOrEmpty(tempString))
                {
                    decimal val = defalutValue;
                    decimal.TryParse(tempString, NumberStyles.Any, nfi, out val);
                    retValue = val;
                }
                nfi = null;
            }
            catch (Exception)
            {
                retValue = defalutValue;
            }

            return retValue;
        }

        /// <summary>
        /// get Int value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetValueInt(object value, int defaultValue)
        {
            int retValue = defaultValue;
            if (value == null) return retValue;
            try
            {
                if (!DBNull.Value.Equals(value))
                {
                    string temp = RemoveThousandSeperator(value.ToString().Trim());
                    temp = temp.Replace(",", ".");

                    if ("" != temp)
                    {
                        int val = 0;
                        if (temp.IndexOf('.') > 0)
                        {
                            temp = temp.Substring(0, temp.IndexOf('.'));
                        }
                        int.TryParse(temp, out val);
                        retValue = val;
                    }
                }
            }
            catch { retValue = 0; }
            return retValue;
        }

        /// <summary>
        /// get float value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float GetValueFloat(object value)
        {
            float retValue = 0f;
            string tempString = GetValueString(value);
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = 2;
            nfi.NumberDecimalSeparator = ".";

            tempString = tempString.Replace("€", "");

            if (!DBNull.Value.Equals(value))
            {
                tempString = RemoveThousandSeperator(value.ToString().Trim());
                tempString = tempString.Replace(",", ".");
            }


            if (!string.IsNullOrEmpty(tempString))
            {
                float val = 0f;
                float.TryParse(tempString, NumberStyles.Any, nfi, out val);
                retValue = val;
            }
            return retValue;
        }

        /// <summary>
        /// get float value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float GetValueFloat(object value, float defaultValue)
        {
            float retValue = defaultValue;
            try
            {
                string tempString = GetValueString(value);
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalDigits = 2;
                nfi.NumberDecimalSeparator = ".";

                tempString = tempString.Replace("€", "");

                if (!DBNull.Value.Equals(value))
                {
                    tempString = RemoveThousandSeperator(value.ToString().Trim());
                    tempString = tempString.Replace(",", ".");
                }
                if (!string.IsNullOrEmpty(tempString))
                {
                    float val = 0f;
                    float.TryParse(tempString, NumberStyles.Any, nfi, out val);
                    retValue = val;
                }
            }
            catch (Exception)
            {
                retValue = defaultValue;
            }
            return retValue;
        }

        /// <summary>
        /// get string formatted value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetValueStringFormated(object value)
        {
            string retValue = "";
            if (!DBNull.Value.Equals(value))
            {
                retValue = value.ToString();
                retValue = ProperCase(retValue.Trim());
            }
            return retValue;
        }

        /// <summary>
        /// Convert to propercase
        /// </summary>
        /// <param name="stringInput"></param>
        /// <returns></returns>
        public static string ProperCase(string stringInput)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool fEmptyBefore = true;
            foreach (char ch in stringInput)
            {
                char chThis = ch;
                if (Char.IsWhiteSpace(chThis))
                    fEmptyBefore = true;
                else
                {
                    if (Char.IsLetter(chThis) && fEmptyBefore)
                        chThis = Char.ToUpper(chThis);
                    else
                        chThis = Char.ToLower(chThis);
                    fEmptyBefore = false;
                }
                sb.Append(chThis);
            }
            return sb.ToString();
        } 

        /// <summary>
        /// remove thousand seperator
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string RemoveThousandSeperator(string value)
        {
            if (value.Contains(".") && value.Contains(","))
            {
                if (value.IndexOf('.') < value.IndexOf(','))
                    value = value.Replace(".", string.Empty);
                else
                    value = value.Replace(",", string.Empty);
            }
            return value;
        }

        /// <summary>
        /// get datetime according to current culture
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime GetValueShortDateTime(object value)
        {
            DateTime tempDateTime = DateTime.Now;
            string tempString = GetValueString(value);

            if (!string.IsNullOrEmpty(tempString))
            {
                DateTime.TryParse(tempString, Thread.CurrentThread.CurrentCulture.DateTimeFormat, DateTimeStyles.None, out tempDateTime);
            }
            return tempDateTime;
        }
   
    }
}
