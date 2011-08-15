using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Tests.LinqToSql
{
  public class DepartmentConverter : JsonConverter
  {
      public override void WriteJson(JsonWriter writer, JsonProperty member, object value, JsonSerializer serializer)
    {
      Department department = (Department)value;

      JObject o = new JObject();
      o["DepartmentId"] = new JValue(department.DepartmentId.ToString());
      o["Name"] = new JValue(new string(department.Name.Reverse().ToArray()));

      o.WriteTo(writer);
    }

      public override object ReadJson(JsonReader reader, Type objectType, JsonProperty member, object existingValue, JsonSerializer serializer)
    {
      JObject o = JObject.Load(reader);

      Department department = new Department();
      department.DepartmentId = new Guid((string)o["DepartmentId"]);
      department.Name = new string(((string) o["Name"]).Reverse().ToArray());

      return department;
    }

      public override bool CanConvert(Type objectType, JsonProperty member)
    {
      return (objectType == typeof (Department));
    }
  }
}