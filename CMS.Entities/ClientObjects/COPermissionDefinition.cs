using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COPermissionDefinition")] 
 public partial class COPermissionDefinition : Entity    
 { 
private string m_CodePermision ; 
private string m_NamePermission ; 
private string m_Description ; 
private string m_ActionType ; 
private int? m_SortNumber ; 
public string CodePermision 
 {    
       get 
     { 
         return this.m_CodePermision; 
     } 
      set 
    { 
         this.m_CodePermision = value; 
  RaisePropertyChanged("CodePermision");  
 } 
 } 
 //------------------------ 
public string NamePermission 
 {    
       get 
     { 
         return this.m_NamePermission; 
     } 
      set 
    { 
         this.m_NamePermission = value; 
  RaisePropertyChanged("NamePermission");  
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
public string ActionType 
 {    
       get 
     { 
         return this.m_ActionType; 
     } 
      set 
    { 
         this.m_ActionType = value; 
  RaisePropertyChanged("ActionType");  
 } 
 } 
 //------------------------ 
public int? SortNumber 
 {    
       get 
     { 
         return this.m_SortNumber; 
     } 
      set 
    { 
         this.m_SortNumber = value; 
  RaisePropertyChanged("SortNumber");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COPermissionDefinition()
{ 
        this.m_CodePermision = "" ;
        this.m_NamePermission = "" ;
        this.m_Description = "" ;
        this.m_ActionType = "" ;
        this.m_SortNumber = 0 ;

 }
 #region Properties relation
#endregion
public virtual ICollection<COGrantPermission> ListCOGrantPermission { get; set; } 

 }
 }