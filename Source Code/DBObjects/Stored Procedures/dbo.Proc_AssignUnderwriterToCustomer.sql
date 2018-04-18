IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AssignUnderwriterToCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AssignUnderwriterToCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran  
--drop proc Proc_AssignUnderwriterToCustomer  
--go  

/*----------------------------------------------------------                                                                  
Proc Name       : Proc_AssignUnderwriterToCustomer                                  
Created by      : Sumit Chhabra                  
Date            : 14/05/2006                                 
Purpose         : Assignment of underwriter to an application/policy based on round-robin algorithm                  
Revison History :                                  
modified by  :Pravesh K Chandel    
modified Date : 6 Aug 2007    
Purpose  : Error while Parsing RULEXML     
  
modified by  :Praveen Kasana    
modified Date : 18 April 2008    
Purpose  : Error while Parsing RULEXML    
 --APP XML CONTAINS INVALID CHARS (<,>) ...Replace with Space. (Praveen : 18 April 2008)   
 --Error is coming when try to Submit an application which having Invalid Customer name  
*/        
-- drop proc dbo.Proc_AssignUnderwriterToCustomer                             
CREATE PROC [dbo].[Proc_AssignUnderwriterToCustomer]                                  
(                                  
 @CUSTOMER_ID  INT,                                  
 @APP_ID  INT,                                  
 @APP_VERSION_ID INT,                                  
 @LOB_ID NVARCHAR(20),                  
 @SYSTEM_ID NVARCHAR(20) = NULL,              
 @CREATED_BY INT = NULL,    
 @CALLED_FROM NVARCHAR(20) = NULL,                   
 @SELECTED_UNDERWRITER INT = NULL OUTPUT,    
 @SUBJECT_LINE NVARCHAR(100) = NULL OUTPUT,  
 @RESULT INT = NULL OUTPUT                 
)                                  
AS                                  
BEGIN                       
                 
DECLARE @UNDERWRITERS NVARCHAR(200)

SET @UNDERWRITERS = NULL

--Commented by Charles on 14-May-10 for Itrack 51              
--declare @POLICY_ID int      
--declare @POLICY_VERSION_ID smallint      

DECLARE @CARRIER_AGENCY_ID INT,@CARRIER_CODE VARCHAR(50)

 SELECT  @CARRIER_CODE=ISNULL(REIN.REIN_COMAPANY_CODE,'')  FROM MNT_SYSTEM_PARAMS SYSP WITH(NOLOCK)             
 INNER JOIN MNT_REIN_COMAPANY_LIST REIN WITH(NOLOCK) ON REIN.REIN_COMAPANY_ID = SYSP.SYS_CARRIER_ID     

 SELECT @CARRIER_AGENCY_ID=AGENCY_ID FROM MNT_AGENCY_LIST WITH(NOLOCK) WHERE AGENCY_CODE=@CARRIER_CODE  

	
IF @LOB_ID IS NULL OR @LOB_ID=''                  
 RETURN -2   
     
--Added by Charles on 14-May-10 for Itrack 51           
 SELECT @UNDERWRITERS=UNDERWRITERS FROM MNT_AGENCY_UNDERWRITERS MNT WITH(NOLOCK) 
 LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)      
 ON MNT.AGENCY_ID = POL.AGENCY_ID        
 WHERE POL.CUSTOMER_ID = @CUSTOMER_ID AND POL.APP_ID =@APP_ID AND POL.APP_VERSION_ID= @APP_VERSION_ID   
 AND MNT.LOB_ID = @LOB_ID
 
 --Added By Lalit March 11,2011
 --Assign Default underwriter
 if(@UNDERWRITERS IS NULL OR @UNDERWRITERS = '')    
 BEGIN
	IF EXISTS( SELECT UNDERWRITERS FROM MNT_AGENCY_UNDERWRITERS MNT WITH(NOLOCK) 
	LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK) ON MNT.AGENCY_ID =  @CARRIER_AGENCY_ID       
	WHERE POL.CUSTOMER_ID = @CUSTOMER_ID AND POL.APP_ID =@APP_ID AND POL.APP_VERSION_ID= @APP_VERSION_ID   
	AND MNT.LOB_ID = 0)
	
	SELECT @UNDERWRITERS = UNDERWRITERS FROM MNT_AGENCY_UNDERWRITERS MNT WITH(NOLOCK) 
	LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK) ON MNT.AGENCY_ID =  @CARRIER_AGENCY_ID       
	WHERE POL.CUSTOMER_ID = @CUSTOMER_ID AND POL.APP_ID =@APP_ID AND POL.APP_VERSION_ID= @APP_VERSION_ID   
	AND MNT.LOB_ID = 0
 END            
 --Added till here
                  
