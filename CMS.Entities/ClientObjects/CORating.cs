using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Entities.ClientObjects
{
    [Table("CORating")]
    public partial class CORating : Entity
    {
        private Guid? m_IdBelong;
        private int? m_Discriminator;
        private int? m_SumRating;
        private int? m_CountCurrent;
        public Guid? IdBelong
        {
            get
            {
                return this.m_IdBelong;
            }
            set
            {
                this.m_IdBelong = value;
                RaisePropertyChanged("IdBelong");
            }
        }
        //------------------------ 
        public int? Discriminator
        {
            get
            {
                return this.m_Discriminator;
            }
            set
            {
                this.m_Discriminator = value;
                RaisePropertyChanged("Discriminator");
            }
        }
        //------------------------ 
        public int? SumRating
        {
            get
            {
                return this.m_SumRating;
            }
            set
            {
                this.m_SumRating = value;
                RaisePropertyChanged("SumRating");
            }
        }
        //------------------------ 
        public int? CountCurrent
        {
            get
            {
                return this.m_CountCurrent;
            }
            set
            {
                this.m_CountCurrent = value;
                RaisePropertyChanged("CountCurrent");
            }
        }
        //------------------------ 



        //Khởi tạo đối tượng rỗng 


        public CORating()
        {
            this.m_IdBelong = Guid.NewGuid();
            this.m_Discriminator = 0;
            this.m_SumRating = 0;
            this.m_CountCurrent = 0;

        }
        #region Properties relation
        #endregion

    }
}