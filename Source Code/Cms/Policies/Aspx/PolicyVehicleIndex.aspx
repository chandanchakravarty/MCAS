<%@ Page Language="c#" CodeBehind="PolicyVehicleIndex.aspx.cs" AutoEventWireup="false"
    Inherits="Cms.Policies.Aspx.PolicyVehicleIndex" %>

<%@ Register TagPrefix="webcontrol" TagName="Tab" Src="/cms/cmsweb/webcontrols/basetabcontrol.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Footer" Src="/cms/cmsweb/webcontrols/footer.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="Menu" Src="/cms/cmsweb/webcontrols/menu.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="ClientTop" Src="/cms/cmsweb/webcontrols/ClientTop.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="WorkFlow" Src="/cms/cmsweb/webcontrols/WorkFlow.ascx" %>
<%@ Register TagPrefix="webcontrol" TagName="GridSpacer" Src="/cms/cmsweb/webcontrols/GridSpacer.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Policy Vehicle Index</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link rel="stylesheet" type="text/css" href="/cms/cmsweb/css/css<%=GetColorScheme()%>.css">
    <script src="/cms/cmsweb/scripts/xmldom.js"></script>
    <script src="/cms/cmsweb/scripts/common.js"></script>
    <script src="/cms/cmsweb/scripts/form.js"></script>
    <!--<LINK href="~/cmsweb/css/menu.css" type="text/css" rel="stylesheet">-->
    <style>
        .hide
        {
            overflow: hidden;
            top: 5px;
        }
        .show
        {
            overflow: hidden;
            top: 5px;
        }
        #tabContent
        {
            position: absolute;
            top: 160px;
        }
    </style>
    <script src="../../cmsweb/scripts/xmldom.js"></script>
    <script src="../../cmsweb/scripts/common.js"></script>
    <script src="../../cmsweb/scripts/form.js"></script>
    <script language="javascript">

        function findMouseIn() {
            if (!top.topframe.main1.mousein) {
                //createActiveMenu();
                top.topframe.main1.mousein = true;
            }
            setTimeout('findMouseIn()', 5000);
        }
        function onRowClicked(num, msDg) {
            rowNum = num;
            if (parseInt(num) == 0)
                strXML = "";
            populateXML(num, msDg);
            //changeTab(0,0);
        }
        function CopySchRecords() {
            window.open('/cms/policies/aspx/PolCopyUmbSchRecords.aspx?CalledFrom=' + document.getElementById("hidCalledFrom").value + '&CalledFor=VEHICLES&CUSTOMER_ID=' + document.getElementById("hidCustomerID").value + '&POLICY_ID=' + document.getElementById("hidPolicyID").value + '&POLICY_VERSION_ID=' + document.getElementById("hidPolicyVersionID").value, 'Copy', 600, 300, 'Yes', 'Yes', 'No', 'No', 'No');
        }

        /*Sets the different tabs urls of tab control*/
        /*Pretab contains the selcted tab
        loadPage contains whether to load page again or not*/
        function SetTabs(pretab, loadPage) {

            if (strSelectedRecordXML != "")		//If record xml exists then adding the tabs
            {
                var CalledFrom = document.getElementById("hidCalledFrom").value; //Stores the refrence , where the form is called from

                var objXmlHandler = new XMLHandler();
                var tree = objXmlHandler.quickParseXML(strSelectedRecordXML);
                if (tree != null) {
                    //Fetching the values from xml to be passed in url as query string
                    var strPolId = tree.getElementsByTagName('POLICY_ID')[0].firstChild.text;
                    var strPolVerId = tree.getElementsByTagName('POLICY_VERSION_ID')[0].firstChild.text;
                    var strCustomerId = tree.getElementsByTagName('CUSTOMER_ID')[0].firstChild.text;
                    var strVehicleID = tree.getElementsByTagName('VEHICLE_ID')[0].firstChild.text;
                    var strLobID = tree.getElementsByTagName('POLICY_LOB')[0].firstChild.text;

                    if (CalledFrom != "UMB") {

                        var TabTitle = "";
                        var TabCounter = 2;
                        if (CalledFrom == "MOT") {
                            //IF called from mototcyle lob then caption of tab should be motorcycle coverage
                            TabTitle = "Motorcycle Coverages";
                        }
                        else {
                            //else caption of tab should be Vehicle Covg Info
                            TabTitle = "Vehicle Coverages";

                        }

                        Url = "../../Policies/aspx/PolMiscellaneousEquipmentValuesDetails.aspx?" + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolId + "&POLICY_VERSION_ID=" + strPolVerId + "&RISK_ID=" + strVehicleID + "&VEHICLE_ID=" + strVehicleID + "&";
                        DrawTab(TabCounter++, this, "Miscellaneous Equipment", Url, null, pretab, !loadPage);

                        Url="../../policies/aspx/PolicyCoverages.aspx?CalledFrom="+CalledFrom+"&pageTitle=Coverages" + "&VEHICLEID=" + strVehicleID  + "&LOB_ID="+strLobID+"&" ;
                        DrawTab(TabCounter++, this, "Vehicle Coverages", Url, null, pretab, !loadPage); 

                        Url = "../../policies/aspx/PolicyEndorsement.aspx?CalledFrom=" + CalledFrom + "&pageTitle=Coverages" + "&VEHICLEID=" + strVehicleID + "&LOB_ID=" + strLobID + "&";
                        DrawTab(TabCounter++, this, 'Endorsements', Url, null, pretab, !loadPage);

                        Url = "../../policies/aspx/Automobile/PolicyAdditionalInterestIndex.aspx?CalledFrom=" + CalledFrom + "&CUSTOMER_ID=" + strCustomerId + "&POLICY_ID=" + strPolId + "&POLICY_VERSION_ID=" + strPolVerId + "&RISK_ID=" + strVehicleID + "&VEHICLE_ID=" + strVehicleID + "&";
                        DrawTab(TabCounter++, this, 'Additional Interest', Url, null, pretab, !loadPage);

                    }
                    else {

                        Url = "../../policies/aspx/PolicyCoverages.aspx?CalledFrom=UMB&pageTitle=Vehicle Coverages" + "&VEHICLEID=" + strVehicleID + "&";
                        DrawTab(2, this, 'Vehicle Coverages', Url, null, pretab, !loadPage);

                        //Url="../../policies/aspx/PolicyEndorsements.aspx?CalledFrom="+CalledFrom+"&pageTitle=Vehicle Coverages" + "&VEHICLEID=" + strVehicleID  + "&LOB_ID="+strLobID+"&" ; 
                        //DrawTab(3,this,'Endorsements',Url,null,pretab,! loadPage); 

                        //Url="../../policies/aspx/Automobile/PolicyAdditionalInterestIndex.aspx?CalledFrom="+CalledFrom+"&CUSTOMER_ID="+strCustomerId+"&POLICY_ID="+strPolId+"&POLICY_VERSION_ID="+strPolVerId +"&VEHICLE_ID="+strVehicleID+"&"; 
                        //DrawTab(4,this,'Remarks',Url,null,pretab,! loadPage); 
                        RemoveTab(2, this);
                    }


                    return;
                }
            }
            //Record not exists , hence only one tab should come
            //Hence removing other tabs if exists

            RemoveTab(5, this);
            RemoveTab(4, this);
            RemoveTab(3, this);
            RemoveTab(2, this);
            changeTab(0, 0);


        }


        function FetchLossReport() {
            window.open('/cms/Policies/Aspx/PolicyLossReport.aspx?LOB_ID=2&STATE_ID=14&CUSTOMER_ID=<%=hidCustomerID.Value%>&POL_ID=<%=hidPolicyID.Value%>&POLICY_VERSION_ID=<%=hidPolicyVersionID.Value%>&CalledFor=MVR', null, "width=600,height=600,scrollbars=0,menubar=0,resizable=1");

        }

        function PriorLossTab() {
            window.open('/cms/Application/priorloss/priorlossindex.aspx', null, "width=800,height=600,scrollbars=1,menubar=0,resizable=1");
        }



        function copy() {

            var customerid = '';
            var polid;
            var polversionid;


            if (document.getElementById('hidCustomerID') != null) {
                customerid = document.getElementById('hidCustomerID').value;
            }

            if (document.getElementById('hidPolicyID') != null) {
                polid = document.getElementById('hidPolicyID').value;
            }

            if (document.getElementById('hidPolicyVersionID') != null) {
                polversionid = document.getElementById('hidPolicyVersionID').value;
            }

            var caption = '';
            if (document.getElementById('hidCalledFrom') != null) {
                if (document.getElementById('hidCalledFrom') == 'APP') {
                    caption = "Search Vehicle Information";
                }
                else if (document.getElementById('hidCalledFrom') == 'MOT') {
                    caption = "Search Motorcycle Information";
                }
                else if (document.getElementById('hidCalledFrom') == 'UMB') {
                    caption = "Search Umbrella Information";
                }
                else {
                    caption = "Search Vehicle Information";
                }

            }
            window.open("/cms/Policies/Aspx/PolicyCustomerVehicle.aspx?CUSTOMER_ID=" + customerid + "&POLICY_ID=" + polid + "&POLICY_VERSION_ID=" + polversionid + "&CALLEDFROM=" + document.getElementById('hidCalledFrom').value, 'VehicleInformation', 'width=950,height=400,menubars=NO,scrollbars=yes,top=225,left=30,statusbar=NO,toolbars=NO');

        }

        /********************************************************************************************************/
    </script>
