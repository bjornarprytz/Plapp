using System;
namespace Plapp.Core
{
    public class Note
    {
        public int Id { get; set; }
        public Topic Topic { get; set; }
        public DateTime Date { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string ImageUri { get; set; }
    }
}
