using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using CMS.Entities.ServerObjects;

namespace CMS.WebMVC.Models
{
    public class NewsByGroup : Collection<News>
    {
        private string m_NameGroup;
        public string NameGroup
        {
            get { return m_NameGroup; }
            set { m_NameGroup = value; }
        }

        private Guid m_GuildGroup;
        public Guid GuildGroup
        {
            get { return m_GuildGroup; }
            set { m_GuildGroup = value; }
        }

        private long m_IdGroup;
        public long IdGroup
        {
            get { return m_IdGroup; }
            set { m_IdGroup = value; }
        }


        private string m_LinkGroup;
        public string LinkGroup
        {
            get { return m_LinkGroup; }
            set { m_LinkGroup = value; }
        }


        private string m_ImageGroup;
        public string ImageGroup
        {
            get { return m_ImageGroup; }
            set { m_ImageGroup = value; }
        }

        private int m_OderNumber;
        public int OderNumber
        {
            get { return m_OderNumber; }
            set { m_OderNumber = value; }
        }

    }
}