//Created by Vipul - Apr 3, 2003
//Purpose: To define the common functions for client side checking and visual effects

//Purpose: To change the class on a particular HTML object
//Params: obj -> HTML object where css class can be applied
//Params: clsName -> css class name to be applied

/*
Multilingual Support Added for Currency, Number --Charles (4-Jun-2010)
sCurrencyFormat = 'R$' (Brazil), '$' (US)
sDecimalSep = ',' (Brazil), '.' (US)
sGroupSep = '.' (Brazil), ',' (US)
*/

function f_ChangeClass(obj, clsName) {
	obj.className = clsName;
}

function OpenLookupWindow(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Arg1,Arg2,Arg3,Arg4)
			{
				//window.showModalDialog(Url);
				
				//alert(Url);
				//alert(DataTextFieldID);
				//alert(DataValueFieldID);
				//alert(Url + '?DataTextFieldID=' + DataTextFieldID + '&DataValueFieldID=' + DataValueFieldID, 'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no');
				
				var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
									DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
									'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
									'&Arg1=' + Arg1 + '&Arg2=' + Arg2 + '&Arg3=' + Arg3 + '&Arg4=' + Arg4,
									'review','height=500, width=700,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' );
				
				win.document.title = Title;				
			}
			
//Created by vivek - May 6, 2003
//Purpose: To define the common function for password length checking.
//Purpose: To validate length of userpassword. Length must be of 8 or more characters.
//Params: obj -> HTML object where object is the respective control
//This function returns false and true depending on the result.
function usrPwdLenC(source, arguments)
{
	var usrPwd = arguments.Value;
	if(usrPwd.length < 8) {
		arguments.IsValid = false;
		return;   // invalid userName
	}
}

function businessDescCheck(source, arguments)
{
	var usrPwd = arguments.Value;
	if(usrPwd.length > 500 ) {
		arguments.IsValid = false;
		return;   // invalid userName
	}
}
// The following function added by rajul,as per the new requirement of
// accepting the business desc from the Add client screen upto 1000 chars.
function newbusinessDescCheck(source, arguments)
{
	var usrPwd = arguments.Value;
	if(usrPwd.length > 1000 ) {
		arguments.IsValid = false;
		return;   // invalid userName
	}
}
function trim(inputString) {
   // Removes leading and trailing spaces from the passed string. Also removes
   // consecutive spaces and replaces it with one space. If something besides
   // a string is passed in (null, custom object, etc.) then return the input.
   if (typeof inputString != "string") { return inputString; }
   var retValue = inputString;
   var ch = retValue.substring(0, 1);
   while (ch == " ") { // Check for spaces at the beginning of the string
      retValue = retValue.substring(1, retValue.length);
      ch = retValue.substring(0, 1);
   }
   ch = retValue.substring(retValue.length-1, retValue.length);
   while (ch == " ") { // Check for spaces at the end of the string
      retValue = retValue.substring(0, retValue.length-1);
      ch = retValue.substring(retValue.length-1, retValue.length);
   }
   while (retValue.indexOf("  ") != -1) { // Note that there are two spaces in the string - look for multiple spaces within the string
      retValue = retValue.substring(0, retValue.indexOf("  ")) + retValue.substring(retValue.indexOf("  ")+1, retValue.length); // Again, there are two spaces in each of the strings
   }
   return retValue; // Return the trimmed string back to the user
} // Ends the


/*-----------------------------------------------------------------------------------
 *Added by Amar
 *This function validates the conditions of a validator control and shows its messsage
 ------------------------------------------------------------------------------------*/
function ValidateValidator(validator)
{
	//In Built function of ASP.Net Client Validation which validates the 
	//validation for a validator except CompareValidator
	
	ValidatorValidate(validator);
	
	//After Calling ValidatorValidate on the validator we can check the isvalid property
	if(!validator.isvalid)
		validator.style.display = '';
	else
		validator.style.display = 'None';
}
/*-----------------------------------------------------------------------------------
 *Added by Amar
 *This function checks for the entry of special characters like k,K,m,M and changes
 * k, K to 1000
 * m, M to 1000000
 * and multiplies the actual value with these value
-------------------------------------------------------------------------------------*/
function getActualValue(num)
{
	//var strVal	 = window.event.srcElement.value;
	var strVal = num;
	var unitChar = trim(strVal.toString().substr(strVal.length-1,1));
	var numVal   = trim(strVal.toString().substr(0, strVal.toString().length-1));
	
	if((isNaN(numVal)) || (numVal==''))
		return strVal;
		
	switch(unitChar.toLowerCase())
	{
		case 'k':
			strVal = parseFloat(numVal) * 1000;
			break;
		case 'm':
			strVal = parseFloat(numVal) * 1000000;
			break;
		default:
			strVal = strVal;
			break;
	}	
	return strVal;
}

