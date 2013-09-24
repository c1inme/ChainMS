using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COPeriodPayment")] 
 public partial class COPeriodPayment : Entity    
 { 
private string m_Code ; 
private string m_Name ; 
private int? m_DaysForPayment ; 
private string m_Description ; 
public string Code 
 {    
       get 
     { 
         return this.m_Code; 
     } 
      set 
    { 
         this.m_Code = value; 
  RaisePropertyChanged("Code");  
 } 
 } 
 //------------------------ 
public string Name 
 {    
       get 
     { 
         return this.m_Name; 
     } 
      set 
    { 
         this.m_Name = value; 
  RaisePropertyChanged("Name");  
 } 
 } 
 //------------------------ 
public int? DaysForPayment 
 {    
       get 
     { 
         return this.m_DaysForPayment; 
     } 
      set 
    { 
         this.m_DaysForPayment = value; 
  RaisePropertyChanged("DaysForPayment");  
 } 
 } 
 //------------------------ 
public string Description 
 {    
       get 
     { 
         return this.m_Description; 
     } 
      set 
    { 
         this.m_Description = value; 
  RaisePropertyChanged("Description");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COPeriodPayment()
{ 
        this.m_Code = "" ;
        this.m_Name = "" ;
        this.m_DaysForPayment = 0 ;
        this.m_Description = "" ;

 }
 #region Properties relation
#endregion

 }
 }