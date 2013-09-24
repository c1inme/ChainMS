using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COLocationCategory")] 
 public partial class COLocationCategory : Entity    
 { 
private string m_Code ; 
private string m_Name ; 
private string m_Description ; 
private string m_Discriminator ; 
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
public string Discriminator 
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

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COLocationCategory()
{ 
        this.m_Code = "" ;
        this.m_Name = "" ;
        this.m_Description = "" ;
        this.m_Discriminator = "" ;

 }
 #region Properties relation
#endregion

 }
 }