IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPrivacyPagetext]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPrivacyPagetext]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC Proc_GetPrivacyPagetext

As
BEGIN 

select 'PRIVACY POLICY' as PRIVACY_POLICY,
'We are required by law to provide you, our customer, with the following summary of our policy regarding "nonpublic personal
 information" we collect from and about you in the course of doing business.' as PRIV_TEXT,
'INFORMATION WE COLLECT' as inf_coll,
'We collect nonpublic personal information about you from the following sources' as infr_txt,
'•' + '	Information which we receive from you on applications or other forms (such as your name, address and date of birth)' as inf_bull1,
'•' + '	Information about your transactions with us or others, and' as inf_bull2,
'•' + '	Information which we receive from third parties, such as consumer reporting agencies.' as inf_bull3,
'INFORMATION WE DISCLOSE' as inf_disc,
'We do not disclose any nonpublic personal information about our customers or former customers to anyone, except as permitted by law. As permitted by law, we may disclose personal information we obtain about our customers and former customers to third par
ties as is necessary for these parties to help us service your business with us. We may also disclose non-public personal information about you to unaffiliated third parties as permitted by law.' as inf_txt,
'WOLVERINE MUTUAL INDEPENDENT AGENTS' as wolv_mut,
'Independent agents authorized to sell Wolverine Mutual products are not Wolverine Mutual employees. Because they have a unique business relationship with you, they may have additional personal information about you that we do not have.  If your agent use
s your personal information for reasons other than selling and servicing Wolverine Mutual products, your agent may need to provide you with the agency"s privacy policy.' as wolv_txt,
'OUR SECURITY PROCEDURES' as sec_proc,
'We maintain physical, electronic and procedural safeguards to protect customer information. We also restrict access to your personal information to those persons who have a business purpose to see it.' as sec_txt,
'FCRA ADVERSE ACTION DISCLOSURE 15 USC 1681m & 16 CFR 601,' as fcra_hd1,
'MIB Nos.  2003-1 & 2003-2 & IC 27-2-21 Sec. 19' as fcra_hd2,
'Based upon information contained in a consumer report supplied by Trans Union, P. O. Box 1000, Chester, PA 19022, toll free telephone number 800-645-1938, Wolverine Mutual Insurance Company has calculated your insurance score and determined your premium 
discounts which you will find on your declarations page. Trans Union did not make the decision to take this action and cannot give you the specific reason for it. However, you have a right to dispute directly to Trans Union the accuracy or completeness of
 any information furnished by it, and you have a right to obtain a free copy of the consumer report from Trans Union if you request it not later than 60 days after receipt of this notice.' as fcra_txt,
'The following factors in your credit history identified by Trans Union were the primary influences in determining your insurance score and associated premium discount:' as fcra_reas,
'NOTICE TO POLICY HOLDERS ' as not_pol,
'Questions regarding your policy or coverage should be directed to: Wolverine Mutual Insurance Company, PO Box 530, Dowagiac,MI 49047,  (800) 733-3320.' as not_txt,
'As our policyholder, your satisfaction is very important to us. If you have any questions about your policy, if you need assistance with a problem, or if you have a claim, you should first contact your insurance agency or us. Should you have a valid clai
m, we fully expect to provide a fair settlement in a timely fashion.' as not_txt1,
'If you (a) need the assistance of the governmental agency that regulates insurance; or (b) have a complaint you have been unable to resolve with your insurer you may contact the Department of Insurance by mail, telephone or email:' as not_txt2,
' State of Indiana Department of Insurance Consumer Services Division, 311 West Washington Street, Suite 300, Indianapolis, Indiana 46204' as ins_add,
'Consumer Hotline: (800) 622-4461; (317) 232-2395' as hotline,
'Complaints can be filed electronically at www.in.gov/idoi' as website
end


GO

