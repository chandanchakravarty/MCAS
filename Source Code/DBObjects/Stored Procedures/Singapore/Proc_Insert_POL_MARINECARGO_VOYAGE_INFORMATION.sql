/*----------------------------------------------------------          
Proc Name       : [Proc_Insert_POL_MARINECARGO_VOYAGE_INFORMATION]          
Created by      : Ruchika Chauhan  
Date            : 23-March-2012        
Purpose   : Demo          
Revison History :          
Used In        : Singapore          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_POL_MARINECARGO_VOYAGE_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_POL_MARINECARGO_VOYAGE_INFORMATION]
GO


CREATE PROC dbo.Proc_Insert_POL_MARINECARGO_VOYAGE_INFORMATION(          
   @CUSTOMER_ID int          
           ,@POLICY_ID int          
           ,@POLICY_VERSION_ID smallint          
           --,@RISK_ID int          
           ,@VOYAGE_TO int = null          
           ,@VOYAGE_FROM int = null          
           ,@THENCE_TO int = null                             
           ,@QUANTITY_DESCRIPTION nvarchar(2000) = null                                
           ,@IS_ACTIVE nchar(1) = null          
           ,@CREATED_BY int = null          
           ,@CREATED_DATETIME datetime = null          
           ,@MODIFIED_BY int  = null          
           ,@LAST_UPDATED_DATETIME datetime = null 
           ,@VOYAGE_INFO_ID int OUTPUT         
           )          
          
AS                             
          
DECLARE @MAX_ID INT,
@RISK_ID INT


SELECT @MAX_ID=ISNULL(MAX(VOYAGE_INFO_ID),0)+1 FROM POL_MARINECARGO_VOYAGE_INFORMATION

SELECT @RISK_ID=ISNULL(MAX(VOYAGE_INFO_ID),0)+1 FROM POL_MARINECARGO_VOYAGE_INFORMATION where CUSTOMER_ID = @CUSTOMER_ID          
and POLICY_ID = @POLICY_ID          
          
INSERT INTO [dbo].[POL_MARINECARGO_VOYAGE_INFORMATION]          
           ([VOYAGE_INFO_ID]  
           ,[CUSTOMER_ID]          
           ,[POLICY_ID]          
           ,[POLICY_VERSION_ID]          
           ,[RISK_ID]          
           ,[VOYAGE_TO]          
           ,[VOYAGE_FROM]          
           ,[THENCE_TO]                           
           ,[QUANTITY_DESCRIPTION]                     
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
           ,@RISK_ID         
           ,@VOYAGE_TO          
           ,@VOYAGE_FROM          
           ,@THENCE_TO                            
           ,@QUANTITY_DESCRIPTION                                   
           ,@IS_ACTIVE           
           ,@CREATED_BY          
           ,@CREATED_DATETIME          
           ,@MODIFIED_BY          
           ,@LAST_UPDATED_DATETIME        
           )          
         
SET @VOYAGE_INFO_ID=  @MAX_ID            

RETURN 1                     
          
       
  



