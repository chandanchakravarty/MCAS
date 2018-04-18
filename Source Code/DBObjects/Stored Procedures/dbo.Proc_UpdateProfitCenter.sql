IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateProfitCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateProfitCenter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------            
Proc Name       : dbo.Proc_UpdateProfitCenter           
Created by      : Priya          
Date            : 3/9/2005            
Purpose         : To Upadte the data in Profit Center table.            
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/            
CREATE PROC Dbo.Proc_UpdateProfitCenter           
(            
           
  @PC_ID      nchar(8),            
  @PC_CODE     nchar(8),            
  @PC_NAME     nvarchar(200),            
  @PC_ADD1     nvarchar(140),            
  @PC_ADD2     nvarchar(140),            
  @PC_CITY     nvarchar(80),            
  @PC_STATE    nvarchar(10),            
  @PC_ZIP      nvarchar(20),            
  @PC_COUNTRY     nvarchar(10),            
  @PC_PHONE     nvarchar(40),            
  @PC_EXT     nvarchar(10),            
  @PC_FAX     nvarchar(40),            
  @PC_EMAIL     nvarchar(100),  
  @MODIFIED_BY     int,            
  @LAST_UPDATED_DATETIME     datetime   
)            
AS            
BEGIN          
         

 /*Check for Unique Code of Department  */ 
IF NOT EXISTS (SELECT PC_ID FROM MNT_PROFIT_CENTER_LIST WHERE PC_CODE = @PC_CODE AND PC_ID <> @PC_ID) 
 BEGIN           
 UPDATE MNT_PROFIT_CENTER_LIST
  	 SET              
	  PC_CODE =@PC_CODE,      
	  PC_NAME= @PC_NAME,       
	  PC_ADD1 =@PC_ADD1,            
	  PC_ADD2 =@PC_ADD2,       
	  PC_CITY =@PC_CITY,        
	  PC_STATE =@PC_STATE,            
	  PC_ZIP =@PC_ZIP,          
	  PC_COUNTRY=@PC_COUNTRY ,          
	  PC_PHONE =@PC_PHONE,            
	  PC_EXT =@PC_EXT,        
	  PC_FAX= @PC_FAX,          
	  PC_EMAIL = @PC_EMAIL,    
	  MODIFIED_BY =@MODIFIED_BY,       
	  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME  
	WHERE PC_ID = @PC_ID  
END
	     
END





GO

