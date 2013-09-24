using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COManufacturer")] 
 public partial class COManufacturer : Entity    
 { 
private string m_Code ; 
private string m_Name ; 
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
 
 
public COManufacturer()
{ 
        this.m_Code = "" ;
        this.m_Name = "" ;
        this.m_Description = "" ;

 }
 #region Properties relation
#endregion

 }
 }