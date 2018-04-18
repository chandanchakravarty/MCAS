IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyCoveragesHome]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyCoveragesHome]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetPolicyCoveragesHome        
Created by      : Ravindra        
Date            : 03-13-2005       
Purpose         :       
Revison History :        
Used In         : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
      
--drop proc Proc_GetPolicyCoveragesHome        
      
 CREATE PROCEDURE dbo.Proc_GetPolicyCoveragesHome        
(            
 @CUSTOMER_ID int  ,        
 @POLICY_ID   int    ,    
 @POLICY_VERSION_ID int,  
 @CALLED_FROM varchar(10)        
         
)                
AS                     
        
BEGIN                          
        
/*SELECT B.COV_ID,      
 B.COV_CODE,      
 B.COV_DES,      
 A.LIMIT_1 AS COV_AMOUNT       
 FROM POL_DWELLING_SECTION_COVERAGES A        
INNER JOIN MNT_COVERAGE B ON        
 A.COVERAGE_CODE_ID = B.COV_ID        
WHERE  B.COV_ID IN(13,171,170,10,8,137,136,7,5,135,134,3,    
773,  
793,    
794,    
774,    
779,    
799,    
795,    
775,    
776,    
796,    
292,    
293,    
777,    
797,    
798,    
778    
)        
        
 AND POLICY_ID = @POLICY_ID         
 --AND POLICY_VERSION_ID = @POLICY_VERSION_ID         
 AND CUSTOMER_ID = @CUSTOMER_ID        
        
 AND A.DWELLING_ID = ( SELECT MIN(DWELLING_ID)         
     FROM POL_DWELLINGS_INFO        
     WHERE CUSTOMER_ID =@CUSTOMER_ID         
     AND POLICY_ID = @POLICY_ID         
     AND POLICY_VERSION_ID =@POLICY_VERSION_ID        
   )        
*/        

if @CALLED_FROM IS NULL OR @CALLED_FROM = ''
begin
	select 
		M.COV_ID,M.COV_CODE,M.COV_DES,C.LIMIT_1 AS COV_AMOUNT
	from
		POL_DWELLINGS_INFO D
	JOIN
		POL_DWELLING_SECTION_COVERAGES C
	ON
		D.CUSTOMER_ID = C.CUSTOMER_ID AND
		D.POLICY_ID = C.POLICY_ID AND
		D.POLICY_VERSION_ID = C.POLICY_VERSION_ID 
	JOIN
		MNT_COVERAGE M 
	ON
		M.COV_ID = C.COVERAGE_CODE_ID
	WHERE
		D.CUSTOMER_ID = @CUSTOMER_ID AND
		D.POLICY_ID = @POLICY_ID AND
		D.POLICY_VERSION_ID = @POLICY_VERSION_ID AND
		D.DWELLING_ID = 
			(SELECT TOP 1 DWELLING_ID FROM POL_DWELLINGS_INFO WHERE
				CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND
				POLICY_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'Y')='Y')
		AND ISNULL(D.IS_ACTIVE,'Y')='Y' AND
	 M.COV_ID IN 
	(13,171,170,10,8,137,136,7,5,135,134,3,773,793,794,774,779,799,795,775,776,796,292,293,777,797,798,778)		
end
else
begin
	select 
		M.COV_ID,M.COV_CODE,M.COV_DES,C.LIMIT_1 AS COV_AMOUNT
	from
		APP_DWELLINGS_INFO D
	JOIN
		APP_DWELLING_SECTION_COVERAGES C
	ON
		D.CUSTOMER_ID = C.CUSTOMER_ID AND
		D.APP_ID = C.APP_ID AND
		D.APP_VERSION_ID = C.APP_VERSION_ID 
	JOIN
		MNT_COVERAGE M 
	ON
		M.COV_ID = C.COVERAGE_CODE_ID
	WHERE
		D.CUSTOMER_ID = @CUSTOMER_ID AND
		D.APP_ID = @POLICY_ID AND
		D.APP_VERSION_ID = @POLICY_VERSION_ID AND
		D.DWELLING_ID = 
			(SELECT TOP 1 DWELLING_ID FROM APP_DWELLINGS_INFO WHERE
				CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @POLICY_ID AND
				APP_VERSION_ID = @POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'Y')='Y')
		AND ISNULL(D.IS_ACTIVE,'Y')='Y' AND
	 M.COV_ID IN 
	(13,171,170,10,8,137,136,7,5,135,134,3,773,793,794,774,779,799,795,775,776,796,292,293,777,797,798,778)		
end        
End          
          
      
      
        
      
    
  



GO

