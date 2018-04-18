CREATE PROCEDURE [dbo].[Proc_VisionGenCaseRefNo]
	@TranType [varchar](20)
WITH EXECUTE AS CALLER
AS
Declare   
@Suffix		VARCHAR(10),    
@Prefix		VARCHAR(10),    
@MaxLength	INT,    
@CurrentNo	INT,    
@Result		VARCHAR(50),  
@Type		VARCHAR(3),  
@Country	VARCHAR(3)  
BEGIN  
	SET @Type = LEFT(@TranType,3)  
	PRINT '@Type = ' + @Type  

	--IF @TranType <> LEFT(@TranType,2)  
	IF @Type='CC-'OR @Type='BR-' OR @Type='CL-'  
	BEGIN  
		SET @Type = LEFT(@TranType,2)  
		IF @Type='CC'OR @Type='BR' OR @Type='CL'  
		BEGIN  
			PRINT 'INSIDE CC'  
			SET @Country = RIGHT(@TranType,2)  
			SET @CurrentNo = 0  
			SELECT @CurrentNo = CurrentNo,@MaxLength=MaxLength FROM MNT_NoAutoGenerator WHERE TranTypeCode= @Type and Country = @Country  

			PRINT '@CurrentNo ='+ CONVERT(VARCHAR,@CurrentNo)  
			IF @CurrentNo > 0  
			BEGIN  
			SET @CurrentNo = @CurrentNo + 1  
			UPDATE MNT_NoAutoGenerator SET CurrentNo = @CurrentNo WHERE TranTypeCode= @Type and Country = @Country  
		END  
		ELSE  
		BEGIN  
			PRINT 'INSIDE HERE'  
			DELETE FROM MNT_NoAutoGenerator WHERE TranTypeCode= @Type and Country = @Country AND CurrentNo=0  
			IF @Type='CC'
			BEGIN
				INSERT INTO MNT_NoAutoGenerator VALUES('Cedant',@Country,'CC',3,1)  
			END
			ELSE IF @Type='BR' 
			BEGIN
				INSERT INTO MNT_NoAutoGenerator VALUES('Broker',@Country,'BR',3,1)  
			END
			ELSE IF @Type='CL'  
			BEGIN
				INSERT INTO MNT_NoAutoGenerator VALUES('Client',@Country,'CL',3,1)  
			END
			SET @CurrentNo = 1  
			SET @MaxLength =3  
		END  

		IF  LEN(@CurrentNo)<=@MaxLength   
		BEGIN  
			SET @Result = REPLICATE('0',@MaxLength-LEN(LTRIM(RTRIM(@CurrentNo))))  
			SET @Result = @Type+'-'+@Country+'-'+@Result+CONVERT(VARCHAR,@CurrentNo)  
		END  
		ELSE  
		BEGIN  
			SET @Result = @Type+'-'+@Country+'-'+CONVERT(VARCHAR,@CurrentNo)  
		END  
	END  
	ELSE  
	BEGIN  
		if exists(SELECT 1 FROM VISION_TransRefNos WHERE ltrim(rtrim(isnull(TranTypeCode,''))) = ltrim(rtrim(@TranType)))    
		BEGIN    
			PRINT 'INSIDE 1'  
			SELECT @Suffix = isnull(Suffix,''), @Prefix = isnull(Prefix,''), @MaxLength = isnull(MaxLength,1), @CurrentNo = isnull(CurrentNo,0) FROM VISION_TransRefNos WHERE ltrim(rtrim(isnull(TranTypeCode,''))) = ltrim(rtrim(@TranType))    
			if @CurrentNo >= 0 SET @CurrentNo = @CurrentNo + 1    
			UPDATE VISION_TransRefNos SET CurrentNo = @CurrentNo  WHERE ltrim(rtrim(isnull(TranTypeCode,''))) = ltrim(rtrim(@TranType))    
			if @TranType = 'RISK' SET @Suffix = @Suffix + cast(year(getdate()) as VARCHAR(4)) + '-'    
			PRINT @Suffix    
			PRINT @Prefix    
			PRINT @MaxLength    
			PRINT len(@Suffix)    
			PRINT len(ltrim(rtrim(@CurrentNo)))    
			PRINT len(@Prefix)    
			SET @Result = @Suffix + replicate('0',@MaxLength - (len(ltrim(rtrim(@CurrentNo))) + len(@Suffix) + len(@Prefix) ) ) + ltrim(rtrim(@CurrentNo)) + @Prefix    
		END   
		ELSE   
			SET @Result = ''    
			If ltrim(rtrim(@Result)) != ''    
			Begin    
				--If (@TranType = 'R') INSERT INTO AC_ReceiptM(ReceiptNo,ReceiptDate,TranDate,CreateDate) VALUES(@Result,getDate(),getDate(),getDate())    
				PRINT 'here to implement Tran Specific Code 1'    
			End    
		END  
	END  
	ELSE  
	BEGIN  
		if exists(SELECT 1 FROM VISION_TransRefNos WHERE ltrim(rtrim(isnull(TranTypeCode,''))) = ltrim(rtrim(@TranType)))    
		BEGIN    
			PRINT 'INSIDE 2'  
			SELECT @Suffix = isnull(Suffix,''), @Prefix = isnull(Prefix,''), @MaxLength = isnull(MaxLength,1), @CurrentNo = isnull(CurrentNo,0) FROM VISION_TransRefNos WHERE ltrim(rtrim(isnull(TranTypeCode,''))) = ltrim(rtrim(@TranType))    
			if @CurrentNo >= 0 SET @CurrentNo = @CurrentNo + 1    
			UPDATE VISION_TransRefNos SET CurrentNo = @CurrentNo  WHERE ltrim(rtrim(isnull(TranTypeCode,''))) = ltrim(rtrim(@TranType))    
			if @TranType = 'RISK' SET @Suffix = @Suffix + cast(year(getdate()) as VARCHAR(4)) + '-'    
			PRINT @Suffix    
			PRINT @Prefix    
			PRINT @MaxLength    
			PRINT len(@Suffix)    
			PRINT len(ltrim(rtrim(@CurrentNo)))    
			PRINT len(@Prefix)    
			SET @Result = @Suffix + replicate('0',@MaxLength - (len(ltrim(rtrim(@CurrentNo))) + len(@Suffix) + len(@Prefix) ) ) + ltrim(rtrim(@CurrentNo)) + @Prefix    
		END   
		ELSE   
			SET @Result = ''    
			If ltrim(rtrim(@Result)) != ''    
			Begin    
			--If (@TranType = 'R') INSERT INTO AC_ReceiptM(ReceiptNo,ReceiptDate,TranDate,CreateDate) VALUES(@Result,getDate(),getDate(),getDate())    
			PRINT 'here to implement Tran Specific Code 2'    
			End    
		END  
	SELECT @Result as Result    
END


