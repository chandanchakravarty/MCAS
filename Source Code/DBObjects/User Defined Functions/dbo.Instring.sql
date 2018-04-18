IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Instring]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Instring]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*#################################  
Created By  :Pravesh K Chandel  
Dated  :6 June 2007  
Purpose  : to find a substring (word) in a string Expression  
parameters 1.String Expression  
    2. search String   
    3.Delemeter of String Expression   
##################################*/  
--drop function dbo.Instring  
create function dbo.Instring(@stringExpression nvarchar(4000),@searchString nvarchar(20)) returns int  
as  
begin  
 declare @foundPos int  
 declare @Start int  
 declare @element varchar(25)  
 declare @element1 varchar(100)  
 declare @delimeter char(1)  
 --if (@delimeter is null)  
 set @delimeter= ' '  
 set @Start=1  
 set @foundPos=1  
 set @element1 = replace(@stringExpression,',',' ') + @delimeter  
 while CHARINDEX(@delimeter,@element1) > 0   
    begin  
  set @element = substring(@element1, @Start,CHARINDEX(@delimeter,@element1))   
  set  @element = replace(@element,@delimeter,'')  
                 if (ltrim(rtrim(@element))=@searchString)  
                    return @foundPos  
  set @Start =CHARINDEX(@delimeter,@element1)  
  set @element1 = substring(@element1, @Start+1,len(@element1))   
  set @foundPos=@foundPos+@Start  
  set @Start=1  
          end    
    return 0  
end  
  



GO

