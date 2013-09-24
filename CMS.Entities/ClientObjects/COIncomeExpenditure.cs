using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COIncomeExpenditure")] 
 public partial class COIncomeExpenditure : Entity    
 { 
private string m_CodeMethod ; 
private string m_NameMethod ; 
private string m_Description ; 
public string CodeMethod 
 {    
       get 
     { 
         return this.m_CodeMethod; 
     } 
      set 
    { 
         this.m_CodeMethod = value; 
  RaisePropertyChanged("CodeMethod");  
 } 
 } 
 //------------------------ 
public string NameMethod 
 {    
       get 
     { 
         return this.m_NameMethod; 
     } 
      set 
    { 
         this.m_NameMethod = value; 
  RaisePropertyChanged("NameMethod");  
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
 
 
public COIncomeExpenditure()
{ 
        this.m_CodeMethod = "" ;
        this.m_NameMethod = "" ;
        this.m_Description = "" ;

 }
 #region Properties relation
#endregion

 }
 }