IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GenerateOfficialClaimNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GenerateOfficialClaimNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                
Proc Name       : dbo.Proc_GenerateOfficialClaimNumber                                                
Created by      : Santosh Kumar Gautam                                               
Date            : 13 Dec 2010                                           
Purpose         : Generate official claim number      
Revison History :                                                
Used In        : CLAIM                                                
------------------------------------------------------------                                                                                                                               
Date     Review By          Comments                                                
------   ------------       -------------------------*/                                                
--DROP PROC dbo.Proc_GenerateOfficialClaimNumber     
                                              
CREATE PROC [dbo].[Proc_GenerateOfficialClaimNumber]                                            
 @CUSTOMER_ID int,                                                
 @POLICY_ID int,                                                
 @POLICY_VERSION_ID smallint,                                                
 @CLAIM_ID int output,                                                
 @LOSS_DATE datetime,      
 @FNOL_DATE datetime,                                                        
 @ADJUSTER_ID int,    
 @LANG_ID int    
    
         
AS                                  
BEGIN                           
                                             
DECLARE @PREVIOUS_CLAIM_NUMBER NVARCHAR(50)     
--DECLARE @ADJUSTER_BRANCH  NVARCHAR(2)     
DECLARE @POLICY_LOB           NVARCHAR(4)     
DECLARE @LOSSYEAR             INT     
DECLARE @CLAIM_NUMBER         NVARCHAR(18)   
DECLARE @POLICY_BRANCH_CODE   NVARCHAR(10)  
DECLARE @DELIMETER            CHAR(1)='-'    
DECLARE @SEQUENCE_NUMBER      INT=0    
DECLARE @SEQUENCE			  NVARCHAR(5)   
DECLARE @ADJUSTER_CODE		  NVARCHAR(2)     
DECLARE @DATE_TO_CLAIM_NUMBER DATETIME
 

 -----------------------------------------------------------------------
 -- IF FNOL DATE IS NOT PROVIDED THEN CONSIDER LOSS DATE AS FNOL DATE
 -----------------------------------------------------------------------
 IF(@FNOL_DATE IS NOT NULL)
    SET @DATE_TO_CLAIM_NUMBER=@FNOL_DATE
 ELSE
   SET @DATE_TO_CLAIM_NUMBER=@LOSS_DATE
 
 --=====================================================================
 -- MODIFIED BY SANTOSH GAUTAM ON 06 DEC 2011 REF ITRACK(1771)
 --=====================================================================
 -- OFFICIAL CLAIM LOGIC
 -- 1. Adjuster Branch: 2 digits 
 -- 2. Policy LOB: 4 digits 
 -- 3. Claim FNOL Year: 2 digits 
 -- 4. Claim number: 5 sequential digits based on Adjuster and LOB 
 --=====================================================================
            
--SELECT @POLICY_BRANCH_CODE= D.BRANCH_CODE 
--FROM POL_CUSTOMER_POLICY_LIST P WItH(NOLOCK) INNER JOIN
--  MNT_DIV_LIST D WItH(NOLOCK) ON P.DIV_ID = D.DIV_ID
--WHERE P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID=@POLICY_VERSION_ID


SELECT @POLICY_BRANCH_CODE =DIV.BRANCH_CODE
FROM  CLM_ADJUSTER  ADJ WITH(NOLOCK) INNER JOIN 
      MNT_USER_LIST USR WITH(NOLOCK)  ON ADJ.[USER_ID] = USR.[USER_ID] INNER JOIN
      MNT_DIV_LIST  DIV WITH(NOLOCK)  ON DIV.DIV_ID    = USR.USER_DEF_DIV_ID
WHERE ADJ.ADJUSTER_ID =@ADJUSTER_ID

--SELECT @ADJUSTER_CODE=ADJUSTER_CODE FROM CLM_ADJUSTER WHERE ADJUSTER_ID =@ADJUSTER_ID 
    
    
IF (LEN(@POLICY_BRANCH_CODE)>2)    
    SET @POLICY_BRANCH_CODE =SUBSTRING(@POLICY_BRANCH_CODE,0,2)     
ELSE    
BEGIN    
   SET @POLICY_BRANCH_CODE=ISNULL(@POLICY_BRANCH_CODE,'00')    
