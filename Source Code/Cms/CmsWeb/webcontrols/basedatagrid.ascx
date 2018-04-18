<%@ Control Language="c#" AutoEventWireup="false" Codebehind="basedatagrid.ascx.cs" Inherits="Cms.CmsWeb.WebControls.BaseDataGrid" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<style type="text/css">

		.HeadRow { BORDER-RIGHT: 1px outset; BORDER-TOP: 1px outset; BORDER-LEFT: 1px outset; WIDTH: 100%;  BORDER-BOTTOM: 1px outset }
		.HeadColClicked { BORDER-RIGHT: 1px inset; BORDER-TOP: 1px inset; BORDER-LEFT: 1px inset; WIDTH: 100%; BORDER-BOTTOM: 1px inset }
</style>
<script type="text/javascript" language="JavaScript" src="/cms/cmsweb/scripts/webcommon.js"></script>
<script type="text/javascript" language="JavaScript" src="/cms/cmsweb/scripts/common.js"></script>
<script type="text/javascript" language="JavaScript" src="/cms/cmsweb/scripts/xmldom.js"></script>
<script type="text/javascript" language="JavaScript" src="/cms/cmsweb/scripts/AJAXCommon.js"></script>
<script type="text/javascript" language="JavaScript">

var SEPARATOR ="~";
var prevRowSelected;
var prevRowClassName;
var sRowData = "";
var bBaseDataGridReady = false;
var sSearchCriteria = "";
var originalrow_color, originalitem_color,original_color;
var prevObj;
var strXMLBase="";
var checkDblClick=1;
var prmId="";
var stPageNo="";
var advanceSearch;
var myuniqueId="";
var storedUnqId;
var unqRoId=1;//row Id sent from tab page after insert
var locQueryStr=""; // for storing query string
var rowNum=0;
var isCheck=0;
var isComCheck=0;
var selectFlag=0;
var strSelectedRecordXML = "";
var imageClick=0;
//Comma separated values of primary keys
var primaryKeyValues = "";

/***********************************************************/
var mainSearch="";
var rowPrimary="";		
var show=1;		

var headRow=-1;
var srhCrt="";
var globSelected = 1;


