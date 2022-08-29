using Newtonsoft.Json;

namespace DAL.DataBase
{
    public class Options
    {
        [JsonIgnore]
        public int Index
        {
            get
            {
                return index;
            }
            set
            {
                index = value;
                Update();
            }
        }

        [JsonIgnore]
        public int Lines
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
                Update();
            }
        }

        private readonly string path = string.Empty;

        [JsonRequired]
        private int index;
        [JsonRequired]
        private int lines;

        public Options()
        {

        }

        public Options(string tableOptionsPath)
        {
            path = tableOptionsPath;
        }

        public void Init()
        {
            using var sr = new StreamReader(path);

            var temp = JsonConvert.DeserializeObject<Options>(sr.ReadToEnd());
            lines = temp!.lines;
            index = temp.index;
        }

        private void Update()
        {
            using var sr = new StreamWriter(path);
            sr.Write(JsonConvert.SerializeObject(this));
        }
    }
}
