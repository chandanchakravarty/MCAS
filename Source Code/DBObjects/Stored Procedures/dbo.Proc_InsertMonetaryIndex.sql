IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertMonetaryIndex]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertMonetaryIndex]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                  
Proc Name       : dbo.[[Proc_InsertMntMonetaryIndex]]          
Created by      : ADITYA GOEL      
Date            : 21/12/2010                  
Purpose         :INSERT RECORDS IN MNT_MONETORY_INDEX TABLE.                  
Revison History :                  
Used In        : Ebix Advantage                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------  
DROP PROC dbo.[Proc_InsertMntMonetaryIndex]    
  
*/   
/****** Script for POL_REMUNERATION into DATABASE  ******/       
  
CREATE PROC [dbo].[Proc_InsertMonetaryIndex]  
(    
@ROW_ID INT OUT,  
@DATE DATETIME,  
@INFLATION_RATE DECIMAL(15, 2),  
@INTEREST_RATE DECIMAL (15, 2),  
@CREATED_BY INT,  
@CREATED_DATETIME DATETIME  
  
)  
As            
BEGIN   
  
SELECT  @ROW_ID=ISNULL(MAX(ROW_ID),0)+1 FROM MNT_MONETORY_INDEX  
  
 INSERT INTO MNT_MONETORY_INDEX          
 (    
  ROW_ID ,  
  [DATE],  
  INFLATION_RATE,  
  INTEREST_RATE,  
  IS_ACTIVE,  
  CREATED_BY,  
  CREATED_DATETIME    
 )  
 VALUES            
 (   
  @ROW_ID ,  
  @DATE,  
  @INFLATION_RATE ,  
  @INTEREST_RATE ,  
  'Y',  
  @CREATED_BY,  
  @CREATED_DATETIME  
   
 )  
END  
GO

