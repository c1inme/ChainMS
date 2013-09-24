using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ControlLibrary;


namespace CMS.WPFHeadOffice
{
    public class LanguageXml : IDisposable
    {
        private static LanguageXml _instance;
        public static LanguageXml Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LanguageXml();
                return _instance;
            }
        }

        private Dictionary<string, string> languageDictionary;
        public event EventHandler XmlLoaded;
        public void LoadLanguage(string lang)
        {
            try
            {
                languageDictionary = new Dictionary<string, string>();
                using (XmlTextReader reader = new XmlTextReader(string.Format("{0}\\Languages\\{1}.xml", Directory.GetCurrentDirectory(), lang)))
                {
                    try
                    {
                        string key = null;
                        while (reader.Read())
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                if (reader.Name.ToLower() == "item")
                                {
                                    if (true == reader.MoveToFirstAttribute())
                                        key = reader.Value;
                                }
                                else
                                    //By pass if not exist item
                                    reader.Read();
                            }
                            else if (reader.NodeType == XmlNodeType.Text)
                            {
                                
                                if (languageDictionary.ContainsKey(key))
                                    MessageBox.Show(key + "is duplicated");
                                languageDictionary.Add(key, reader.Value.Replace("#rn#", "\r\n"));
                            }
                        }
                        Global.Instance.LanguageDictionary = languageDictionary;
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                if (XmlLoaded != null)
                    XmlLoaded(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                ModernDialog.Show(ex.Message);
            }
        }
        public void Dispose()
        {
            languageDictionary = null;
            _instance = null;
        }
    }
}