//This Function is to format the currency
function formatCurrency(num) {

        var regExp = '/\[' + sCurrencyFormat + ']|\\' + sGroupSep + '/g'
        //num = num.toString().replace(/\$|\,/g,'');
        num = num.toString().replace(regExp, '');
		
		if(num==null || num=='')
		{
		 return num;
		}
		
		//Check for [kKmM] entry and change value
		//if(!((window.event.srcElement == null) || (window.event.srcElement == 'undefined'))) 
		num = getActualValue(num);		
		
		if(isNaN(num))
		{
		//num = "0";
		num=trim(num);
		return num;
		}
		else
		{
		sign = (num == (num = Math.abs(num)));
		num = Math.floor(num*100+0.50000000001);
		cents = num%100;
		num = Math.floor(num/100).toString();
		if(cents<10)
		cents = "0" + cents;
		for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
		    num = num.substring(0, num.length - (4 * i + 3)) + sGroupSep /* ',' */ +
		num.substring(num.length-(4*i+3));
		//this.value=num;
			num=trim(num);
			cents=trim(cents);
		return (((sign)?'':'-') +  num + sDecimalSep /* '.' */ + cents);
		}
		
}
function formatCurrencyInteger(num) {

        var regExp = '/\[' + sCurrencyFormat + ']|\\' + sGroupSep + '/g'
        //num = num.toString().replace(/\$|\,/g,'');
        num = num.toString().replace(regExp, '');
		
		if(num==null || num=='')
		{
		 return num;
		}
		
		//Check for [kKmM] entry and change value
		//if(!((window.event.srcElement == null) || (window.event.srcElement == 'undefined'))) 
			num = getActualValue(num);
		
		if(isNaN(num))
		{
		return num;
		}
		else
		{
		sign = (num == (num = Math.abs(num)));
		num = Math.floor(num*100+0.50000000001);
		cents = num%100;
		num = Math.floor(num/100).toString();
		if(cents<10)
		cents = "0" + cents;
		for (var i = 0; i < Math.floor((num.length-(1+i))/3); i++)
		    num = num.substring(0, num.length - (4 * i + 3)) + sGroupSep /* ',' */ +
		num.substring(num.length-(4*i+3));
		//this.value=num;
		num=trim(num);
		return (((sign)?'':'-') +  num);
		}
}

function OpenPopupWindow(Url,DataTextFieldID,DataValueFieldID)
{
	window.open(Url + '?DataTextFieldID=' + DataTextFieldID + '&DataValueFieldID=' + hDataValueID, 'review','height=500,width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' )
}

/*----------------------------------------------------------------------
		Function Name           : ShowPopup(url, winname, width, height) 
		Parameters				: url - the url of the popup window
								  winname - name of the popup window
								  width - width of the popup window
								  height - height of the popup window
		Author                  : Rohit Sinha
		Purpose                 : Loading the popup window in the left of the screen.			
		Creation Date           : 14 March 2003
		------------------------------------------------------------------------*/
		function ShowPopup(url, winname, width, height) 
		{
			var MyURL = url;
			var MyWindowName = winname;
			var MyWidth = width;
			var MyHeight = height;
			var MyScrollBars = 'Yes';
			var MyResizable = 'Yes';
			var MyMenuBar = 'No';
			var MyToolBar = 'No';
			var MyStatusBar = 'No';

			    if (document.all)
			        var xMax = screen.width, yMax = screen.height;
			    else
			        if (document.layers)
			            var xMax = window.outerWidth, yMax = window.outerHeight;
			        else
			            var xMax = 640, yMax=480;

			    var xOffset = (xMax - MyWidth)/2, yOffset = (yMax - MyHeight)/2;

				MyWin = window.open(MyURL,MyWindowName,'width=' + MyWidth + ',height=' + MyHeight + ',screenX= ' + xOffset + ',screenY=' + yOffset + ',top=' + yOffset + ',left=' + xOffset + ',scrollbars=' + MyScrollBars + ',resizable=' + MyResizable + ',menubar=' + MyMenuBar + ',toolbar=' + MyToolBar + ',status=' + MyStatusBar + '' );
				MyWin.focus();
		}
		
