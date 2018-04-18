IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------                                                      
Proc Name       : dbo.Proc_GetCustomerStatus                                                      
Created by      :  Mohit Agarwal                                                      
Date            :  20-Nov-2007                                                      
Purpose         :  To copy Customer, Co-Applicants, Attention Notes Tabs                                                      
Revison History :                                                      
Used In         :    Wolverine                                                      
                                                      
Modified By :                                                   
Modified Date  :                                      
Purpose  :                                                
                                      
------------------------------------------------------------                                                      
Date     Review By          Comments                                                      
------   ------------       -------------------------*/                                                      
-- drop PROC Dbo.Proc_GetCustomerStatus                                                        
CREATE PROC Dbo.Proc_GetCustomerStatus                                                      
(       
@CUSTOMER_ID int,  
@CALLED_FOR varchar(50)                                            
)                                                            
AS                                                            
BEGIN                         
DECLARE @CUSTOMER_CODE nvarchar(10),  
 @CUSTOMER_TYPE int,  
  @CUSTOMER_FIRST_NAME nvarchar(25),  
  @CUSTOMER_MIDDLE_NAME nvarchar(15),  
  @CUSTOMER_LAST_NAME nvarchar(25),  
 @COPY_CUSTOMER_ID int  
    
 SELECT @CUSTOMER_CODE=CUSTOMER_CODE,@CUSTOMER_TYPE=CUSTOMER_TYPE, @CUSTOMER_FIRST_NAME=CUSTOMER_FIRST_NAME,  
       @CUSTOMER_MIDDLE_NAME=CUSTOMER_MIDDLE_NAME, @CUSTOMER_LAST_NAME=CUSTOMER_LAST_NAME  
       FROM CLT_CUSTOMER_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID  
  
 SELECT  @COPY_CUSTOMER_ID=MAX(CUSTOMER_ID) FROM CLT_CUSTOMER_LIST WHERE     
ISNULL(@CUSTOMER_CODE,'')=ISNULL(CUSTOMER_CODE,'') AND ISNULL(@CUSTOMER_TYPE,0)=ISNULL(CUSTOMER_TYPE,0) AND ISNULL(@CUSTOMER_FIRST_NAME,'')=ISNULL(CUSTOMER_FIRST_NAME,'') AND   
       ISNULL(@CUSTOMER_MIDDLE_NAME,'')=ISNULL(CUSTOMER_MIDDLE_NAME,'') AND ISNULL(@CUSTOMER_LAST_NAME,'')=ISNULL(CUSTOMER_LAST_NAME,'')             
  
 IF (@CALLED_FOR = 'Agency')  
 BEGIN
  IF( EXISTS (SELECT * FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID )) 
	SELECT * FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID
  ELSE
	SELECT * FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID
 END

 ELSE IF (@CALLED_FOR = 'Customer')  
 BEGIN
  IF( EXISTS (SELECT * FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID )) 
	SELECT * FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID
  ELSE
	SELECT * FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID
 END 
  
END  
  


GO