/* Commented by Charles on 14-May-10 for Itrack 51                 
IF(@LOB_ID='1') --Homeowner                  
begin                   
-- select @underwriters=home_underwriter from mnt_agency_list where upper(agency_code)=upper(@system_id)                
 select @underwriters=home_underwriter from mnt_agency_list m with(nolock) join app_list a   with(nolock)      
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
end                  
else IF(@LOB_ID='2') --Automobile                  
begin                   
-- select @underwriters=private_underwriter from mnt_agency_list where upper(agency_code)=upper(@system_id)                  
 select @underwriters=private_underwriter from mnt_agency_list m with(nolock) join app_list a  with(nolock)       
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
end                  
else IF(@LOB_ID='3') --Motorcycle                  
begin                   
-- select @underwriters=motor_underwriter from mnt_agency_list where upper(agency_code)=upper(@system_id)                   
 select @underwriters=motor_underwriter from mnt_agency_list m with(nolock) join app_list a with(nolock)        
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
end                  
else IF(@LOB_ID='4') --Watercraft                  
begin                   
-- select @underwriters=water_underwriter from mnt_agency_list where upper(agency_code)=upper(@system_id)                   
 select @underwriters=water_underwriter from mnt_agency_list m with(nolock) join app_list a with(nolock)        
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
end                  
else IF(@LOB_ID='5') --Umbrella                  
begin                   
-- select @underwriters=umbrella_underwriter from mnt_agency_list where upper(agency_code)=upper(@system_id)                  
 select @underwriters=umbrella_underwriter from mnt_agency_list m with(nolock)join app_list a with(nolock)        
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
        
end                  
else IF(@LOB_ID='6') --Rental                  
begin                   
-- select @underwriters=rental_underwriter from mnt_agency_list where upper(agency_code)=upper(@system_id)                
 select @underwriters=rental_underwriter from mnt_agency_list m with(nolock)join app_list a with(nolock)     
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
    end                  
else IF(@LOB_ID='7') --General Liability                  
begin                   
-- select @underwriters=general_underwriter from mnt_agency_list where upper(agency_code)=upper(@system_id)                
 select @underwriters=general_underwriter from mnt_agency_list m with(nolock) join app_list a with(nolock)        
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
        
end                  
else IF(@LOB_ID='8') --Aviation  
begin                   
 select @underwriters=private_underwriter from mnt_agency_list m with(nolock) join app_list a  with(nolock)       
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
end   
else --Other LOB's
begin                   
 select @underwriters=private_underwriter from mnt_agency_list m with(nolock) join app_list a  with(nolock)       
  on m.agency_id = a.app_agency_id        
  where a.customer_id = @CUSTOMER_ID and a.app_id =@APP_ID and a.app_version_id= @APP_VERSION_ID        
end   

*/
                 
--select @underwriters          
    
IF @UNDERWRITERS IS NULL OR @UNDERWRITERS=''       
   BEGIN                
 SET @SELECTED_UNDERWRITER=0    
 RETURN -2           
    END    
ELSE    
   BEGIN    
 IF (@CALLED_FROM='VALIDATE')    
 BEGIN    
  SET @SELECTED_UNDERWRITER=1    
  RETURN 1           
 END    
   END                      
    
--FIND APPLICATIONS ASSIGNED TO THESE UNDERWRITERS                  
DECLARE @CURRENT_UNDERWRITER VARCHAR(10)                  
DECLARE @COUNT INT                  
DECLARE @ASSIGNED_APPLICATIONS INT                  
DECLARE @MIN_APPLICATION INT                  
--DECLARE @SELECTED_UNDERWRITER INT                  
SET @COUNT=2                  
                    
 SET @CURRENT_UNDERWRITER = DBO.PIECE(@UNDERWRITERS,',',1)                                
 --LOOP THROUGH THE STRING TO FIND APPLICATIONS ASSIGNED TO EACH UNDERWRITER                  
 WHILE @CURRENT_UNDERWRITER IS NOT NULL                                
 BEGIN                        
   --FIND APPLICATIONS ASSIGNED TO THE CURRENT UNDERWRITER BEGIN TRAVERSED                  
   --CHANGED BY CHARLES ON 14-MAY-10 FOR ITRACK 51        
   --SELECT @ASSIGNED_APPLICATIONS=COUNT(CUSTOMER_ID) FROM APP_LIST WITH(NOLOCK) WHERE UNDERWRITER=@CURRENT_UNDERWRITER 
   SELECT @ASSIGNED_APPLICATIONS=COUNT(CUSTOMER_ID) FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE UNDERWRITER=@CURRENT_UNDERWRITER                  
   
   IF(@COUNT=2)                  
   BEGIN                  
    --INITIALIZED THE VARIABLES AT THE START                  
    SET @MIN_APPLICATION=@ASSIGNED_APPLICATIONS                  
    SET @SELECTED_UNDERWRITER = @CURRENT_UNDERWRITER                  
   END                  
   ELSE                  
   BEGIN                  
    --SET THE VARIABLE IF WE FIND ANOTHER UNDERWRITER WITH LESS APPLICATIONS ASSIGNED THAN WE HAVE                  
    IF(@ASSIGNED_APPLICATIONS<@MIN_APPLICATION)                  
    BEGIN                  
     SET @MIN_APPLICATION=@ASSIGNED_APPLICATIONS              
     SET @SELECTED_UNDERWRITER = @CURRENT_UNDERWRITER                  
 END                  
   END                  
      SET @CURRENT_UNDERWRITER=DBO.PIECE(@UNDERWRITERS,',',@COUNT)                  
      SET @COUNT=@COUNT+1                                
                    
 END                        
      