function testing(obj,obj1)
{
	ShowAllApp();
	//fPopCalendar(obj,obj1);
}
		
		function ShowCalendar()
		{
			/*document.getElementById('hlkEXPDT_DATE').style.display="none"; 
			document.getElementById('imgEXPDT_DATE').style.display="none";			
			
			if(!advanceSearch)
			{
				var ind=document.all('SearchCol').selectedIndex;

				var arrSType=searchType.split("^");
				if(arrSType.length>0)
				{
					if(arrSType[ind]=="D")
					{
						document.getElementById('hlkEXPDT_DATE').style.display="inline"; 
						document.getElementById('imgEXPDT_DATE').style.display="inline";			
					}
				}			
			}
			else
			{
				for(jamboora=0; jamboora<document.all('AdvSearchCols').length; jamboora++)
				{
					var ind=document.all('AdvSearchCols').item(jamboora).selectedIndex;	
					var arrSType=searchType.split("^");

					if(arrSType.length>0)
					{
						if(arrSType[ind]=="D")
						{
							document.getElementById('hlkEXPDT_DATE').style.display="inline"; 
							document.getElementById('imgEXPDT_DATE').style.display="inline";			
						}
					}
				}			
			}*/
			
			if(!advanceSearch)
			{
				var ind=document.all('SearchCol').selectedIndex;

				var arrSType=searchType.split("^");
				
				if(arrSType.length>0)
				{
					
					if(arrSType[ind]!="L")
					{
						/*document.getElementById('hlkEXPDT_DATE').style.display="inline"; 
						document.getElementById('imgEXPDT_DATE').style.display="inline";			*/

						//document.all('SearchVal').value='';
					}
				}			
			}
			else
			{
				for(jamboora=0; jamboora<document.all('AdvSearchCols').length; jamboora++)
				{
					var ind=document.all('AdvSearchCols').item(jamboora).selectedIndex;	
					var arrSType=searchType.split("^");

					if(arrSType.length>0)
					{
						if(arrSType[ind]!="L")
						{
							/*document.getElementById('hlkEXPDT_DATE').style.display="inline"; 
							document.getElementById('imgEXPDT_DATE').style.display="inline";			*/
							//document.all('AdvSearchVals').item(jamboora).value='';
						}
					}
				}			
			}
		}
		//Format Amount for accounting Search : Praveen kasana 
		//Formats the amount and convert 111 into 1.11
		function ReplaceAll(string,text,by) 
		{
			// Replaces text with by in string
			var strLength = string.length, txtLength = text.length;
			if ((strLength == 0) || (txtLength == 0)) return string;

			var i = string.indexOf(text);
			if ((!i) && (text != string.substring(0,txtLength))) return string;
			if (i == -1) return string;

			var newstr = string.substring(0,i) + by;

			if (i+txtLength < strLength)
				newstr += ReplaceAll(string.substring(i+txtLength,strLength),text,by);

			return newstr;
		}
		function InsertDecimal(AmtValues)
		{
			AmtValues = ReplaceAll(AmtValues,".","");
			DollarPart = AmtValues.substring(0, AmtValues.length - 2);
			CentPart = AmtValues.substring(AmtValues.length - 2);
			tmp = formatCurrency(DollarPart) + "." + CentPart;
			//tmp = DollarPart + "." + CentPart;
			return tmp;
			
		}
		
		function FormatAmount(txtSearchAmount) {
		     
			txtSearchAmount.style.textAlign = "Right";
			if (txtSearchAmount.value != "")
			{
			    
			    if (iLangID == 2) {
			        iDecimalSep = ',';
			        iGroupSep = '.';
			        
			    }
			    else {
			        iDecimalSep = '.';
			        iGroupSep = ',';
			    }

			    num = txtSearchAmount.value;
			    //Added by Pradeep Kushwaha on 29-April-2011 itrack-966/913
			    var roundToDecimalPlace = 2;
			    var decimalSymbol = iDecimalSep;
			    var digitGroupSymbol = iGroupSep;
			    var groupDigits = true;
			    var regex = generateRegex(decimalSymbol);
			    var symbol = '';
			    var positiveFormat = '%s%n';
			    var negativeFormat = '-%s%n' //'(%s%n)';

			    if (num === '' || (num === '-' && roundToDecimalPlace === -1)) {
			        return num;
			    }
			    // if the number is valid use it, otherwise clean it
			    if (isNaN(num)) {
			        // clean number
			        num = num.replace(regex, '');

			        if (num === '' || (num === '-' && roundToDecimalPlace === -1)) {
			            return num;
			        }

			        if (decimalSymbol != '.') {
			            num = num.replace(decimalSymbol, '.');  // reset to US decimal for arithmetic
			        }
			        if (isNaN(num)) {
			            num = '0';
			        }
			    }

			    // evalutate number input
			    var numParts = String(num).split('.');
			    var isPositive = (num == Math.abs(num));
			    var hasDecimals = (numParts.length > 1);
			    var decimals = (hasDecimals ? numParts[1].toString() : '0');
			    var originalDecimals = decimals;


			    // format number
			    num = Math.abs(numParts[0]);
			    num = isNaN(num) ? 0 : num;
			    if (roundToDecimalPlace >= 0) {
			        decimals = parseFloat('1.' + decimals);
			        decimals = decimals.toFixed(roundToDecimalPlace); // round
			        if (decimals.substring(0, 1) == '2') {
			            num = Number(num) + 1;
			        }
			        decimals = decimals.substring(2); // remove "0."
			    }
			    num = String(num);

			    if (groupDigits) {
			        for (var i = 0; i < Math.floor((num.length - (1 + i)) / 3); i++) {
			            num = num.substring(0, num.length - (4 * i + 3)) + digitGroupSymbol + num.substring(num.length - (4 * i + 3));
			        }
			    }

			    if ((hasDecimals && roundToDecimalPlace == -1) || roundToDecimalPlace > 0) {
			        num += decimalSymbol + decimals;
			    }
			    // format negative
			    var format = isPositive ? positiveFormat : negativeFormat;
			    var money = format.replace(/%s/g, symbol);
			    money = money.replace(/%n/g, num);
			    txtSearchAmount.value = money;
			    
            }


           

		}
		function FormatAmountSearchGrid() {
		     
			var ind=document.all('SearchCol').selectedIndex;
			var arrSType=searchType.split("^");
			var colSearch = document.all('SearchCol').value.trim();
			if(arrSType.length>0)
				{
//					if( (arrSType[ind]=="T") && ((colSearch == "ACT_CURRENT_DEPOSITS.TOTAL_DEPOSIT_AMOUNT") || (colSearch == "AppliedAmount") || (colSearch == "INVOICE_AMOUNT") || (colSearch == "RemainingAmount") || (colSearch == "CHECK_AMOUNT") || (colSearch == "AMOUNT")) )
				if( (arrSType[ind]=="T") && ((colSearch == "AppliedAmount") || (colSearch == "INVOICE_AMOUNT") || (colSearch == "RemainingAmount") || (colSearch == "CHECK_AMOUNT") || (colSearch.indexOf("ACT_CURRENT_DEPOSITS.TOTAL_DEPOSIT_AMOUNT") >=0)|| (colSearch.indexOf("UNFORMATTED_INVOICE_AMOUNT") >=0) || (colSearch.indexOf("UNFORMATTED_AppliedAmount") >=0) || (colSearch.indexOf("UNFORMATTED_RemainingAmount") >=0) || (colSearch.indexOf("AMOUNT") >=0) || (colSearch == "AMOUNT")))
					{
						FormatAmount(document.all('SearchVal'));
					}
				}
		}
		
		//End Formating Amount Accounting Search		
		function ChangeDateTo(advindex)
		{				
			if(!advanceSearch)
			{
				var ind=document.all('SearchCol').selectedIndex;
				//alert(document.all('SearchCol').value);

				var arrSType=searchType.split("^");
				if(arrSType.length>0)
				{
				//alert(document.all('SearchVal').value);
					if(arrSType[ind]=="D")
					{
						searchDateFrom=FormatDateForGrid(document.all('SearchVal'),document.all('SearchVal').value);
						searchDate=FormatDateForGrid(document.all('SearchVal1'),document.all('SearchVal1').value);
						//document.all('SearchVal').value=searchDate;
						//alert(document.getElementById('hlkEXPDT_DATE'))
						//alert(document.all('hlkEXPDT_DATE'))
						
						//document.getElementById('hlkEXPDT_DATE').style.display="inline"; 
						//document.getElementById('imgEXPDT_DATE').style.display="inline";
						if(searchDate=="" && document.all('SearchVal1').value != "")
						{
						    document.all('SearchVal1').focus();
						    if (iLangID == 2) {
						        alert('Por favor, indique até o momento no formato dd/mm/aaaa.');
						    }
                            else if(iLangID == 3) {
                                alert('Please enter the To date in dd/mm/yyyy format.');
                            }
						    else {
						        alert('Please enter the To date in mm/dd/yyyy format.');
						    }
							return false;
						}
					}
					if(arrSType[ind]=="L")
					{
						
						if(isNaN(document.all('SearchVal').value))
						{
						    document.all('SearchVal').value = '';
						    if (iLangID == 2) {
						        alert('Por favor, digite apenas o valor numérico.');
						    }
						    else {
						        alert('Please enter only numeric value.');
						    }
							return;
						}
					}
					
					if(arrSType[ind]=="P")
					{
						phoneNo=FormatPhoneForGrid(document.all('SearchVal'));
						if(phoneNo!="")
							document.all('SearchVal').value=phoneNo;
						
					}
				}
			}
			else
			{
				for(jamboora=0; jamboora<document.all('AdvSearchCols').length; jamboora++)
				{
						if(advindex != jamboora)
							continue;
						var ind=document.all('AdvSearchCols').item(jamboora).selectedIndex;	
						var arrSType=searchType.split("^");

						if(arrSType.length>0)
							if(arrSType[ind]=="D")
							{
								searchDateFrom=FormatDateForGrid(document.all('AdvSearchVals').item(jamboora),document.all('AdvSearchVals').item(jamboora).value);
								searchDate=FormatDateForGrid(document.all('AdvSearchVals1').item(jamboora),document.all('AdvSearchVals1').item(jamboora).value);
								//document.all('AdvSearchVals1').item(jamboora).value=searchDate;
								if(searchDate=="" && document.all('AdvSearchVals1').item(jamboora).value != "")
								{
								    document.all('AdvSearchVals1').item(jamboora).focus();
								    if (iLangID == 2) {
								        alert('Por favor, indique até o momento no formato dd/mm/aaaa.');
								    }
								    else {
								        alert('Please enter the To date in mm/dd/yyyy format.');
								    }
									return false;
								}
							}
							if(arrSType[ind]=="L")
							{
								if(isNaN(document.all('AdvSearchVals').item(jamboora).value))
								{
								    document.all('AdvSearchVals').item(jamboora).value = '';
								    if (iLangID == 2) {
								        alert('Por favor, digite apenas o valor numérico.');
								    }
								    else {
								        alert('Please enter only numeric value.');
								    }
									return false;
								}							
							}	
							
							if(arrSType[ind]=="P")
							{
								phoneNo=FormatPhoneForGrid(document.all('AdvSearchVals').item(jamboora));
								if(phoneNo!="")
									document.all('AdvSearchVals').item(jamboora).value=phoneNo;
								else
								{
									return false;
								}	
							}
					}
			}			
		}
		
		
		function ChangeDate(advindex)
		{
			if(!advanceSearch)
			{
				var ind=document.all('SearchCol').selectedIndex;
				//alert(document.all('SearchCol').value);
				var arrSType=searchType.split("^");
				if(arrSType.length>0)
				{
				//alert(arrSType[ind]);
				//alert(document.all('SearchVal').value);
					if(arrSType[ind]=="D")
					{
						searchDate=FormatDateForGrid(document.all('SearchVal'),document.all('SearchVal').value);
						searchDateTo=FormatDateForGrid(document.all('SearchVal1'),document.all('SearchVal1').value);
						//document.all('SearchVal').value=searchDate;
						//alert(document.getElementById('hlkEXPDT_DATE'))
						//alert(document.all('hlkEXPDT_DATE'))
						
						//document.getElementById('hlkEXPDT_DATE').style.display="inline"; 
						//document.getElementById('imgEXPDT_DATE').style.display="inline";
						
						if(searchDate=="" && document.all('SearchVal').value != "") {

						    document.all('SearchVal').focus();
						    document.all('SearchVal').select();
						    if (iLangID == 2) {
						        alert('Por favor insira o de data no formato dd/mm/aaaa.');
						    }
						    else if (iLangID == 3) {
						        alert('Please enter the From date in dd/mm/yyyy format.');
                            }
						    else {
						        alert('Please enter the From date in mm/dd/yyyy format.');
						    }
							return;
						}
					}
					if(arrSType[ind]=="L")
					{
						
						if(isNaN(document.all('SearchVal').value))
						{
						    document.all('SearchVal').value = '';
						    if (iLangID == 2) {
						        alert('Por favor, digite apenas o valor numérico.');
						    }
						    else {
						        alert('Please enter only numeric value.');
						    }
							return;
						}
					}
					if(arrSType[ind]=="LT")
					{
						if(isNaN(document.all('SearchVal').value))
						{
						    document.all('SearchVal').value = '';
						    if (iLangID == 2) {
						        alert('Por favor, digite apenas o valor numérico.');
						    }
						    else {
						        alert('Please enter only numeric value.');
						    }
							return;
						}
					}
					if(arrSType[ind]=="P")
					{
						phoneNo=FormatPhoneForGrid(document.all('SearchVal'));
						if(phoneNo!="")
							document.all('SearchVal').value=phoneNo;
						
					}
				}
			}
			else
			{
				for(jamboora=0; jamboora<document.all('AdvSearchCols').length; jamboora++)
				{
						if(advindex != jamboora)
							continue;
							
						var ind=document.all('AdvSearchCols').item(jamboora).selectedIndex;	
						var arrSType=searchType.split("^");

						if(arrSType.length>0)
							if(arrSType[ind]=="D")
							{
								searchDate=FormatDateForGrid(document.all('AdvSearchVals').item(jamboora),document.all('AdvSearchVals').item(jamboora).value);
								searchDateTo=FormatDateForGrid(document.all('AdvSearchVals1').item(jamboora),document.all('AdvSearchVals1').item(jamboora).value);
								//document.all('AdvSearchVals').item(jamboora).value=searchDate;
								if(searchDate=="" && document.all('AdvSearchVals').item(jamboora).value != "") {

								    if (iLangID == 2) {
								        alert('Por favor insira o de data no formato dd/mm/aaaa.');
								    }
								    else {
								        alert('Please enter the From date in mm/dd/yyyy format.');
								    }
									return false;
								}
							}
							if(arrSType[ind]=="L")
							{
								if(isNaN(document.all('AdvSearchVals').item(jamboora).value))
								{
								    document.all('AdvSearchVals').item(jamboora).value = '';
								    if (iLangID == 2) {
								        alert('Por favor, digite apenas o valor numérico.');
								    }
								    else {
								        alert('Please enter only numeric value.');
								    }
									return false;
								}							
							}	
							
							if(arrSType[ind]=="P")
							{
								phoneNo=FormatPhoneForGrid(document.all('AdvSearchVals').item(jamboora));
								if(phoneNo!="")
									document.all('AdvSearchVals').item(jamboora).value=phoneNo;
								else
								{
									return false;
								}	
							}
					}
			}			
		}
		
		function Delete(formName)
		{			
			return false;		
		}
		
		function DeleteRow()
		{
			//alert(ids);
			
			if ( primaryKeyValues == '' ) {

			    if (iLangID == 2) {
			        alert("Por favor, selecione uma linha para excluir");
			    }
			    else {
			        alert("Please select a row to delete");
			    }
				return false;
			}
			
			if ( document.getElementById('hidKeyValues') == null ) {

			    if (iLangID == 2) {
			        alert("A variável escondida hidKeyValues não encontrado.");
			    }
			    else {
			        alert("The hidden variable hidKeyValues not found.");
			    }
				return false;
			}

			if (iLangID == 2) {
			    var flag = confirm('Tem certeza de que deseja excluir o registro selecionado?');
			}
			else {
			    var flag = confirm('Are you sure you want to delete the selected record?');
			}
			
			if ( flag == false ) return false;
			
			document.getElementById('hidKeyValues').value = primaryKeyValues;
			document.forms[0].submit();
			
			//top.frames[1].document.frames["tabLayer"].document.forms[0].submit();
			
		}
		
		
			
		function populateXML(num,msDg)
		{
			var tempXML;			
			var tmpTree="";
			var unqId=0;
			//aa();

			/*var arrAll=msDg.split("~");
			var arrColno=arrAll[0].split("^");
			var arrColname=arrAll[1].split("^");			
			var arrColSize=arrAll[3].split("^");		
			var arrPrimaryCols=sPc.split("^");				*/
			var i;				
			var rowID=parseInt(num);
			
			locQueryStr="";
			if(document.getElementById('hidlocQueryStr'))
			if(document.getElementById('hidlocQueryStr').value!="Add")
				document.getElementById('hidlocQueryStr').value="Edit";
				
			
			if(strXMLBase!="")
			{
				
				tempXML=strXMLBase;
				
				var objXmlHandler = new XMLHandler();
				var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('temp')[rowID-1]);
				var str=new String();									
				var strSingleRow="";
				var fetch=fetchColumns.split("^");		
				var queryCol;
				
				
				if(tree)
				{				
					for(i=0;i<tree.childNodes.length;i++)
					{
						
						if(tree.childNodes[i].nodeName=="UniqueGrdId" || tree.childNodes[i].nodeName=="UniqueGrdId1")
						{
						
							if(tree.childNodes[i].firstChild)
								unqId=tree.childNodes[i].firstChild.text;
						}
					}					
					
					if(strRequire=="Y")
					{
						queryCol=QueryColumns.split("^");
						
						locQueryStr="";
						for(i=0;i<queryCol.length;i++)			
						{
							var col=queryCol[i];
							
							locQueryStr = locQueryStr +  col + "=";
							
							for(j=0;j<tree.childNodes.length;j++)
							{
								if(tree.childNodes[j].nodeName==col)
								{
									if(tree.childNodes[j].firstChild)
									{
										locQueryStr += tree.childNodes[j].firstChild.text + "&";
										break;
									}	
								}
							}
					
						}						
						
						if(locQueryStr!="")
						{
							locQueryStr = locQueryStr.substring(0,locQueryStr.length-1);
						}
					//alert(locQueryStr)
					}
					//else
					//{		
						strXML="<NewDataSet><Table>";
						
						for(i=0;i<tree.childNodes.length;i++)			
						{
							col = i+1;
							
							if(tree.childNodes[col-1] != null )
							{
								if(tree.childNodes[col-1].firstChild)
								{
									strXML += "<" + tree.childNodes[col-1].nodeName + ">";
												
									//if(tree.childNodes[col-1].firstChild)
									//{						
									strXML += tree.childNodes[col-1].firstChild.text ;							
									//}			
									strXML += "</" + tree.childNodes[col-1].nodeName + ">";
								}
							}	
						
						}
						
						strXML+= "</Table>";
						strXML+= "</NewDataSet>";
					//}	
					strSelectedRecordXML = strXML;
					
					/*for(i=0;i<arrPrimaryCols.length;i++)
						rowPrimary += arrPrimaryCols[i] + "^" ;
				*/
					setmyuniqueId(unqId)
				}
			
			}		
			
			if(document.getElementById('hidlocQueryStr'))
			document.getElementById('hidlocQueryStr').value = locQueryStr ; 
		}		//-->

