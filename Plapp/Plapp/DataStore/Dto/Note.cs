using System;

namespace Plapp
{
    public class Note : DataTable
    {
        public int TopicId { get; set; }
        public DateTime Date { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string ImageUri { get; set; }
    }
}
