using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("CONameDictionary")] 
 public partial class CONameDictionary : Entity    
 { 
private string m_InternalName ; 
private string m_DisplayName ; 
private string m_TableName ; 
private bool? m_IsLookup ; 
private string m_TableLookup ; 
private string m_PropertyLookupDisplay ; 
public string InternalName 
 {    
       get 
     { 
         return this.m_InternalName; 
     } 
      set 
    { 
         this.m_InternalName = value; 
  RaisePropertyChanged("InternalName");  
 } 
 } 
 //------------------------ 
public string DisplayName 
 {    
       get 
     { 
         return this.m_DisplayName; 
     } 
      set 
    { 
         this.m_DisplayName = value; 
  RaisePropertyChanged("DisplayName");  
 } 
 } 
 //------------------------ 
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
public bool? IsLookup 
 {    
       get 
     { 
         return this.m_IsLookup; 
     } 
      set 
    { 
         this.m_IsLookup = value; 
  RaisePropertyChanged("IsLookup");  
 } 
 } 
 //------------------------ 
public string TableLookup 
 {    
       get 
     { 
         return this.m_TableLookup; 
     } 
      set 
    { 
         this.m_TableLookup = value; 
  RaisePropertyChanged("TableLookup");  
 } 
 } 
 //------------------------ 
public string PropertyLookupDisplay 
 {    
       get 
     { 
         return this.m_PropertyLookupDisplay; 
     } 
      set 
    { 
         this.m_PropertyLookupDisplay = value; 
  RaisePropertyChanged("PropertyLookupDisplay");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public CONameDictionary()
{ 
        this.m_InternalName = "" ;
        this.m_DisplayName = "" ;
        this.m_TableName = "" ;
        this.m_IsLookup = false ;
        this.m_TableLookup = "" ;
        this.m_PropertyLookupDisplay = "" ;

 }
 #region Properties relation
#endregion

 }
 }