/**********************************************************/


//Start: code added by ajit on 29/3/2005
//Purpose:  To maintain the position of tabs acording to web grid's height.
	var defaultGridTop = 5;
	var defaultRowLength=213;
	var SearchStatus;
	var NoSearch=-100-defaultGridTop;
	var Search=-40-defaultGridTop;
	var AdvSearch=+9-defaultGridTop;
	var AdvSeacrhNoSearch=-55-defaultGridTop;
	var SearchStatus = "";
	var addNew=false;
//Start: code added by ajit on 29/3/2005
//function to supress the auto form submit in case 'enter' key is pressed in the 'SearchVal' textfield
function keyValue(e) {
    //e = event; //	alert(e.srcElement.id);
    var eventInstance = window.event ? event : e;
    var keyCode = eventInstance.keyCode ? eventInstance.keyCode : eventInstance.which ? eventInstance.which : eventInstance.charCode;
    if (keyCode == 13) {
        if (browser.isIE)
            elm = window.event.srcElement;
        else
            elm = (eventInstance.target.tagName ? eventInstance.target : eventInstance.target.parentNode);
        if (elm.id == 'SearchVal') {
			initSearch(1, false)
			return false;
		}
		else if (elm.id == 'AdvSearchVals')
		{
			initSearch(1, true)
			return false;
		}
		else
			return true;
	}
}

function setmyuniqueId(unqId)
{
	myuniqueId=unqId;

	var arrunq=myuniqueId.split("=")
	storedUnqId=unqId;
	//alert('gdfg=='+stPageNo)
	if(stPageNo=="")
		stPageNo=1;
	//setTabContent();	
	//initSearch(stPageNo,advanceSearch);
}

function serviceInitialise() { //private
//	alert('serviceInitialise : '+sSrvURL)
  //myWebService.useService(sSrvURL, "gridWebService");
  // value 0 changed to 1 
  getDefaultPage(1, false);
}

