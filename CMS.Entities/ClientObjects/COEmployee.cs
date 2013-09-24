using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COEmployee")] 
 public partial class COEmployee : Entity    
 { 
private string m_Code ; 
private string m_Name ; 
private DateTime? m_BirthDay ; 
private DateTime? m_BeginWork ; 
private string m_AccountNumber ; 
private string m_NameAccountNumber ; 
private string m_Description ; 
private Guid? m_UserID ; 
private bool? m_IsActive ; 
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
public DateTime? BirthDay 
 {    
       get 
     { 
         return this.m_BirthDay; 
     } 
      set 
    { 
         this.m_BirthDay = value; 
  RaisePropertyChanged("BirthDay");  
 } 
 } 
 //------------------------ 
public DateTime? BeginWork 
 {    
       get 
     { 
         return this.m_BeginWork; 
     } 
      set 
    { 
         this.m_BeginWork = value; 
  RaisePropertyChanged("BeginWork");  
 } 
 } 
 //------------------------ 
public string AccountNumber 
 {    
       get 
     { 
         return this.m_AccountNumber; 
     } 
      set 
    { 
         this.m_AccountNumber = value; 
  RaisePropertyChanged("AccountNumber");  
 } 
 } 
 //------------------------ 
public string NameAccountNumber 
 {    
       get 
     { 
         return this.m_NameAccountNumber; 
     } 
      set 
    { 
         this.m_NameAccountNumber = value; 
  RaisePropertyChanged("NameAccountNumber");  
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
public Guid? UserID 
 {    
       get 
     { 
         return this.m_UserID; 
     } 
      set 
    { 
         this.m_UserID = value; 
  RaisePropertyChanged("UserID");  
 } 
 } 
 //------------------------ 
public bool? IsActive 
 {    
       get 
     { 
         return this.m_IsActive; 
     } 
      set 
    { 
         this.m_IsActive = value; 
  RaisePropertyChanged("IsActive");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COEmployee()
{ 
        this.m_Code = "" ;
        this.m_Name = "" ;
        this.m_BirthDay = DateTime.Now ;
        this.m_BeginWork = DateTime.Now ;
        this.m_AccountNumber = "" ;
        this.m_NameAccountNumber = "" ;
        this.m_Description = "" ;
        this.m_UserID = Guid.NewGuid() ;
        this.m_IsActive = false ;

 }
 #region Properties relation
#endregion

 }
 }