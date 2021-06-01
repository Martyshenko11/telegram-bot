using System.Collections.Generic;

namespace NYT_Bot1
{
    public class Parameters4
    {
        public List<Result4> results { get; set; }
    }

    public class Link
    {
        public string url { get; set; }
    }

    public class Result4
    {
        public string display_title { get; set; }
        public Link link { get; set; }
    }

}
