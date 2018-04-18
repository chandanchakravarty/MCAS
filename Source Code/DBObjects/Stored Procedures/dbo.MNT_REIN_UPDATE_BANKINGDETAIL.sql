IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_UPDATE_BANKINGDETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_UPDATE_BANKINGDETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.[MNT_REIN_UPDATE_BANKINGDETAIL]                
Created by      : Harmanjeet Singh               
Date            : April 20, 2007              
Purpose         : To insert the data into Reinsurer Contact table.                
Revison History :      
modified by	: Pravesh K Chandel
Modified Date	:23 aug 2007
purpose		: Add modify by and modify date time param        
Used In         : Wolverine         
               drop PROC [dbo].[MNT_REIN_UPDATE_BANKINGDETAIL]   
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC [dbo].[MNT_REIN_UPDATE_BANKINGDETAIL]                
(                
               
	@REIN_BANK_DETAIL_ID	int ,
	@REIN_COMAPANY_ID INT ,
	@REIN_BANK_DETAIL_ADDRESS_1	nvarchar(50),
	@REIN_BANK_DETAIL_ADDRESS_2	nvarchar(50),
	@REIN_BANK_DETAIL_CITY	nvarchar(50),
	@REIN_BANK_DETAIL_COUNTRY	nvarchar(50),
	@REIN_BANK_DETAIL_STATE	nvarchar(5),
	@REIN_BANK_DETAIL_ZIP	varchar(11),
	
	@M_REIN_BANK_DETAIL_ADDRESS_1	nvarchar(50),
	@M_REIN_BANK_DETAIL_ADDRESS_2	nvarchar(50),
	@M_REIN_BANK_DETAIL_CITY	nvarchar(50),
	@M_REIN_BANK_DETAIL_COUNTRY	nvarchar(50),
	@M_REIN_BANK_DETAIL_STATE	nvarchar(5),
	@M_REIN_BANK_DETAIL_ZIP	varchar(11),
	
	@REIN_PAYMENT_BASIS nvarchar(75),
	@REIN_BANK_NAME	nvarchar(75),
	@REIN_TRANSIT_ROUTING	nvarchar(50),
	@REIN_BANK_ACCOUNT	nvarchar(30),
	@MODIFIED_BY		int=0,	
	@LAST_UPDATED_DATETIME datetime =null
  )                
AS                
BEGIN      
                
            
   UPDATE MNT_REIN_BANKING_DETAIL 
	SET               
          
	REIN_BANK_DETAIL_ID=@REIN_BANK_DETAIL_ID,
	REIN_COMAPANY_ID= @REIN_COMAPANY_ID ,
	REIN_BANK_DETAIL_ADDRESS_1=@REIN_BANK_DETAIL_ADDRESS_1,
	REIN_BANK_DETAIL_ADDRESS_2=@REIN_BANK_DETAIL_ADDRESS_2,     
	REIN_BANK_DETAIL_CITY=@REIN_BANK_DETAIL_CITY,
	REIN_BANK_DETAIL_COUNTRY=@REIN_BANK_DETAIL_COUNTRY,
	REIN_BANK_DETAIL_STATE=@REIN_BANK_DETAIL_STATE,
	REIN_BANK_DETAIL_ZIP=@REIN_BANK_DETAIL_ZIP,

	M_REIN_BANK_DETAIL_ADDRESS_1=@M_REIN_BANK_DETAIL_ADDRESS_1 ,      
	M_REIN_BANK_DETAIL_ADDRESS_2=@M_REIN_BANK_DETAIL_ADDRESS_2,        
	M_REIN_BANK_DETAIL_CITY  = @M_REIN_BANK_DETAIL_CITY   , 
	M_REIN_BANK_DETAIL_COUNTRY =@M_REIN_BANK_DETAIL_COUNTRY,       
	M_REIN_BANK_DETAIL_STATE=@M_REIN_BANK_DETAIL_STATE,       
	M_REIN_BANK_DETAIL_ZIP=@M_REIN_BANK_DETAIL_ZIP,  
	
	REIN_PAYMENT_BASIS=@REIN_PAYMENT_BASIS,
	REIN_BANK_NAME=@REIN_BANK_NAME,
	REIN_TRANSIT_ROUTING=@REIN_TRANSIT_ROUTING,
	REIN_BANK_ACCOUNT=@REIN_BANK_ACCOUNT,
        MODIFIED_BY=@MODIFIED_BY,     
	LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME

WHERE     REIN_COMAPANY_ID= @REIN_COMAPANY_ID           
       
   
        
     END






GO

