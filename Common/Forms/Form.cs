using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Forms
{
    public class Form
    {
        public static string SerializeToJson(Form form)
        {
            var tmp = JsonSerializer.Serialize(Convert.ChangeType(form, form.GetType()));
            return tmp;
        }
        public static T DerializeFromJson<T>(string formJson)
        {
            return JsonSerializer.Deserialize<T>(formJson);
        }
    }
}
