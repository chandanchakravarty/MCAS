IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                      
Proc Name       : PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT              
Created by      : SHAFI             
Date            : 4 July 2006                               
Purpose         :UPDATE COVERAGE OF CO-APPLICANT  ON BASES OCCUPATION
Revison History :        
Modified By		:Pravesh K chandel
Modified Date	:28 JUly 09
Purpose			: Add pOL id and pOL version id to handle if Co applicant changed on POLICY
                              
Used In  : Wolverine                                      
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/  
-- DROP PROC Dbo.PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT                                            
CREATE           PROC Dbo.PROC_UPDATE_POLICY_HOME_COVERAGE_BY_APPLICANT                  
(                                      
 @CUSTOMER_ID     int,                                      
 @APPLICANT_ID     int,
 @POLICY_ID INT =NULL,
 @POLICY_VERSION_ID INT =NULL        
)                   
                  
AS                   
                  
BEGIN  
 DECLARE @POLICYID INT
 DECLARE @POLICYVERSIONID INT
 DECLARE @DWELLING INT
 DECLARE @COVERAGECODE VARCHAR(20)
 SET @COVERAGECODE='PERIJ'    
 DECLARE  @HO82 int

                  

/*
sp_find 'Proc_SAVE_HOME_COVERAGES_FOR_POLICY',p
 if occupation staisfies the condition then delete the Ho-82 from all application where this co-applicant exists
*/
IF EXISTS (
			SELECT  POL.APPLICANT_ID   FROM 
			CLT_APPLICANT_LIST CLT WITH(NOLOCK) INNER JOIN POL_APPLICANT_LIST POL WITH(NOLOCK)
			ON CLT.APPLICANT_ID=POL.APPLICANT_ID
			WHERE
			CLT.CUSTOMER_ID=@CUSTOMER_ID AND 
			CLT.APPLICANT_ID=@APPLICANT_ID AND 
			POL.POLICY_ID=ISNULL(@POLICY_ID,POL.POLICY_ID) -- ADDED BY PRAVESH ON 28 JULY 09
			AND POL.POLICY_VERSION_ID=ISNULL(@POLICY_VERSION_ID,POL.POLICY_VERSION_ID) -- ADDED BY PRAVESH ON 28 JULY 09
			AND CLT.CO_APPLI_OCCU IN(280,250,275,11817,1181,432,11825,11818,11819,561,11820,11821,11822,11823,11824,602,607) and
            ISNULL(CLT.IS_ACTIVE,'Y')= 'Y'   
          )
 BEGIN   
	
	 
     DECLARE POL_CURSOR CURSOR              
	    FOR               
	     SELECT  DWE.CUSTOMER_ID, DWE.POLICY_ID,DWE.POLICY_VERSION_ID,DWE.DWELLING_ID FROM POL_DWELLINGS_INFO DWE WITH(NOLOCK)
	             INNER JOIN POL_APPLICANT_LIST POL ON 
	                       DWE.CUSTOMER_ID=POL.CUSTOMER_ID AND 
	                       DWE.POLICY_ID =POL.POLICY_ID AND 
	                       DWE.POLICY_VERSION_ID=POL.POLICY_VERSION_ID 
	            
				WHERE        
	            POL.CUSTOMER_ID=@CUSTOMER_ID AND 
				POL.APPLICANT_ID=@APPLICANT_ID 
				AND POL.POLICY_ID=ISNULL(@POLICY_ID,POL.POLICY_ID) -- ADDED BY PRAVESH ON 28 JULY 09
				AND POL.POLICY_VERSION_ID=ISNULL(@POLICY_VERSION_ID,POL.POLICY_VERSION_ID) -- ADDED BY PRAVESH ON 28 JULY 09


         OPEN POL_CURSOR              
     
         FETCH NEXT FROM POL_CURSOR              
     	INTO @CUSTOMER_ID,@POLICYID,@POLICYVERSIONID ,@DWELLING

		 WHILE @@FETCH_STATUS = 0              
		 BEGIN 
			EXEC Proc_DeletePolicyHomeCoverage @CUSTOMER_ID,@POLICYID,@POLICYVERSIONID,@DWELLING,@COVERAGECODE
            FETCH NEXT FROM POL_CURSOR              
			INTO @CUSTOMER_ID, @POLICYID,@POLICYVERSIONID ,@DWELLING
			
			
		 END	
		
		CLOSE  POL_CURSOR              
		DEALLOCATE POL_CURSOR         
	     
	  
	  
	
  END
ELSE
/*
 if occupation  does not staisfies condition then if ho-24 is taken default Ho-82 for all application where this co-applicant exists
*/

 BEGIN
       DECLARE POL_CURSOR CURSOR              
	    FOR               
	     SELECT  DWE.CUSTOMER_ID, DWE.POLICY_ID,DWE.POLICY_VERSION_ID,DWE.DWELLING_ID 
	            FROM POL_DWELLING_SECTION_COVERAGES DWE
	             INNER JOIN POL_APPLICANT_LIST POL ON 
	                       DWE.CUSTOMER_ID=POL.CUSTOMER_ID AND 
	                       DWE.POLICY_ID =POL.POLICY_ID AND 
	                       DWE.POLICY_VERSION_ID=POL.POLICY_VERSION_ID 
                INNER JOIN MNT_COVERAGE  MNT ON DWE.COVERAGE_CODE_ID=MNT.COV_ID
	            WHERE        
	            POL.CUSTOMER_ID=@CUSTOMER_ID AND 
				POL.APPLICANT_ID=@APPLICANT_ID AND
                MNT.COV_CODE='EBP24'
			AND POL.POLICY_ID=ISNULL(@POLICY_ID,POL.POLICY_ID) -- ADDED BY PRAVESH ON 28 JULY 09
			AND POL.POLICY_VERSION_ID=ISNULL(@POLICY_VERSION_ID,POL.POLICY_VERSION_ID) -- ADDED BY PRAVESH ON 28 JULY 09

         OPEN POL_CURSOR  

        FETCH NEXT FROM POL_CURSOR              
     	INTO @CUSTOMER_ID,@POLICYID,@POLICYVERSIONID ,@DWELLING
            
		 WHILE @@FETCH_STATUS = 0              
		 BEGIN 
			 EXEC  @HO82 =  Proc_GetCOVERAGE_ID @CUSTOMER_ID,                          
  @POLICYID,                                                            
                    @POLICYVERSIONID,                  
                    @COVERAGECODE
             IF ( @HO82 = 0 )                                                      
			   BEGIN                                                      
				RAISERROR ('COV_ID not found for  Personal Ho-82',                                                      
				16, 1)                                                      
               END    
             ELSE
               BEGIN
				 exec Proc_SAVE_HOME_COVERAGES_FOR_POLICY
	                    @CUSTOMER_ID,                          
	                    @POLICYID,                                                            
	                    @POLICYVERSIONID, 
	                    @DWELLING,
	                    -1,
	                    @HO82,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    "S2",
	                    NULL,
	                    NULL,
	                    NULL, 
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL,
	                    NULL 
                    END 
           FETCH NEXT FROM POL_CURSOR              
			INTO @CUSTOMER_ID,@POLICYID,@POLICYVERSIONID ,@DWELLING
			
		 END	
		
		CLOSE  POL_CURSOR              
		DEALLOCATE POL_CURSOR     
   
 END

  
  
         
END                                     
                
              
              
            
          

GO

