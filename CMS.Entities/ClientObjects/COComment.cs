using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COComment")] 
 public partial class COComment : Entity    
 { 
private Guid? m_IdFather ; 
private Guid? m_IdBelong ; 
private int? m_Discriminator ; 
private string m_Content ; 
private Guid? m_UserId ; 
public Guid? IdFather 
 {    
       get 
     { 
         return this.m_IdFather; 
     } 
      set 
    { 
         this.m_IdFather = value; 
  RaisePropertyChanged("IdFather");  
 } 
 } 
 //------------------------ 
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
public string Content 
 {    
       get 
     { 
         return this.m_Content; 
     } 
      set 
    { 
         this.m_Content = value; 
  RaisePropertyChanged("Content");  
 } 
 } 
 //------------------------ 
public Guid? UserId 
 {    
       get 
     { 
         return this.m_UserId; 
     } 
      set 
    { 
         this.m_UserId = value; 
  RaisePropertyChanged("UserId");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COComment()
{ 
        this.m_IdFather = Guid.NewGuid() ;
        this.m_IdBelong = Guid.NewGuid() ;
        this.m_Discriminator = 0 ;
        this.m_Content = "" ;
        this.m_UserId = Guid.NewGuid() ;

 }
 #region Properties relation
#endregion

 }
 }