function setSearchCriteria(advSearch) { //private
    
	var sAndOrClause = "";
	var tmpVal = new String();
	if (document.all('ShowExcluded') && document.all('ShowExcluded').checked) bSx = true;
	else bSx = false;
	
	if(!eval(advSearch)){ //basic search
		
		/*if (document.all('SearchVal').value != "" && eval("_bdgSearchParam" + document.all('SearchCol').selectedIndex) == "DATE")
		{
		alert('2')
			tmpVal = document.all('SearchVal').value;
			if(tmpVal.length < 12) 
			{
				var arrTmp = tmpVal.split("/");
				tmpVal = (arrTmp[0].length == 1)? "0"+arrTmp[0] : arrTmp[0]; 
				if (typeof arrTmp[1] != "undefined") 
				{
					tmpVal += "/";
					tmpVal += (arrTmp[1].length == 1)? "0"+arrTmp[1] : arrTmp[1];
					if (typeof arrTmp[2] != "undefined") 
					{
						tmpVal += "/" + arrTmp[2];
					}
				}
				document.all('SearchVal').value = tmpVal;
			}			
			sSearchCriteria = document.all('SearchCol').options[document.all('SearchCol').selectedIndex].value + SEPARATOR + document.all('SearchVal').value;
		}*/
		//if()
		//{
			//NULL check for document.all('SearchVal')
			if (document.all('SearchVal') && document.all('SearchVal').value != ""){
				if(selectFlag != 3)
				{
					//Done for Itrack Issue 6380 on 1 Dec 09
					var arrSType=searchType.split("^");
					var ind=document.all('SearchCol').selectedIndex;
					if(arrSType[ind]=="LT")
					{
						if(isNaN(document.all('SearchVal').value)) {

						    if (iLangID == 2) {
						        setTimeout('alert("Por favor, digite apenas o valor numérico.")', 50);
						    }
						    else {
						        setTimeout('alert("Please enter only numeric value.")', 50);
						    }
							return false;
						}
						else
						{
							sSearchCriteria = document.all('SearchCol').options[document.all('SearchCol').selectedIndex].value + SEPARATOR + document.all('SearchVal').value;	
						} 
					}
					else
					  sSearchCriteria = document.all('SearchCol').options[document.all('SearchCol').selectedIndex].value + SEPARATOR + document.all('SearchVal').value;	
				}		
				else
				{
					var FromDate = document.all('SearchVal').value;
					var ToDate = document.all('SearchVal1').value;
					if(FromDate == '')
						FromDate = '01/01/1900';
					if(ToDate == '')
						ToDate = '01/01/3000';
					sSearchCriteria = document.all('SearchCol').options[document.all('SearchCol').selectedIndex].value + SEPARATOR + FromDate + '-' + ToDate;			
				}
			}
			else if(selectFlag == 3 && document.all('SearchVal1') && document.all('SearchVal1').value != "")
			{
					var FromDate = document.all('SearchVal').value;
					var ToDate = document.all('SearchVal1').value;
					if(FromDate == '')
						FromDate = '01/01/1900';
					if(ToDate == '')
						ToDate = '01/01/3000';
					sSearchCriteria = document.all('SearchCol').options[document.all('SearchCol').selectedIndex].value + SEPARATOR + FromDate + '-' + ToDate;			
			}
			// if we want blank search, then uncomment below lines
			//else{
			//	sSearchCriteria = document.all('SearchCol').options[document.all('SearchCol').selectedIndex].value + SEPARATOR + '';
			//}
		//}	
	}
	else { //advanced search 
	      //Added For Itrack Issue #5552
	      isComCheck=0;
		//alert('advanced search')
		//Done for Itrack Issue 6380 on 4 Nov 09
		var arrSType=searchType.split("^");
		for(var i=0;i<document.all('AdvSearchCols').length;i++)
		{
			var ind=document.all('AdvSearchCols').item(i).selectedIndex;
			if(arrSType[ind]=="LT")
			{
				if(isNaN(document.all('AdvSearchVals').item(i).value))
				{
					alert('Please enter only numeric value.');
					return false;
				}
			}
		}
	
		for(i=0; i<document.all('AdvSearchCols').length; i++){
			if (document.all('AdvSearchVals').item(i).value != "") {
				/*if(eval("_bdgSearchParam" + document.all('AdvSearchCols').item(i).selectedIndex) == "DATE")
				{
					tmpVal = document.all('AdvSearchVals').item(i).value;
					if(tmpVal.length < 12) 
					{
						var arrTmp = tmpVal.split("/");
						tmpVal = (arrTmp[0].length == 1)? "0"+arrTmp[0] : arrTmp[0]; 
						if (typeof arrTmp[1] != "undefined") 
						{
							tmpVal += "/";
							tmpVal += (arrTmp[1].length == 1)? "0"+arrTmp[1] : arrTmp[1];
							if (typeof arrTmp[2] != "undefined") 
							{
								tmpVal += "/" + arrTmp[2];
							}
						}
						document.all('AdvSearchVals').item(i).value = tmpVal;
					}
				}*/
				if(eval("_bdgSearchParam" + document.all('AdvSearchCols').item(i).selectedIndex) != "DATE")
					sSearchCriteria += sAndOrClause + SEPARATOR + document.all('AdvSearchCols').item(i).options[document.all('AdvSearchCols').item(i).selectedIndex].value + SEPARATOR + document.all('AdvSearchVals').item(i).value + SEPARATOR;
				else
				{
					var FromDate = document.all('AdvSearchVals').item(i).value;
					var ToDate = document.all('AdvSearchVals1').item(i).value;
					if(FromDate == '')
						FromDate = '01/01/1900';
					if(ToDate == '')
						ToDate = '01/01/3000';

					sSearchCriteria += sAndOrClause + SEPARATOR + document.all('AdvSearchCols').item(i).options[document.all('AdvSearchCols').item(i).selectedIndex].value + SEPARATOR + FromDate + '-' + ToDate + SEPARATOR;
				}
				if (document.all('AdvSearchOpt').item(i))
					sAndOrClause = document.all('AdvSearchOpt').item(i).options[document.all('AdvSearchOpt').item(i).selectedIndex].value;
			} // end if
			else if(eval("_bdgSearchParam" + document.all('AdvSearchCols').item(i).selectedIndex) == "DATE" && document.all('AdvSearchVals1').item(i).value != "")
			{
					var FromDate = document.all('AdvSearchVals').item(i).value;
					var ToDate = document.all('AdvSearchVals1').item(i).value;
					if(FromDate == '')
						FromDate = '01/01/1900';
					if(ToDate == '')
						ToDate = '01/01/3000';

					sSearchCriteria += sAndOrClause + SEPARATOR + document.all('AdvSearchCols').item(i).options[document.all('AdvSearchCols').item(i).selectedIndex].value + SEPARATOR + FromDate + '-' + ToDate + SEPARATOR;
			}

		} //end for
		sSearchCriteria = sSearchCriteria.substr(1);
	}
}

function GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,unqId,lstId,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,sesFlag,screenIDString,requireNormalCursor, sCellHorizontalAlign)
{
var Result='';
Result=BaseDataGrid.GetSortedData(sQg, sOc, sDg, sPc, sSg , sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,unqId,lstId,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,sesFlag,screenIDString,requireNormalCursor, sCellHorizontalAlign,Function_CallBack);
}
function GetSortedDataSaveJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,unqId,lstId,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,sesFlag,screenIDString,requireNormalCursor, sCellHorizontalAlign)
{
var Result='';
Result=BaseDataGrid.GetSortedData(sQg, sOc, sDg, sPc, sSg , sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,unqId,lstId,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,sesFlag,screenIDString,requireNormalCursor, sCellHorizontalAlign,Function_CallBackSave);
}
function Function_CallBack(response)
	{
				var Xml = response;
				//alert('response=' + response.value);
				var objResponse=new Object();
				objResponse.value = Xml;
				refreshGridResult(response); 
	}
function Function_CallBackSave(response)
	{
				var Xml = response;
				//alert('response in  save=' + response.value);
				var objResponse=new Object();
				objResponse.value = Xml;
				refreshGridResultSave(response); 
	}
	
function getDefaultPage(pageNo, advSearch) 
{ 
	HideTab();

	globSelected=1
	searchClick=1;	
	doubleClick=-1			
	sSearchCriteria = "";
	bBaseDataGridReady = false;

	if (advSearch && typeof document.all('SearchVal') == "object") {
		sSearchCriteria=mainSearch;
	}
	advanceSearch=advSearch;
	
	var sSgArr;
	var searchCol;

	if(sSg!="")
	  sSgArr=sSg.split("~");
	 
	if(sSgArr.length>0)
		searchCol=sSgArr[0].split("^");
	
	if(firstTime==1)
	{
		if(searchCol.length>0)
			sSearchCriteria = searchCol[0] + "~*#)(";
	}
	if(firstTime==1)
	{
		if(advSearch)
		{
			if(searchCol.length>0)	
				sSearchCriteria = searchCol[0] + "~*#)(";			
		}
	}
	headRow=0;
	
	if(DefaultSearch=="Y")
		sSearchCriteria = searchCol[0] + "~ ";	
	//if(typeof myWebService.gridWebService == 'undefined')
	//	myWebService.useService(sSrvURL, "gridWebService");
	//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
	GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
}

function initSearch(pageNo, advSearch) 
{ 	
	HideTab();
	searchClick=1;
	sSearchCriteria = "";
	bBaseDataGridReady = false;	
	if(headRow!=1)
	{	
		setSearchCriteria(advSearch);
	}
	else
	{		
		if(firstTime==1)
			sSearchCriteria=srhCrt; 			
		headRow=0;
	}	
	stPageNo=pageNo;		
	firstTime=2;
	isComCheck=0;
	advanceSearch=advSearch;
	/***********************************/
	//DO NOT DELETE -- CODE FORSHOWING SINGLE RECORD BASED ON MYUNIQUEID
	/*
	var arrunq=myuniqueId.split("=")
	
	if(arrunq.length>1)
		myWebService.gridWebService.callService(refreshGridResult, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,arrunq[0],arrunq[1],bFValue,lookUpDetails,searchType);		
	else
	*/
	//if(typeof myWebService.gridWebService == 'undefined')
	//	myWebService.useService(sSrvURL, "gridWebService");

	//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
	GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
}

