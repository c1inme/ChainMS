using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COMenuCategory")] 
 public partial class COMenuCategory : Entity    
 { 
private string m_MenuName ; 
private Guid? m_ParentId ; 
private int? m_Order ; 
private string m_IconImage ; 
private string m_Description ; 
private bool? m_IsShowHome ; 
private bool? m_IsActive ; 
private string m_Link ; 
public string MenuName 
 {    
       get 
     { 
         return this.m_MenuName; 
     } 
      set 
    { 
         this.m_MenuName = value; 
  RaisePropertyChanged("MenuName");  
 } 
 } 
 //------------------------ 
public Guid? ParentId 
 {    
       get 
     { 
         return this.m_ParentId; 
     } 
      set 
    { 
         this.m_ParentId = value; 
  RaisePropertyChanged("ParentId");  
 } 
 } 
 //------------------------ 
public int? Order 
 {    
       get 
     { 
         return this.m_Order; 
     } 
      set 
    { 
         this.m_Order = value; 
  RaisePropertyChanged("Order");  
 } 
 } 
 //------------------------ 
public string IconImage 
 {    
       get 
     { 
         return this.m_IconImage; 
     } 
      set 
    { 
         this.m_IconImage = value; 
  RaisePropertyChanged("IconImage");  
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
public bool? IsShowHome 
 {    
       get 
     { 
         return this.m_IsShowHome; 
     } 
      set 
    { 
         this.m_IsShowHome = value; 
  RaisePropertyChanged("IsShowHome");  
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
public string Link 
 {    
       get 
     { 
         return this.m_Link; 
     } 
      set 
    { 
         this.m_Link = value; 
  RaisePropertyChanged("Link");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COMenuCategory()
{ 
        this.m_MenuName = "" ;
        this.m_ParentId = Guid.NewGuid() ;
        this.m_Order = 0 ;
        this.m_IconImage = "" ;
        this.m_Description = "" ;
        this.m_IsShowHome = false ;
        this.m_IsActive = false ;
        this.m_Link = "" ;

 }
 #region Properties relation
#endregion

 }
 }