using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COGallery")] 
 public partial class COGallery : Entity    
 { 
private string m_Name ; 
private string m_Description ; 
private string m_Link ; 
private string m_SourceUrl ; 
private int? m_SortOrder ; 
private bool? m_IsActive ; 
private int? m_TypeEnum ; 
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
public string SourceUrl 
 {    
       get 
     { 
         return this.m_SourceUrl; 
     } 
      set 
    { 
         this.m_SourceUrl = value; 
  RaisePropertyChanged("SourceUrl");  
 } 
 } 
 //------------------------ 
public int? SortOrder 
 {    
       get 
     { 
         return this.m_SortOrder; 
     } 
      set 
    { 
         this.m_SortOrder = value; 
  RaisePropertyChanged("SortOrder");  
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
public int? TypeEnum 
 {    
       get 
     { 
         return this.m_TypeEnum; 
     } 
      set 
    { 
         this.m_TypeEnum = value; 
  RaisePropertyChanged("TypeEnum");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COGallery()
{ 
        this.m_Name = "" ;
        this.m_Description = "" ;
        this.m_Link = "" ;
        this.m_SourceUrl = "" ;
        this.m_SortOrder = 0 ;
        this.m_IsActive = false ;
        this.m_TypeEnum = 0 ;

 }
 #region Properties relation
#endregion

 }
 }