//-->
						/*----------------------------------------------------------------------
		Function Name           : changeBtnColor(color) 
		Parameters				: Color - The color to be displayed onMouserOver and onMouseOut on the btn
		Author                  : Balaji. V
		Purpose                 : This function is used for People's Choice.
								  This function is called on the buttons which is found at the bottom of every page
		Creation Date           : 22 March 2002
		Last Modified Date      : 22 March 2002
		------------------------------------------------------------------------*/

		function changeBtnColor(color)
		{
			var el= event.srcElement
			if (el.tagName=="INPUT"&&(el.type=="button" ||el.type=="submit") )
			event.srcElement.style.backgroundColor=color
		}
		
		/*------------------------------------------------------------------
			The following functions are used in Date validation 
		Author                  : MPhate  
		Purpose                 : This function is used Date validation.								  
		Creation Date           : 30 April 2003
		Last Modified Date      : 30 April 2003								*/
		
		var sznotValid = true;
		function FormatDateEx(szFormName,szFieldName,szDateFormat)
		{ 	
		  	var strDate;
			var strDay;
			var strMonth;
			var strYear;
			var objVal;
			var dtDSep;
			//var szDateFormat = "E";
			ObjVal =eval('document.'+szFormName+'.'+szFieldName);
			dtDateValue = ObjVal.value;
			if (dtDateValue=="")
			{
			   return true;
			}

	   		dtDSep = GetDateSeparator(dtDateValue);

	   		if(dtDSep == "")
	   		{
	   			dtDSep = "/";
	   		}
	   		else if(dtDSep == " ")
	   		{
	   			dtDSep = "/"
	   			dtDateValue = ReplaceString(dtDateValue," ", "/")
	   		}
            
			if (IsProperDate(ObjVal,szDateFormat)==true)
			{
				// After coming out of 'IsProperDate' function if the date value contains '/' then 
				// it means that the value is of correct format. So this value is returned
					if (FindChar(dtDateValue,dtDSep)==true)
					{
					  strDate = dtDateValue;
					}
					else if(dtDateValue.length == 8)
					{	
						if(szDateFormat== 'US')	//Country is US
						{
							strMonth = dtDateValue.charAt(0)+dtDateValue.charAt(1)
							strDay = dtDateValue.charAt(2)+dtDateValue.charAt(3)
							strYear  = dtDateValue.charAt(4)+dtDateValue.charAt(5)+dtDateValue.charAt(6)+dtDateValue.charAt(7)
							
							strDate  = strMonth + dtDSep + strDay + dtDSep + strYear
						}
						else
						{
							strDay   = dtDateValue.charAt(0)+dtDateValue.charAt(1)
							strMonth = dtDateValue.charAt(2)+dtDateValue.charAt(3)
							strYear  = dtDateValue.charAt(4)+dtDateValue.charAt(5)+dtDateValue.charAt(6)+dtDateValue.charAt(7)
							strDate  = strDay + dtDSep + strMonth + dtDSep + strYear
							
						}
					}
				else if (dtDateValue.length == 6)
				{
					//strDate  = strDay +"/"+ strMonth +"/"+ strYear
					
					if(szDateFormat == 'US')	//Country is US
					{
						strMonth = "0"+ dtDateValue.charAt(0)
						strDay = "0" + dtDateValue.charAt(1)
						strYear  = dtDateValue.charAt(2)+dtDateValue.charAt(3)+dtDateValue.charAt(4)+dtDateValue.charAt(5)
						strDate  = strMonth + dtDSep + strDay + dtDSep + strYear
					}
					else
					{
						strDay = "0"+ dtDateValue.charAt(0)
						strMonth = "0" + dtDateValue.charAt(1)
						strYear  = dtDateValue.charAt(2)+dtDateValue.charAt(3)+dtDateValue.charAt(4)+dtDateValue.charAt(5)
						strDate  = strDay + dtDSep + strMonth + dtDSep + strYear
					}
				 
				}
			    ObjVal.value=strDate;
			    //objArgs.IsValid = true;			    
			    return true;
			}
			else
			{  
			    sznotValid = false;
				ObjVal.focus();				
				return false;
			}
		}
		
		/**********************************************************
		Function Name	GetDateSeparator
		Input			Date value
		Creation Date	02/18/02
		Purpose			To extract the separator of the date
		Return Value	Date Separator		
		***********************************************************/
		function GetDateSeparator(dtDateValue)
		{
			var retDateSeparator="";			
			var strSeparatorArray = new Array("-"," ","/",".");
			
			for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++)			
			{
				var separator = dtDateValue.indexOf(strSeparatorArray[intElementNr]);
				if ( separator!= -1)
				{
					retDateSeparator = dtDateValue.charAt(separator);
					break;
				}
			}
			return retDateSeparator;
		}
		// End of function
		
		function IsProperDate(ctr,strDateFormat)
	   	{ 
	   	    var varErrorMsg;
	   	    
	   		//var strDateFormat = "E";
	   		var intDateValueLength = ctr.value.length;
	   		var dtDSep;
	   		var	dtDateValue = ctr.value;
	   		var boolget = true;
			//var nummonth="0";

	   		
	   		/*********
	   		Extracting the separator
	   		If the separator is not present the default separator would be "/"
	   		*********/
	   		dtDSep = GetDateSeparator(dtDateValue);
	   		if(dtDSep =="")
	   			dtDSep = "/"
	   		//*********


	   		// Displaying of Date Validation Message according to the Country
	   		// If any other Country needs to be added just add another case
	   		switch(strDateFormat)
	   		{
	   			case 'US': varErrorMsg = "Enter Date in (MM" + dtDSep + "DD" + dtDSep + "YYYY) or (M" + dtDSep + "D" + dtDSep + "YYYY) or (MMDDYYYY) or (MDYYYY) format";
	   						break;
	   			case 'UK': varErrorMsg = "Enter Date in (DD" + dtDSep + "MM" + dtDSep + "YYYY) or (D" + dtDSep + "M" + dtDSep + "YYYY) or (DDMMYYYY) or (DMYYYY) format";
	   						break;
	   			default	: dtDSep = "/"
	   					  varErrorMsg = "Enter Date in (DD" + dtDSep + "MM" + dtDSep + "YYYY) or (D" + dtDSep + "M" + dtDSep + "YYYY) or (DDMMYYYY) or (DMYYYY) format";				
	   						break;
	   			
	   		}
			// End of Validation Message			
			
			// Here we are assuming that for all the countries maximum date length will be 10
			// So no need of chaning this according to Country
			if (intDateValueLength > 10)
			{
			     //alert("Date length cannot exceed 10 characters");
			     ctr.focus()
			     return false;
			}
					     
			if(intDateValueLength!=10 && intDateValueLength!=9 && intDateValueLength!=8 && intDateValueLength!=6)
			{
			   //alert(varErrorMsg);
			   ctr.focus()
			   return false;
			}			
			
			if(intDateValueLength==10)
			{
				if ((dtDateValue.charAt(2) != dtDSep) || (dtDateValue.charAt(5) != dtDSep))
		 		{
					//alert(varErrorMsg);
					ctr.focus()
					return false;
	}
	            //Added for Multilingual Support
	            if (sCultureDateFormat == 'DD/MM/YYYY') {

	                numday = dtDateValue.charAt(0) + dtDateValue.charAt(1);
	                nummonth = dtDateValue.charAt(3) + dtDateValue.charAt(4);
	                numyear = dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8) + dtDateValue.charAt(9);
	            }
	            else {
	                nummonth = dtDateValue.charAt(0) + dtDateValue.charAt(1)
	                numday = dtDateValue.charAt(3) + dtDateValue.charAt(4)
	                numyear = dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8) + dtDateValue.charAt(9)
	            }
			}
			
			if(ctr.value.length==8)
			{
				if ((dtDateValue.charAt(1) != dtDSep) || (dtDateValue.charAt(3) != dtDSep))
				{
				   if (FindChar(dtDateValue,dtDSep)==true)
				   {
					//alert(varErrorMsg);
					ctr.focus()
					return false;
				   }
				   else {
				       //Added for Multilingual Support
				       if (sCultureDateFormat == 'DD/MM/YYYY') {
				           numday = dtDateValue.charAt(0) + dtDateValue.charAt(1);
				           nummonth = dtDateValue.charAt(2) + dtDateValue.charAt(3);
				           numyear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
				       }
				       else {
				           nummonth = dtDateValue.charAt(0) + dtDateValue.charAt(1)
				           numday = dtDateValue.charAt(2) + dtDateValue.charAt(3)
				           numyear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7)
				       }
				   }
				}
				else {
				//Added for Multilingual Support
				    if (sCultureDateFormat == 'DD/MM/YYYY') {

				        numday = dtDateValue.charAt(0);
				        nummonth = dtDateValue.charAt(2);
				        numyear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
				    }
				    else {
				        nummonth = dtDateValue.charAt(0)
				        numday = dtDateValue.charAt(2)
				        numyear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7)
				    }
				}
			}
			if(dtDateValue.length==9)
			{
				if (((dtDateValue.charAt(1) != dtDSep) || (dtDateValue.charAt(4) != dtDSep)) &&((dtDateValue.charAt(2) != dtDSep) || (dtDateValue.charAt(4) != dtDSep)))
				{
				   //alert(varErrorMsg);
				   ctr.focus()
				   return false;
				}
				if (dtDateValue.charAt(1)== dtDSep) {
				 //Added for Multilingual Support
				    if (sCultureDateFormat == 'DD/MM/YYYY') {
				        numday = dtDateValue.charAt(0);
				        nummonth = dtDateValue.charAt(2) + dtDateValue.charAt(3);
				        numyear = dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8);
				    }
				    else {
				        nummonth = dtDateValue.charAt(0)
				        numday = dtDateValue.charAt(2) + dtDateValue.charAt(3)
				        numyear = dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8)
				    }
				}
				else {
				 //Added for Multilingual Support
				    if (sCultureDateFormat == 'DD/MM/YYYY') {
				        numday = dtDateValue.charAt(0) + dtDateValue.charAt(1);
				        nummonth = dtDateValue.charAt(3);
				        numyear = dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8);
				    }
				    else {
				        nummonth = dtDateValue.charAt(0) + dtDateValue.charAt(1)
				        numday = dtDateValue.charAt(3)
				        numyear = dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8)
				    }
				}
			}	
	 
			if(dtDateValue.length==6) {
			//Added for Multilingual Support
			    if (sCultureDateFormat == 'DD/MM/YYYY') {
			        numday = dtDateValue.charAt(0);
			        nummonth = dtDateValue.charAt(1);
			        numyear = dtDateValue.charAt(2) + dtDateValue.charAt(3) + dtDateValue.charAt(4) + dtDateValue.charAt(5);
			    }
			    else {
			        nummonth = dtDateValue.charAt(0)
			        numday = dtDateValue.charAt(1)
			        numyear = dtDateValue.charAt(2) + dtDateValue.charAt(3) + dtDateValue.charAt(4) + dtDateValue.charAt(5)
			    }
						
			}
			
			//swaping based on country
			// Here we are swapping the fields based on the country
			// If different date format comes than we need to manipulate the swapping of MM DD YYYY accordingly
			if(strDateFormat == 'UK')
			{
				tempVar = nummonth;
				nummonth = numday;
				numday = tempVar;
			}
			//End swaping
			
			
			if (numyear.length < 4)
			{
				//alert("Year format is YYYY")
				return false;
			}
						 
			if (!IsVal("Day value in date",numday))
			{
				return false;
			}
			if (!IsVal("Month value in date",nummonth))
			{
				return false;
			}
			if (!IsVal("Year value in date",numyear)) 
			{
				return false;
			}

			day = parseInt(numday,10)
			month = parseInt(nummonth,10)
			year = parseInt(numyear,10)
			if (isNaN(day) || isNaN(month) || isNaN(year))
			{
				//alert("Invalid Date");
				return false;
			}
			if ((day < 0) || (month < 0) || (year < 0))
			{
				//alert("Invalid character in Date");
				return false;
			}
			if ((day == 0) || (month == 0) || (year == 0))
			{
				//alert("Invalid Date");
				return false;
			}
			if (month > 12)
			{
			   //alert("Month cannot be greater than 12");
			   return false;
			}

			if (day > 31 )
			{
			     //alert("Day cannot be greater than 31")
			     return false;
			}
			if ((month==4)||(month==6)||(month==9)||(month==11))
			{
				if (day > 30 ) 
			    {
			        //alert("Day cannot be greater than 30")
			        return false;
			    }
			}
			if (month==2)
			{
				if  ((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0) ) ) 
			    {
					if (day > 29)
			        {
						//alert("Day cannot be greater than 29 for a Leap Year")
			            return false;
			        }        
						               
			    }  
			    else     
			    {
			        if (day > 28)
			        {
						//alert("Day cannot be greater than 28 for a non-leap year")
			            return false;
			        } 
			    }    
			}  
			if (numyear < 1900)
			{
				//alert("Year cannot be less than 1900.");
				return false;
			}

			return true;
		}
    
    
		function IsDateValid(strDate, dateFormat, dateSeparator)
	   	{
	   		var strDateFormat = "DD/MM/YYYY";
	   		var varErrorMsg;
	   		var intDateValueLength = strDate.length;
	   		var dtDSep="/";
	   		var	dtDateValue = strDate;
	   		var boolget = true;
			var Feb	=2;
			var Apr	=4;
			var Jun	=6;
			var Sep	=9;
			var Nov	=11;
			var Dec	=12;
			var IsMMDDYYYYFormat=false;
	   		
	   		if((dateFormat != null) && (dateFormat != 'undefined'))
	   			strDateFormat = dateFormat.toString().toUpperCase();
	   		
	   		if((dateSeparator != null) && (dateSeparator != 'undefined'))
	   			dtDSep = dateSeparator.toString();
	   		
	   		IsMMDDYYYYFormat = (strDateFormat.toString().indexOf('MM')==0);
	   				
	   		// Here we are assuming that for all the countries maximum date length will be 10
			if (intDateValueLength > 10)
			     return false;
					     
			if(intDateValueLength!=10 && intDateValueLength!=9 && intDateValueLength!=8 && intDateValueLength!=6)
			   return false;
			
			if(intDateValueLength==10)
			{
				if ((dtDateValue.charAt(2) != dtDSep) || (dtDateValue.charAt(5) != dtDSep))
		 			return false;
			
				nummonth = dtDateValue.toString().substr(strDateFormat.toString().indexOf('MM')		, 2);
				numday   = dtDateValue.toString().substr(strDateFormat.toString().indexOf('DD')		, 2);
				numyear  = dtDateValue.toString().substr(strDateFormat.toString().indexOf('YYYY')	, 4);
			}
			
			if(dtDateValue.length==8)
			{
				if ((dtDateValue.charAt(1) != dtDSep) || (dtDateValue.charAt(3) != dtDSep))
				{
					if (FindChar(dtDateValue,dtDSep)==true)
				   			return false;
					else
					{
						if(IsMMDDYYYYFormat)
						{
							nummonth = dtDateValue.toString().substr(0, 1);
							numday   = dtDateValue.toString().substr(2, 1);
							numyear  = dtDateValue.toString().substr(4, 4);
						}
						else
						{
							nummonth = dtDateValue.toString().substr(2, 1);
							numday   = dtDateValue.toString().substr(0, 1);
							numyear  = dtDateValue.toString().substr(4, 4);
						}
					}
				}
				else
				{
					if(IsMMDDYYYYFormat)
					{
						nummonth = dtDateValue.toString().substr(0, 1);
						numday   = dtDateValue.toString().substr(2, 1);
						numyear  = dtDateValue.toString().substr(4, 4);
					}
					else
					{
						nummonth = dtDateValue.toString().substr(2, 1);
						numday   = dtDateValue.toString().substr(0, 1);
						numyear  = dtDateValue.toString().substr(4, 4);
					}
				}
			}
			if(dtDateValue.length==9)
			{
				if (((dtDateValue.charAt(1) != dtDSep) || (dtDateValue.charAt(4) != dtDSep)) &&((dtDateValue.charAt(2) != dtDSep) || (dtDateValue.charAt(4) != dtDSep)))
				   return false;
				
				if (dtDateValue.charAt(1)== dtDSep) 
				{
					if(IsMMDDYYYYFormat) //MM/DD/YYYY
					{
						nummonth = dtDateValue.toString().substr(0, 1);
						numday   = dtDateValue.toString().substr(2, 2);
						numyear  = dtDateValue.toString().substr(5, 4);
					}
					else  //DD/YY/YYYY
					{
						nummonth = dtDateValue.toString().substr(2, 2);
						numday   = dtDateValue.toString().substr(0, 1);
						numyear  = dtDateValue.toString().substr(5, 4);
					}
				}
				else
				{
					if(IsMMDDYYYYFormat) //MM/DD/YYYY
					{
						nummonth = dtDateValue.toString().substr(0, 2);
						numday   = dtDateValue.toString().substr(3, 1);
						numyear  = dtDateValue.toString().substr(5, 4);
					}
					else  //DD/YY/YYYY
					{
						nummonth = dtDateValue.toString().substr(3, 1);
						numday   = dtDateValue.toString().substr(0, 2);
						numyear  = dtDateValue.toString().substr(5, 4);
					}
				}
			}	
	 
			if(dtDateValue.length==6)
			{
				if(IsMMDDYYYYFormat)
				{
					nummonth = dtDateValue.toString().substr(0, 1);
					numday   = dtDateValue.toString().substr(2, 1);
					numyear  = dtDateValue.toString().substr(4, 2);
				}
				else
				{
					nummonth = dtDateValue.toString().substr(2, 1);
					numday   = dtDateValue.toString().substr(0, 1);
					numyear  = dtDateValue.toString().substr(4, 2);
				}
			}
			
			if (numyear.length < 4)
				return false;
						 
			if (!IsVal("Day value in date",numday))
				return false;
			
			if (!IsVal("Month value in date",nummonth))
				return false;
			
			if (!IsVal("Year value in date",numyear)) 
				return false;
			
			day		= parseInt(numday,10)
			month	= parseInt(nummonth,10)
			year	= parseInt(numyear,10)
			
			if (isNaN(day) || isNaN(month) || isNaN(year))
				return false;
			
			if ((day < 0) || (month < 0) || (year < 0))
				return false;
			
			if ((day == 0) || (month == 0) || (year == 0))
				return false;
			
			if (month > Dec)
			   return false;
			
			if (day > 31)
			     return false;
			
			
			
			if ((month==Apr)||(month==Jun)||(month==Sep)||(month==Nov))
				if (day > 30 ) 
			        return false;
			
			
			//Leap Year Validation
			if (month==Feb)
			{
				var isLeapYear = false;
					isLeapYear = (parseInt(isleapYear(year))==1);
					
				if(isLeapYear)
				{
					if(parseInt(day)>29)
						return false;
				}
				else
				{
					if(parseInt(day)>28)
						return false;
				}
			}  
			
			
			if (numyear < 1900)
				return false;
			

			return true;
		}
		
	function FindChar(strValue,strChar)
    {
		for(i=0;i<strValue.length;i++)
		{
			if (strValue.charAt(i)==strChar)
			{
				return true;
			}
		}
		return false;
    }
    function ReplaceString(lstrString, lcharReplaceChar, lstrReplaceWith)
{
	if (lstrString == "" || lstrString == null)
	{
		return ""
	}
	else
	{
		var lintcounter, lintcurrLoc, retText, lintStringLength
		retText = ""
		for (lintcounter =0 ; lintcounter < lstrString.length; lintcounter++)
			if (lstrString.charAt(lintcounter) == lcharReplaceChar)
				retText = retText + lstrReplaceWith;
			else
				retText = retText + lstrString.charAt(lintcounter)
		return retText;
	}
}
function IsVal(msg,val)	
{
if (isNaN(val))
	{
		var write
		write = msg + " has to be Numeric"
		//alert(write)
		return false
	}
return true
}
	function HelpReset()
	{

	 TotalCollection = document.getElementsByTagName('SPAN');

		for(i=0; i<TotalCollection.length; i++)
		{

			itemAttribute = document.getElementsByTagName('SPAN')[i].getAttribute("CONTROLTOVALIDATE");

			if(itemAttribute)
				document.getElementsByTagName('SPAN')[i].style.visibility = "HIDDEN";

		}
	}
	