--UPDATE THE APP_LIST WITH SELECTED UNDERWRITER                
IF @SELECTED_UNDERWRITER IS NOT NULL AND @SELECTED_UNDERWRITER <> '' AND @SELECTED_UNDERWRITER <> 0    
BEGIN  

--Commented by Charles on 14-May-10 for Itrack 51 
/*        
 SELECT @POLICY_ID=POLICY_ID,@POLICY_VERSION_ID=POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST with(nolock) 
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID          
 update app_list set underwriter=@selected_underwriter where customer_id=@customer_id and app_id=@app_id and app_version_id=@app_version_id   
 */               
 UPDATE POL_CUSTOMER_POLICY_LIST SET UNDERWRITER=@SELECTED_UNDERWRITER WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID          
  
SET @RESULT = -10  
RETURN @RESULT ---------under writer Exisits  
  
  
   
  /*Commented  on 03 feb 2009*/ ----#########-----------STARTS Comment  
  /*declare @LISTTYPEID int--New Business =7                                                                                    
  declare @UNDERWRITER int -- 0                                             
  declare @appStatus nvarchar(100)          
  -----added by pravesh on 29 nov 2006          
  select @SUBJECT_LINE='New Application Submitted'          
  if @CALLED_FROM = 'ANYWAY'              
   set @SUBJECT_LINE=@SUBJECT_LINE+ ' Type - Submit Anyway'           
  else           
  begin          
          
   DECLARE @IDOC INT             
   DECLARE @RULE_XML varchar(8000)            
   SELECT @RULE_XML = isnull(APP_VERIFICATION_XML,'') FROM APP_LIST with(nolock) WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                
   if (@RULE_XML!='')          
   begin          
    SELECT @RULE_XML=REPLACE(@RULE_XML,'stylesheet">','stylesheet"></LINK>')          
    SELECT @RULE_XML=REPLACE(@RULE_XML,'charset=utf-16">','charset=utf-16"></META>')          
    --SELECT @RULE_XML=REPLACE(@RULE_XML,'&GT;','>')    
    --APP XML CONTAINS INVALID CHARS (<,>) ...Replace with Space. (Praveen : 18 April 2008)   
    --Error is coming when try to Submit an application which having Invalid Customer name  
 SELECT @RULE_XML=REPLACE(@RULE_XML,'&gt;','')  
 SELECT @RULE_XML=REPLACE(@RULE_XML,'&GT;','')  
 SELECT @RULE_XML=REPLACE(@RULE_XML,'&lt;','')  
   --END      
    SELECT @RULE_XML=REPLACE(@RULE_XML,'&AMP','')          
    SELECT @RULE_XML='<ROOT>' + upper(@RULE_XML) + '</ROOT>'          
    SELECT @RULE_XML=replace(@RULE_XML,'XMLNS','xmlns')          
            
    EXEC SP_XML_PREPAREDOCUMENT @IDOC OUTPUT, @RULE_XML              
      DECLARE @RULE_DESC VARCHAR(200)              
               
      SELECT   @RULE_DESC = REFEREDSTATUS            
      FROM    OPENXML (@IDOC, '/ROOT/SPAN',2)    --2 for Node value          
               WITH( REFEREDSTATUS  VARCHAR(5) )      
EXEC SP_XML_REMOVEDOCUMENT @IDOC          
    if (@RULE_DESC  ='0')          
    set @SUBJECT_LINE=isnull(@SUBJECT_LINE,'') + ' Type - Refer to Underwriter'          
    else          
    set @SUBJECT_LINE= isnull(@SUBJECT_LINE,'') + ' Type - Meets Requirements'          
        
   end          
  
  end   */     /*END Comment*/     
    
 --Add diary entry for the underwriter              
--Diary entry will be done at page level to provide information at transaction log    
/* INSERT into TODOLIST                                                         
 (                                                                            
  RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,POLICYVERSION,                                                        
  POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,            
  FROMUSERID,STARTTIME,ENDTIME,NOTE,PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,                                
  FROMENTITYID,CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID                                                                            
 )                                           
values                                                    
 (                                                                            
  null,getdate(),getdate(),7,@CUSTOMER_ID,@POLICY_ID,                                                                            
  @POLICY_VERSION_ID,null,null,'New Application Submitted','Y',                                                                   
  null,'M',@selected_underwriter,@CREATED_BY,null,null,null,null,null,null,null,null,null,                                                                            
  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@POLICY_ID,@POLICY_VERSION_ID              
 )    
*/              
END            
ELSE    
 RETURN -2       
  
END      
 
 
--go  
  
--declare @ret int  
--declare @subUn int  
--declare @subLine varchar(100)  
-- exec @ret = Proc_AssignUnderwriterToCustomer 2156,629,1,'9',null,398,'submit', @subUn out,@subLine out  
--select @ret  
  
--select @subUn,@subLine  
--rollback tran  
    

GO

