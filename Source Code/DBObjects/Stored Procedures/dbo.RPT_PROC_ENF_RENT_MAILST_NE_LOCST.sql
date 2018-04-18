IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RPT_PROC_ENF_RENT_MAILST_NE_LOCST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[RPT_PROC_ENF_RENT_MAILST_NE_LOCST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*********************************************************************
CREATED BY	:	ANURAG VERMA
CREATED ON	:	09/15/2009
PURPOSE		:	This is a query to fetch mailing address of enforce rental policies 
				which have different mailing address state (@ Customer Details) than location state (@ location tab)

MODIFIED BY	:	ANURAG VERMA
MODIFIED ON	:	09/21/2009
PURPOSE		:	ADDING EXPIRATION DATE AND INSPECTION DATE
**********************************************************************/

CREATE PROCEDURE [RPT_PROC_ENF_RENT_MAILST_NE_LOCST]
AS

SELECT  PCPL.POLICY_NUMBER,CONVERT(VARCHAR(12),PCPL.APP_EXPIRATION_DATE,101) [EXPIRATION DATE],
ISNULL(CONVERT(VARCHAR(12),LAST_INSPECTED_DATE,101),'') [INSPECTION DATE],
CUSTOMER_ADDRESS1 ADDRESS1,ISNULL(CUSTOMER_ADDRESS2,'') ADDRESS2,CUSTOMER_CITY CITY,STATE_NAME STATE,CUSTOMER_ZIP ZIP,COUNTRY_NAME COUNTRY

FROM
  POL_CUSTOMER_POLICY_LIST PCPL WITH ( NOLOCK )
  INNER JOIN POL_POLICY_PROCESS PPP WITH ( NOLOCK ) ON PPP.CUSTOMER_ID = PCPL.CUSTOMER_ID AND PPP.POLICY_ID = PCPL.POLICY_ID
       AND PCPL.POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID
       AND PCPL.POLICY_VERSION_ID >= (
                                       SELECT MIN(NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS WITH ( NOLOCK )
                                       WHERE CUSTOMER_ID = PPP.CUSTOMER_ID AND POLICY_ID = PPP.POLICY_ID
                                        AND PROCESS_ID IN ( 25, 18 ) AND PROCESS_STATUS = 'COMPLETE'
                                        AND ISNULL(REVERT_BACK, '') <> 'Y'
                                     )
       AND PPP.PROCESS_STATUS = 'COMPLETE'
	INNER JOIN POL_LOCATIONS PL WITH ( NOLOCK )	ON
	PL.CUSTOMER_ID=PCPL.CUSTOMER_ID AND PL.POLICY_ID=PCPL.POLICY_ID AND PL.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID	
	INNER JOIN POL_HOME_OWNER_GEN_INFO PHOGI WITH ( NOLOCK ) ON PHOGI.CUSTOMER_ID=PCPL.CUSTOMER_ID AND PHOGI.POLICY_ID=PCPL.POLICY_ID AND 
												PHOGI.POLICY_VERSION_ID=PCPL.POLICY_VERSION_ID	
	INNER JOIN CLT_CUSTOMER_LIST CCL WITH (NOLOCK) ON CCL.CUSTOMER_ID=PCPL.CUSTOMER_ID	
	INNER JOIN MNT_COUNTRY_STATE_LIST MCSL WITH ( NOLOCK ) ON CCL.CUSTOMER_STATE=MCSL.STATE_ID
	INNER JOIN MNT_COUNTRY_LIST MCL WITH ( NOLOCK ) ON CCL.CUSTOMER_COUNTRY=MCL.COUNTRY_ID
WHERE
  POLICY_STATUS = 'NORMAL'
  AND APP_EXPIRATION_DATE >= GETDATE()
  AND APP_EFFECTIVE_DATE <= GETDATE()
  AND POLICY_LOB=6	
  AND CUSTOMER_STATE!=LOC_STATE
ORDER BY PCPL.POLICY_NUMBER




GO