END    
    
SET @POLICY_BRANCH_CODE =REPLICATE('0',2-LEN(@POLICY_BRANCH_CODE))+@POLICY_BRANCH_CODE    
    
 --itrack # 1493/susep code from pol_customer_policy_list Table.
 --itrack # 1562 and tfs # 548 
 /*
  SELECT @POLICY_LOB=ISNULL(L.SUSEP_LOB_CODE,'0')    
  FROM     
   POL_CUSTOMER_POLICY_LIST P LEFT OUTER JOIN    
   MNT_LOB_MASTER L ON P.POLICY_LOB=L.LOB_ID     
  WHERE (P.CUSTOMER_ID=@CUSTOMER_ID AND P.POLICY_ID=@POLICY_ID AND P.POLICY_VERSION_ID=@POLICY_VERSION_ID)    
 */
  
  
SELECT @POLICY_LOB=ISNULL(SUSEP_LOB_CODE,'0')  FROM    POL_CUSTOMER_POLICY_LIST WItH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID
AND POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID = @POLICY_VERSION_ID

     
IF (LEN(@POLICY_LOB)>4)    
    SET @POLICY_LOB =SUBSTRING(@POLICY_LOB,0,4)     
ELSE    
BEGIN    
    SET @POLICY_LOB=ISNULL(@POLICY_LOB,'0000')    
END    
    
SET @POLICY_LOB =REPLICATE('0',4-LEN(@POLICY_LOB))+UPPER(@POLICY_LOB)    
    
    
      
-- FOR PREVIOUS SEQUENCE NUMBER                                              
SELECT TOP 1 @PREVIOUS_CLAIM_NUMBER=OFFCIAL_CLAIM_NUMBER    
FROM CLM_CLAIM_INFO  WItH(NOLOCK)   
WHERE ADJUSTER_ID=@ADJUSTER_ID     
AND     
ISNULL(dbo.Piece(OFFCIAL_CLAIM_NUMBER,@DELIMETER,2),'0')=@POLICY_LOB    
ORDER BY CREATED_DATETIME DESC    
    
SET  @PREVIOUS_CLAIM_NUMBER=ISNULL(@PREVIOUS_CLAIM_NUMBER,'1')         
                               
SELECT @PREVIOUS_CLAIM_NUMBER=ISNULL(dbo.Piece(@PREVIOUS_CLAIM_NUMBER,@DELIMETER,3),'0')    
    
        
    
IF(@PREVIOUS_CLAIM_NUMBER IS NOT NULL  )    
BEGIN    
    
  SELECT @PREVIOUS_CLAIM_NUMBER=SUBSTRING(@PREVIOUS_CLAIM_NUMBER,3,LEN(@PREVIOUS_CLAIM_NUMBER))    
  SET @SEQUENCE_NUMBER =CAST(@PREVIOUS_CLAIM_NUMBER AS INT)+1    
      
      
END    
ELSE     
  SET @SEQUENCE_NUMBER=1    
      
     
      
SET @SEQUENCE=REPLICATE('0',5-LEN(@SEQUENCE_NUMBER))+CAST(@SEQUENCE_NUMBER AS NVARCHAR(5))    
    
    
    
-- FOR FNOL YEAR    
IF(@DATE_TO_CLAIM_NUMBER IS NOT NULL)    
  SET @LOSSYEAR=YEAR(@DATE_TO_CLAIM_NUMBER)    
ELSE    
  SET @LOSSYEAR=YEAR(GETDATE())     
    
SET @LOSSYEAR = CAST(SUBSTRING(CAST( @LOSSYEAR AS VARCHAR(4)),3,2) AS INT)    
    
SELECT @CLAIM_NUMBER=@POLICY_BRANCH_CODE+'-'+@POLICY_LOB+'-'+CAST(@LOSSYEAR AS NVARCHAR(2))+@SEQUENCE    
    
UPDATE CLM_CLAIM_INFO     
SET OFFCIAL_CLAIM_NUMBER=@CLAIM_NUMBER    
WHERE CLAIM_ID=@CLAIM_ID     
    
    
SELECT @CLAIM_NUMBER    
                 
END 

GO