namespace OGUBumper.Models
{
    /// <summary>
    /// Content body used for the request making the reply
    /// </summary>
    internal class WebForm
    {
        /// <summary>
        /// my_post_key value
        /// </summary>
        public string my_post_key { get; set; }

        /// <summary>
        /// subject value
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// posthash value
        /// </summary>
        public string posthash { get; set; }

        /// <summary>
        /// lastpid value
        /// </summary>
        public string lastpid { get; set; }

        /// <summary>
        /// from_page value
        /// </summary>
        public string from_page { get; set; }

        /// <summary>
        /// tid value
        /// </summary>
        public string tid { get; set; }

        /// <summary>
        /// method value
        /// </summary>
        public string method { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
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