function checkSearch(fpageNo,fadvSearch)
{	
	//if(firstTime==2)
		initSearch(fpageNo,fadvSearch);
}

//Modified by Asfa(18-June-2008) - iTrack #3906(Note: 1)
function changeSort(pageNo, colPos, order, advSearch, ColsType) 
{ //internal
	HideTab();
	globSelected=1
	sSearchCriteria = "";
	bBaseDataGridReady = false;
	if(order)
	{
	  if(ColsType == "N") 
		sOc = colPos.toString() + "_1 ASC";
	  else
		sOc = colPos.toString() + " ASC";
	}
	else
	{
	  if(ColsType == "N") 
		sOc = colPos.toString() + "_1 DESC";
	  else
		sOc = colPos.toString() + " DESC";
	}
	sOc=BaseDataGrid.EncriptValue(sOc).value;
	stPageNo=pageNo;
	setSearchCriteria(advSearch);
	advanceSearch=advSearch;
	//if(typeof myWebService.gridWebService == 'undefined')
	//	myWebService.useService(sSrvURL, "gridWebService");

	//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
	GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
}



function changeSortFirstTime(pageNo, colPos, order, advSearch) 
{
	HideTab();
	globSelected=1
	sSearchCriteria = "";
	bBaseDataGridReady = false;
	if (order) sOc = colPos.toString() + " ASC";
	else sOc = colPos.toString() + " DESC";
	sOc=BaseDataGrid.EncriptValue(sOc).value;
	stPageNo=pageNo;	
}


function changePage(pageNo, sortOrd, advSearch) { //internal
	//alert("PageNo="+pageNo)
	
	HideTab();
	globSelected=1
	sSearchCriteria = "";
	bBaseDataGridReady = false;
	stPageNo=pageNo;
	sOc = sortOrd.toString();
	sOc=BaseDataGrid.EncriptValue(sOc).value;
	setSearchCriteria(advSearch);
	advanceSearch=advSearch;
	//if(typeof myWebService.gridWebService == 'undefined')
	//	myWebService.useService(sSrvURL, "gridWebService");

	//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
	GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
}

//function added by manab
function gotoPage(txcontrol,totpages, sortOrd, advSearch) { 
	//alert('5')
	globSelected=1
	if (event.keyCode ==13)
	{	
		if (txcontrol && !isNaN(txcontrol.value) && parseInt(txcontrol.value) <= totpages && parseInt(txcontrol.value) > 0)
		{
			pageNo=parseInt(txcontrol.value);
			sSearchCriteria = "";
			bBaseDataGridReady = false;
			sOc = sortOrd.toString();
			sOc=BaseDataGrid.EncriptValue(sOc).value;
			setSearchCriteria(advSearch);
			advanceSearch=advSearch;
			//if(typeof myWebService.gridWebService == 'undefined')
			//	myWebService.useService(sSrvURL, "gridWebService");

			//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
			GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, advSearch, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
		}
		else {

		    if (iLangID == 2) {
		        alert("Número da página inválido entrou. Digite o número da página entre uma e " + totpages);
		    }
		    else {
		        alert("Invalid page number entered. Enter a page number between 1 and " + totpages);
		    }
		}		
		event.returnValue=false;		
	}
}
function changecursor(obj)
{
    obj.style.cursor="hand";
}

function checkboxCheck()
{	
	isCheck=1;	
}

function button_Click(str) {
    
	var amparrDDL=str.split("&");
	var RowIdPos=4;//default value is 4
	if(amparrDDL!=null && amparrDDL.length>0)
	{
		for(var iTemp =0;iTemp<amparrDDL.length;iTemp++)
		{
			if(amparrDDL[iTemp].split("=")[0]=="rowid")
			{
				RowIdPos = iTemp;
				break;		
			}
		}
	}
	var eqlarrDDL = amparrDDL[RowIdPos].split("="); 
	
	//Added  by Charles on 25-Jun-10 for Itrack 174
	var ctlID = 0;
	if ((parseInt(eqlarrDDL[1]) + 2) > 9) {
	    ctlID = parseInt(eqlarrDDL[1]) + 2;
	}
	else {
	    ctlID = '0' + (parseInt(eqlarrDDL[1]) + 2)
	}
	var ctrlDDL = eval("document.getElementById('ctl" + ctlID + "_ddl_" + eqlarrDDL[1] + "')")
	//Added till here

	//Commented  by Charles on 25-Jun-10 for Itrack 174
	//var ctrlDDL=eval("document.getElementById('ddl_" + eqlarrDDL[1] + "')")
	
	if(ctrlDDL==null || ctrlDDL.selectedIndex==-1 || ctrlDDL.selectedIndex==0)
		return false;
	var selectedStatus=ctrlDDL.options[ctrlDDL.selectedIndex].value;	
	if (TrimTheString(selectedStatus) != "")
	{
		str=str + "&process=" + selectedStatus;
		var strURL="policyprocess.aspx?" + str;
		parent.location="/cms/policies/processes/" + strURL;		
	}	
	return false;
}

function image_Click(str)
{
	imageClick=1;
	var url;
	url="/cms/application/aspx/quotegenerator.aspx?" + str;
	window.open(url,'BRICS','resizable=yes,scrollbars=yes,left=150,top=50,width=800,height=500') 
	
	return false;
}

function dropdownChange(obj)
{
}


function selectAll() {

if(isComCheck==0)
{
	for(i=1;i<=iPs;i++) {

	    //var ctrl=eval("document.getElementById('ck_" + i + "')");
        //Added by Charles on 2-Jun-10 for Itrack 37
	    if ((i + 2) < 10) {
	        var ctrl = eval("document.getElementById('ctl0" + (i + 2) +"_ck_"+ i + "')");
	    }
	    else {
	        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
	    }
	    
		var rowTag=eval("document.getElementById('Row_" + i + "')");								
		if(ctrl)						
			ctrl.checked=true
	}
	isComCheck=1;
}
else
{	
  	for(i=1;i<=iPs;i++) {

  	    //var ctrl=eval("document.getElementById('ck_" + i + "')");
  	    //Added by Charles on 2-Jun-10 for Itrack 37
	    if ((i + 2) < 10) {
	        var ctrl = eval("document.getElementById('ctl0" + (i + 2) + "_ck_" + i + "')");
	    }
	    else {
	        var ctrl = eval("document.getElementById('ctl" + (i + 2) + "_ck_" + i + "')");
	    }	    
		
		var rowTag=eval("document.getElementById('Row_" + i + "')");
		//alert(ctrl + "rowTag");				
		if(ctrl)						
			ctrl.checked=false
	} 
	isComCheck=0;
}
			
}

function highlightrowClick(obj)
{
	
	if(isCheck!=1)
	{
		isCheck=0;
		ShowTab();
		//if(document.getElementById('tabLayer').style.display=='none')
		//	document.getElementById('tabLayer').style.display='inline'
		doubleClick=1;
		
		//added by vj on 05-04-2006   -- Checking that object must not be null
		if (obj != null)
		{
			var arrRowNum=obj.id.split("_");
			var num=arrRowNum[1];
			if(prevObj)
			{
				if(obj.style.backgroundColor!=selColor)    
				{			
					var i			
					for(i=1;i<=parseInt(iPs);i++)
					{
						if(document.getElementById("Row_"+ i))
						{
							
							if(document.getElementById("Row_"+ i).style.color!="red")
							document.getElementById("Row_"+ i).style.color="black";  		
						}							
					}
						
					originalrow_color=obj.style.backgroundColor;
					prevObj.style.backgroundColor=originalrow_color;
					if(obj.style.color!="red")
					{
						original_color=obj.style.color;
						
						if(prevObj.style.color!="red")
							prevObj.style.color=original_color;
						obj.style.color="#0066CC";
						prevObj=obj;
						
					}	
					obj.style.backgroundColor=selColor;					
				}	
			}
			else
			{		
				var i
				for(i=1;i<=parseInt(iPs);i++)
				{
					if(document.getElementById("Row_"+ i))
					{
				
						if(document.getElementById("Row_"+ i).style.color!="red")
							document.getElementById("Row_"+ i).style.color="black";  				
					}
				}					
					
				prevObj=obj;		
				originalrow_color=prevObj.style.backgroundColor;		
				prevObj.style.backgroundColor=selColor;		
				if(prevObj.style.color!="red")
				{
					original_color=prevObj.style.color;
					prevObj.style.color="#0066CC";		
				}
			}
			
			//alert(obj.pkvalues);
			primaryKeyValues = obj.pkvalues;
			
			var arrRowNum=obj.id.split("_");
			prmId=arrRowNum[1];			
			
			checkDblClick=2;
			//show=2
			prmId=num;
			globSelected=prmId;
			
			/*************************************************************/
			//DO NOT DELETE -- CODE FOR TOGGLING IMAGE
			//string="toggleImage('/cms/cmsweb/images/expand_icon.gif')";
			//setTimeout(string,100);	
			
			if (window.onRowClicked) onRowClicked(num,msDg);
			
			//Added by vijay 20 Sep
			//Refreshing the tabs of tab controls
			if (window.SetTabs) 
			{
				//First of all we will fetch the currently selected tab from tab control
				//this is in prvsTab variable defined in basetabcontrol in 0,0 format
				if (prvsTab != "undefined" && prvsTab != "" && prvsTab != null)
				{
					baseGridPrevRab = prvsTab.split(',')[1];
				}
				else
				{
					baseGridPrevRab = 0;
				}
				
				//Refreshing the tab pages, by calling a function defined in page
				SetTabs(baseGridPrevRab, true);
			}	
		}	
	}
	isCheck=0;
}

