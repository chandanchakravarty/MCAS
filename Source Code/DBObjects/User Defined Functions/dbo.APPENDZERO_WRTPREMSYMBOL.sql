IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[APPENDZERO_WRTPREMSYMBOL]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[APPENDZERO_WRTPREMSYMBOL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************************************    
 Create By : Manoj Rathore  
  
 Dated     : 15 May 2008    

 Purpose   : to appent zero and primium symbols  

**********************************************************************************************/ 

Create FUNCTION [dbo].[APPENDZERO_WRTPREMSYMBOL]
( 
 @WRITTEN_PREM NUMERIC(17,9)--varchar(20)
)          
RETURNS VARCHAR(20) AS            
BEGIN         
            
     
DECLARE @writter_primium VARCHAR(20),@substr  INT,@symbol VARCHAR(4),@writter_primium_with_symbol VARCHAR(20),
@REPLC VARCHAR(20)

IF(@WRITTEN_PREM >= 0)
BEGIN 
	select @writter_primium=dbo.APPENDZERO(REPLACE(cast(round(@WRITTEN_PREM,3) as numeric(12,3)),'.',''),8),
	@substr=substring(@writter_primium,len(@writter_primium),1) ,
	@symbol=
	CASE  
	WHEN  @substr=0  THEN '0'  
	WHEN  @substr=1  THEN 'A'
	WHEN  @substr=2  THEN 'B'
	WHEN  @substr=3  THEN 'C' 
	WHEN  @substr=4  THEN 'D'  
	WHEN  @substr=5  THEN 'E'
	WHEN  @substr=6  THEN 'F'
	WHEN  @substr=7  THEN 'G'
	WHEN  @substr=8  THEN 'H'
	WHEN  @substr=9  THEN 'I'
	ELSE ''
	END,
	@writter_primium_with_symbol=left(@writter_primium,len(@writter_primium)-1) + REPLACE(substring(@writter_primium,len(@writter_primium),1),right(@writter_primium,1),@symbol)	
	
END 
ELSE
BEGIN 
	set @replc=replace(@WRITTEN_PREM,'-','')
        select @writter_primium=dbo.APPENDZERO(REPLACE(cast(round(@replc,3) as numeric(12,3)),'.',''),8),

	@substr=substring(@writter_primium,len(@writter_primium),1) ,
	@symbol=
	CASE  
	WHEN  @substr=0  THEN '}'  
	WHEN  @substr=1  THEN 'J'
	WHEN  @substr=2  THEN 'K'
	WHEN  @substr=3  THEN 'L' 
	WHEN  @substr=4  THEN 'M'  
	WHEN  @substr=5  THEN 'N'
	WHEN  @substr=6  THEN 'O'
	WHEN  @substr=7  THEN 'P'
	WHEN  @substr=8  THEN 'Q'
	WHEN  @substr=9  THEN 'R'
	ELSE ''
	END,	
	@writter_primium_with_symbol=left(@writter_primium,len(@writter_primium)-1) + REPLACE(substring(@writter_primium,len(@writter_primium),1),right(@writter_primium,1),@symbol)
	
END

return(@writter_primium_with_symbol)
    
      
END   
  
  
  






GO