////Function used in the Add New Quote And Add New Policy for Calculation of Expiration Date
// Check whether string s is empty.
	// returns true if the string is empty
	function isEmpty(StringToCheck)
	{   
			
		return ((StringToCheck == null) || (StringToCheck.length == 0))
	}
	// Returns true if string s is empty or 
	// whitespace characters only.
	function isWhitespace (StringToCheck)
	{
		var reWhitespace = /^\s+$/
		return (isEmpty(StringToCheck) || reWhitespace.test(StringToCheck));
	}
	
		function ReplaceString(lstrString, lcharReplaceChar, lstrReplaceWith)
	{
		if (lstrString == "" || lstrString == null)
		{
			return ""
		}
		else
		{
			var lintcounter, lintcurrLoc, retText, lintStringLength
			retText = ""
			for (lintcounter =0 ; lintcounter < lstrString.length; lintcounter++)
				if (lstrString.charAt(lintcounter) == lcharReplaceChar)
					retText = retText + lstrReplaceWith;
				else
					retText = retText + lstrString.charAt(lintcounter)
			return retText;
		}
	}

		//Function Name      : ReplaceDateSeparator
		//Parameters		   : Date Value
		//Author             : Balaji. V
		//Purpose            : To Replace the Date Separator to "/", to use the new Date() constructor in Javascript
		//Creation Date      : 25/Feb/2002
		//Last Modified Date : 
		//---------------------------------------------------------------------------------------------------------------*/
		function ReplaceDateSeparator(dtDateValue)
		{
			var dtDSep;
	   		dtDSep = GetDateSeparator(dtDateValue);
	   		if(dtDSep =="")
	   			dtDSep = "/"
	   		else(dtDSep ==" ")
	   		{
	   			dtDSep = "/"
	   			dtDateValue = ReplaceString(dtDateValue," ", "")
	   		}
			retDateValue = dtDateValue;
			retDateValue = ReplaceString(dtDateValue, dtDSep, "/");
			return retDateValue;
		}

			// Returns the string after triming it.
			function trimTheString(stringToTrim)
			{
			
				var flag = true;
				var i = 0;
				if (isWhitespace(stringToTrim)==true)
					return "";
				while ((i < stringToTrim.length) && (flag)) {
					retChar = stringToTrim.charAt(i++);
					if (retChar != " ") flag = false;
				}
				if (flag) return "";
				var j = stringToTrim.length-1;
				flag = true;
				while ((j >= 0) && (flag)) {
					retChar = stringToTrim.charAt(j--);
					if (retChar != " ") flag = false;
				}
				if (flag) return "";
				stringToTrim = stringToTrim.substring(i-1,j+2);
			return stringToTrim;
			}

		/*-------------------------------------------------------------------------------------------------------------
		Function Name      : BuildDateForCnty
		Parameters		   : Date Object, Format for which country
		Author             : 
		Purpose            : To Change the date value to the country specific, so that it will be use in new Date() Contructor in Javascript
		Creation Date      : 
		Last Modified Date :
		---------------------------------------------------------------------------------------------------------------*/
		function BuildDateForCnty(dtObj, FormatFor)
		{
			var varErrorMsg;
			var dtDSep;
			var intDateValueLength = dtObj.value.length;
			var dtDateValue = dtObj.value;
			var strDate = "";
			var nummonth; // Added By Namit on 20/1/2003 To Remove NumMonth Problem
			var numday;   // Added By Namit on 20/1/2003 To Remove NumMonth Problem
			var numyear;  // Added By Namit on 20/1/2003 To Remove NumMonth Problem
			if (trimTheString(dtDateValue) == "")
				return true;
			
			/*********
			Extracting the separator
			*********/
	   		dtDSep = GetDateSeparator(dtDateValue);
	   		if(dtDSep =="")
	   			dtDSep = "/"
	   		else(dtDSep ==" ")
	   		{
	   			dtDSep = "/"
	   			dtDateValue = ReplaceString(dtDateValue," ", "")
	   		}
			//*********
					
			if(intDateValueLength==10)
			{
				nummonth = dtDateValue.charAt(0)+dtDateValue.charAt(1)
				numday   = dtDateValue.charAt(3)+dtDateValue.charAt(4)
				numyear  = dtDateValue.charAt(6)+dtDateValue.charAt(7) + dtDateValue.charAt(8)+dtDateValue.charAt(9)
			}
					
			if(intDateValueLength==8)
			{
				if ((dtDateValue.charAt(1) != dtDSep) || (dtDateValue.charAt(3) != dtDSep))
				{
					nummonth = dtDateValue.charAt(0)+dtDateValue.charAt(1)
					numday   = dtDateValue.charAt(2)+dtDateValue.charAt(3)
					numyear  = dtDateValue.charAt(4)+dtDateValue.charAt(5) + dtDateValue.charAt(6)+dtDateValue.charAt(7)
				}
				else
				{
					nummonth = dtDateValue.charAt(0)
					numday   = dtDateValue.charAt(2)
					numyear  = dtDateValue.charAt(4)+dtDateValue.charAt(5) + dtDateValue.charAt(6)+dtDateValue.charAt(7)
				}
			}
			if(intDateValueLength==9)
			{
				if (dtDateValue.charAt(1)== dtDSep) 
				{
					nummonth = dtDateValue.charAt(0)
					numday   = dtDateValue.charAt(2)+dtDateValue.charAt(3)
					numyear  = dtDateValue.charAt(5) + dtDateValue.charAt(6)+dtDateValue.charAt(7)+ dtDateValue.charAt(8)
				}
				else
				{
					nummonth = dtDateValue.charAt(0)+dtDateValue.charAt(1)
					numday   = dtDateValue.charAt(3)
					numyear  = dtDateValue.charAt(5) + dtDateValue.charAt(6)+dtDateValue.charAt(7)+ dtDateValue.charAt(8)
				}
			}	

			if(intDateValueLength==6)
			{
				nummonth = dtDateValue.charAt(0)
				numday   = dtDateValue.charAt(1)
				numyear  = dtDateValue.charAt(2)+dtDateValue.charAt(3) + dtDateValue.charAt(4)+dtDateValue.charAt(5)
								
			}
					
			//swaping based on country
			// Here we are swapping the fields based on the country
			// If different date format comes than we need to manipulate the swapping of MM DD YYYY accordingly
			if(FormatFor == 'UK')
			{
				tempVar = nummonth;
				nummonth = numday;
				numday = tempVar;
			}
			strDate = nummonth + "/" + numday + "/" + numyear
			return strDate;
		}
		//End Function

	// This function accepts a string and returns the date in the format specified in the web.config file.
		function GetFormattedDate(lDtVar,RequestedFormat,aAppWebDtFormat,aAppWebDtSep)
		{	
			
				var lStrNewDt;
				
				if (aAppWebDtSep!="/")
				{
					aAppWebDtFormat.replace(aAppWebDtSep,"/");
				}
				if (aAppWebDtFormat.toLowerCase()==RequestedFormat.toLowerCase())
				{
					// do nothing
					return lDtVar;
				}
				else
				{
					//that means given date is of opposite format and need conversion in requested format 
					if(RequestedFormat.toLowerCase()=="dd/mm/yyyy")
					{
				
						var lIntVarD=lDtVar.indexOf("/");
						var lStrMonth =lDtVar.substring(0,lIntVarD);
						lDtVar=lDtVar.substring(lIntVarD+1);
						var lIntVarM=lDtVar.indexOf("/");
						var lStrDay=lDtVar.substring(0,lIntVarM);
						var lStrYear=lDtVar.substring(lIntVarM+1);
						if(lStrDay.Length==1)
						{
							lStrDay="0" + lStrDay;
						}
						if(lStrMonth.Length==1)
						{
							lStrMonth="0" + lStrMonth;
						}
						lStrNewDt= lStrDay + "/" +  lStrMonth + "/" + lStrYear;					
					}
					else
					{
				
						var lIntVarD=lDtVar.indexOf("/");
						var lStrDay=lDtVar.substring(0,lIntVarD);
						lDtVar=lDtVar.substring(lIntVarD+1);
						var lIntVarM=lDtVar.indexOf("/");
						var lStrMonth=lDtVar.substring(0,lIntVarM);
						var lStrYear=lDtVar.substring(lIntVarM+1);
						if(lStrDay.Length==1)
						{
							lStrDay="0" + lStrDay;
						}
						if(lStrMonth.Length==1)
						{
							lStrMonth="0" + lStrMonth;
						}
						lStrNewDt=lStrMonth + "/" + lStrDay + "/" + lStrYear;				
					}
				}
				return lStrNewDt;		 			
		}	
		
		function DateComparer(DateFirst, DateSec, FormatOfComparision)
		{

			var saperator = '/';
			var firstDate, secDate;

			var strDateFirst = DateFirst.split("/");
			var strDateSec = DateSec.split("/");

			if(FormatOfComparision.toLowerCase() == "dd/mm/yyyy")
			{
				//alert("dd/mm/yyyy")
				firstDate = (strDateFirst[1].length != 2 ? '0' + strDateFirst[1] : strDateFirst[1]) + '/' + (strDateFirst[0].length != 2 ? '0' + strDateFirst[0] : strDateFirst[0])  + '/' + (strDateFirst[2].length != 4 ? '0' + strDateFirst[2] : strDateFirst[2]);
				secDate = (strDateSec[1].length != 2 ? '0' + strDateSec[1] : strDateSec[1]) + '/' + (strDateSec[0].length != 2 ? '0' + strDateSec[0] : strDateSec[0])  + '/' + (strDateSec[2].length != 4 ? '0' + strDateSec[2] : strDateSec[2]);
			}

			if(FormatOfComparision.toLowerCase() == "mm/dd/yyyy")
			{
				//alert("mm/dd/yyyy")
				firstDate = DateFirst
				secDate = DateSec;
			}

			firstDate = new Date(firstDate);
			secDate = new Date(secDate);

			firstSpan = Date.parse(firstDate);
			secSpan = Date.parse(secDate);

			if(firstSpan > secSpan) 
				return true;	// first is greater
			else 
				return false;	// secound is greater
		}
