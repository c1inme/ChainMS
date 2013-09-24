using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COLiabilitiesPeriod")] 
 public partial class COLiabilitiesPeriod : Entity    
 { 
private Guid? m_CustomerSupplierID ; 
private decimal? m_Receivables ; 
private decimal? m_Payables ; 
private decimal? m_FirstPayPeriod ; 
private decimal? m_FirstReceivablePeriod ; 
public Guid? CustomerSupplierID 
 {    
       get 
     { 
         return this.m_CustomerSupplierID; 
     } 
      set 
    { 
         this.m_CustomerSupplierID = value; 
  RaisePropertyChanged("CustomerSupplierID");  
 } 
 } 
 //------------------------ 
public decimal? Receivables 
 {    
       get 
     { 
         return this.m_Receivables; 
     } 
      set 
    { 
         this.m_Receivables = value; 
  RaisePropertyChanged("Receivables");  
 } 
 } 
 //------------------------ 
public decimal? Payables 
 {    
       get 
     { 
         return this.m_Payables; 
     } 
      set 
    { 
         this.m_Payables = value; 
  RaisePropertyChanged("Payables");  
 } 
 } 
 //------------------------ 
public decimal? FirstPayPeriod 
 {    
       get 
     { 
         return this.m_FirstPayPeriod; 
     } 
      set 
    { 
         this.m_FirstPayPeriod = value; 
  RaisePropertyChanged("FirstPayPeriod");  
 } 
 } 
 //------------------------ 
public decimal? FirstReceivablePeriod 
 {    
       get 
     { 
         return this.m_FirstReceivablePeriod; 
     } 
      set 
    { 
         this.m_FirstReceivablePeriod = value; 
  RaisePropertyChanged("FirstReceivablePeriod");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COLiabilitiesPeriod()
{ 
        this.m_CustomerSupplierID = Guid.NewGuid() ;
        this.m_Receivables = 0 ;
        this.m_Payables = 0 ;
        this.m_FirstPayPeriod = 0 ;
        this.m_FirstReceivablePeriod = 0 ;

 }
 #region Properties relation
#endregion

 }
 }