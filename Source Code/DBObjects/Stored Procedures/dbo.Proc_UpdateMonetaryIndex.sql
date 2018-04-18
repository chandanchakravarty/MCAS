IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMonetaryIndex]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMonetaryIndex]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Proc Name       : dbo.Proc_UpdateMntMonetaryIndex       
--Created by      : ADITYA GOEL    
--Date            : 21/12/2010              
--Purpose   :To Update Reinsurer in MNT_MONETORY_INDEX  
--Revison History :              
--Used In   : Ebix Advantage              
--------------------------------------------------------------              
--Date     Review By          Comments              
--------   ------------       -------------------------*/              
--DROP PROC dbo.Proc_UpdateMonetaryIndex  
CREATE PROC [dbo].[Proc_UpdateMonetaryIndex]  
(   
@ROW_ID INT,  
@DATE DATETIME,  
@INFLATION_RATE DECIMAL(15, 2),  
@INTEREST_RATE DECIMAL (15, 2),  
@MODIFIED_BY INT,  
@LAST_UPDATED_DATETIME DATETIME  
  
)  
AS  
BEGIN  
  
 UPDATE MNT_MONETORY_INDEX  
 SET  
 [DATE] = @DATE,  
 INFLATION_RATE = @INFLATION_RATE,  
 INTEREST_RATE = @INTEREST_RATE,  
 MODIFIED_BY = @MODIFIED_BY,  
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME  
 WHERE ROW_ID = @ROW_ID  
END  
GO

