IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Fun_GetMAX_MINValueForHome]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Fun_GetMAX_MINValueForHome]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                                        
----------------------------------------------------------                                            
Proc Name       : dbo.Proc_GetMAX_MINValueForHome                                        
Created by      : Manoj Rathore                                          
Date            : 7th Jan 2008            
Purpose         :             
Revison History :              
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------                                           
Proc_GetMAX_MINValueForHome 793,1,1,1  
*/             
--drop function dbo.Fun_GetMAX_MINValueForHome        
CREATE function dbo.Fun_GetMAX_MINValueForHome      
(            
 @CUSTOMER_ID INT,            
 @APP_POL_ID INT,            
 @VERSION_ID INT,
 @CALLED_FOR varchar(5)   
)   returns varchar(1)         
            
AS            
BEGIN            
  DECLARE @APP_INCEPTION_DATE varchar(20)
  DECLARE @EFF_DATE  VARCHAR(20)
  SET @EFF_DATE='01/01/2008'
if (@CALLED_FOR='APP')
  SELECT  @APP_INCEPTION_DATE=ISNULL(DATEDIFF(DAY,@EFF_DATE,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101)),'')          
  FROM APP_LIST    WITH(NOLOCK)               
  WHERE                           
  CUSTOMER_ID=@CUSTOMER_ID AND                          
  APP_ID=@APP_POL_ID AND                          
  APP_VERSION_ID=@VERSION_ID   
else
  SELECT  @APP_INCEPTION_DATE=ISNULL(DATEDIFF(DAY,@EFF_DATE,CONVERT(VARCHAR(20),APP_INCEPTION_DATE,101)),'')          
  FROM POL_customer_policy_LIST    WITH(NOLOCK)               
  WHERE                           
  CUSTOMER_ID=@CUSTOMER_ID AND                          
  policy_ID=@APP_POL_ID AND                          
  POLICY_VERSION_ID=@VERSION_ID   

            
 DECLARE @VERIFY_RULE VARCHAR
 IF (@APP_INCEPTION_DATE >=0)
	 BEGIN              
	 SET @VERIFY_RULE ='Y'                                    
	 END              
 ELSE              
	 BEGIN              
	 SET @VERIFY_RULE ='N'                                    
	 END              
            
return @VERIFY_RULE

END








GO

