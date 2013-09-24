using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COSEOContent")] 
 public partial class COSEOContent : Entity    
 { 
private string m_Title ; 
private string m_Description ; 
public string Title 
 {    
       get 
     { 
         return this.m_Title; 
     } 
      set 
    { 
         this.m_Title = value; 
  RaisePropertyChanged("Title");  
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
 
 
public COSEOContent()
{ 
        this.m_Title = "" ;
        this.m_Description = "" ;

 }
 #region Properties relation
#endregion

 }
 }