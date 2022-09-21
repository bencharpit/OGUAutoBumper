namespace OGUBumper.Models
{
    internal class WebForm
    {
        public string my_post_key { get; set; }

        public string subject { get; set; }

        public string posthash { get; set; }

        public string lastpid { get; set; }

        public string from_page { get; set; }

        public string tid { get; set; }

        public string method { get; set; }

        public WebForm(string my_post_key, string subject, string posthash, string lastpid, string from_page, string tid, string method)
        {
            this.my_post_key = my_post_key;
            this.subject = subject;
            this.posthash = posthash;
            this.lastpid = lastpid;
            this.from_page = from_page;
            this.tid = tid;
            this.method = method;
        }
    }
}