function selectROWID(str)
{
	var rwid=eval("document.getElementById('Row_" + str + "')");
	
	if(rwid)
	{
		rwid.style.backgroundColor="black";
		//rwid.style.color="#0066CC";
		rwid.style.color="pink";
		prevObj=rwid;
	}		
	//string="selectColor('"+str+"');";
	//setTimeout(string,100);
}

function selectColor(str)
{	
	var rwid=eval("document.getElementById('" + str + "')");
	if(rwid)
	{
		rwid.style.backgroundColor="black";
		//rwid.style.color="#0066CC";
		prevObj=rwid;
	}
}

/******************************************************************************************
	<Author					: - > Pradeep
	<Description			: - > Double click functionlity for look up
	*/	
function OnDoubleClick(TextFieldID,ValueFieldID,TextFieldValue,ValueFieldValue)
{
	window.opener.document.getElementById(TextFieldID).value = TextFieldValue;
	window.opener.document.getElementById(ValueFieldID).value = ValueFieldValue;
	window.close();
}

function deselectRow(){ //public
	if (prevObj != null){
		//document.all(prevRowSelected).className=prevRowClassName;
		prevObj.style.backgroundColor=originalrow_color;
	}
	prevObj= null;
	//prevRowClassName = null;
}

function refreshGrid(rowData,roID) 
{ 
	sRowData = rowData;

	if(firstTime==1)
		stPageNo=1;	

	var sSgArr;
	var searchCol;
	if(sSg!="")
	  sSgArr=sSg.split("~");
	 
	if(sSgArr.length>0)
		searchCol=sSgArr[0].split("^");
		
	//comment removed to enable search after save when search is not clicked and directly add button is clicked	
	sSearchCriteria = searchCol[0] + "~ "; 	
	
	if(advanceSearch)		
	{
		if(roID!="")
		{
			// Added by Mohit Agarwal 17 Oct 2008 ITrack 4902
			//if(typeof myWebService.gridWebService == 'undefined')
			//	myWebService.useService(sSrvURL, "gridWebService");

			//myWebService.gridWebService.callService(refreshGridResultSave, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo, sSearchCriteria, true, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"",roID,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
			GetSortedDataSaveJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo, sSearchCriteria, true, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"",roID,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
		}
		else
		{	
			//if(typeof myWebService.gridWebService == 'undefined')
			//	myWebService.useService(sSrvURL, "gridWebService");

			//myWebService.gridWebService.callService(refreshGridResultSave, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo,sSearchCriteria, true, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
			GetSortedDataSaveJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo,sSearchCriteria, true, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
		}
	}
	else
	{		
		if(roID!="")
		{
			//if(typeof myWebService.gridWebService == 'undefined')
			//	myWebService.useService(sSrvURL, "gridWebService");

			//myWebService.gridWebService.callService(refreshGridResultSave, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"",roID,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
			GetSortedDataSaveJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"",roID,bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
		}
		else
		{	
			//if(typeof myWebService.gridWebService == 'undefined')
			//	myWebService.useService(sSrvURL, "gridWebService");

			//myWebService.gridWebService.callService(refreshGridResultSave, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo,sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
			GetSortedDataSaveJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, stPageNo,sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);	
		}		
	}			
}


function setTabContent()
{
	var scrWidth = window.screen.width
			
	if(scrWidth > 800 && scrWidth < 1000 )
	{

		tabContent.style.left = 188
	}

	if(scrWidth > 1000 && scrWidth < 1280 )
	{

		tabContent.style.left = 188
	}

	if(scrWidth >= 1280 )
	{

		tabContent.style.left =316
	}
}

function deleteRow()
{	
	var arrprm=prmId.split("^");
	
	initSearch(pgno,advsrch,arrprm[1]);
	singleClick(prmId)

	if (retRec == -1 || retRec == 0 || retRec == -100) {
	    if (iLangID == 2) {
	        alert("Linha não pode ser excluída");
	    }
	    else {
	        alert("Row cannot be deleted");
	    }
	}
	else if (parseInt(retRec) > 0) {
	if (iLangID == 2) {
	    alert("Linha excluída permanentemente");
	}
	else {
	    alert("Row deleted permanently");
	}	    
	}
}

function refreshGridResult(result) 
{ 
	if(result.error)
	{
		//theResult1.innerHTML = result.errorDetail.string;
		top.location.href		=	"/cms/cmsweb/aspx/error.aspx?ExMesssage=" + result.errorDetail.string + "&refPath=" + document.location.href;
	}
	else
	{	
		if(result.value!="")
		{
			var arr=result.value.split("^^#@");	
			theResult1.innerHTML =arr[0];

		 	prmId="";
			mainSearch=sSearchCriteria;
			if(arr.length>1)
				strXMLBase=arr[1];
				
				var flag=0;
				var arrDrop=DropdownCols.split("^");
				if(arrDrop.length>0)
				 for(i=0;i<arrDrop.length;i++)
					if(arrDrop[i]!="")
						flag=1;
				
				_bdgPopulateSearch(true); 
				if(flag==1)		
				{
					//Added by RPSINGH --- if RequireFocus =="Y" then focus on text box										
					if (RequireFocus =="Y")
					{ 
					     
						if (document.all('SearchCol')) //basic search
						{
							if(eval("_bdgSearchParam" + document.all('SearchCol').selectedIndex) == "TEXT" || eval("_bdgSearchParam" + document.all('SearchCol').selectedIndex) == "DATE_RANGE")
							{
								document.all('SearchVal').focus();
								document.all('SearchVal').select();           
							}
							else
								document.all('SearchVal').focus();
						}
						else //Advanced Search
						{
							if(eval("_bdgSearchParam" + document.all('AdvSearchCols').item(0).selectedIndex) == "TEXT" || eval("_bdgSearchParam" + document.all('AdvSearchCols').item(0).selectedIndex) == "DATE_RANGE")
							{
								document.all('AdvSearchVals').item(0).focus();
								document.all('AdvSearchVals').item(0).select();
							}
							else
								document.all('AdvSearchVals').item(0).focus();
						}

					}//req focus="Y"
					//End of addition by RPSINGH
				}
				
			return; /// Testing by Rajan for Grid Changes -- Height Adjustment 	
		}	
	}
}

function refreshGridResultSave(result) 
{ 
	if(result.error)
	{
		//theResult1.innerHTML = result.errorDetail.string;
		top.location.href		=	"/cms/cmsweb/aspx/error.aspx?ExMesssage=" + result.errorDetail.string + "&refPath=" + document.location.href;
	}
	else
	{	
		if(result.value!="")
		{
			var arr=result.value.split("^^#@");	
			theResult1.innerHTML =arr[0];
			
		 	prmId="";
			mainSearch=sSearchCriteria;
			if(arr.length>1)
				strXMLBase=arr[1];
				
				var flag=0;
				var arrDrop=DropdownCols.split("^");
				if(arrDrop.length>0)
				 for(i=0;i<arrDrop.length;i++)
					if(arrDrop[i]!="")
						flag=1;
					
		
				_bdgPopulateSearch(true); 
				if(flag==1)		
				{				
			
				}
					
				if(arr.length>2)	
				{
					populateXML(arr[2],msDg)
				}
				
			return; /// Testing by Rajan for Grid Changes -- Height Adjustment 	
		}	
	}
}


