using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CMS.WPFHeadOffice
{  
    public enum MessageButtons
        {
            YesNo = 0,
            OkCancel,
            Ok,
            None
        }
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox  :  IDisposable
    {
      
        public bool HasCloseButton { get; set; }
        public static void Show(string message, string title, MessageButtons messageButtons, Action<object, EventArgs> result = null)
        {
            MessageBox messageBox = new MessageBox(message, title, null, messageButtons);
            if (result != null)
            {
                messageBox.Closed += (s, a) =>
                {
                    result(s, a);
                    messageBox_Closed(s, a);
                };
            }
            else
            {
                messageBox.Closed += new EventHandler(messageBox_Closed);
            }
            messageBox.ShowDialog();
        }

        private void ShowForm(MessageBox messageBox)
        {
            messageBox.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
        }

        static void messageBox_Closed(object sender, EventArgs e)
        {
            var messageBox = (MessageBox)sender;
            messageBox.Dispose();
        }
        public static void Show(Exception ex)
        {
            ShowError(ex.Message, "Error");
        }
        public static void Show(string message)
        {
            ShowError(message, "Error");
        }

        public static void ShowError(string message, string title)
        {
            var messageBox = new MessageBox(message, title, null, MessageButtons.Ok);
            messageBox.Closed += new EventHandler(messageBox_Closed);
            messageBox.ShowDialog();
        }

        public static void ShowErrorTranslated(Exception ex)
        {
            if (ex == null || String.IsNullOrEmpty(ex.Message)) return;

            string[] code = ex.Message.Split('\n');
            string result = "";
            if (code.Length < 1)
            {
                result = ex.Message;
            }
            else
            {
                for (int i = 1; i < code.Length - 1; i++)
                {
                    string str = code[i];
                    int index = str.IndexOf("(");
                    if (index >= 0)
                    {
                        string Code = str.Substring(0, index - 1);
                        string entityNameAndProperty = str.Substring(index, str.Length - index);
                        if (Global.Instance.LanguageDictionary != null && Global.Instance.LanguageDictionary.ContainsKey(Code))
                            Code = Global.Instance.LanguageDictionary[Code];
                        result += Code + entityNameAndProperty + "\n";
                    }
                    else
                    {
                        result += str;
                    }

                }
            }
            if (string.IsNullOrEmpty(result))
                result = ex.Message;
            var messageBox = new MessageBox(result, "Error", ex.Message, MessageButtons.Ok);
            messageBox.Closed += new EventHandler(messageBox_Closed);
            messageBox.ShowDialog();

        }
        public static void ShowError(string message, string title, Exception ex)
        {
            string errorMessage = "";
            while (ex != null)
            {
                errorMessage += ex.Message + "\r\n";
                ex = ex.InnerException;
            }
            // Hung, 27 Oct 2011 09:53 CET
            if (Global.Instance.LanguageDictionary != null)
            {
                // Dictionary is initialized 
                MessageBox messageBox = new MessageBox(message, title, errorMessage, MessageButtons.Ok);
                messageBox.Closed += new EventHandler(messageBox_Closed);
                messageBox.ShowDialog();
            }
            else
            {
                // fix activation bug, somehow url is going wrong or user already activated, because dictionary is not yet loaded!
                System.Windows.MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK);
            }
        }

        public MessageBox(string message, string title, string errorMessage, MessageButtons messageButtons)
        {
            InitializeComponent();
          
                this.HasCloseButton = false;
                this.Message.Text = message;
                this.Title = title;

                if (Global.Instance.LanguageDictionary == null)
                {
                    // for case not yest log on we support only OK at the moment
                    if (messageButtons == MessageButtons.Ok)
                        NoButton.Visibility = System.Windows.Visibility.Collapsed;
                    return;
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    ErrorExpander.Visibility = System.Windows.Visibility.Visible;
                    ErrorExpander.Header = Global.Instance.LanguageDictionary["MessageBox_ErrorDetails"];
                    ErrorMessage.Text = errorMessage;
                }

                switch (messageButtons)
                {
                    case MessageButtons.YesNo:
                        YesButton.Content = Global.Instance.LanguageDictionary["General_Yes"];
                        NoButton.Content = Global.Instance.LanguageDictionary["General_No"];
                        break;

                    case MessageButtons.OkCancel:
                        YesButton.Content = Global.Instance.LanguageDictionary["General_Ok"];
                        NoButton.Content = Global.Instance.LanguageDictionary["General_Cancel"];
                        break;

                    case MessageButtons.Ok:
                        YesButton.Content = Global.Instance.LanguageDictionary["General_Ok"];
                        NoButton.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                    case MessageButtons.None:
                        YesButton.Visibility = System.Windows.Visibility.Collapsed;
                        NoButton.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                }
            }

        

        public void Dispose()
        {

        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            NoButton.Focus();
        }

       

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        
        //public MessageBox()
        //{
        //    InitializeComponent();
        //}

    }
}
