 --author: Raheela and Andrej

CREATE DATABASE CanteenDB
GO 
USE CanteenDB
GO 
CREATE TABLE Employee
 ( 
  EmpId INT NOT NULL PRIMARY KEY, 
    EmpName NVARCHAR(50) NOT NULL, 
  
) 
GO 
CREATE TABLE Receipt 
  ( 
    ReceiptNumber INT NOT NULL PRIMARY KEY, 
    Receiptcreated DATETIME NOT NULL CONSTRAINT DF_MyTable_CreateDate_GETDATE DEFAULT GETDATE(),
    EmpId INT NOT NULL FOREIGN KEY REFERENCES Employee(EmpId)
 ) 

GO 

CREATE TABLE FoodCategory
  ( 
     CategoryNumber INT NOT NULL PRIMARY KEY,      
	 CategoryName NVARCHAR(30) NOT NULL,
     
  )
  GO

CREATE TABLE  FoodItems
  ( 
     ItemId INT NOT NULL, 
     ItemName NVARCHAR(100) NOT NULL, 
	 ItemPrice INT NOT NULL,
     PRIMARY KEY (ItemId),
	 CategoryNumber int  FOREIGN KEY REFERENCES FoodCategory ( CategoryNumber)

  )
GO 

CREATE TABLE ShoppingCart  ( 
	 ShoppingCartId  INT NOT NULL PRIMARY KEY,
     Amount INT NOT NULL,
     ReceiptNumber int  NOT NULL FOREIGN KEY REFERENCES Receipt(ReceiptNumber),
     ItemId int NOT NULL FOREIGN KEY REFERENCES FoodItems(ItemId)
	 ) 
	 GO 