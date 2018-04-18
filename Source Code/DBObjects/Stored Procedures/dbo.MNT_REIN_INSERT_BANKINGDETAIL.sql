IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_INSERT_BANKINGDETAIL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_INSERT_BANKINGDETAIL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.[MNT_REIN_INSERT_BANKINGDETAIL]                
Created by      : Harmanjeet Singh               
Date            : April 20, 2007              
Purpose         : To insert the data into Reinsurer Contact table.                
Revison History :                
modified by	: Pravesh K Chandel
Modified Date	:23 aug 2007
purpose		: Add created by and creted date time param
Used In         : Wolverine         
        drop  PROC [dbo].[MNT_REIN_INSERT_BANKINGDETAIL]      
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
CREATE PROC [dbo].[MNT_REIN_INSERT_BANKINGDETAIL]                
(                
               
	@REIN_BANK_DETAIL_ID	int OUTPUT,
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
	@CREATED_BY	int=0,
	@CREATED_DATETIME	datetime=null
	
	
  )                
AS                
BEGIN      
                
SELECT @REIN_BANK_DETAIL_ID = isnull(Max(REIN_BANK_DETAIL_ID),0)+1 FROM MNT_REIN_BANKING_DETAIL        
              
   INSERT INTO MNT_REIN_BANKING_DETAIL                
   (               
	REIN_BANK_DETAIL_ID,
	REIN_COMAPANY_ID,
	REIN_BANK_DETAIL_ADDRESS_1,
	REIN_BANK_DETAIL_ADDRESS_2,     
	REIN_BANK_DETAIL_CITY,
	REIN_BANK_DETAIL_COUNTRY,
	REIN_BANK_DETAIL_STATE,
	REIN_BANK_DETAIL_ZIP,

	M_REIN_BANK_DETAIL_ADDRESS_1 ,      
	M_REIN_BANK_DETAIL_ADDRESS_2,        
	M_REIN_BANK_DETAIL_CITY      , 
	M_REIN_BANK_DETAIL_COUNTRY ,       
	M_REIN_BANK_DETAIL_STATE,       
	M_REIN_BANK_DETAIL_ZIP,  
  
	REIN_PAYMENT_BASIS,
	REIN_BANK_NAME,
	REIN_TRANSIT_ROUTING,
	REIN_BANK_ACCOUNT,
	IS_ACTIVE,
	CREATED_BY,
	CREATED_DATETIME
	              
   )                
   VALUES                
   (    
	@REIN_BANK_DETAIL_ID	,
	@REIN_COMAPANY_ID ,
	@REIN_BANK_DETAIL_ADDRESS_1,
	@REIN_BANK_DETAIL_ADDRESS_2,
	@REIN_BANK_DETAIL_CITY,
	@REIN_BANK_DETAIL_COUNTRY,
	@REIN_BANK_DETAIL_STATE,
	@REIN_BANK_DETAIL_ZIP,

	@M_REIN_BANK_DETAIL_ADDRESS_1,
	@M_REIN_BANK_DETAIL_ADDRESS_2,
	@M_REIN_BANK_DETAIL_CITY,
	@M_REIN_BANK_DETAIL_COUNTRY	,
	@M_REIN_BANK_DETAIL_STATE,
	@M_REIN_BANK_DETAIL_ZIP	,

	@REIN_PAYMENT_BASIS,
	@REIN_BANK_NAME	,
	@REIN_TRANSIT_ROUTING	,
	@REIN_BANK_ACCOUNT,
	'Y',
	@CREATED_BY	,
	@CREATED_DATETIME
	
	  )    
   
        
     END






GO

