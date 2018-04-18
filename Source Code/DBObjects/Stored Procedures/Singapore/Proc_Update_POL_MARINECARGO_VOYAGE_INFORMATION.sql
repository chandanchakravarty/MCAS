/*----------------------------------------------------------          
Proc Name       : [Proc_Update_POL_MARINECARGO_VOYAGE_INFORMATION]          
Created by      : Ruchika Chauhan  
Date            : 23-March-2012        
Purpose   : Demo          
Revison History :          
Used In        : Singapore          
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/      


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Update_POL_MARINECARGO_VOYAGE_INFORMATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Update_POL_MARINECARGO_VOYAGE_INFORMATION]
GO


CREATE PROC dbo.Proc_Update_POL_MARINECARGO_VOYAGE_INFORMATION(          
			@VOYAGE_INFO_ID int
			--,@CUSTOMER_ID int          
   --        ,@POLICY_ID int          
   --        ,@POLICY_VERSION_ID smallint                           
           ,@VOYAGE_TO int = null          
           ,@VOYAGE_FROM int = null          
           ,@THENCE_TO int = null                             
           ,@QUANTITY_DESCRIPTION nvarchar(2000) = null                                                 
           ,@MODIFIED_BY int  = null          
           ,@LAST_UPDATED_DATETIME datetime = null        
           )          
          
AS                                      

IF EXISTS(select * from POL_MARINECARGO_VOYAGE_INFORMATION where VOYAGE_INFO_ID=@VOYAGE_INFO_ID)

BEGIN       

UPDATE [dbo].[POL_MARINECARGO_VOYAGE_INFORMATION]          
   SET [VOYAGE_TO] = @VOYAGE_TO          
      ,[VOYAGE_FROM] = @VOYAGE_FROM          
      ,[THENCE_TO] = @THENCE_TO                    
      ,[QUANTITY_DESCRIPTION] = @QUANTITY_DESCRIPTION  
      ,[MODIFIED_BY] = @MODIFIED_BY
      ,[LAST_UPDATED_DATETIME] = @LAST_UPDATED_DATETIME
              
WHERE [VOYAGE_INFO_ID] = @VOYAGE_INFO_ID
 --[CUSTOMER_ID] = @CUSTOMER_ID and          
 --      [POLICY_ID] = @POLICY_ID and          
 --      [POLICY_VERSION_ID] = @POLICY_VERSION_ID and          
              
     
END
                                
RETURN 1                     
          
       
  