</head>
<body oncontextmenu="return false;" leftmargin="0" topmargin="0" onload="setfirstTime();top.topframe.main1.mousein = false;findMouseIn();"
    ms_positioning="GridLayout">
    <!-- To add bottom menu -->
    <webcontrol:Menu id="bottomMenu" runat="server">
    </webcontrol:Menu>
    <!-- To add bottom menu ends here -->
    <div id="bodyHeight" class="pageContent">
        <form id="indexForm" method="post" runat="server">
        <table class="tableWidth" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="4">
                    <asp:Label ID="lblError" runat="server" CssClass="errMsg"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="tableWidthHeader" cellspacing="0" cellpadding="0" border="0" align="center">
                        <tr>
                            <td id="tdWorkflow" class="pageHeader" colspan="4">
                                <webcontrol:WorkFlow id="myWorkFlow" runat="server">
                                </webcontrol:WorkFlow>
                            </td>
                        </tr>
                        <tr>
                            <td id="tdClientTop" class="pageHeader" colspan="4">
                                <webcontrol:ClientTop id="cltClientTop" runat="server" width="100%">
                                </webcontrol:ClientTop>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <webcontrol:GridSpacer id="grdSpacer" runat="server">
                                </webcontrol:GridSpacer>
                                <asp:PlaceHolder ID="GridHolder" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <webcontrol:GridSpacer id="Gridspacer" runat="server">
                                </webcontrol:GridSpacer>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="tableWidthHeader" cellspacing="0" cellpadding="0" border="0" align="center">
                                    <tr id="tabCtlRow">
                                        <td>
                                            <webcontrol:Tab ID="TabCtl" runat="server" TabTitles="Vehicle Info" TabLength="150">
                                            </webcontrol:Tab>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table class="tableeffect" cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td>
                                                        <iframe class="iframsHeightLong" id="tabLayer" runat="server" src="" scrolling="no"
                                                            frameborder="0" width="100%"></iframe>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <asp:Label ID="lblTemplate" runat="server" Visible="false"></asp:Label>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <webcontrol:Footer id="pageFooter" runat="server">
                                </webcontrol:Footer>
                            </td>
                        </tr>
                    </table>
                    <input type="hidden" name="hidTemplateID">
                    <input type="hidden" name="hidRowID">
                    <input type="hidden" name="hidlocQueryStr" id="hidlocQueryStr">
                    <input type="hidden" name="hidMode">
                    <input id="hide" type="hidden" name="ConVar">
                    <span id="singleRec"></span>
                    <input id="hidCustomerID" type="hidden" value="0" name="hidCustomerID" runat="server">
                    <input id="hidAPPID" type="hidden" value="0" name="hidAPPID" runat="server">
                    <input id="hidAppVersionID" type="hidden" value="0" name="hidAppVersionID" runat="server">
                    <input id="hidCalledFrom" type="hidden" name="hidCalledFrom" runat="server">
                    <input id="hidPolicyID" type="hidden" name="hidPolicyID" runat="server">
                    <input id="hidPolicyVersionID" type="hidden" name="hidPolicyVersionID" runat="server">
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div>
    </div>
</body>
</html>