function changeRow(rowData) { //public
  sRowData = rowData;

  if (prevRowSelected != null){
		document.all(prevRowSelected).className=prevRowClassName;
  }
 
  if (document.all("Row^"+sRowData)) {
    prevRowSelected = "Row^"+sRowData;
    prevRowClassName = document.all(prevRowSelected).className;
	document.all(prevRowSelected).className = "Selected";
  }
  else {
    sRowData = "";
    prevRowSelected = null;
  }
}

function inset(colId){ //internal
	document.all(colId).className="HeadColClicked";
}

function outset(colId){ //internal
	document.all(colId).className="gridheader";
}

function selectMultiRow(rowdata){ //internal
	//alert(getAllSelected());
}

function _bdgPopulateSearch(flag) { //internal
   var From = '<%=strFrom %>';
   var To = '<%=strTo %>';
	if (document.all('SearchCol')) { //i.e. basic search
	//alert('1' + 'basic');
	   //Added For Itrack Issue #5552.
	    isComCheck = 0;
	   
	    var tmpSearchVal = document.all('SearchVal').value;
	    var type = document.all('SearchVal').type;//Added for tfd 466 -itrack-1537 -- Pradeep Kushwaha 24-Oct-2011


		if (eval("_bdgSearchParam" + document.all('SearchCol').selectedIndex) != "DATE" && eval("_bdgSearchParam" + document.all('SearchCol').selectedIndex) != "TEXT") { //ensure that the search type is neither 'date' nor 'text'
			selectFlag=1;
			document.all("_bdgSearchDiv").innerHTML = eval("_bdgSearchParam" + document.all('SearchCol').selectedIndex); //set the innerHTML of the div to dropdown
			for (i=0; i<document.all('SearchVal').options.length; i++)
				if (document.all('SearchVal').options[i].value == tmpSearchVal) document.all('SearchVal').selectedIndex = i;
				var ind = document.all('SearchCol').selectedIndex;
				var arrSType = searchType.split("^");
				if (type == "select-one") {//Added for tfd 466 -itrack-1537 -- Pradeep Kushwaha 24-Oct-2011
				    document.all('SearchVal').value = '';
				}

		}
		else if (eval("_bdgSearchParam" + document.all('SearchCol').selectedIndex) != "DATE")
		{
			document.all("_bdgSearchDiv").innerHTML = "<input type=text id=SearchVal  name=SearchVal onBlur='ChangeDate(0);FormatAmountSearchGrid();' size=15 value='" + tmpSearchVal  + "'>";
			document.all('SearchVal').value=tmpSearchVal;
			selectFlag=2;
			var ind=document.all('SearchCol').selectedIndex;
			var arrSType = searchType.split("^");

			if (type == "select-one") {//Added for tfd 466 -itrack-1537 -- Pradeep Kushwaha 24-Oct-2011
			    document.all('SearchVal').value = '';
			}
			if(arrSType[ind]=="LT")
					{
						
						if(isNaN(document.all('SearchVal').value))
						{
							document.all('SearchVal').value='';
							//alert('Please enter only numeric value.');
							return;
						}
					}
		}
		else
		{
			var arrSearchVal=tmpSearchVal.split("-");
			if(arrSearchVal.length>1)
			{
				if(arrSearchVal[0] == "01/01/1900")
					arrSearchVal[0] = "";
				if(arrSearchVal[1] == "01/01/3000")
				    arrSearchVal[1] = "";
				document.all("_bdgSearchDiv").innerHTML = "<label for='SearchVal'><b> "+ From +" :</b></label><input type=text id=SearchVal  name=SearchVal onBlur='ChangeDate(0);FormatAmountSearchGrid();' size=15 value='" + arrSearchVal[0]  + "'><label for='SearchVal1'><b> "+ To +": </b></label><input type=text id=SearchVal1  name=SearchVal1 onBlur='ChangeDateTo(0);FormatAmountSearchGrid();' size=15 value='" + arrSearchVal[1]  + "'>";
				document.all('SearchVal').value=arrSearchVal[0];
				document.all('SearchVal1').value=arrSearchVal[1];
				selectFlag=3;
			}
			else
			{
			    document.all("_bdgSearchDiv").innerHTML = "<label for='SearchVal'><b> " + From + ": </b></label><input type=text id=SearchVal  name=SearchVal onBlur='ChangeDate(0);FormatAmountSearchGrid();' size=15 value=''><label for='SearchVal1'><b> " + To + ": </b></label><input type=text id=SearchVal1  name=SearchVal1 onBlur='ChangeDateTo(0);FormatAmountSearchGrid();' size=15 value=''>";
				//document.all('SearchVal').value=tmpSearchVal;
				selectFlag=3;
			}
		}
		
	}
		else {
		    var tmpSearchVal;//advance search
		    var From = '<%=strFrom %>';
		    var To = '<%=strTo %>';
		for(i=0; i<document.all('AdvSearchCols').length; i++)
		{  
		    tmpSearchVal = document.all('AdvSearchVals').item(i).value;
		    var type = document.all('AdvSearchVals').item(i).type; //Added for tfd 466 -itrack-1537 -- Pradeep Kushwaha 24-Oct-2011
			if (eval("_bdgSearchParam" + document.all('AdvSearchCols').item(i).selectedIndex) != "DATE" && eval("_bdgSearchParam" + document.all('AdvSearchCols').item(i).selectedIndex) != "TEXT") { //ensure that the search type is neither 'date' nor 'text'
				document.all("_bdgAdvSearchDiv").item(i).innerHTML = eval("_bdgSearchParam" + document.all('AdvSearchCols').item(i).selectedIndex).replace("SearchVal", "AdvSearchVals") + "<input type=text id=AdvSearchVals1  name=AdvSearchVals1 size=15 value='' style='display:none;'>"; //replace the SearchVal with AdvSearchVals for advanced search and set the innerHTML of the div to dropdown 
				for (k=0; k<document.all('AdvSearchVals').item(i).options.length; k++)
				    if (document.all('AdvSearchVals').item(i).options[k].value == tmpSearchVal) document.all('AdvSearchVals').item(i).selectedIndex = k;
				
				    var ind = document.all('AdvSearchVals').item(i).value;
				    var arrSType = searchType.split("^");
				    var colInd = document.all('AdvSearchCols').item(i).selectedIndex; // changed by praveer for itrack no 1537/TFS# 466
				    if (type == "select-one" && arrSType[colInd] == "T") {// changed by praveer for itrack no 1537/TFS# 466 .//Added for tfd 466 -itrack-1537 -- Pradeep Kushwaha 24-Oct-2011
				        document.all('AdvSearchVals').item(i).value = '';
				    }
			}
			else if (eval("_bdgSearchParam" + document.all('AdvSearchCols').item(i).selectedIndex) != "DATE")
			{
				document.all("_bdgAdvSearchDiv").item(i).innerHTML = "<input type=text id=AdvSearchVals name=AdvSearchVals size=15>"+ "<input type=text id=AdvSearchVals1  name=AdvSearchVals1 size=15 value='' style='display:none;'>";

				document.all('AdvSearchVals').item(i).value = tmpSearchVal;
				var ind = document.all('AdvSearchCols').item(i).selectedIndex;
				var arrSType = searchType.split("^");

				if (type == "select-one" && arrSType[ind] == "T") {// changed by praveer for itrack no 1537/TFS# 466 .//Added for tfd 466 -itrack-1537 -- Pradeep Kushwaha 24-Oct-2011
				    document.all('AdvSearchVals').item(i).value = '';
				}
				//alert(document.all("_bdgAdvSearchDiv").item(i).innerHTML)
			}
			else
			{
				if(tmpSearchVal.indexOf('-') < 0 && document.all('AdvSearchVals1') != null && typeof document.all('AdvSearchVals1').length != 'undefined' && document.all('AdvSearchVals1').length > i)
				{
					tmpSearchVal = document.all('AdvSearchVals').item(i).value+'-'+document.all('AdvSearchVals1').item(i).value;
				}
				var arrSearchVal=tmpSearchVal.split("-");
				if(arrSearchVal.length>1 && arrSearchVal[1] != '')
				{
					if(arrSearchVal[0] == "01/01/1900")
						arrSearchVal[0] = "";
					if(arrSearchVal[1] == "01/01/3000")
						arrSearchVal[1] = "";
		document.all("_bdgAdvSearchDiv").item(i).innerHTML = "<label for='AdvSearchVals'><b> " + From + ": </b></label><input type=text id=AdvSearchVals  name=AdvSearchVals onBlur='ChangeDate(" + i + ");' size=15 value='" + arrSearchVal[0] + "'><label for='AdvSearchVals1'><b>" + To + ":</b></label><input type=text id=AdvSearchVals1  name=AdvSearchVals1 onBlur='ChangeDateTo(" + i + ");' size=15 value='" + arrSearchVal[1] + "'>";
					document.all('AdvSearchVals').item(i).value=arrSearchVal[0];
					//document.all('AdvSearchVals1').item(i).value=arrSearchVal[1];
					selectFlag=3;
				}
				else if(arrSearchVal.length>1 && arrSearchVal[1] == '')
				{
					if(arrSearchVal[0] == "01/01/1900")
						arrSearchVal[0] = "";
					if(arrSearchVal[1] == "01/01/3000")
						arrSearchVal[1] = "";
		document.all("_bdgAdvSearchDiv").item(i).innerHTML = "<label for='AdvSearchVals'><b> " + From + ":</b></label><input type=text id=AdvSearchVals  name=AdvSearchVals onBlur='ChangeDate(" + i + ");' size=15 value='" + arrSearchVal[0] + "'><label for='AdvSearchVals1'><b>" + To + ":</b></label><input type=text id=AdvSearchVals1  name=AdvSearchVals1 onBlur='ChangeDateTo(" + i + ");' size=15 value='" + arrSearchVal[1] + "'>";
					document.all('AdvSearchVals').item(i).value=arrSearchVal[0];
				}
				else
				{
				    document.all("_bdgAdvSearchDiv").item(i).innerHTML = "<label for='AdvSearchVals'><b> " + From + ":</b></label><input type=text id=AdvSearchVals  name=AdvSearchVals onBlur='ChangeDate(" + i + ");' size=15 value='" + arrSearchVal[0] + "'><label for='AdvSearchVals1'><b>" + To + ": </b></label><input type=text id=AdvSearchVals1  name=AdvSearchVals1 onBlur='ChangeDateTo(" + i + ");' size=15 value='" + arrSearchVal[0] + "'>";
					document.all('AdvSearchVals').item(i).value=tmpSearchVal;
				}
				
			}
		} //end for			
	}
}

