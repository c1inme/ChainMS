using System; 
using System.Collections.Generic; 
using System.Text; 
  using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
namespace CMS.Entities.ClientObjects 
 { 
[Table("COProduct")] 
 public partial class COProduct : Entity    
 { 
private string m_Code ; 
private string m_Name ; 
private string m_Description ; 
private Guid? m_ProductCategoryId ; 
private int? m_SellPrice ; 
private int? m_BuyPrice ; 
private int? m_ManufactureId ; 
private int? m_CurrentStock ; 
private int? m_Stock ; 
private bool? m_IsAvailable ; 
private float m_Rating ; 
private string m_ImagePath ; 
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
public Guid? ProductCategoryId 
 {    
       get 
     { 
         return this.m_ProductCategoryId; 
     } 
      set 
    { 
         this.m_ProductCategoryId = value; 
  RaisePropertyChanged("ProductCategoryId");  
 } 
 } 
 //------------------------ 
public int? SellPrice 
 {    
       get 
     { 
         return this.m_SellPrice; 
     } 
      set 
    { 
         this.m_SellPrice = value; 
  RaisePropertyChanged("SellPrice");  
 } 
 } 
 //------------------------ 
public int? BuyPrice 
 {    
       get 
     { 
         return this.m_BuyPrice; 
     } 
      set 
    { 
         this.m_BuyPrice = value; 
  RaisePropertyChanged("BuyPrice");  
 } 
 } 
 //------------------------ 
public int? ManufactureId 
 {    
       get 
     { 
         return this.m_ManufactureId; 
     } 
      set 
    { 
         this.m_ManufactureId = value; 
  RaisePropertyChanged("ManufactureId");  
 } 
 } 
 //------------------------ 
public int? CurrentStock 
 {    
       get 
     { 
         return this.m_CurrentStock; 
     } 
      set 
    { 
         this.m_CurrentStock = value; 
  RaisePropertyChanged("CurrentStock");  
 } 
 } 
 //------------------------ 
public int? Stock 
 {    
       get 
     { 
         return this.m_Stock; 
     } 
      set 
    { 
         this.m_Stock = value; 
  RaisePropertyChanged("Stock");  
 } 
 } 
 //------------------------ 
public bool? IsAvailable 
 {    
       get 
     { 
         return this.m_IsAvailable; 
     } 
      set 
    { 
         this.m_IsAvailable = value; 
  RaisePropertyChanged("IsAvailable");  
 } 
 } 
 //------------------------ 
public float Rating 
 {    
       get 
     { 
         return this.m_Rating; 
     } 
      set 
    { 
         this.m_Rating = value; 
  RaisePropertyChanged("Rating");  
 } 
 } 
 //------------------------ 
public string ImagePath 
 {    
       get 
     { 
         return this.m_ImagePath; 
     } 
      set 
    { 
         this.m_ImagePath = value; 
  RaisePropertyChanged("ImagePath");  
 } 
 } 
 //------------------------ 

 
 
 //Khởi tạo đối tượng rỗng 
 
 
public COProduct()
{ 
        this.m_Code = "" ;
        this.m_Name = "" ;
        this.m_Description = "" ;
        this.m_ProductCategoryId = Guid.NewGuid() ;
        this.m_SellPrice = 0 ;
        this.m_BuyPrice = 0 ;
        this.m_ManufactureId = 0 ;
        this.m_CurrentStock = 0 ;
        this.m_Stock = 0 ;
        this.m_IsAvailable = false ;
        this.m_Rating = 0 ;
        this.m_ImagePath = "" ;

 }
 #region Properties relation
#endregion

 }
 }