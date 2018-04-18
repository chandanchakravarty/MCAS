/*Class Definition for Parameter*/

function Parameter(Key,Value) 
{
	this.Key=Key;
	this.Value=Value;
}
	
	
function _CreateXMLHTTPObject()
{
	var objRequest=null;
	try
	{
		var objBrowser = navigator.appName;
		if(objBrowser == "Microsoft Internet Explorer")
		{
			objRequest = new ActiveXObject("Microsoft.XMLHTTP");
		}
		else
		{
			objRequest = new XMLHttpRequest();
		}
		return objRequest;
	}
	catch(e)
	{
		alert("Yor browser does not support XMLHTTP please upgrade your browser");
	}
}

function _SendAJAXRequest(objRequest,Action,Parameters,RequestHandeler)
{
	var strAmp ="&";
	var QueryString= "";
	var arrLen = Parameters.length; 	
	for(i=0;i<arrLen;i++)
	{	
		objParam = Parameters.pop() 
		
		with (objParam)  QueryString =QueryString + strAmp + Key + "=" + Value;
	}
	
	strUrl = '/cms/cmsweb/aspx/AJAXRequestHandler.aspx?ACTION='+Action + QueryString;
	objRequest.open('get', strUrl,true);
	objRequest.onreadystatechange = function()
		{
			if(objRequest.readyState == 4)
			{
				var response = objRequest.responseText;
				RequestHandeler(response);
			}	
		}
	objRequest.send(null);

}
function _SendAJAXRequestForLogOut(objRequest,Action,RequestHandeler)
{
	/*The choice to use a synchronous call is
	deliberate and important. A synchronous request will stall the browser until it
	gets a reply back from the server. If we were to initiate an asynchronous
	request the request would be made but the browser would continue the unload
	event and chances are the browser would never get the response back from the
	server.: Praveen*/
	
	strUrl = '/cms/cmsweb/aspx/AJAXRequestHandler.aspx?ACTION='+Action;
	objRequest.open('get', strUrl,false);
	objRequest.onreadystatechange = function()
		{
			if(objRequest.readyState == 4)
			{
				var response = objRequest.responseText;
				RequestHandeler(response);
			}	
		}
	objRequest.send(null);

}
		
