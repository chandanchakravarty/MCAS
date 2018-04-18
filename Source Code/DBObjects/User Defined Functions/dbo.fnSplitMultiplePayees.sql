IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnSplitMultiplePayees]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnSplitMultiplePayees]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
create FUNCTION [dbo].[fnSplitMultiplePayees](
    @sInputList VARCHAR(8000) -- List of delimited items
  , @sDelimiter VARCHAR(8000) = ',' -- delimiter that separates items,
  , @Check_id int
  , @Check_Amount decimal(18,2)
  , @Claim_Id int
) RETURNS @List TABLE (item VARCHAR(8000),check_id int,Check_Amount decimal(18,2),Claim_Id int)

BEGIN
DECLARE @sItem VARCHAR(8000)
WHILE CHARINDEX(@sDelimiter,@sInputList,0) <> 0
 BEGIN
 SELECT
  @sItem=RTRIM(LTRIM(SUBSTRING(@sInputList,1,CHARINDEX(@sDelimiter,@sInputList,0)-1))),
  @sInputList=RTRIM(LTRIM(SUBSTRING(@sInputList,CHARINDEX(@sDelimiter,@sInputList,0)+LEN(@sDelimiter),LEN(@sInputList))))
 
 IF LEN(@sItem) > 0
  INSERT INTO @List SELECT @sItem ,@check_id ,@Check_Amount,@Claim_Id
 END

IF LEN(@sInputList) > 0
 INSERT INTO @List SELECT @sInputList ,@check_id ,@Check_Amount,@Claim_Id -- Put the last item in
RETURN
END




























  
  
  



      
       















        
        
        
        

        
        
        
        
        
        
        
        
        
        
       
        
        
        
        
        
        











GO