//By adding this line back functionality of the browser is disabled
// Commented by Skumar for testing purpose 0n 13th Sep 2003.
javascript:window.history.forward(1);
// The following functions added By Rajul,for checking whether the given year is a Leap Year or not.
// Return 1 if the year is a leap year else return 0
function checkYear(tempYear) { 
return (((tempYear % 4 == 0) && (tempYear % 100 != 0)) || (tempYear % 400 == 0)) ? 1 : 0;
}
function isleapYear(tempYear) {
var Check1 = parseFloat(tempYear);
checkYear(tempYear);
if (!checkYear(tempYear)) 
return 0;
else return 1;
}
// end of leap year functions

//By Rajiv
// Below function is added for the Time validation

function IsValidTime(timeStr) {
	// Checks if time is in HH:MM:SS AM/PM format.
	// The seconds and AM/PM are optional.

	var timePat = /^(\d{1,2}):(\d{2})(:(\d{2}))?(\s?(AM|am|PM|pm))?$/;

	var matchArray = timeStr.match(timePat);
	if (matchArray == null)
	{
		//alert("Time is not in a valid format.");
		return false;
	}
	
	hour = matchArray[1];
	minute = matchArray[2];
	second = matchArray[4];
	ampm = matchArray[6];

	if (second=="") { second = null; }
	if (ampm=="") { ampm = null }

	if (hour < 0  || hour > 23) 
	{
		//alert("Hour must be between 1 and 12. (or 0 and 23 for military time)");
		return false;
	}
	
	/*if (hour <= 12 && ampm == null) 
	{
		if (confirm("Please indicate which time format you are using.  OK = Standard Time, CANCEL = Military Time")) {
			alert("You must specify AM or PM.");
			return false;
		}
	}*/
	
	if  (hour > 12 && ampm != null) 
	{
		//alert("You can't specify AM or PM for military time.");
		return false;
	}
	
	if (minute<0 || minute > 59) 
	{
		//alert ("Minute must be between 0 and 59.");
		return false;
	}
	
	if (second != null && (second < 0 || second > 59)) 
	{
		//alert ("Second must be between 0 and 59.");
		return false;
	}
	
	return true;
}
// end Rajiv