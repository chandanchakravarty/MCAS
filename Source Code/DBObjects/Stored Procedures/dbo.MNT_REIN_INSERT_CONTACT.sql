IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_REIN_INSERT_CONTACT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[MNT_REIN_INSERT_CONTACT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                  
Proc Name       : dbo.MNT_REIN_INSERT_CONTACT                  
Created by      : Harmanjeet Singh                 
Date            : April 20, 2007                
Purpose         : To insert the data into Reinsurer Contact table.                  
Revison History :                  
Used In         : Wolverine           
drop PROC [dbo].[MNT_REIN_INSERT_CONTACT]  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/     
--drop PROC [dbo].[MNT_REIN_INSERT_CONTACT]      
CREATE PROC [dbo].[MNT_REIN_INSERT_CONTACT]                  
(                  
                 
 @REIN_CONTACT_ID int OUTPUT,  
 @REIN_CONTACT_NAME nvarchar(75),  
 @REIN_CONTACT_CODE nvarchar(6),  
 @REIN_CONTACT_POSITION nvarchar(30),  
 @REIN_CONTACT_SALUTATION nvarchar(50),  
 @REIN_CONTACT_ADDRESS_1 nvarchar(50),  
 @REIN_CONTACT_ADDRESS_2 nvarchar(50),  
 @REIN_CONTACT_CITY nvarchar(50),  
 @REIN_CONTACT_COUNTRY nvarchar(50),  
 @REIN_CONTACT_STATE nvarchar(50),  
 @REIN_CONTACT_ZIP varchar(11),  
 @REIN_CONTACT_PHONE_1 nvarchar(20),  
 @REIN_CONTACT_PHONE_2 nvarchar(20),  
 @REIN_CONTACT_EXT_1 nvarchar(5),  
 @REIN_CONTACT_EXT_2 nvarchar(5),  
 @M_REIN_CONTACT_ADDRESS_1 nvarchar(50),  
 @M_REIN_CONTACT_ADDRESS_2 nvarchar(50),  
 @M_REIN_CONTACT_CITY nvarchar(50),  
 @M_REIN_CONTACT_COUNTRY nvarchar(50),  
 @M_REIN_CONTACT_STATE nvarchar(50),  
 @M_REIN_CONTACT_ZIP varchar(11),  
 @M_REIN_CONTACT_PHONE_1 nvarchar(20),  
 @M_REIN_CONTACT_PHONE_2 nvarchar(20),  
 @M_REIN_CONTACT_EXT_1 nvarchar(5),  
 @M_REIN_CONTACT_EXT_2 nvarchar(5),  
 @REIN_CONTACT_MOBILE nvarchar(20),  
 @REIN_CONTACT_FAX nvarchar(20),  
 @REIN_CONTACT_SPEED_DIAL nvarchar(4),  
 @REIN_CONTACT_EMAIL_ADDRESS nvarchar(50),  
 @REIN_CONTACT_CONTRACT_DESC nvarchar(30),  
 @REIN_CONTACT_COMMENTS nvarchar(255),  
 @REIN_COMAPANY_ID int,  
 @CREATED_BY INT=0,  
 @CREATED_DATETIME DATETIME  =null  
   
   
  )                  
AS                  
BEGIN        
                  
SELECT @REIN_CONTACT_ID = isnull(Max(REIN_CONTACT_ID),0)+1 FROM MNT_REIN_CONTACT          
                
   INSERT INTO MNT_REIN_CONTACT                  
   (                 
 REIN_CONTACT_ID,  
 REIN_CONTACT_NAME,  
 REIN_CONTACT_CODE,  
 REIN_CONTACT_POSITION,  
 REIN_CONTACT_SALUTATION,  
 REIN_CONTACT_ADDRESS_1,  
 REIN_CONTACT_ADDRESS_2,  
 REIN_CONTACT_CITY,  
 REIN_CONTACT_COUNTRY,  
 REIN_CONTACT_STATE,  
 REIN_CONTACT_ZIP,  
 REIN_CONTACT_PHONE_1,  
 REIN_CONTACT_PHONE_2,  
 REIN_CONTACT_EXT_1,  
 REIN_CONTACT_EXT_2,  
 M_REIN_CONTACT_ADDRESS_1,  
 M_REIN_CONTACT_ADDRESS_2,  
 M_REIN_CONTACT_CITY,  
 M_REIN_CONTACT_COUNTRY,  
 M_REIN_CONTACT_STATE,  
 M_REIN_CONTACT_ZIP,  
 M_REIN_CONTACT_PHONE_1,  
 M_REIN_CONTACT_PHONE_2,  
 M_REIN_CONTACT_EXT_1,  
 M_REIN_CONTACT_EXT_2,  
 REIN_CONTACT_MOBILE,  
 REIN_CONTACT_FAX,  
 REIN_CONTACT_SPEED_DIAL,  
 REIN_CONTACT_EMAIL_ADDRESS,  
 REIN_CONTACT_CONTRACT_DESC,  
 REIN_CONTACT_COMMENTS,  
 REIN_COMAPANY_ID,  
 IS_ACTIVE,  
 CREATED_BY,  
 CREATED_DATETIME   
   
                 
   )                  
   VALUES                  
   (      
 @REIN_CONTACT_ID,  
 @REIN_CONTACT_NAME,  
 @REIN_CONTACT_CODE,  
 @REIN_CONTACT_POSITION,  
 @REIN_CONTACT_SALUTATION,  
 @REIN_CONTACT_ADDRESS_1,  
 @REIN_CONTACT_ADDRESS_2,  
 @REIN_CONTACT_CITY,  
 @REIN_CONTACT_COUNTRY,  
 @REIN_CONTACT_STATE,  
 @REIN_CONTACT_ZIP,  
 @REIN_CONTACT_PHONE_1,  
 @REIN_CONTACT_PHONE_2,  
 @REIN_CONTACT_EXT_1,  
 @REIN_CONTACT_EXT_2,  
 @M_REIN_CONTACT_ADDRESS_1,  
 @M_REIN_CONTACT_ADDRESS_2,  
 @M_REIN_CONTACT_CITY,  
 @M_REIN_CONTACT_COUNTRY,  
 @M_REIN_CONTACT_STATE,  
 @M_REIN_CONTACT_ZIP,  
 @M_REIN_CONTACT_PHONE_1,  
 @M_REIN_CONTACT_PHONE_2,  
 @M_REIN_CONTACT_EXT_1,  
 @M_REIN_CONTACT_EXT_2,  
 @REIN_CONTACT_MOBILE,  
 @REIN_CONTACT_FAX,  
 @REIN_CONTACT_SPEED_DIAL,  
 @REIN_CONTACT_EMAIL_ADDRESS,  
 @REIN_CONTACT_CONTRACT_DESC,  
 @REIN_CONTACT_COMMENTS,  
 @REIN_COMAPANY_ID,  
 'Y',  
 @CREATED_BY ,  
 @CREATED_DATETIME  
    )      
     
       
     END  
  
  
  
  
  
  
  
  
  




GO

