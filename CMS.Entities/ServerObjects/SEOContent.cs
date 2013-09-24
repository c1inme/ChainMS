using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ServerObjects 
 { 
[Table("SEOContent")] 
 public partial class SEOContent : Entity    
 { 

#region Fields
private string m_Title ; 
private string m_Description ; 

#endregion

#region Properties
public string Title 
 {    
       get 
     { 
         return this.m_Title; 
     } 
      set 
    { 
         this.m_Title = value; 
  RaisePropertyChanged("Title");  
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

#endregion

#region Constructor method
public SEOContent()
{ 
        this.m_Title = "" ;
        this.m_Description = "" ;

 }
#endregion

 #region Properties relation
#endregion

 #region List Properties relation
#endregion

 }
 }