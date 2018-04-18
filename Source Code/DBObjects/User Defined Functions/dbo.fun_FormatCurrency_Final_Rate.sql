IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_FormatCurrency_Final_Rate]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_FormatCurrency_Final_Rate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Function Name :  dbo.fun_FormatCurrency_Final_Rate  
CREATED BY      : Abhishek Goel         
DATE            : 14 July 2011      
PURPOSE         : TO Formate Final Rate basis of LAnguage  
REVISON HISTORY :                                        
USED IN         : Ebix Advantage  
DROP FUNCTION [dbo].[fun_FormatCurrency_Final_Rate]       
*/  
CREATE function [dbo].[fun_FormatCurrency_Final_Rate]  
(  
@AmountToFormat Decimal(25, 4),  
@Lang_id nvarchar(4) ='USD'  
) returns  varchar(25)  
as  
begin  
DECLARE @MAX_LENGTH MONEY  
DECLARE @SUB_AMOUNT NVARCHAR(1)  
 SET @MAX_LENGTH =922337203685477.5807  
  
 IF(@AmountToFormat>@MAX_LENGTH)  
 BEGIN  
 SELECT @SUB_AMOUNT = SUBSTRING(CAST(@AmountToFormat AS NVARCHAR(25)), 1, 1);  
 SET @AmountToFormat= CAST( SUBSTRING(CAST(@AmountToFormat AS NVARCHAR(25)),  2,LEN(@AmountToFormat)) AS MONEY)  
   
 END  
 ELSE  
 SET @SUB_AMOUNT=''  
  
DECLARE @Amount MONEY   
select @Amount = @AmountToFormat   
Declare @FormatedCurAmount Varchar (25)  
if (@Lang_id='2' OR @Lang_id='BRL')  
begin  
 SELECT @FormatedCurAmount = CONVERT(VARCHAR,@Amount,2)                               
 SELECT @FormatedCurAmount = replace(@FormatedCurAmount, '.', '#')         
 SELECT @FormatedCurAmount = replace(@FormatedCurAmount, ',', '.')        
 SELECT @FormatedCurAmount = replace(@FormatedCurAmount, '#', ',')         
  
end  
else  
begin  
 SELECT @FormatedCurAmount = CONVERT(VARCHAR,@Amount,1)                               
end  
  
return @SUB_AMOUNT+@FormatedCurAmount  
  
end  
  
  

GO

