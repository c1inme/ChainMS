using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COGroupMemberPermission")] 
 public partial class COGroupMemberPermission : Entity    
 { 
private Guid? m_IDUser ; 
private Guid? m_IDGroupPermission ; 
public Guid? IDUser 
 {    
       get 
     { 
         return this.m_IDUser; 
     } 
      set 
    { 
         this.m_IDUser = value; 
  RaisePropertyChanged("IDUser");  
 } 
 } 
 //------------------------ 
public Guid? IDGroupPermission 
 {    
       get 
     { 
         return this.m_IDGroupPermission; 
     } 
      set 
    { 
         this.m_IDGroupPermission = value; 
  RaisePropertyChanged("IDGroupPermission");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COGroupMemberPermission()
{ 
        this.m_IDUser = Guid.NewGuid() ;
        this.m_IDGroupPermission = Guid.NewGuid() ;

 }
 #region Properties relation

private COGroupPermission m_COGroupPermission ;

 [ForeignKey("IDGroupPermission")]
public COGroupPermission COGroupPermission
 { 
get { return m_COGroupPermission; } 
set { m_COGroupPermission = value; 
 RaisePropertyChanged("COGroupPermission"); 
  if (value != null) 
 IDGroupPermission = value.GuidId;
 } 
 } 

private COUsers m_COUsers ;

 [ForeignKey("IDUser")]
public COUsers COUsers
 { 
get { return m_COUsers; } 
set { m_COUsers = value; 
 RaisePropertyChanged("COUsers"); 
  if (value != null) 
 IDUser = value.GuidId;
 } 
 } 
#endregion

 }
 }