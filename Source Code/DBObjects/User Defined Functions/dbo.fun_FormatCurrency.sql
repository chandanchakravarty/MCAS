IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_FormatCurrency]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_FormatCurrency]
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
DROP FUNCTION [dbo].[fun_FormatCurrency]     
*/
create function [dbo].[fun_FormatCurrency]
(
@AmountToFormat Decimal(30, 4),
@Lang_id nvarchar(4) ='USD'
) returns  varchar(50)
as
begin
DECLARE @MAX_LENGTH MONEY
DECLARE @SUB_AMOUNT NVARCHAR(1)
 SET @MAX_LENGTH =922337203685477.5807
 Declare @FormatedCurAmount Varchar (50)
 IF(@AmountToFormat <= @MAX_LENGTH)
 BEGIN
		--SELECT @SUB_AMOUNT = SUBSTRING(CAST(@AmountToFormat AS NVARCHAR(25)), 1, 1);
		--SET @AmountToFormat= CAST( SUBSTRING(CAST(@AmountToFormat AS NVARCHAR(25)),  2,LEN(@AmountToFormat)) AS MONEY)
 
	 --END
	 --ELSE
		--SET @SUB_AMOUNT=''

	DECLARE @Amount MONEY 
	select @Amount = @AmountToFormat 

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
END 

ELSE 
	BEGIN 
	
		DECLARE @Group_Seprator NVARCHAR(1)
		DECLARE @Decimal_Seprator NVARCHAR(1)		
		DECLARE @after NVARCHAR(40)		
		DECLARE @before NVARCHAR(40)		
		DECLARE @return_value NVARCHAR(50)
		DECLARE @i INT
		DECLARE @AmountToFormat_nvarchar nvarchar(50) = CAST( CAST(@AmountToFormat AS DECIMAL(25,2)) AS NVARCHAR(50))
		
		if (@Lang_id='2' OR @Lang_id='BRL') 
			SELECT @Group_Seprator = '.' ,@Decimal_Seprator = ','
		ELSE
			SELECT @Group_Seprator = ',' ,@Decimal_Seprator = '.' 
			
       SELECT @before =  substring(@AmountToFormat_nvarchar, 1, charindex ('.', @AmountToFormat_nvarchar)-1)	
	   SELECT @after  =  substring(@AmountToFormat_nvarchar,charindex ('.', @AmountToFormat_nvarchar )+1 ,  len(@AmountToFormat_nvarchar))		
	--SELECT @before,@after,@AmountToFormat_nvarchar
    if len(@before)>3 
	 begin
		set @i = 3
		while @i>1 and @i < len(@before)
			begin
				set @before = substring(@before,1,len(@before)-@i) + @Group_Seprator + right(@before,@i)
				set @i = @i + 4
			end
	 end

	
	

	SET @FormatedCurAmount =   @before + @Decimal_Seprator	+  @after 
		
			  	
	END 
	

--return @SUB_AMOUNT+ @FormatedCurAmount
return @FormatedCurAmount

end



GO
--select dbo.[fun_FormatCurrency](1417853432369856981.12,'1')  
