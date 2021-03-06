using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BioStore.Shared.Helpers
{
    public static class DictionaryExtensions
    {
        public static Dictionary<string, object> ToDictionary(this object obj, Dictionary<string, object> parametros = null, string name = null)
        {
            try
            {
                if (parametros == null)
                    parametros = new Dictionary<string, object>();

                // automatically create parameters from object props
                var type = obj.GetType();
                var props = type.GetProperties();

                foreach (var prop in props)
                {
                    var propType = prop.PropertyType;
                    var val = prop.GetValue(obj, null);
                    var isList = (propType.IsGenericType || propType.IsGenericTypeDefinition) && (propType.GetGenericTypeDefinition() == typeof(List<>));
                    if (val == null || isList)
                        continue;

                    if (propType.IsClass && propType != typeof(string))
                    {
                        var pai = prop.Name;
                        if (name != null)
                            pai = $"{name}.{prop.Name}";

                        val.ToDictionary(parametros, pai);
                    }
                    else
                    {
                        object valor = val;

                        if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(float))
                        {
                            switch (prop.PropertyType.Name)
                            {
                                case nameof(Decimal):
                                    valor = ((decimal)val).ToString(CultureInfo.InvariantCulture);
                                    break;
                                default:
                                    valor = ((float)val).ToString(CultureInfo.InvariantCulture);
                                    break;
                            }
                        }
                        if (prop.PropertyType == typeof(DateTime))
                        {
                            valor = ((DateTime)val).ToString("d", CultureInfo.GetCultureInfo("pt-BR"));
                        }


                        parametros.Add(name == null ? prop.Name : $"{name}.{prop.Name}", valor);
                    }
                }

                return parametros;


            }
            catch
            {
                return null;
            }
        }
    }
}
