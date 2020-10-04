using Plapp.Core;
using System;

namespace Plapp
{
    public class NoteViewModel : BaseViewModel, INoteViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public string ImageUri { get; set; }
        public string Header { get; set; } = "The plant grew";
        public string Text { get; set; } = "Lorem ipsum er opprinnelig et lettere redigert utdrag fra De finibus bonorum et malorum av Cicero.Opprinnelig begynte avsnittet: Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit.";

        public ITopicViewModel Topic { get; set; }
    }
}
