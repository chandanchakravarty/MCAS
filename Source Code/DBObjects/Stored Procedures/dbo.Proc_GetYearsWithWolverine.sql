IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetYearsWithWolverine]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetYearsWithWolverine]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
/*----------------------------------------------------------                        
Proc Name    : dbo.Proc_GetYearsWithWolverine        
Created by    : Sibin Philip             
Date          : 18 feb 2009              
Purpose       : Get the Years with wolverine        
Revison History  :                              
 ------------------------------------------------------------                                    
Date     Review By          Comments                                  
                         
------   ------------       -------------------------*/                
-- drop proc dbo.Proc_GetYearsWithWolverine   
--DECLARE @YWW INT;                       
--EXEC PROC_GETYEARSWITHWOLVERINE 1692,1,1,@YWW OUTPUT;     
--SELECT @YWW;      
  
CREATE PROCEDURE [dbo].[Proc_GetYearsWithWolverine]              
(              
 @CUSTOMER_ID int,              
 @APP_ID int,              
 @APP_VERSION_ID int,   
 @YEARS_WITH_WOLVERINE INT OUTPUT             
)                  
AS      
 DECLARE @YEARS INT  
BEGIN                        
                         
 SELECT @YEARS=YEARS_INSU_WOL              
 FROM APP_AUTO_GEN_INFO             
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID    
  
 IF(@YEARS>2)  
  SET @YEARS_WITH_WOLVERINE=1  
   
 ELSE  
  SET @YEARS_WITH_WOLVERINE=0  
  
/* Commented by Charles for Itrack Issue 6012 on 2-Jul-2009  
SELECT ISNULL(YEARS_INSU_WOL,0) as "YEARSCONTINSUREDWITHWOLVERINE",ISNULL(YEARS_INSU,0) as "YEARSCONTINSURED"   
FROM APP_AUTO_GEN_INFO                   
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID  
*/
         
END  
  
  
GO

