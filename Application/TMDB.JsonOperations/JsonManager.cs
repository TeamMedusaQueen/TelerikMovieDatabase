using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace TMDB.JsonOperations
{
   public class JsonManager
    {
        public string ExportToJson(IDictionary<string, int> data )
        {
            var jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(data);
            return json;
        }

        public IDictionary<string, int> ImportFromJson(string jsonObject)
        {
            var jsonSerializer = new JavaScriptSerializer();
            var data = jsonSerializer.Deserialize<Dictionary<string,int>>(jsonObject);
            return data;
        }
    }
}