function getAllSelected() { //public
	var str = "";
	if (document.all("cbxSelectRow")) {		
		if (typeof document.all("cbxSelectRow").checked != "undefined") {
			if (document.all("cbxSelectRow").checked) str = SEPARATOR + document.all("cbxSelectRow").value.substr(1);
		}
		else {
			for(i=0; i<document.all("cbxSelectRow").length; i++) {
				if (document.all("cbxSelectRow").item(i).checked) str += SEPARATOR + document.all("cbxSelectRow").item(i).value.substr(1);
			}
		}
		return str.substr(1);
	}
	else
		return "";
}



function externalSearch(searchCol, searchVal) 
{
	HideTab();
	
	var extSearch=true;
	
	if(firstTime==1)
		stPageNo=1;	
		
	if (searchVal != "")
		sSearchCriteria = searchCol + SEPARATOR + searchVal;
		PageNo=1;
		
	var sAndOrClause = "";
	var tmpVal = new String();
	advSearch=true;
	
	if(!advSearch)
	{ 
		var arrSearchVal=searchVal.split("-");
		if(arrSearchVal.length>1 && document.all("_bdgSearchDiv").innerHTML.indexOf('SearchVal1') > 0)
		{
			document.all('SearchVal').value=arrSearchVal[0];
			document.all('SearchVal1').value=arrSearchVal[1];
		}
		else
			document.all('SearchVal').value=searchVal;
		sSearchVal=document.all('SearchVal').value;
	//alert(document.all('SearchCol').options.length)
		for(i=0;i<document.all('SearchCol').options.length;i++)
		{
			//alert(searchCol.toLowerCase() + "--" + document.all('SearchCol').options[i].value.toLowerCase())
			if(searchCol.toLowerCase()==document.all('SearchCol').options[i].value.toLowerCase())	
			{
				document.all('SearchCol').options[i-1].selected=true;
				sSearchCol=document.all('SearchCol').options[i].value.toLowerCase();
				break;
			}
		}		
		sSearchCriteria = document.all('SearchCol').options[i].value + SEPARATOR + document.all('SearchVal').value;		
	}
	else
	{
		//sSearchCriteria = searchCol + SEPARATOR + searchVal;
		//Called from Diary Control to Automate the Search by Clicking on the Calender : kasana
		if(searchCol.toLowerCase() == "t.followupdate")
			sSearchCriteria = searchCol + SEPARATOR + searchVal + '-' + searchVal;
		else	
			sSearchCriteria = searchCol + SEPARATOR + searchVal;
		//Show All Option at Dairy	
		if(searchVal == "")
		{
			sSearchCriteria = searchCol + SEPARATOR + searchVal;
		}
		//EOD FOLLOW_UP_DATE #Itrack 3554
		if(searchVal == "FOLLOW_UP_DATE")
		{
			var d = new Date;
			var day = d.getDate();
			var month = d.getMonth() + 1;
			var year = d.getFullYear();
			if (iLangID == 2)
			    searchVal = day + '/' + month + '/' + year; //
			else 
			    searchVal = month + '/' + day + '/' + year; //
			
			sSearchCriteria = searchCol + SEPARATOR + '' + '-' + searchVal;
		}		
	}
	
	//document.getElementById('tabContent').style.display="none"   
	
	if (document.all('ShowExcluded') && document.all('ShowExcluded').checked) bSx = true;
		else bSx = false;		
		
	//if(typeof myWebService.gridWebService == 'undefined')
	//	myWebService.useService(sSrvURL, "gridWebService");
		
	//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData",sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, PageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor,sCellHorizontalAlign);	
	GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, PageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor,sCellHorizontalAlign);	
}
// write below code in aspx file AD_externalSearch("CL.CLIENTName~R~OR~BI.BrokerName~NEW~", 1);
function AD_externalSearch(searchStr,PageNo,isAdvance) { //public
	sSearchCriteria = ""; 
	stPageNo=PageNo;
	if (searchStr != "")
		sSearchCriteria = searchStr;
	if (PageNo=="" || PageNo==0)
		PageNo=1;
	if (isAdvance=="" || isAdvance=="Y")
	{	
		advanceSearch=advSearch;
		//if(typeof myWebService.gridWebService == 'undefined')
		//	myWebService.useService(sSrvURL, "gridWebService");

		//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData", sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
		GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, pageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
	}
	else
	{
		advanceSearch=advSearch;
		//if(typeof myWebService.gridWebService == 'undefined')
		//	myWebService.useService(sSrvURL, "gridWebService");

		//myWebService.gridWebService.callService(refreshGridResult, "GetSortedData",sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, PageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
		GetSortedDataJS(sQg, sOc, sDg, sPc, sSg, sFg, bMs, sSys, bSx, iPs, PageNo, sSearchCriteria, false, iRn,sDSS,sColType,sPColsName,sColsLink,sDisplayColsName,sSearch,sallowDBLClick,sSearchMessage,ExtraButtons,sCacheSize,ImagePath,colHeader,dispTextLen,HeaderString,"","",bFValue,lookUpDetails,searchType,DropdownCols,Grouping,ColQueryGrouping,requireCheckbox,securityrights,sTotRecords,requireDropdownList,"","",requireNormalCursor, sCellHorizontalAlign);
	}
}

// -->
</script>
<!-- <DIV ID="myWebService" style="BEHAVIOR:url(/cms/cmsweb/htc/webservice.htc)"></DIV> -->
<div id="theResult1"><img id='Loadingimg' src="<%=System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()%>/images/loading.gif" alt="" border="0" /></div>
<asp:Literal id="PublicProps" runat="server"></asp:Literal>
<script type="text/javascript" language="javascript">

var oTimer = setInterval("checkBodyLoad()", 1000);
function checkBodyLoad() {
	//if (document.body && typeof myWebService.useService != 'undefined') {
		
		serviceInitialise();
		clearInterval(oTimer);
	//}
}
if (iLangID == 2)
    document.getElementById('Loadingimg').src = '<%=System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()%>/images/loading.pt-BR.gif'
else
    document.getElementById('Loadingimg').src = '<%=System.Configuration.ConfigurationSettings.AppSettings.Get("RootURL").Trim()%>/images/loading.gif'

document.onkeypress=keyValue;

document.onmousedown= function(e) {
var eventInstance = window.event ? event : e;
if (eventInstance.button == 2) {

      if (iLangID == 2) {
          alert('Botão direito do mouse desabilitado.');
      }
      else {
          alert('Sorry, Right Click Disabled');
      }
     return false;	
   }
}
//-->
</script>
