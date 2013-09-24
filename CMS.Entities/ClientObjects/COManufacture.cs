using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COManufacture")] 
 public partial class COManufacture : Entity    
 { 
private string m_Name ; 
private string m_Description ; 
private string m_HomePage ; 
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
public string HomePage 
 {    
       get 
     { 
         return this.m_HomePage; 
     } 
      set 
    { 
         this.m_HomePage = value; 
  RaisePropertyChanged("HomePage");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COManufacture()
{ 
        this.m_Name = "" ;
        this.m_Description = "" ;
        this.m_HomePage = "" ;

 }
 #region Properties relation
#endregion

 }
 }