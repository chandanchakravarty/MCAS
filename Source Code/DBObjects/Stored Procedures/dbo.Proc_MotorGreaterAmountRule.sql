IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MotorGreaterAmountRule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MotorGreaterAmountRule]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name       : dbo.Proc_MotorGreaterAmountRule               
Created by      : SHAFE      
Date            : 01/20/2006      
Purpose         : To Find the Motor Which Has Amount Greater Than 40000      

Modified by 	: Swastika
Modified Date	: 28th Apr'06
Purpose		: changed amount to 40000              
            
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/      
--drop proc  Proc_MotorGreaterAmountRule       
CREATE PROC dbo.Proc_MotorGreaterAmountRule                
(                
@CUSTOMER_ID    int,                
@APP_ID          int,                
@APP_VERSION_ID  int                
)                
AS                
BEGIN                
         
       
if exists    
   (select customer_id from app_auto_gen_info where       
                    CUSTOMER_ID  =@CUSTOMER_ID AND                
                     APP_ID   =@APP_ID AND                
                     APP_VERSION_ID =@APP_VERSION_ID)    
begin     
 if exists(SELECT customer_id FROM APP_VEHICLES  where    
                    CUSTOMER_ID  =@CUSTOMER_ID AND                
                    APP_ID   =@APP_ID AND                
                    APP_VERSION_ID =@APP_VERSION_ID AND                
                     AMOUNT > 40000 and IS_ACTIVE ='Y')    
 begin     
     update app_auto_gen_info set IS_COST_OVER_DEFINED_LIMIT=1 where     
                    CUSTOMER_ID  =@CUSTOMER_ID AND                
                     APP_ID   =@APP_ID AND                
                     APP_VERSION_ID =@APP_VERSION_ID    
 end    
 else    
 begin     
    update app_auto_gen_info set IS_COST_OVER_DEFINED_LIMIT=0,IS_COST_OVER_DEFINED_LIMIT_DESC='' where     
                    CUSTOMER_ID  =@CUSTOMER_ID AND                
                     APP_ID   =@APP_ID AND                
                     APP_VERSION_ID =@APP_VERSION_ID    
 end    
         
end    
      
/*SELECT * FROM APP_VEHICLES      
WHERE             
  CUSTOMER_ID  =@CUSTOMER_ID AND                
  APP_ID   =@APP_ID AND                
  APP_VERSION_ID =@APP_VERSION_ID AND                
   AMOUNT > 30000 and IS_ACTIVE ='Y'     */    
END                
              
            
          





GO

