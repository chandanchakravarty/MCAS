IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_FormatCurrencyExceedsMoney]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_FormatCurrencyExceedsMoney]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                        
Function Name :  dbo.fun_FormatCurrency  
CREATED BY      : Pravesh K Chandel           
DATE            : 10 Dec 2010      
PURPOSE         : TO Formate Currency basis of LAnguage  
REVISON HISTORY :                                        
USED IN         : Ebix Advantage  
*/  
-- select dbo.[fun_FormatCurrencyExceedsMoney](92233720368546.1233,1)
CREATE function [dbo].[fun_FormatCurrencyExceedsMoney]  
(  
@AmountToFormat Decimal(20, 4)  ,
@Lang_id nvarchar(4) ='USD'  
) returns  varchar(25)  
as  
begin  
  
DECLARE @Amount MONEY
select @Amount = @AmountToFormat   
Declare @FormatedCurAmount Varchar (20)  
if (@Lang_id='2' OR @Lang_id='BRL')  
begin  
 SELECT @FormatedCurAmount = CONVERT(VARCHAR,@Amount,1)                               
 SELECT @FormatedCurAmount = replace(@FormatedCurAmount, '.', '#')         
 SELECT @FormatedCurAmount = replace(@FormatedCurAmount, ',', '.')        
 SELECT @FormatedCurAmount = replace(@FormatedCurAmount, '#', ',')         
  
end  
else  
begin  
 SELECT @FormatedCurAmount = CONVERT(VARCHAR,@Amount,1)                               
end  
  
return @FormatedCurAmount  
  
end  
  
GO

