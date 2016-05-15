using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Reflection;

namespace SampleApi.Formatters
{
    public class CsvMediaTypeFormatter : OutputFormatter
    {
        public CsvMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
            //SupportedEncodings.Add(Encoding.UTF8);
        }

        protected override bool CanWriteType(Type type)
        {
            return IsTypeOfIEnumerable(type);
        }

        private static bool IsTypeOfIEnumerable(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType == typeof(IEnumerable));
        }

        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            var itemType = context.ObjectType.GetElementType() ?? context.ObjectType.GetGenericArguments()[0];

            using (var stringWriter = new StringWriter())
            {
                stringWriter.WriteLine(
                    string.Join<string>(
                        ",", itemType.GetProperties().Select(x => x.Name)
                        )
                    );

                foreach (var obj in (IEnumerable<object>)context.Object)
                {
                    var vals = obj.GetType().GetProperties().Select(
                        pi => new
                        {
                            Value = pi.GetValue(obj, null)
                        }
                        );

                    var valueLine = string.Empty;

                    string _val;
                    foreach (var val in vals)
                    {
                        if (val.Value != null)
                        {
                            _val = val.Value.ToString();
                            //Check if the value contans a comma and place it in quotes if so
                            if (_val.Contains(","))
                                _val = string.Concat("\"", _val, "\"");

                            //Replace any \r or \n special characters from a new line with a space
                            if (_val.Contains("\r"))
                                _val = _val.Replace("\r", " ");
                            if (_val.Contains("\n"))
                                _val = _val.Replace("\n", " ");

                            valueLine = string.Concat(valueLine, _val, ",");

                        }
                        else
                        {
                            valueLine = string.Concat(valueLine, ",");
                        }
                    }

                    stringWriter.WriteLine(valueLine.TrimEnd(','));
                }

                var writer = new StreamWriter(context.HttpContext.Response.Body);
                await writer.WriteAsync(stringWriter.ToString());
                await writer.FlushAsync();
            }
        }
    }
}
