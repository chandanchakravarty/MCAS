IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPCForAppPol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPCForAppPol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name          : Dbo.Proc_GetPPCForPolicy              
Created by           : Pravesh K Chandel
Date                 : 25-Sep-2008            
Purpose              :Geting PPC Class form App/POl
Revison History :              
Used In                :   Wolverine                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
-- drop procedure dbo.Proc_GetPPCForAppPol '2716 mockingbird drive','kalamazoo','MI','49008'             
CREATE   PROCEDURE dbo.Proc_GetPPCForAppPol
(              
@ADD1 NVARCHAR(100) ,                  
@CITY NVARCHAR(50) ,            
@STATE_CODE NVARCHAR(10),            
@ZIP NVARCHAR(12)
)  
as           
BEGIN

DECLARE @HOUSE_NO AS NVARCHAR(10)            
DECLARE @STREET AS NVARCHAR(100)            
DECLARE @INDEXOFSPACE AS INT            
DECLARE @O_E_B AS NVARCHAR(5)            

SET @INDEXOFSPACE = CHARINDEX (' ', @ADD1)                
           
IF(@INDEXOFSPACE <> 0)            
BEGIN            
  SET @HOUSE_NO = SUBSTRING (@ADD1,1,@INDEXOFSPACE - 1)            
  SET @STREET = SUBSTRING(@ADD1,@INDEXOFSPACE+1, LEN(@ADD1))            
          
  IF dbo.Func_IsNumeric(@HOUSE_NO) <> 0          
  BEGIN          
  IF CONVERT(INT, @HOUSE_NO) % 2 = 1            
    SET @O_E_B = 'O'            
  ELSE            
    SET @O_E_B = 'E'            
  END          
END            
ELSE            
BEGIN          
  SET @HOUSE_NO = ''          
  SET @STREET = @ADD1            
END          
        
IF LEN(@ZIP) > 5        
SET @ZIP = SUBSTRING(@ZIP,1,5)        
--SELECT @HOUSE_NO, @STREET, @STATE, @ZIP, @CITY            
IF dbo.Func_IsNumeric(@HOUSE_NO) <> 0          
BEGIN  
        
SELECT * FROM APP_PPC_STATE_ADD APSA WHERE APSA.STATE_CODE = @STATE_CODE AND APSA.ZIP_CODE = @ZIP 
    AND IS_NUMERIC = 'Y' 
	AND CONVERT(INT, isnull(APSA.LOW,0)) <= CONVERT(INT, @HOUSE_NO) 
	AND CONVERT(INT, isnull(APSA.HIGH,@HOUSE_NO)) >= CONVERT(INT, @HOUSE_NO) 
	AND isnull(APSA.O_E_B,'B') IN (@O_E_B, 'B') 
	AND APSA.CITY = upper(@CITY) 
	AND (
		ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,''))) LIKE ('%' + upper(@STREET) + '%')              
		OR
		 upper(@STREET) LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,'')))  + '%')
		OR
		 upper(@STREET) LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')))  + '%')
		OR
		 upper(@STREET) LIKE ('%' + ltrim(APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,'')) + '%')
		)
             
UNION      

SELECT * FROM APP_PPC_STATE_ADD APSA WHERE APSA.STATE_CODE = @STATE_CODE AND APSA.ZIP_CODE = @ZIP 
    AND IS_NUMERIC = 'N' 
	AND (APSA.LOW LIKE '%' + @HOUSE_NO + '%'  OR APSA.HIGH LIKE '%' + @HOUSE_NO + '%' )
	AND ISNULL(APSA.O_E_B,'B') IN (@O_E_B, 'B') 
	AND APSA.CITY = upper(@CITY) 
	AND (
		ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET  + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,''))) LIKE ('%' + upper(@STREET) + '%')              
		 OR
		 upper(@STREET)  LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET  + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,''))) + '%')
		OR
		 upper(@STREET) LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')))  + '%')
		OR
		 upper(@STREET) LIKE ('%' + ltrim(APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,'')) + '%')

         )
  IF @@ROWCOUNT = 0            
  BEGIN            
  SELECT * FROM APP_PPC_STATE_ADD APSA WHERE APSA.STATE_CODE = @STATE_CODE AND APSA.ZIP_CODE = @ZIP 
	AND  APSA.CITY = upper(@CITY) 
	AND (
		ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')+ ' ' + ISNULL(APSA.TYPE,''))) LIKE ('%' + upper(@STREET) + '%')            
		OR
		  upper(@STREET)   LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')+ ' ' + ISNULL(APSA.TYPE,''))) + '%')           
		OR
		 upper(@STREET) LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')))  + '%')
		OR
		 upper(@STREET) LIKE ('%' + ltrim(APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,'')) + '%')
         )   
    IF @@ROWCOUNT = 0             
    SELECT * FROM APP_PPC_STATE_ADD APSA WHERE APSA.STATE_CODE = @STATE_CODE 
		AND APSA.ZIP_CODE = @ZIP 
		AND APSA.CITY = upper(@CITY)             
  END          
END          
ELSE          
   BEGIN          
	   SET @STREET = @ADD1          
	   SELECT * FROM APP_PPC_STATE_ADD APSA WHERE APSA.STATE_CODE = @STATE_CODE AND APSA.ZIP_CODE = @ZIP 
			AND APSA.CITY = upper(@CITY) 
			AND (
				ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')+ ' ' + ISNULL(APSA.TYPE,''))) LIKE ('%' + upper(@STREET) + '%')            
				OR
				upper(@STREET)  LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')+ ' ' + ISNULL(APSA.TYPE,'')))   + '%')
				OR
				 upper(@STREET) LIKE ('%' + ltrim((ISNULL(APSA.PRE_CODE,'') + ' ' + APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'')))  + '%')
				OR
				upper(@STREET) LIKE ('%' + ltrim(APSA.STREET + ' ' + ISNULL(APSA.POST_CODE,'') + ' ' + ISNULL(APSA.TYPE,'')) + '%')

				)
		IF @@ROWCOUNT = 0             
			SELECT * FROM APP_PPC_STATE_ADD APSA WHERE APSA.STATE_CODE = @STATE_CODE 
				AND APSA.ZIP_CODE = @ZIP 
				AND APSA.CITY = upper(@CITY)             
  END          
END






GO

