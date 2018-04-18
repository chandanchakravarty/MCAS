/*----------------------------------------------------------          
Proc Name       : [Proc_Insert_QQ_MARINECARGO_RISK_DETAILS]          
Created by      : Ruchika Chauhan  
Date            : 19-March-2012        
Purpose   : Demo          
Revison History :          
Used In        : Singapore          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_QQ_MARINECARGO_RISK_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_QQ_MARINECARGO_RISK_DETAILS]
GO

  
     
CREATE PROC dbo.Proc_Insert_QQ_MARINECARGO_RISK_DETAILS(          
   @CUSTOMER_ID int          
           ,@POLICY_ID int          
           ,@POLICY_VERSION_ID smallint          
           ,@QUOTE_ID int          
           ,@VOYAGE_TO int = null          
           ,@VOYAGE_FROM int = null          
           ,@THENCE_TO int = null                
           ,@VESSEL int = null                  
           ,@AIRCRAFT_NUMBER nvarchar(40) = null       
           ,@LAND_TRANSPORT nvarchar(40) = null       
           ,@VOYAGE_FROM_DATE datetime = null       
           ,@QUANTITY_DESCRIPTION nvarchar(2000) = null       
           ,@INSURANCE_CONDITIONS1 decimal(18,2) = null       
           ,@INSURANCE_CONDITIONS2 decimal(18,2) = null       
           ,@INSURANCE_CONDITIONS3 decimal(18,2) = null  
           ,@INSURANCE_CONDITIONS1_SELECTION nvarchar(20) = null                       
           ,@IS_ACTIVE nchar(1) = null          
           ,@CREATED_BY int = null          
           ,@CREATED_DATETIME datetime = null          
           ,@MODIFIED_BY int  = null          
           ,@LAST_UPDATED_DATETIME datetime = null        
           )          
          
AS          
          
          
IF NOT EXISTS(select * from QQ_MARINECARGO_RISK_DETAILS where CUSTOMER_ID = @CUSTOMER_ID          
and POLICY_ID = @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID and QUOTE_ID = @QUOTE_ID)          
          
BEGIN          
          
DECLARE @MAX_ID INT,
@MARINE_RATE DECIMAL(18,2)  
  
SELECT @MAX_ID=ISNULL(MAX(QQ_MARINECARGO_RISK_ID),0)+1 FROM QQ_MARINECARGO_RISK_DETAILS         
SET @MARINE_RATE = @INSURANCE_CONDITIONS1 + @INSURANCE_CONDITIONS2 + @INSURANCE_CONDITIONS3
          
INSERT INTO [dbo].[QQ_MARINECARGO_RISK_DETAILS]          
           ([QQ_MARINECARGO_RISK_ID]  
           ,[CUSTOMER_ID]          
           ,[POLICY_ID]          
           ,[POLICY_VERSION_ID]          
           ,[QUOTE_ID]          
           ,[VOYAGE_TO]          
           ,[VOYAGE_FROM]          
           ,[THENCE_TO]          
           ,[VESSEL]          
           ,[AIRCRAFT_NUMBER]          
           ,[LAND_TRANSPORT]          
           ,[VOYAGE_FROM_DATE]          
           ,[QUANTITY_DESCRIPTION]          
           ,[INSURANCE_CONDITIONS1]          
           ,[INSURANCE_CONDITIONS2]          
           ,[INSURANCE_CONDITIONS3] 
           ,[INSURANCE_CONDITIONS1_SELECTION]
           ,[MARINE_RATE] 
           ,[IS_ACTIVE]          
           ,[CREATED_BY]          
           ,[CREATED_DATETIME]          
           ,[MODIFIED_BY]          
           ,[LAST_UPDATED_DATETIME]        
          )          
     VALUES          
           (@MAX_ID  
           ,@CUSTOMER_ID          
           ,@POLICY_ID          
           ,@POLICY_VERSION_ID          
           ,@QUOTE_ID          
           ,@VOYAGE_TO          
           ,@VOYAGE_FROM          
           ,@THENCE_TO          
           ,@VESSEL          
           ,@AIRCRAFT_NUMBER          
           ,@LAND_TRANSPORT          
           ,@VOYAGE_FROM_DATE          
           ,@QUANTITY_DESCRIPTION          
           ,@INSURANCE_CONDITIONS1          
           ,@INSURANCE_CONDITIONS2          
           ,@INSURANCE_CONDITIONS3
           ,@INSURANCE_CONDITIONS1_SELECTION 
           ,@MARINE_RATE         
           ,@IS_ACTIVE           
           ,@CREATED_BY          
           ,@CREATED_DATETIME          
           ,@MODIFIED_BY          
           ,@LAST_UPDATED_DATETIME        
           )          
                     
RETURN 1                     
          
          
END          
          
ELSE          
--- UPDATE           
BEGIN          
 

SET @MARINE_RATE = @INSURANCE_CONDITIONS1 + @INSURANCE_CONDITIONS2 + @INSURANCE_CONDITIONS3
       
UPDATE [dbo].[QQ_MARINECARGO_RISK_DETAILS]          
   SET [VOYAGE_TO] = @VOYAGE_TO          
      ,[VOYAGE_FROM] = @VOYAGE_FROM          
      ,[THENCE_TO] = @THENCE_TO          
      ,[VESSEL] = @VESSEL     
      ,[AIRCRAFT_NUMBER] = @AIRCRAFT_NUMBER          
      ,[LAND_TRANSPORT] = @LAND_TRANSPORT         
      ,[VOYAGE_FROM_DATE] = @VOYAGE_FROM_DATE         
      ,[QUANTITY_DESCRIPTION] = @QUANTITY_DESCRIPTION         
      ,[INSURANCE_CONDITIONS1] = @INSURANCE_CONDITIONS1         
      ,[INSURANCE_CONDITIONS2] = @INSURANCE_CONDITIONS2         
      ,[INSURANCE_CONDITIONS3] = @INSURANCE_CONDITIONS3
      ,[INSURANCE_CONDITIONS1_SELECTION] = @INSURANCE_CONDITIONS1_SELECTION           
      ,[MARINE_RATE] = @MARINE_RATE 
      ,[MODIFIED_BY]=@MODIFIED_BY             
      ,[LAST_UPDATED_DATETIME]    =@LAST_UPDATED_DATETIME       
 WHERE [CUSTOMER_ID] = @CUSTOMER_ID and          
       [POLICY_ID] = @POLICY_ID and          
       [POLICY_VERSION_ID] = @POLICY_VERSION_ID and          
       [QUOTE_ID] = @QUOTE_ID         
                 
                 
 RETURN 2          
END 


