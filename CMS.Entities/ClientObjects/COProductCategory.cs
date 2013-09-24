using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COProductCategory")] 
 public partial class COProductCategory : Entity    
 { 
private string m_Name ; 
private string m_Description ; 
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
 
 
public COProductCategory()
{ 
        this.m_Name = "" ;
        this.m_Description = "" ;

 }
 #region Properties relation
#endregion

 }
 }