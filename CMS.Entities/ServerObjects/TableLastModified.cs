using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ServerObjects 
 { 
[Table("TableLastModified")] 
 public partial class TableLastModified : Entity    
 { 

#region Fields
private string m_TableName ; 

#endregion

#region Properties
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

#endregion

#region Constructor method
public TableLastModified()
{ 
        this.m_TableName = "" ;

 }
#endregion

 #region Properties relation
#endregion

 #region List Properties relation
#endregion

 }
 }