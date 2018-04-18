using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.Resources;

namespace Registration.WinForms.Message
{
    public class MessageService : IMessageService
    {
        private  ResourceManager _resourceManager;

        ResourceManager ResourceManager
        {
            get
            {
                return _resourceManager;
            }
        }

        public MessageService()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
           _resourceManager = new ResourceManager("Registrstion.WinForms.Message.MessageResource", typeof(MessageService).Assembly);
        }

        string[] GetTextAndTitleMessage(string messageAndTitle)
        {
            return messageAndTitle.Split('|');
        }

        public void InfoMessage(string messageAndTitle)
        {
            string[] textAndTitle = GetTextAndTitleMessage(messageAndTitle);
            MessageBox.Show(textAndTitle[0], textAndTitle[1], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ErrorMessage(string messageAndTitle)
        {
            string[] textAndTitle = GetTextAndTitleMessage(messageAndTitle);
            if (textAndTitle.Count() == 1)
            {
                MessageBox.Show(textAndTitle[0], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show(textAndTitle[0], textAndTitle[1], MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public DialogResult QuestionMessage(string messageAndTitle)
        {
            string[] textAndTitle = GetTextAndTitleMessage(messageAndTitle);
            return MessageBox.Show(textAndTitle[0], textAndTitle[1], MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public void SingleMessage(string messageAndTitle)
        {
            string[] textAndTitle = GetTextAndTitleMessage(messageAndTitle);
            MessageBox.Show(textAndTitle[0]);
        }

    }
}
