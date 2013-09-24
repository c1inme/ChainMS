using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CMS.WPFHeadOffice
{
    public class FLabel : Label
    {
        public FLabel()
        {
           
        }

        /// <summary>
        /// Gets or sets TextKey of tab item
        /// </summary>
        public string TextKey
        {
            get { return (string)GetValue(TextKeyProperty); }
            set
            {
                SetValue(TextKeyProperty, value);

            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for TextKey.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TextKeyProperty =
            DependencyProperty.Register("TextKey", typeof(string), typeof(FLabel), new UIPropertyMetadata(null, OnTextKeyChanged));

        // TextKey changed handler
        static void OnTextKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FLabel labelItem = (FLabel)d;
            //labelItem.CoerceValue(ToolTipProperty);
            labelItem.Translate();
            //Translate();
        }

        public void Translate()
        {
            if (Global.Instance.LanguageDictionary == null)
            {
                this.Content = this.TextKey;
                return;
            }
            if (Global.Instance.LanguageDictionary.ContainsKey(TextKey))
                this.Content = Global.Instance.LanguageDictionary[TextKey];
            else
                this.Content = this.TextKey;
            return;
        }

    }
}
