using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COTableLastModified")] 
 public partial class COTableLastModified : Entity    
 { 
private string m_TableName ; 
public string TableName 
 {    
       get 
     { 
         return this.m_TableName; 
     } 
      set 
    { 
         this.m_TableName = value; 
  RaisePropertyChanged("TableName");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COTableLastModified()
{ 
        this.m_TableName = "" ;

 }
 #region Properties relation
#endregion

 }
 }