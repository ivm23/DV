using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Registration.Model
{
    public class Letter
    {
        public Guid Id; 
        public Guid IdSender { get; set; }
        public List<Guid> IdReceivers { get; } = new List<Guid>();
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsRead { get; set; }
        public Guid IdFolder { get; set; }
        public string ExtendedData { get; set; }
        public int Type { get; set; }
    }

    public class LetterType
    {
        public int Id { get; set; }
        public string TypeClientUI { get; set; }
        public string Name { get; set; }
    }

    public class LetterView : Letter
    {
        public string SenderName { get; set; }
        public List<string> ReceiversName { get;} = new List<string>();

        public string nameFolder;

        public override string ToString()
        {
            return GetInfo();
        }

        public string GetInfo()
        {
            string text = string.Empty;
            if (Text.Length > 5)
                text = string.Format("{0}...", Text.Substring(0, 5));
            else
                text = Text;
            return string.Format($"{Date}\n{Name}\n{SenderName}\n{text}");
        }
    }
}
