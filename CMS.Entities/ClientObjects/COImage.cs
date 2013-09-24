using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COImage")] 
 public partial class COImage : Entity    
 { 
private Guid? m_IdBelong ; 
private int? m_Discriminator ; 
private string m_Description ; 
private string m_FullHdPath ; 
private string m_ThumpnailPath ; 
public Guid? IdBelong 
 {    
       get 
     { 
         return this.m_IdBelong; 
     } 
      set 
    { 
         this.m_IdBelong = value; 
  RaisePropertyChanged("IdBelong");  
 } 
 } 
 //------------------------ 
public int? Discriminator 
 {    
       get 
     { 
         return this.m_Discriminator; 
     } 
      set 
    { 
         this.m_Discriminator = value; 
  RaisePropertyChanged("Discriminator");  
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
public string FullHdPath 
 {    
       get 
     { 
         return this.m_FullHdPath; 
     } 
      set 
    { 
         this.m_FullHdPath = value; 
  RaisePropertyChanged("FullHdPath");  
 } 
 } 
 //------------------------ 
public string ThumpnailPath 
 {    
       get 
     { 
         return this.m_ThumpnailPath; 
     } 
      set 
    { 
         this.m_ThumpnailPath = value; 
  RaisePropertyChanged("ThumpnailPath");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COImage()
{ 
        this.m_IdBelong = Guid.NewGuid() ;
        this.m_Discriminator = 0 ;
        this.m_Description = "" ;
        this.m_FullHdPath = "" ;
        this.m_ThumpnailPath = "" ;

 }
 #region Properties relation
#endregion

 }
 }