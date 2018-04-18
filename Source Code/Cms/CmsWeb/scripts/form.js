//by Pravesh to get Browser info
function Browser() {

    var ua, s, i;

    this.isIE = false;  // Internet Explorer
    this.isOP = false;  // Opera
    this.isNS = false;  // Netscape
    this.version = null;

    ua = navigator.userAgent;

    s = "Opera";
    if ((i = ua.indexOf(s)) >= 0) {
        this.isOP = true;
        this.version = parseFloat(ua.substr(i + s.length));
        return;
    }

    s = "Netscape6/";
    if ((i = ua.indexOf(s)) >= 0) {
        this.isNS = true;
        this.version = parseFloat(ua.substr(i + s.length));
        return;
    }

    // Treat any other "Gecko" browser as Netscape 6.1.

    s = "Gecko";
    if ((i = ua.indexOf(s)) >= 0) {
        this.isNS = true;
        this.version = 6.1;
        return;
    }

    s = "MSIE";
    if ((i = ua.indexOf(s))) {
        this.isIE = true;
        this.version = parseFloat(ua.substr(i + s.length));
        return;
    }
}

var browser = new Browser();
/*
Added by Charles for Multilingual Support
iLangID = 1 : en-US
iLangID = 2 : pt-BR

sCultureDateFormat 
DD/MM/YYYY
MM/DD/YYYY

Multilingual Support Added for Currency, Number --Charles (4-Jun-2010)
sCurrencyFormat = 'R$' (Brazil), '$' (US)
sDecimalSep = ',' (Brazil), '.' (US)
sGroupSep = '.' (Brazil), ',' (US)
*/

var isPageValid_ToSubmit = true;
var IsChanged = false;
function ParseFloatEx(SourceValue)
{
	return parseFloat(ReplaceAll(SourceValue , ',' ,''));
}

function CustomError(number,description) {
    this.number = number
    this.description = description
}
function parseNumber(str) {
  if (str==void 0) return Number.NaN
  if (!isNaN(+str)) return +str
  var save = str, pref = /^[^0-9]+/, suff = /[^0-9]+$/, prefix = pref.exec(str), suffix = suff.exec(str), fac = 1;
  if (suffix) {
   if (suffix[0].match(/bi?l?l?i?o?ns?/)) fac = 1000000000;
    else if (suffix[0].match(/mi?l?l?i?o?ns?/)) fac = 1000000;
    else if (suffix[0].match(/tho?u?s?a?n?d?s?/)) fac = 1000;
    else if (suffix[0].match(/hund/)) fac = 100;
  }
  str=str.match(/[0-9,.]+/)||''
  if (str!='') {
    str=str[0].replace(/,/,'')
    str*=fac
    str.prefix = prefix
    str.suffix = suffix
  }
  return str||Number.NaN
}
RegExp.prototype.toString = function () {return this.source}
RegExp.EMAIL = /^([\w-_]+\.)*[\w-_]+\@([\w-_]+\.)+[a-zA-Z]{2,12}$/;
RegExp.URL = /((\w+):\/\/)?(\w+\.+\w+(\.\w+)*)(\/(\S*))?/;
RegExp.NOT_VALID_TELEPHONE = /[^0-9\s\.\(\)\+\/x(ext)(xtn)]/g;
RegExp.COUNT_DIGITS = /(\d)/g;
String.prototype.isValidTelNo = function () {
  var v = this.match(RegExp.COUNT_DIGITS)
  if (v&&v.length>6&&!this.match(RegExp.NOT_VALID_TELEPHONE)) return true
  return new CustomError(0,'\''+this+'\' is not a valid telephone number')
}
String.prototype.isEmpty = function () {
  if (!this.length) return true
  if (this.match(/^\s+$/)) return true
  return false
}
Array.prototype.stripRepeats = function () {
  this.sort();
  for (var i=0,lastMatch='';i<this.length;i++) {
    if (this[i]==lastMatch) {
      delete this[i];
    } else {
      lastMatch = this[i];
    }
  }
  return this
}
String.prototype.encodeSafe = function () {
  var check = this.match(/[^\w- \.,\(\)!']/gi)
  if (!check) return true;
  check = check.stripRepeats().join('').split('').join(',');
  if (check.length==1) return new CustomError(0,check+' is not a valid character for this field')
  return new CustomError(0,check+' are not valid characters for this field')
}
String.prototype.encodeSafeName = function () {
  var check = this.match(/[^ \w-'\.]/gi)
  if (!check) return true;
  check = check.stripRepeats().join('').split('').join(',');
  if (check.length==1) return new CustomError(0,check+' is not allowed in a user name')
  return new CustomError(0,check+' are not allowed in a user name')
}
String.prototype.checkLength = function (min,max) {
  min=min||0
  max=max||512
  if (this.length<min) return new CustomError(0,'text is too short (minimum is '+min+' characters)')
  if (this.length>max) return new CustomError(0,'text is too long (maximum is '+max+' characters)')
  return true  
}
String.prototype.checkMaxLength = function (max) {
  
  max=max||512
   if (this.length>max) return false
  return true  
}
String.prototype.trim = function () {
  return this.replace(/(^\s+)|(\s+$)/g,'')
}
String.prototype.isEmail = function () {
  if (this.match(RegExp.EMAIL)) return true
}
String.prototype.isUrl = function (opt) {
  if (opt&&!this.length) return true
  if (this.match(RegExp.URL)) return true
}
String.prototype.isValidEmail = function () {
  if (this.match(RegExp.EMAIL)) return true
  return new CustomError(0,'\''+this+'\' is not a valid email address')
}
String.prototype.isValidUrl = function () {
  if (this.match(RegExp.URL)) return true
  return new CustomError(0,'\''+this+'\' is not a valid URL')
}
String.prototype.isValidNumber = function (low,high) {
	if (isNaN(this)) return new CustomError(0,'\''+this+'\' is not a valid number');
	var t = +this;
	if (low != null && high != null) {
		if (t<low) {
			return new CustomError(0,'\''+this+'\' is less than '+low);
		}
		if (t>high) {
			return new CustomError(0,'\''+this+'\' is greater than '+high);
		}
	}
	return true;
}
String.prototype.isValidAsCurrency =  function () {
  var m
  if (m=this.match(/^(\D*)\d*(\.\d*)$/)) {
    if (m[1]) return new CustomError(0,'\''+this+'\' should not contain a prefix or currency symbol')
    if (m[2].length>3) return new CustomError(0,'\''+this+'\' should only have 2 decimal places')
    return true
  }
  if (this.match(',')) return new CustomError(0,'\''+this+'\' should not contain commas')
  return new CustomError(0,'\''+this+'\' is not a valid currency figure')
}
String.prototype.isValidPercentage = function (dp) {
  var m
  if (m=this.match(/^(\d*)(.)(\d*)%?$/)) {
    if (m[1]&&!m[2]&&m[1].match(/^0{2,}/)) return new CustomError(0,'\''+this+'\' appears to be missing a decimal point')
    if (parseFloat(this)>100) return new CustomError(0,'\''+this+'\' is greater than 100%')
    return true
  }
  return new CustomError(0,'\''+this+'\' is not a valid percentage')
}
function isValidDate (list) {
  var day = list[0].value||'-';
  var month = list[1].value? list[1].value : '-';
  var year = list[2].value||'-';
	  var yearAdd = 0 ;
	  if (document.layers) {
		  yearAdd = 1900;
	  }
	month +=1;	  
  var d = new Date(year,month,day)

  if (d.getDate()==+day && (d.getMonth())==+month && (d.getYear()+yearAdd)==+year) return true
  return new CustomError(0,day+'/'+month+'/'+year+' is not a valid date')
}
function isValidRenewalDate () {
  var day = parseInt(document.forms[0].renewalday.value);
  var month = parseInt(document.forms[0].renewalmonth.value);
  var year = parseInt(document.forms[0].renewalyear.value);
	  var yearAdd = 0 ;
	  if (document.layers) {
		  yearAdd = 1900;
	  }
 var d = new Date(year,month-1,day)
  if (d.getDate()== day && (d.getMonth())== month-1 && (d.getFullYear())==year) {
     return true
  }
  else
  {
		 alert(day+'/'+month+'/'+year+' is not a valid date'); 
		return false;
   }	 
}
String.prototype.checkForInvalidCharacters = function() {

	var check = this.match(/[^ \w-'\.]/gi)
  if (!check) return true;
  check = check.stripRepeats().join('').split('').join(',');
  if (check.length==1) return new CustomError(0,check+' is not allowed in a user name')
  return new CustomError(0,check+' are not allowed in a user name')
}
function checkNumericFormat(){
	var value = document.forms[0].telephone.value;
	if(!isNaN(value) || value.match(RegExp.NOT_VALID_TELEPHONE) == '-'){
		return true;
	}
	else{
		alert("Telephone number can accept only Numeric characters and the character \'-\'");
		return false;
	}
}
function isValidDateFromNow (list,returnDate) {
  var day = parseInt(list[0].value||'-');
  var monthVal = list[1][list[1].selectedIndex||0].value;
  var month = parseInt(monthVal);
  if(list[2]){
  var year = parseInt(list[2].value||'-');
  var d = new Date(year,month,day);
  if (d.getDate()==+day && d.getMonth()==+month && d.getFullYear()==+year) {
  //alert("1")
    if (Date.parse(document.lastModified)>=d.getTime()) return new CustomError(0,day+'/'+(month+1)+'/'+year+' is in the past');
  
    return returnDate? d : true;
    
  }
  if (day.toString()=='NaN') day ='  _';
  if (year.toString()=='NaN') year='_';
  return new CustomError(0,day+'/'+(month+1)+'/'+year+' is not a valid date');
  }
  else{
  var d = new Date();
  d.setDate(day);
  d.setDate(day);
  d.setMonth(month);
    if (d.getDate()==day && d.getMonth()==month) return true;
    if (day.toString()=='NaN') day ='_';
  return new CustomError(0,day+'/'+(month+1)+' is not a valid date');
  }
}
function Validate(form,silent) {
  Validate.currentForm = form;
  form.onreset = function () {Validate.resetErroredElements()}
  Validate.ErroredElements = [];
  Validate.errorMessage = [];
  Validate.errorMessage.addError = function (message) {
    if (this[message] ) return
    this.add(message)
    this[message]=true;
  }
  var validate , currentGroup, currentGroupList = [];
  var elms = form.elements, valid, emptyCount = 0;
  for (var i=0;i<elms.length;i++) {
    var elm = elms[i];
    if (elm.type=="button") continue;
	  if (!document.layers&&!document.all&&elm.type!='hidden') elm.focus()
    if (elm.onblur) {
      elm.validator = Validate.getValidator(elm,elm.onblur());
      valid = elm.validator.check(elm);
	  elm.title = valid.description||'';
      if (valid!=true) {
        if (valid.number==-1) emptyCount++
        else if (Validate.errorMessage[Validate.errorMessage.length]!=(elm.validator.description+valid.description)) Validate.errorMessage.addError(elm.validator.description+valid.description)
        Validate.ErroredElements.add(elm)

      }
      if (elm.validator.hiddenField) {
        var ev = elm.validator
        if (String.prototype[ev.hiddenAction]) elms[ev.hiddenField].value = elm.value[ev.hiddenAction](elms[ev.hiddenField])
        else elms[ev.hiddenField].value = self[ev.hiddenAction](elm,elms[ev.hiddenField])
      }
      if (elm.validator.groupFunc) {
        var list = currentGroupList[elm.validator.groupName]
        if (!list) {
          list = currentGroupList[elm.validator.groupName] = new Validate.ValidateList(elm.validator.groupName,elm.validator.groupFunc);
          currentGroupList.add(list)
        }
        list.addElement(elm)
      }
    }
  }
  for (i=0;i<currentGroupList.length;i++) {
    Validate.checkGroup(currentGroupList[i]);
    }
  if (emptyCount) Validate.errorMessage.addError(emptyCount+' required field(s) were empty')
  Validate.onValidate(Validate.errorMessage,silent)
  if (Validate.errorMessage.length) return Validate.onCustomError(Validate.errorMessage,silent)
  return true
  }
  Validate.errorMessage = [];
  Validate.errorMessage.addError = function (message) {
    if (this[message] ) return
    this.add(message)
    this[message]=true;
  }
  Validate.checkGroup = function (group,simple) {
    var valid
  if (self[group.func]) valid = self[group.func](group.elementList)
  else alert('Debug: Group function '+group.func+' not found');
  if (simple) return valid
    if (valid!=true) {
      Validate.errorMessage.add((group.name? group.name+': ' : '')+valid.description)
    for (j=0;j<group.elementList.length;j++) {
        Validate.ErroredElements.add(group.elementList[j])
      }
  }
  }
  Validate.onValidate = function () {return true}
  Validate.validators = {};
  Validate.functions = {};
  Validate.getFunc = function (name) {
    return this.functions[name]||name
  }
  Validate.getValidator = function (elm,str) {
    if (this.validators[str]) return this.validators[str]
   return this.validators[str]= new this.Validator(str)  
  }
  

  Validate.setErroredElements = function () {
    if (!document.layers) {
      var elms = this.ErroredElements
      for (var i=0;i<elms.length;i++) {
        var e = elms[i]
        if (e.type=="radio") continue;
        if (e.style.origColor == void 0) e.style.origColor = e.style.backgroundColor||''
        e.style.backgroundColor = this.CustomErrorColor
        e.onkeypress=e.onkeydown=e.onkeyup=e.onmouseup= Validate.elementCheck
        //e.onchange=
      }
      this.lastErroredElements = elms
    }
  }
  Validate.setElementStatus= function (list,valid) {
  	for (var i=0;i<list.length;i++)	{
    if (list[i].type=="radio") continue
    if (list[i].style.origColor == void 0) list[i].style.origColor = list[i].style.backgroundColor||''
		list[i].style.backgroundColor = valid==true ? list[i].style.origColor : Validate.CustomErrorColor 
		list[i].title = valid.description||''
	}
 }
  Validate.elementCheck = function () {
    if (this.type=="radio"||document.layers) return
    var test=this.validator.check(this);
	if (test==true) this.style.backgroundColor = this.style.origColor
	else this.style.backgroundColor = Validate.CustomErrorColor   
	this.title = test.description||''
    if (this.validateGroup) {
      test = Validate.checkGroup(this.validateGroup,true)
	  Validate.setElementStatus(this.validateGroup.elementList,test)
    }
  }
  Validate.resetErroredElements = function () {
    if (!document.layers) {
      var elms = this.currentForm.elements
      for (var i=0;i<elms.length;i++) {
         if (elms[i].style.origColor!=void 0) elms[i].style.backgroundColor = elms[i].style.origColor
      }
    }
  }
  Validate.ValidateList = function (name,func) {
    this.name = name;
    this.func = func;
    this.elementList = [];
  }
  Validate.ValidateList.prototype.addElement = function (elm) {
    this.elementList.add(elm)
    this.finalElement = elm
    elm.validateGroup = this
  }
  Validate.Validator = function (str) {
    var parseArray = str.split('_')
    this.description = this.formatDescription(parseArray[0])
    if (parseArray[1]!='opt') this.required = true
    if (parseArray[1]=='rad') this.radioButton = true
    if (parseArray[2]) {
      this.func = Validate.getFunc(parseArray[2])
      this.args=parseArray[3]||'()';
    }
    if (parseArray[4]&&parseArray[4]!='null') {
      this.groupFunc = Validate.getFunc(parseArray[4]);
      this.groupName = parseArray[5]||parseArray[4]
    }
    if (parseArray[6]) {
      this.hiddenField = parseArray[6]
      this.hiddenAction = parseArray[7]
    }
  }
  Validate.Validator.prototype.formatDescription = function (desc) {
    if (!desc||desc=='null') return ''
    return desc+': '
  }
  Validate.Validator.prototype.check = function (elm) {
    if (this.radioButton) return this.handleRadioButtons(elm)
    var value = elm.value||'';
    if (!value&&elm.type.match('select')) {
      value = elm.options[elm.selectedIndex].value
      if (!value&&elm.selectedIndex!=0) value = elm.options[elm.selectedIndex].text;
    }
    var empt = value.isEmpty()
    var chklen = false;
    var maxlen = 0;
    
    /*
	 Raman Pal Singh - 20 July 2004 - Bug # 151	
     Changed max len from 30 to 50 for  externalemailaddress 
    */
    if(elm.form.elements[elm.name].name == "externalemailaddress") 
    {
		maxlen = 50;
    }
    
    //Removed elm.form.elements[elm.name].name == "externalemailaddress" ||      
    if(elm.form.elements[elm.name].name =="forename" || elm.form.elements[elm.name].name =="surname"){
		maxlen = 30;
	}
	
	if(elm.form.elements[elm.name].name == "password"){
		maxlen = 20; // 8 len is changed by Raman on 11 sep 2003
	} 
	if(elm.form.elements[elm.name].name == "company"){
		maxlen = 255;
	} 
    chklen = value.checkMaxLength(maxlen);
    if(elm.form.elements[elm.name].name == "forename"){
		var junkchar = elm.form.elements[elm.name].value.encodeSafeName();
		
		//junkchar[1] = junkchar[1].stripRepeats().join('').split('').join(',');
		//alert(junkchar.description);		
		
		if(junkchar.description) {
		
			return new CustomError(0,junkchar.description)
		}
	}
	
    if (this.required&&empt) {
      var errNo = 0
      if (!this.description) errNo=-1
      return new CustomError(errNo,'field was empty')
    }
     if (this.required&&empt) {
      var errNo = 0
      if (!this.description) errNo=-1
      return new CustomError(errNo,'field was empty')
    }
    if (this.required&&!chklen){
    var errNo = 0
    if(!this.description) errNo=-1
    return new CustomError(0,'cannot be greater than '+maxlen+' characters')
    } 
    if (!this.required&&empt) return true
    if (String.prototype[this.func]) {
      self.$check  = value;
      return eval('self.$check.'+this.func+this.args)
      
    }
    else if (self[this.func]) return self[this.func](value)
    else if (!this.func||this.func=='null') return true
    else alert('Debug: function '+this.func+' not found');
    return true
  }
  Validate.Validator.prototype.handleRadioButtons = function (elm) {
    !this.buttonGroup&&(this.buttonGroup = elm.form.elements[elm.name])
    var checked = false,i=0
    for (var i=0;i<this.buttonGroup.length&&!checked;i++) {
      checked = this.buttonGroup[i].checked
    }
    if (checked) return true
    else return new CustomError(0,'no choice made (not highlighted)')
  }
  function confirmPassword(list) {
    if (list.length!=2) alert('Debug: 2 password fields not found');
  if (list[0].value==list[1].value) return true
  else return new CustomError(0,'fields do not match')
  } 
  
  Validate.CustomErrorColor = '#ffffcc';
  Validate.CustomErrorDescription = 'yellow';
  Validate.onCustomError = function (errorMessage,silent) {
 // alert('called')
    if (silent) return false;
    Validate.setErroredElements();
    alert(
		'The following problems were encountered with the form:  \n'
	    +((!document.layers)? ('\nThe fields in question will be highlighted '+(this.CustomErrorDescription)) : '')+'\n\n  '
        + errorMessage.join('\n  ') 
		+ '\n\nPlease correct the problems and resubmit the form'
	)
  return false
  }
  
  Validate.functions = {
    pct : 'isValidPercentage',
    cur : 'isValidAsCurrency',
    num : 'isValidNumber',
    eml : 'isValidEmail',
    url : 'isValidUrl',
    grpDat : 'isValidDate',
    grpDat3 : 'isValidDateFromNow',
    tel : 'isValidTelNo',
    cnf : 'confirmPassword',
    len : 'checkLength',
    mus : 'mustAgree',
    enc : 'encodeSafe',
    nam : 'encodeSafeName'
  }
  function setHidden(elm,hidden){
    var val = parseNumber(elm.value)
    var desc = elm.validator.description ? elm.validator.description+': ' : '';
    var correct = confirm(desc+'field with value: '+elm.value+' has been interpreted as: '+val)
  }

  // string validate format:
  
  //[field description]_[opt(ional)|req(uired)|rad(required radio))]_[(type of field (3) or null)]_([comma separated argument list])_[type of group]_[groupname]_[hidden fieldname]_[hidden field parsing function]
  
  function formSubmit(name) {
    var form = document.getElementById(name)
    if (!form.onsubmit||form.onsubmit()) form.submit()
  }
  function formReset(name) {
    var form = document.getElementById(name)
    form.reset()
  }
  
  function setElementValue(elm,value) {
    if (!elm.type) elm.type="radio"
    else if (elm.type=="radio") elm=elm.form.elements[elm.name];
    switch (elm.type) {
      case 'radio': {
        //alert('radio: '+elm.length)
        for (var i=0;i<elm.length;i++) if (elm[i].value==value) elm[i].checked=true;
        break;
      }
      case 'select-one':
      case 'select-multiple': {
        for (var i=0;i<elm.options.length;i++) {
          if (elm.options[i].value==value||elm.options[i].text==value) {
            elm.selectedIndex=i;
            break;
          }
        }
        break;
      }
      case 'checkbox': {
        if (elm.value==value) elm.checked = true;
        break;    
      }
      default: {
        elm.value=value;
      }
    }
  }

Date.prototype.parseXML = function (xmlDate) {
 var d = xmlDate.replace('Z', '').split('T');
 if (d.length==0) return Number.NaN;
 d[0] = d[0].split('-');
 if (d[0].length!=3) return Number.NaN;
 if (d.length>1&&d[1].length>0) {
  d[1] = d[1].split(':');
  if (d[1].length!=3) return Number.NaN;
  return new Date(d[0][0], d[0][1]-1, d[0][2], d[1][0], d[1][1], d[1][2]);
 } else {
  return new Date(d[0][0], d[0][1]-1, d[0][2]);
 }

}
		
/***Disable button which is hitted on page******* written by Pravesh on 8 may 208
common method to disable a button which is hitted and do post back
Parameters: button control
*/
function DisableButton(buttonElem) {
   
		  if (typeof(Page_ClientValidate) == 'function') 
		  {
				Page_ClientValidate();
				if (!Page_IsValid)
				 return false;
		  }
		var x= document.forms[0].elements;	
			for (var i=0;i<x.length;i++)
			{
			if (x.elements[i].type=='button' || x.elements[i].type=='submit' )
			    x.elements[i].disabled = true;
}
if (iLangID == 2) {
    buttonElem.value = 'Aguarde por favor ...';
}
else {
    buttonElem.value = 'Please Wait...'; 
}
		buttonElem.disabled = true;
		 __doPostBack(buttonElem.id,buttonElem.id);
}
// written by Pravesh K chandel
function DisableButtonOnClick(buttonElem)
	 {
		if(buttonElem.disabled==true) return false;
		if(!isPageValid_ToSubmit) return false;
		buttonElem.disabled=true;
		var retVal ;
		var onClickAttribute = buttonElem.getAttributeNode("onclick").value;
		if(onClickAttribute)
		{
			onClickAttribute=onClickAttribute.replace("if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate();","");
			onClickAttribute=onClickAttribute.replace("javascript:DisableButtonOnClick(document.getElementById('" + buttonElem.id + "'));return false;","");
			onClickAttribute=onClickAttribute.replace("return","");
			if(TrimTheString(onClickAttribute)!="")
			{
			retVal = eval(onClickAttribute);
			if(!isPageValid_ToSubmit) return false;
				if(retVal!=null && retVal==false)
				{
				buttonElem.disabled=false;
				 return false;
				}
			}
		}
        //buttonElem.click();
		if(!isPageValid_ToSubmit) return false;
		buttonElem.disabled=true;
     	 if (typeof(Page_ClientValidate) == 'function') 
		  {
				Page_ClientValidate();
				if (!Page_IsValid)
				{
				 buttonElem.disabled=false;
				 return false;
				 }
		  }
		  ValidateCustomValidator();
		  if (typeof(Page_ClientValidate) == 'function') 
		  {
				if (!Page_IsValid)
				{
				 buttonElem.disabled=false;
				 return false;
				}
		  }		
		var x= document.forms[0].elements;	
		if (retVal==null || retVal=="undefined" || retVal==true)
		{
			for (var i=0;i<x.length;i++)
			{
			if (x.elements[i].type=='button' || x.elements[i].type=='submit' )
				x.elements[i].disabled=true;
			}
			if (iLangID == 2) {
			    buttonElem.value = 'Aguarde por favor ...';
			}
			else {
			    buttonElem.value = 'Please Wait...';
			}

		buttonElem.disabled = true;
		  __doPostBack(buttonElem.id,buttonElem.id); 
		}
		else
		buttonElem.disabled=false;
}
// writen by Pravesh Chandel to validate custom Validator of the page
function ValidateCustomValidator() 
{
 if (typeof(Page_Validators) == "undefined")
        return;
    var i, val;
    for (i = 0; i < Page_Validators.length; i++) 
    {
        val = Page_Validators[i];
        if (val.id.substring(0,3) == "csv") 
		{
          if (typeof(val.isvalid) == "string") 
			{
				if (val.isvalid == "False") 
				{
                val.isvalid = false;                                
                Page_IsValid = false;
				} 
			    else 
	                val.isvalid = true;
            } 
            else 
            {
				if (val.isvalid == false)
					Page_IsValid = false;
            }
         ValidatorUpdateDisplay(val);    
        }
    }    
}
/*****************wait***************
common method to wait processing on page for some miliseconds
Parameters: 	ms: miliseconds
Written BY	:Pravesh K Chandel			
*/
function wait(ms)
{
var start = new Date().getTime();
 while ((new Date().getTime() - start) < ms)
	{
	// Do nothing
	}
}

function clearFormData()
{
}

/*'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
		FUNCTION [ SearchAndReplace] 
		[PURPOSE]  -> To find any straing/word in a string, and if found
			replace the string with new string
		          
		[Parameter]  -> strAll   -> The whole string into which we perform search
			-> strSearch  -> String which we search
			-> strReplace  -> String which we need to replace
		[INVOKE FROM] -> internally from query resultset function(s) [QueryString_Parse]
		[AUTHOR]  -> Himadrish May 25th 2005
		'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/
		function SearchAndReplace(strAll,strSearch,strReplace)
		{
			//Purpose: To search and replace in a string
			//For example search the LAST_SELECTED_ID=12
			//in a string / url, if found replace it
			//with new value LAST_SELECTED_ID=14
			myOldString = new String(strAll)
			//rExp = '/' + strSearch + '/g'
			var rg = new RegExp(strSearch,"g"); 
			newString = new String (strReplace)
			//var results = myOldString.replace(rExp, newString)
			var results = myOldString.replace(rg, newString)
			return results
		}

		function ReplaceAll(string, text, by) {
		    // Replaces text with by in string
		    var strLength = string.length, txtLength = text.length;
		    if ((strLength == 0) || (txtLength == 0)) return string;

		    try {


		        var i = string.indexOf(text);
		        if ((!i) && (text != string.substring(0, txtLength))) return string;
		        if (i == -1) return string;

		        var newstr = string.substring(0, i) + by;

		        if (i + txtLength < strLength)
		            newstr += ReplaceAll(string.substring(i + txtLength, strLength), text, by);
		    } catch (er)
		{ newstr = string }
		    return newstr;
		}
		
function DecodeXML(strInput)
{
	var str1 = SearchAndReplace(strInput,'&amp;','&');
	str1 = SearchAndReplace(str1,'&lt;','<');
	str1 = SearchAndReplace(str1,'&gt;','>');
	str1 = SearchAndReplace(str1,'&apos;','\'');
	
	return str1;	
}
		
/*****************populateFormData***************
common method to fill controls from xml provided
Parameters:
			xml: Xml to parse
			objForm: Form object of the page.
Notes:
	1. The name of nodes of xml and ids of form controls must be same apart from the prefixes of the controls
	2. The Id of radio buttons must be in the format: rdb+XmlNodeName+NodeValue 
	3. Prefixes assumed: txt,cmb,rdb,chk,lbl,hid,lst
*/
function populateFormData(xml,objForm)
{
		if(xml!="")
		{
			
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(xml).getElementsByTagName('Table')[0]);
			
			var i=0;
			for(i=0;i<tree.childNodes.length;i++)
			{
			//{alert((tree.childNodes[i].firstChild == 'undefined' || tree.childNodes[i].firstChild == null) ? "" : tree.childNodes[i].firstChild.text);				
				//if(!tree.childNodes[i].firstChild) continue;
				
				var nodeName = tree.childNodes[i].nodeName;
				var nodeValue ;
				
				//tree.childNodes[i].firstChild.text;
				
				if ( tree.childNodes[i].firstChild == 'undefined' || tree.childNodes[i].firstChild == null )
				{
					nodeValue = '';
				}
				else
				{
					nodeValue = tree.childNodes[i].firstChild.text;
					//alert(nodeValue);
					nodeValue = DecodeXML(nodeValue);
					//alert(nodeValue);
				}
				
				//**** Setting Textfield value *********
				if(document.getElementById("txt"+nodeName))
				{
					//document.getElementById("txt"+nodeName).value = tree.childNodes[i].firstChild.text;
					document.getElementById("txt"+nodeName).value = nodeValue;
					//continue;
				}
				
				//**** Setting List box value *********
				if(document.getElementById("lst"+nodeName))
				{
					//SelectComboOption("lst"+nodeName,tree.childNodes[i].firstChild.text)
					SelectComboOption("lst"+nodeName,nodeValue)
					//continue;
				}
				//**** Setting hidden field value *********
				if(document.getElementById("hid"+nodeName))
				{
					//document.getElementById("hid"+nodeName).value = tree.childNodes[i].firstChild.text;
					document.getElementById("hid"+nodeName).value = nodeValue;
					//alert(document.getElementById("hid"+nodeName).value);
					//continue;
				}
				//**** Setting drop down list value *********
				if(document.getElementById("cmb"+nodeName))
				{
					//SelectComboOption("cmb"+nodeName,tree.childNodes[i].firstChild.text)
					if(("cmb"+nodeName)== "cmbOTHER_DESCRIPTION")//Added by Charles on 8-Dec-09 for Itrack 6489
					{
						if(document.getElementById('hidCalledFrom'))
						{
							if(document.getElementById('hidCalledFrom').value=='RENTAL')
							{
								SelectComboOption("cmb"+nodeName,nodeValue);
							}
						}
					}
					else //Added till here
						SelectComboOption("cmb"+nodeName,nodeValue)
					//continue;
				}
				//ADDED BY PRAVEEN KUMAR(03-03-2009):ITRACK 5518
				if(document.getElementById("cmb"+nodeName+"_COM"))
				{
					//SelectComboOption("cmb"+nodeName,tree.childNodes[i].firstChild.text)
					SelectComboOption("cmb"+nodeName+"_COM",nodeValue);
					//continue;
				}
				//END PRAVEEN KUMAR
				//**** Setting list Box value *********
				if(document.getElementById("lst"+nodeName))
				{
					//SelectComboOption("cmb"+nodeName,tree.childNodes[i].firstChild.text)
					SelectListOptions("lst"+nodeName,nodeValue.split(','))
					//continue;
				}
				//**** Setting label value *********
				if(document.getElementById("lbl"+nodeName))
				{
					//document.getElementById("lbl"+nodeName).innerText = tree.childNodes[i].firstChild.text;
					document.getElementById("lbl"+nodeName).innerText = nodeValue;
					//continue;
				}
				//**** Setting checkbox value *********
				if(document.getElementById("chk"+nodeName))
				{
					//alert(document.getElementById("chk"+nodeName).checked);
					var strNodeValue = new String(nodeValue);										
					//if(nodeValue=="Y" || nodeValue=="1" || nodeValue==true || nodeValue=="true")
					if(strNodeValue.toUpperCase() =="Y" || nodeValue=="1" || nodeValue==true || strNodeValue.toUpperCase()=="TRUE")
						document.getElementById("chk"+nodeName).checked = true;					
					else 
						document.getElementById("chk"+nodeName).checked = false;
						//continue;
				}
				//**** Setting radio button value *********
				if(document.getElementById("rdb"+nodeName+nodeValue))
				{
					//var nodeValue = tree.childNodes[i].firstChild.text;
					document.getElementById("rdb"+nodeName+nodeValue).checked = true;
					//continue;
				}		
			}			
			//**** Setting Activate/Deacttivate caption *********
			//SetActivateDeactivateButton(xml);	//Commented by Charles on 7-Apr-10 for Multilingual Support, use ClsMessages.FetchActivateDeactivateButtonsText(hidIS_ACTIVE.Value)
		}		
}

/****************** SelectComboOption ****************
Created by : Ajit Singh Chahal, 22/6/2005
Description: Is useded to select multiple options of a listbox.
Parameters:  listId:- javascript Id of listbox,
			 selectedValues array of values to be selected in list box.
Note: All options whose value equal to any of the elements of the selectedValues array get selected.
*/
function SelectListOptions(listId,selectedValues)
{
	for(var j=0; j<document.getElementById(listId).options.length; j++)
	{
	for(var i=0;i<selectedValues.length;i++)
	{
		if(document.getElementById(listId).options[j].value == selectedValues[i])
		{
			document.getElementById(listId).options[j].selected = true;
			break;
		}
	}
	}
}
/******************populateFormData1***************
common method to fill controls from xml provided
Parameters:
			xml: Xml to parse
			objForm: Form object of the page.
			index: the index of row to be populated or anything that should be appended to all node names before fetching the corresponding control name.
Notes:
	1. The name of nodes of xml and ids of form controls must be same apart from the prefixes of the controls
	2. The Id of radio buttons must be in the format: rdb+XmlNodeName+NodeValue 
	3. Prefixes assumed: txt,cmb,rdb,chk,lbl,hid,lst
*/
function populateFormData1(xml,objForm,index)
{
	if(xml!="")
		{
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(xml).getElementsByTagName('Table')[0]);
			
			var i=0;
			
			for(i=0;i<tree.childNodes.length;i++)
			{
				if(!tree.childNodes[i].firstChild) continue;
				var nodeName = tree.childNodes[i].nodeName;
				var nodeValue = tree.childNodes[i].firstChild.text;
				
				//**** Setting Textfield value *********
				if(document.getElementById("txt"+nodeName+index))
				{
					document.getElementById("txt"+nodeName+index).value = tree.childNodes[i].firstChild.text;
					continue;
				}
				//**** Setting drop down list value *********
				if(document.getElementById("cmb"+nodeName+index))
				{
					SelectComboOption("cmb"+nodeName+index,tree.childNodes[i].firstChild.text)
					continue;
				}
				//**** Setting List box value *********
				if(document.getElementById("lst"+nodeName+index))
				{
					SelectComboOption("lst"+nodeName+index,tree.childNodes[i].firstChild.text)
					continue;
				}
				//**** Setting hidden field value *********
				if(document.getElementById("hid"+nodeName+index))
				{
					document.getElementById("hid"+nodeName+index).value = tree.childNodes[i].firstChild.text;
					//////alert(document.getElementById("hid"+nodeName+index).value);
					continue;
				}
				
				//**** Setting label value *********
				if(document.getElementById("lbl"+nodeName+index))
				{
					document.getElementById("lbl"+nodeName+index).innerText = tree.childNodes[i].firstChild.text;
					continue;
				}
				//**** Setting checkbox value *********
				if(document.getElementById("chk"+nodeName+index))
				{
					//////alert(document.getElementById("chk"+nodeName+index).checked);
					if(nodeValue=="Y" || nodeValue=="1" || nodeValue==true)
						document.getElementById("chk"+nodeName+index).checked = true;
					else 
						document.getElementById("chk"+nodeName+index).checked = false;
						continue;
				}
				//**** Setting radio button value *********
				if(document.getElementById("rdb"+nodeName+index+nodeValue))
				{
					var nodeValue = tree.childNodes[i].firstChild.text;
					document.getElementById("rdb"+nodeName+index+nodeValue).checked = true;
					continue;
				}				
			}		
		}
}	

/*****************RefreshWindowsGrid***************
common method to fill refresh windows grid and the xml in top frame (grid container)
Parameters:
			isformSaved: Refereshes grid only if the value is one
			rowId: RowId of record to be fetched for xml cache variable
Notes:
	1. Parameter is passed can be value of a Hidden form field to track form loaded after a save event.
	2. This method is intended to be called from tab fram only as it referes to windows grid.

*/
function RefreshWindowsGrid(isformSaved,rowId)
{		
	if(isformSaved == '1')
	{		
		if(parent.document.gridObject) //refreshing windows grid
		{
			parent.document.gridObject.refreshcompletegrid();
			parent.document.strXML	=	parent.document.gridObject.getRefreshedRowData(rowId);
			document.getElementById('hidOldData').value		=	 parent.document.strXML;
		}
		else //refreshing web grid
		{
					parent.refreshGrid("",rowId);
			if(parent.document.prmId!="")				
			{
				var rrid="Row_" + parent.document.prmId;
				parent.selectROWID(rrid)
			}
		}		
	}	
	//**** Setting Activate/Deacttivate caption *********
	//SetActivateDeactivateButton(this.parent.strXML);
	//alert(parent.document.strXML);
	SetActivateDeactivateButton(parent.document.strXML);
}
/*function RefreshWindowsGrid(isformSaved,rowId)
{		
	if(isformSaved == '1')
	{		
		if(top.frames[1].document.gridObject) //refreshing windows grid
		{
			top.frames[1].document.gridObject.refreshcompletegrid();
			top.frames[1].strXML	=	top.frames[1].document.gridObject.getRefreshedRowData(rowId);
			document.getElementById('hidOldData').value		=	 top.frames[1].strXML;
		}
		else //refreshing web grid
		{
					top.frames[1].refreshGrid("",rowId);
			if(top.frames[1].prmId!="")				
			{
				var rrid="Row_" + top.frames[1].prmId;
				top.frames[1].selectROWID(rrid)
			}
		}		
	}	
	//**** Setting Activate/Deactivate caption *********
	SetActivateDeactivateButton(top.frames[1].strXML);
}*/

function SetActivateDeactivateButton(xml)
{	
	//If no activate deactivate button found
	if ( document.getElementById("btnActivateDeactivate") == null ) return;
	
		if(xml!=null && xml!="")
			{
			    if (document.getElementById("btnActivateDeactivate").value == "") {
			    
			        var objXmlHandler = new XMLHandler();
			        var tree = (objXmlHandler.quickParseXML(xml).getElementsByTagName('Table')[0]);
			        var i = 0;
			        for (i = 0; i < tree.childNodes.length; i++) {
			            if (!tree.childNodes[i].firstChild) continue;
			            var nodeName = tree.childNodes[i].nodeName;
			            var nodeValue = tree.childNodes[i].firstChild.text;

			            if (nodeName.toUpperCase() == "IS_ACTIVE") {
			                if (nodeValue.toUpperCase() == "Y" || nodeValue == "1") {
			                    if (iLangID == 2) {
			                        document.getElementById("btnActivateDeactivate").value = "Desativar";
			                    }
			                    else {
			                        document.getElementById("btnActivateDeactivate").value = "Deactivate";
			                    }
			                }
			                else {
			                    if (iLangID == 2) {
			                        document.getElementById("btnActivateDeactivate").value = "Ativar";
			                    }
			                    else {
			                        document.getElementById("btnActivateDeactivate").value = "Activate";
			                    }
			                }
			                continue;
			            }
			        }
			    }		    
				
				//Added by Anurag Verma on 20/04/2005
				// purpose: added if condition to check whether control exists
				if(document.getElementById("btnActivateDeactivate"))
					document.getElementById("btnActivateDeactivate").setAttribute("disabled",false);
			}
			else
			{
				//Added by Anurag Verma on 20/04/2005
				// purpose: added if condition to check whether control exists
//				if(document.getElementById("btnActivateDeactivate"))
//					document.getElementById("btnActivateDeactivate").setAttribute("disabled",true);
			}
}

//Refreshes the grid based on isformSaved variable
//do not refreshes the tabs on parent page if notRefreshTabs != null
function RefreshWebGrid(isformSaved,rowId,isActivateDeactivateRequired,notRefreshTabs)
{	
	if (isformSaved == "5")
	{
		parent.strSelectedRecordXML = "-1";
	}
	
	if(isformSaved == '1' || isformSaved == '5')
	{
		//parent.TestFn();
		parent.RefreshWebgrid(rowId);
		if (notRefreshTabs == null)
		{
			//Refrehing the tabs
			RefreshTabs(false)
			
		}
	}
	//**** Setting Activate/Deactivate caption *********
	if(isActivateDeactivateRequired != null && isActivateDeactivateRequired != false)
		SetActivateDeactivateButton(document.getElementById('hidOldData').value);	
}

/*function RefreshTabs(loadPage)
{
	if (this.parent.prvsTab != "undefined" && this.parent.prvsTab != "" && this.parent.prvsTab != null)
	{
		pretab = this.parent.prvsTab.split(',')[1];
	}
	else
		pretab = 0;
	
	if (this.parent.SetTabs)
		setTimeout('this.parent.SetTabs(pretab,' + loadPage + ')', 500);
	
}The given function has been modified to include a check for null value for strSelectedRecordXML.
The check has been added to prevent vehicle info page at umbrella and automobile LOB from going to 
Add New mode after inserting record for the first time.
*/
function RefreshTabs(loadPage)
{
	if (this.parent.strSelectedRecordXML == "-1" || TrimTheString(this.parent.strSelectedRecordXML) == "")
	{
		setTimeout("RefreshTabs(" + loadPage + ")",100);
		return;
	}
	if (this.parent.prvsTab != "undefined" && this.parent.prvsTab != "" && this.parent.prvsTab != null)
	{
		pretab = this.parent.prvsTab.split(',')[1];
	}
	else
	{
		pretab = 0;
	}
	
	//calling the index page setabs method which will makes different tabs
	if (this.parent.SetTabs)
	{
		setTimeout('this.parent.SetTabs(pretab,' + loadPage + ')', 100);
	}	
}

function ResetForm(formname)
{    
	switch(document.getElementById('hidFormSaved').value)
	{
		case "0":
			AddData();
			populateXML();
			break;
		case "1":
			eval('document.'+ formname +'.reset()');
			DisableValidators();
			break;
		case "2":
			AddData();
			document.getElementById('hidFormSaved').value = "0";
			populateXML();
			break;
	}
	ChangeColor();
	return false;
	
}

/****************** DisableValidators ****************
Description: Is used to disable the validators when reset button clicked
to be call in AddData javascript method
*/
function DisableValidators()
{
	var ctr=0;
	//if(typeof('Page_Validators') != null)
	if(typeof(Page_Validators) != 'undefined')
	{
		for( ctr=0;ctr<Page_Validators.length;ctr++)
		{
			Page_Validators[ctr].isvalid=true;
			ValidatorUpdateDisplay(Page_Validators[ctr]);
		}
	}
}

/*function SelectComboIndex(objIndex)
{
		if(document.getElementById(objIndex).options.selectedIndex > 0)
		{
		//DisableValidators();
		}
		else
		{
			document.getElementById(objIndex).options.selectedIndex = 0;
			
		}
}*/

function isValidTime(timeStr) {
// Time validation function courtesty of 
// Checks if time is in HH:MM:SS AM/PM format.
// The seconds and AM/PM are optional.

var timePat = /^(\d{1,2}):(\d{2})(:(\d{2}))?(\s?(AM|am|PM|pm))?$/;

var matchArray = timeStr.match(timePat);
if (matchArray == null) {
//alert("Time is not in a valid format.");
return false;
}
hour = matchArray[1];
minute = matchArray[2];
second = matchArray[4];
ampm = matchArray[6];

if (second=="") { second = null; }
if (ampm=="") { ampm = null }

if (hour < 0  || hour > 23) {
//alert("Hour must be between 1 and 12. (or 0 and 23 for military time)");
return false;
}
if (hour <= 12 && ampm == null) {
if (confirm("Please indicate which time format you are using.  OK = Standard Time, CANCEL = Military Time")) {
//alert("You must specify AM or PM.");
return false;
   }
}
if  (hour > 12 && ampm != null) {
//alert("You can't specify AM or PM for military time.");
return false;
}
if (minute < 0 || minute > 59) {
//alert ("Minute must be between 0 and 59.");
return false;
}
if (second != null && (second < 0 || second > 59)) {
//alert ("Second must be between 0 and 59.");
return false;
}
return true;
}

function SubmitForm(formID,evt)
{
	/*******************************************************************
	'*	Function Name	:	SubmitForm() 
	'*	Type			:   JavaScript Function
	'*	Author			:	Ashwani
	'*	Parameters		:   formID 
	'*	Purpose			:   This function sets the save button as a default button
	'*						
	'*	Creation Date	:   24 May,2005
	'*  Modified  Date	:					 
	'*	Returns			:    
	'*******************************************************************/
    var eventInstance = window.event ? event : evt;
    var eventElement = eventInstance.target ? eventInstance.target : eventInstance.srcElement;
	//if(event.srcElement.type != 'submit' && event.srcElement.type != 'textarea')
    //if ((eventInstance.srcElement.type != 'submit' && eventInstance.srcElement.type != 'textarea') || eventInstance.srcElement.id == 'btnSave')
    if ((eventElement.type != 'submit' && eventElement.type != 'textarea') || eventElement.id == 'btnSave')
	{
		{
			//alert(document.getElementById(formID).type);
		    var keyCode = eventInstance.keyCode ? eventInstance.keyCode : eventInstance.which ? eventInstance.which : eventInstance.charCode;
		    if (keyCode == 13)
			{
			    //var btnSave = document.getElementById(formID).btnSave;
			    var btnSave = document.forms[formID].btnSave;
				//alert(btnSave);
				
				//style check added by Charles on 29-Jul-09 for Itrack 6188
				if( btnSave != null && btnSave.style.display!='none' )
				{		
					//document.getElementById(formID).btnSave.click();
					//if(formID!="CLM_CLAIM_INFO")
					//{
						try
						{
						document.getElementById(formID).btnSave.focus()
						}
						catch(ex){}
						DisableButtonOnClick(btnSave); // added by Pravesh K Chandel on 16 July 09 to prevent multipale postback if user press and hold Enter Key
						//btnSave.click();
					//}	
						return false;
					
				}
				else	//else added by Charles on 4-Aug-09 for Itrack 6188
					return false;
			}
		}
	}		
}					

function ChangeColor()
{
	/*******************************************************************
	'*	Function Name	:	ChangeColor() 
	'*	Type			:   JavaScript Function
	'*	Author			:	Ashwani
	'*	Parameters		:   
	'*	Purpose			:   This function is used to change the background 
	'*						color of mandatory field.
	'*	Creation Date	:   31 March,2005
	'*  Modified  Date	:   30 May 2005
	'*  Modified  By	:	Ashwani( Corrected the overwritting of css class problem)		 
	'*	Returns			:    
	'*******************************************************************/
	var ctr=0;	
	if (typeof(arrClass) == 'undefined')
		arrClass = document.createElement("Array()");
	
	//if(typeof('Page_Validators') != null)
	if(typeof(Page_Validators) != 'undefined')
	{
		for( ctr=0;ctr<Page_Validators.length;ctr++)
		{
			if (Page_Validators[ctr].id.substring(0,3) == "rfv"  && (Page_Validators[ctr].enabled==true || typeof(Page_Validators[ctr].enabled)=='undefined'))
			{							
				if (document.getElementById(Page_Validators[ctr].controltovalidate).value == "")
				{	
					if (document.getElementById(Page_Validators[ctr].controltovalidate).className != "" 
						&& document.getElementById(Page_Validators[ctr].controltovalidate).className != "MandatoryControl")
					{
						//Adding the previous class in array
						arrClass[ctr] = document.getElementById(Page_Validators[ctr].controltovalidate).className;
					}
					
					//applying the active control class 
					document.getElementById(Page_Validators[ctr].controltovalidate).className ="MandatoryControl";
				}
				else
				{
					if (typeof(arrClass[ctr]) == 'undefined' || arrClass[ctr] == "")
					{
						//Previously class was blank
						if (document.getElementById(Page_Validators[ctr].controltovalidate).className == "MandatoryControl")
							document.getElementById(Page_Validators[ctr].controltovalidate).className = "";
					}
					else
					{
						//Previously some class was there, hence applying that class again
						document.getElementById(Page_Validators[ctr].controltovalidate).className = arrClass[ctr];
					}
				}
				
			}
		}
	}
}
		
function ApplyColor() {
   
	/*******************************************************************
	'*	Function Name	:	ApplyColor() 
	'*	Type			:   JavaScript Function
	'*	Author			:	Ashwani
	'*	Parameters		:   
	'*	Purpose			:   This function is used to change the background 
	'*						color of mandatory field and also call ChangeColor() function. 	
	'*	Creation Date	:   31 March,2005
	'*  Modified  Date	:					 
	'*	Returns			:    
	'*******************************************************************/
	var ctr=0;
	//if(typeof('Page_Validators') != null)
	if(typeof(Page_Validators) != 'undefined')
	{
		for( ctr=0;ctr<Page_Validators.length;ctr++)
		{
		    if (Page_Validators[ctr].id.substring(0, 3) == "rfv")
			{
				if(document.getElementById(Page_Validators[ctr].controltovalidate).tagName == "SELECT")
				{
				    if (browser.isIE)
				        document.getElementById(Page_Validators[ctr].controltovalidate).attachEvent("onclick", ChangeColor);
				    else
				        document.getElementById(Page_Validators[ctr].controltovalidate).addEventListener("onclick", ChangeColor, false);

				}
				if (browser.isIE)
				    document.getElementById(Page_Validators[ctr].controltovalidate).attachEvent("onblur", ChangeColor);
				else
				    document.getElementById(Page_Validators[ctr].controltovalidate).addEventListener("onblur", ChangeColor, false);
			}	
			else if(Page_Validators[ctr].id.substring(0,3) == "rev")
			{
				if(typeof(Page_Validators[ctr].validationexpression) != 'undefined')
				{
					if (Page_Validators[ctr].validationexpression.toString() == aRegExpPhone)
					{
						//document.getElementById(Page_Validators[ctr].controltovalidate).onchange = FormatPhone;
					    if (browser.isIE)
					        document.getElementById(Page_Validators[ctr].controltovalidate).attachEvent("onblur", FormatPhone);
					    else
					         document.getElementById(Page_Validators[ctr].controltovalidate).addEventListener("onblur", FormatPhone,false);
						
					}				
					if (Page_Validators[ctr].validationexpression.toString() == aRegExpDate)
					{
					    if (browser.isIE)
					        document.getElementById(Page_Validators[ctr].controltovalidate).attachEvent("onblur", FormatDate);
					    else
						    document.getElementById(Page_Validators[ctr].controltovalidate).addEventListener("onblur", FormatDate,false);
					}
					if (Page_Validators[ctr].validationexpression.toString() == aRegExpZip)
					{
					    if (browser.isIE)
					        document.getElementById(Page_Validators[ctr].controltovalidate).attachEvent("onblur", FormatZip);
					    else
						    document.getElementById(Page_Validators[ctr].controltovalidate).addEventListener("onblur", FormatZip,false);					
					}
					
					if (Page_Validators[ctr].validationexpression.toString() == aRegExpSSN)
					{
					    if (browser.isIE)
					        document.getElementById(Page_Validators[ctr].controltovalidate).attachEvent("onblur", FormatSSN);
					    else
						    document.getElementById(Page_Validators[ctr].controltovalidate).addEventListener("onblur", FormatSSN,false);					
					}
					
					if (Page_Validators[ctr].validationexpression.toString() == aRegExpShortDate)
					{
					    if (browser.isIE)
					        document.getElementById(Page_Validators[ctr].controltovalidate).attachEvent("onblur", FormatShortDate);
					    else
						    document.getElementById(Page_Validators[ctr].controltovalidate).addEventListener("onblur", FormatShortDate,false);					
					}
				}				
			}						
		}
	}
}

	//This function is used to show scrool on resizing of windows
	function SmallScroll()
		{
			document.getElementById('bodyHeight').style.height	=	document.body.offsetHeight-20;
			document.getElementById('bodyHeight').style.width	=	document.body.offsetWidth-1;
		}	

	/*******************************************************************
	'*	Function Name	:	DateAdd() 
	'*	Type			:   JavaScript Function
	'*	Author			:	Priya
	'*	Parameters		:   num
		'*	Purpose			:   This function is used to display the date.If user adds any integer value it will add 
								to the current  system date and show the respective date and if user enters
								non-integeral value it will show the current system date 						
		'*	Creation Date	:   06 April ,2005
		'*  Modified  Date	:					 
		'*	Returns			:    
		'*******************************************************************/
       function DateAdd(num)
        {			
			if((num != null)&&!((isNaN(num))))
			{
				var milisecond = 86400000; // no. of milliseconds in one day
				var myDateTime = new Date();
		        var param = ((myDateTime.getMonth()+1)+ "/" + myDateTime.getDate()+ "/" + myDateTime.getYear());
		        var datebuffer = new Date(Date.parse(param) + (parseInt(num) * milisecond));

		        //Added for Multilingual Support
		        if (sCultureDateFormat == 'DD/MM/YYYY') {
		            return (datebuffer.getDate() + "/" + (datebuffer.getMonth() + 1) + "/" + datebuffer.getFullYear().toString());		            
		        }
		        else {
		            return ((datebuffer.getMonth() + 1) + "/" + datebuffer.getDate() + "/" + datebuffer.getFullYear().toString());
		        }
			}
			else
			{
			    var datebuffer = new Date();

			    //Added for Multilingual Support
			    if (sCultureDateFormat == 'DD/MM/YYYY') {
			        return (datebuffer.getDate() + "/" + (datebuffer.getMonth() + 1) + "/" + datebuffer.getFullYear().toString());
			    }
			    else {
			        return ((datebuffer.getMonth() + 1) + "/" + datebuffer.getDate() + "/" + datebuffer.getFullYear().toString());
			    }
			}
		}
		
			function StripDash(szSSNNo)
			{
				/*******************************************************************
				'*	Function Name	:	StripDash() 
				'*	Parameters		:	
				'*	Author			:	Ashwani
				'*	Purpose			:   Removes the strips & Dashes 
				'*	Creation Date	:   May 04,2005
				'*  Modified  Date	:					 
				'*	Returns			:    
				'*******************************************************************/
			
				var szStrippedSSN;
				var intCount;
				szStrippedSSN = "";
				var arrSSN;
				if (TrimTheString(szSSNNo) != "")
				{
					arrSSN = szSSNNo.split("-");
				for(intCount = 0; intCount<arrSSN.length; intCount++)
				{
					szStrippedSSN = szStrippedSSN + arrSSN[intCount];
				}
				}
				return(szStrippedSSN);
			}

			/*******************************************************************
			'*	Author			:	Asfa
			'*	Purpose			:   To convert Date field in MM-YY format
			'*	Creation Date	:   June 10,2008			
			'*******************************************************************/
			function FormatShortDate()
			{				
				var szFieldName = event.srcElement.id;
				var szFormName = document.forms[0].name;
				var szDate = eval(szFormName + "." + szFieldName).value; // get the field value
				var arrDate;
				var intCount;
				var szStrippedDate;
				var FieldToStore;	
				szStrippedDate = StripDash(szDate);
				if(szDate.length == 4 || szStrippedDate.length == 4)
				{
					if (TrimTheString(szDate) != "" ||  TrimTheString(szStrippedDate) != "")
					{
						szStrippedDate = StripDash(szDate);
						FirstPart = szStrippedDate.substr(0,2)
						SecondPart = szStrippedDate.substr(2,2)
						FullDate = FirstPart + "-" + SecondPart ;
						eval('document.' + szFormName + '.' + szFieldName).value = FullDate;
						//Added for Multilingual Support
						if (sCultureDateFormat == 'DD/MM/YYYY') {
						    if (SecondPart > 0 && SecondPart <= 12 && FirstPart > 0)
						        DisableValidatorsById(szFieldName);
						}
						else {
						    if (FirstPart > 0 && FirstPart <= 12 && SecondPart > 0)
						        DisableValidatorsById(szFieldName);
						}						
					}
				}							
			}

			/*******************************************************************
			'*	Function Name	:	Validate_SSN() 			
			'*	Author			:	Ashwani
			'*	Purpose			:   To convert the Social Security Number format ########### 
			'*						to the format ###-##-####
			'*	Creation Date	:   May 04,2005		
			'*******************************************************************/
			function FormatSSN()
			{				
				var szFieldName = event.srcElement.id;
				var szFormName = document.forms[0].name;
				var szSSNNo = eval(szFormName + "." + szFieldName).value; // get the field value
				var arrSSN;
				var intCount;
				var szStrippedSSN;
				var FieldToStore;	
				szStrippedSSN = StripDash(szSSNNo);
				if(szSSNNo.length == 9 || szStrippedSSN.length == 9)
				{
					if (TrimTheString(szSSNNo) != "" ||  TrimTheString(szStrippedSSN) != "")
					{
						szStrippedSSN = StripDash(szSSNNo);
						FirstPart = szStrippedSSN.substr(0,3)
						SecondPart = szStrippedSSN.substr(3,2)
						ThirdPart = szStrippedSSN.substr(5,4)
						FullSSN = FirstPart + "-" + SecondPart + "-" + ThirdPart;
						eval('document.'+szFormName+'.'+szFieldName).value = FullSSN;
						
						//Disabling the associated validator
						if (szFieldName.length > 3)
						{
							document.getElementById("rev" + szFieldName.substring(3)).isvalid = true;
							ValidatorUpdateDisplay(document.getElementById("rev" + szFieldName.substring(3)));
						}						
					}
				}							
			}		
		
		/*******************************************************************
		'*	Function Name	:	FormatPhone() 
		'*	Parameters		:	
		'*	Author			:	Ashwani
		'*	Purpose			:   To convert the phone/fax format ########## 
		'*						to the format(###)###-####
		'*	Creation Date	:   31 March,2005
		'*  Modified  Date	:   27 march 2008 Praveen kasana					 
		'*	Returns			:    
		'*******************************************************************/
		function FormatPhone()
		{		
			var szFieldName = event.srcElement.id;
			var szFormName = document.forms[0].name;
			var szPhoneNo = eval(szFormName + "." + szFieldName).value;
			var szStrippedPhone; 
			var strLength;
			var strCharAtPos
					
			szPhoneNo = TrimTheString(szPhoneNo) // store the phone no after triming
			strLength = szPhoneNo.length; //store the length of the phone no.
			strCharAtPos = "";
			 // change by praveer for itrack 458
			if (iLangID == 2) {

			    if (szPhoneNo != "") {
			        szPhoneNo = szPhoneNo.replace(/[(]/g, "");
			        szPhoneNo = szPhoneNo.replace(/[)]/g, "");
			        szPhoneNo = szPhoneNo.replace("/", "");
			        szPhoneNo = szPhoneNo.replace(/[-]/g, "");
			        num = szPhoneNo.length;
			        if (num == 10) {
			            szPhoneNo = '(' + szPhoneNo.substr(0, 2) + ')' + szPhoneNo.substr(2, 4) + '-' + szPhoneNo.substr(6, 4);
			            eval('document.' + szFormName + '.' + szFieldName).value = szPhoneNo;
			            DisableValidatorsById(szFieldName);
			        }

			    }
			}
			else {
			    if (TrimTheString(szPhoneNo) != "") {
			        {
			            szPhoneNo = TrimTheString(szPhoneNo)
			            strLength = szPhoneNo.length;
			            szPhoneNo = StripDashAndBracket(szPhoneNo);
			            if ((szPhoneNo.length > 10 || szPhoneNo.length < 10)) {
			                //eval('document.'+szFormName+'.'+szFieldName).focus();						
			            }
			            if (szPhoneNo.length == 11) {

			                var OnePreFix = szPhoneNo.substr(0, 1)
			                //alert(OnePreFix)
			                var FirstPart = szPhoneNo.substr(1, 3);
			                //alert(FirstPart)
			                var SecondPart = szPhoneNo.substr(4, 3);
			                //alert(SecondPart)
			                var ThirdPart = szPhoneNo.substr(7, 4);

			                if (!(isNaN(FirstPart) || isNaN(SecondPart) || isNaN(ThirdPart) || isNaN(OnePreFix))) {
			                    if (OnePreFix == 1) {
			                        FullPhone = OnePreFix + "(" + FirstPart + ")" + SecondPart + "-" + ThirdPart;
			                        eval('document.' + szFormName + '.' + szFieldName).value = FullPhone;
			                        DisableValidatorsById(szFieldName);
			                    }
			                }
			                else {
			                    //eval('document.'+szFormName+'.'+szFieldName).focus();
			                }
			            }
			            else if (szPhoneNo.length == 10) {
			                var FirstPart = szPhoneNo.substr(0, 3);
			                var SecondPart = szPhoneNo.substr(3, 3);
			                var ThirdPart = szPhoneNo.substr(6, 4);
			                if (!(isNaN(FirstPart) || isNaN(SecondPart) || isNaN(ThirdPart))) {
			                    FullPhone = "(" + FirstPart + ")" + SecondPart + "-" + ThirdPart;
			                    //alert(FullPhone)
			                    eval('document.' + szFormName + '.' + szFieldName).value = FullPhone;
			                    DisableValidatorsById(szFieldName);
			                }
			                else {
			                    //eval('document.'+szFormName+'.'+szFieldName).focus();
			                }
			            }
			        }
			    }
			}
			ValidatorOnChange(); 
		}

		/*******************************************************************
		'*	Function Name	:	FormatBrazilPhone() 
		'*	Parameters		:	
		'*	Author			:	Lalit Kumar Chauhan
		'*	Purpose			:   To convert the phone/fax format ######## or ############
		'*						to the format ####-#### or ##-##-####-####
		'*	Creation Date	:   05 May,2010
		'*  Modified  Date	:   
		'*	Returns			:  Formated Phone No  
		'*******************************************************************/
		function FormatBrazilPhone() {	 
		    var szFieldName = event.srcElement.id;
		    var szFormName = document.forms[0].name;
		    var szPhoneNo = eval(szFormName + "." + szFieldName).value;
		    var szStrippedPhone;
		    var strLength;
		    var strCharAtPos		 
		    szPhoneNo = TrimTheString(szPhoneNo) // store the phone no after triming
		    strLength = szPhoneNo.length; //store the length of the phone no.
		    strCharAtPos = "";
		    // change by praveer for itrack 458
		    if (iLangID == 2) {

		        if (szPhoneNo != "") {
		            szPhoneNo = szPhoneNo.replace(/[(]/g, "");
		            szPhoneNo = szPhoneNo.replace(/[)]/g, "");
		            szPhoneNo = szPhoneNo.replace("/", "");
		            szPhoneNo = szPhoneNo.replace(/[-]/g, "");
		            num = szPhoneNo.length;
		            if (num == 10) {
		                szPhoneNo = '(' + szPhoneNo.substr(0, 2) + ')' + szPhoneNo.substr(2, 4) + '-' + szPhoneNo.substr(6, 4);
		                eval('document.' + szFormName + '.' + szFieldName).value = szPhoneNo;
		                DisableValidatorsById(szFieldName);
		            }

		        }
		    }
		    else {
		        if (TrimTheString(szPhoneNo) != "") {
		            {
		                szPhoneNo = TrimTheString(szPhoneNo)
		                strLength = szPhoneNo.length;
		                szPhoneNo = StripDashAndBracket(szPhoneNo);
		                if ((szPhoneNo.length > 12 || szPhoneNo.length < 8)) {
		                    //eval('document.'+szFormName+'.'+szFieldName).focus();						
		                }
		                if (szPhoneNo.length == 12) {
		                    var FirstPart = szPhoneNo.substr(0, 2);
		                    var SecondPart = szPhoneNo.substr(2, 2);
		                    var ThirdPart = szPhoneNo.substr(4, 4);
		                    var FourthPart = szPhoneNo.substr(8, 4);

		                    if (!(isNaN(FirstPart) || isNaN(SecondPart) || isNaN(ThirdPart) || isNaN(FourthPart))) {
		                        FullPhone = FirstPart + "-" + SecondPart + "-" + ThirdPart + "-" + FourthPart;
		                        eval('document.' + szFormName + '.' + szFieldName).value = FullPhone;
		                        DisableValidatorsById(szFieldName);
		                    }
		                }
		                else if (szPhoneNo.length == 8) {
		                    var FirstPart = szPhoneNo.substr(0, 4);
		                    var SecondPart = szPhoneNo.substr(4, 4);

		                    if (!(isNaN(FirstPart) || isNaN(SecondPart))) {
		                        FullPhone = FirstPart + "-" + SecondPart;
		                        eval('document.' + szFormName + '.' + szFieldName).value = FullPhone;
		                        DisableValidatorsById(szFieldName);
		                    }
		                }
		            }
		        }
		    }
		    ValidatorOnChange();
		}
		
		/*******************************************************************
		'*	Function Name	:	FormatZip() 
		'*	Parameters		:	
		'*	Author			:	Anurag
		'*	Purpose			:   To convert the zip format ########## 
		'*						to the format #####-####
		'*	Creation Date	:   19 May,2005
		'*  Modified  Date	:					 
		'*	Returns			:    
		'*******************************************************************/
		function FormatZip()
		{
			var szFieldName = event.srcElement.id;
			var szFormName = document.forms[0].name;
			var szZipNo = eval(szFormName + "." + szFieldName).value;
			
			//added by vijay joshi on 17-04-2006
			if (szZipNo == "")
				return;
			
			var szStrippedZip; 
			var strLength;
			var strCharAtPos
					
			szZipNo = TrimTheString(szZipNo) // store the phone no after triming
			strLength = szZipNo.length; //store the length of the phone no.
			strCharAtPos = "";
			
			if(TrimTheString(szZipNo) != "")
			{
				{
					szZipNo = TrimTheString(szZipNo)
					strLength = szZipNo.length;
					szZipNo = StripDashAndBracket(szZipNo);					
					if((szZipNo.length > 9) || (szZipNo.length < 5) || ((szZipNo.length > 5) && (szZipNo.length < 9)))
					{
						//eval('document.'+szFormName+'.'+szFieldName).focus();						
					}
					else
					{
						if(szZipNo.length==9)
						{
							var FirstPart = szZipNo.substr(0,5);
							var SecondPart = szZipNo.substr(5,4);
						}
											
						if (!(isNaN(FirstPart) || isNaN(SecondPart)))
						{	
							FullZip = FirstPart + "-" + SecondPart;
							eval('document.'+szFormName+'.'+szFieldName).value=FullZip;
							DisableValidatorsById(szFieldName);										
						}
						else
						{
							//eval('document.'+szFormName+'.'+szFieldName).focus();
						}
					}					
				}															
			}
			ValidatorOnChange(); 
		}	
		
		/*******************************************************************
		'*	Function Name	:	TrimTheString() 
		'*	Parameters		:	stringToTrim
		'*	Author			:	Ashwani
		'*	Purpose			:   Trim the string 
		'*						
		'*	Creation Date	:   31 March,2005
		'*  Modified  Date	:					 
		'*	Returns			:   return the string after triming it. 
		'*******************************************************************/
		function TrimTheString(stringToTrim)
		{
			var flag = true;
			var i = 0;
			if (IsWhitespace(stringToTrim)==true)
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
		
		// Remove the whitespaces
		function IsWhitespace(StringToCheck)
		{
			var reWhitespace = /^\s+$/;
			return (isEmpty(StringToCheck) || reWhitespace.test(StringToCheck));
		}		
		
		// check whether string is empty or not
		function isEmpty(StringToCheck)
		{   
			return ((StringToCheck == null) || (StringToCheck.length == 0));
		}	
			
		
		/*******************************************************************
		'*	Function Name	:	StripDashAndBracket() 
		'*	Parameters		:	szSSNNo as Input variable to be formated
		'*	Author			:	Ashwani
		'*	Purpose			:   To format the phone/fax number field to remove dash and 
		'*						brackets and used within the function
		'*	Creation Date	:   01 April,2005				 
		'*	Returns			:   return the string after formatting. 
		'*******************************************************************/		
		function StripDashAndBracket(szSSNNo)
		{
			var szStrippedSSN;
			var intCount;
			szStrippedSSN = "";
			var arrSSN;
			if (szSSNNo != "")
			{
				arrSSN = szSSNNo.split("-");
				for(intCount = 0; intCount < arrSSN.length; intCount++)
				{
					szStrippedSSN = szStrippedSSN + arrSSN[intCount];
				}
				arrSSN1 = szStrippedSSN.split("(");
				szStrippedSSN = "";
				for(intCount = 0; intCount < arrSSN1.length; intCount++)
				{
					szStrippedSSN = szStrippedSSN + arrSSN1[intCount];

				}
				arrSSN2 = szStrippedSSN.split(")");
				szStrippedSSN = "";
				for(intCount = 0; intCount < arrSSN2.length; intCount++)
				{
					szStrippedSSN = szStrippedSSN + arrSSN2[intCount];
				}
			}
			return(szStrippedSSN);
		}
		/*******************************************************************
		'*	Function Name	:	DisableValidatorsById() 
		'*	Parameters		:	Control as input parameter
		'*	Author			:	Ashwani
		'*	Purpose			:   Disable only that validator which is associated 
		'*						with the specified control
		'*	Creation Date	:   08 April,2005	
		'*******************************************************************/		
		function DisableValidatorsById(Control)
		{
			var ctr=0;
			//if(typeof('Page_Validators') != null)
			if(typeof(Page_Validators) != 'undefined')
			{
				for( ctr=0;ctr<Page_Validators.length;ctr++)
				{
					if (Page_Validators[ctr].controltovalidate == Control)
					{
						Page_Validators[ctr].isvalid=true;
						ValidatorUpdateDisplay(Page_Validators[ctr]);
					}
				}
			}
		}
		
		/*******************************************************************
		'*	Function Name	:	FormatDate() 
		'*	Parameters		:	szFormName, szFieldName
		'*	Author			:	Ashwani
		'*	Purpose			:   To convert the entered date format ###### or ######## 
		'*						into format ##/##/####.
		'*	Creation Date	:   01 April,2005	
		'*******************************************************************/
		function FormatDate()
		{			
			var strDate;
			var strDay;
			var strMonth;
			var strYear;
			var objVal;
			var dtDSep;
			var szFieldName = event.srcElement.id;
			var szFormName = document.forms[0].name;			

			ObjVal =eval('document.'+szFormName+'.'+szFieldName);
			dtDateValue =ObjVal.value;
			if (dtDateValue=="")
			{
			   //ValidatorOnChange(); 
			   return true;
			}

	   		dtDSep = GetDateSeparator(dtDateValue);

	   		if(dtDSep == "")
	   		{
	   			dtDSep = "/";
	   		}
	   		else if(dtDSep == " ")
	   		{
	   			dtDSep = "/";
	   			dtDateValue = ReplaceString(dtDateValue," ", "/");
	   		}
			
			if (IsProperDate(ObjVal)==true)
			{
				// After coming out of 'IsProperDate' function if the date value contains '/' then 
				// it means that the value is of correct format. So this value is returned
				if (FindChar(dtDateValue,dtDSep)==true)
				{
					strDate = dtDateValue;
				}
				else if(dtDateValue.length == 8)
				{
				    //Added for Multilingual Support
				    if (sCultureDateFormat == 'DD/MM/YYYY')
				    {
				        strDay = dtDateValue.charAt(0) + dtDateValue.charAt(1);
				        strMonth = dtDateValue.charAt(2) + dtDateValue.charAt(3);
				        strYear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
				        strDate = strDay + dtDSep + strMonth + dtDSep + strYear;					    
					}
					else
					{
					    strMonth = dtDateValue.charAt(0) + dtDateValue.charAt(1);
					    strDay = dtDateValue.charAt(2) + dtDateValue.charAt(3);
					    strYear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
					    strDate = strMonth + dtDSep + strDay + dtDSep + strYear;
					}
				}
				else if (dtDateValue.length == 6)
				{
				    //Added for Multilingual Support
				    if (sCultureDateFormat == 'DD/MM/YYYY') 
				    {
				        strDay = "0" + dtDateValue.charAt(0);
				        strMonth = "0" + dtDateValue.charAt(1);
				        strYear = dtDateValue.charAt(2) + dtDateValue.charAt(3) + dtDateValue.charAt(4) + dtDateValue.charAt(5);
				        strDate = strDay + dtDSep + strMonth + dtDSep + strYear;					    
					}
					else
					{
					    strMonth = "0" + dtDateValue.charAt(0);
					    strDay = "0" + dtDateValue.charAt(1);
					    strYear = dtDateValue.charAt(2) + dtDateValue.charAt(3) + dtDateValue.charAt(4) + dtDateValue.charAt(5);
					    strDate = strMonth + dtDSep + strDay + dtDSep + strYear;
					}							 
				}			
			    ObjVal.value=strDate;
			    DisableValidatorsById(szFieldName);
			    ValidatorOnChange(); 
				return true ;				 	
			}
			else
			{  
			  	ValidatorOnChange(); 
				return false;
			}
		}			
		
		/*******************************************************************
		'*	Function Name	:	GetDateSeparator() 
		'*	Parameters		:	dtDateValue as Date value
		'*	Author			:	Ashwani
		'*	Purpose			:   To extract the separator of the date
		'*	Creation Date	:   01 April,2005
		'*  Modified  Date	:					 
		'*	Returns			:   Date Separator
		'*******************************************************************/
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
		
		/*******************************************************************
		'*	Function Name	:	ReplaceString() 
		'*	Parameters		:	lstrString,lcharReplaceChar,lstrReplaceWith
		'*	Author			:	Ashwani
		'*	Purpose			:   Replace a charecter in a string with the given charecter
		'*	Creation Date	:   01 April,2005
		'*  Modified  Date	:					 
		'*	Returns			:   Return text after replacing 
		'*******************************************************************/
		function ReplaceString(lstrString, lcharReplaceChar, lstrReplaceWith)
		{			
			if (lstrString == "" || lstrString == null)
			{
				return "";
			}
			else
			{
				var lintcounter, lintcurrLoc, retText, lintStringLength
				retText = "";
				for (lintcounter =0 ; lintcounter < lstrString.length; lintcounter++)
					if (lstrString.charAt(lintcounter) == lcharReplaceChar)
						retText = retText + lstrReplaceWith;
					else
						retText = retText + lstrString.charAt(lintcounter);
				return retText;
			}
		}

		/*******************************************************************
		'*	Function Name	:	IsProperDate() 
		'*	Parameters		:	ctr
		'*	Author			:	Ashwani
		'*	Purpose			:   To check that date must be in proper format
		'*	Creation Date	:   01 April,2005		
		'*******************************************************************/
		function IsProperDate(ctr)
	   	{
	   		var strDateFormat = 'U';
	   		var intDateValueLength = ctr.value.length;
	   		var dtDSep;
	   		var	dtDateValue = ctr.value;
	   		var boolget = true;
			/*
	   		Extracting the separator
	   		If the separator is not present the default separator would be "/"
	   		*/
	   		dtDSep = GetDateSeparator(dtDateValue);
	   		if(dtDSep =="")
	   			dtDSep = "/";
	   		// End of Validation Message
						
			// Here we are assuming that for all the countries maximum date length will be 10
			// So no need of chaning this according to Country
			if (intDateValueLength > 10)
			{
			     //alert("Date length cannot exceed 10 characters");
			     ctr.focus();
			     return false;
			}
					     
			if(intDateValueLength!=10 && intDateValueLength!=9 && intDateValueLength!=8 && intDateValueLength!=6)
			{
			    ctr.focus();
			   return false;
			}			
			
			if(intDateValueLength==10)
			{
				if ((dtDateValue.charAt(2) != dtDSep) || (dtDateValue.charAt(5) != dtDSep))
		 		{
					ctr.focus();
					return false;
				}

				//Added for Multilingual Support
				if (sCultureDateFormat == 'DD/MM/YYYY') {

				    numday = dtDateValue.charAt(0) + dtDateValue.charAt(1);
				    nummonth = dtDateValue.charAt(3) + dtDateValue.charAt(4);
				    numyear = dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8) + dtDateValue.charAt(9);	    
				}
				else {
				    nummonth = dtDateValue.charAt(0) + dtDateValue.charAt(1);
				    numday = dtDateValue.charAt(3) + dtDateValue.charAt(4);
				    numyear = dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8) + dtDateValue.charAt(9);
				}
			}

			if(ctr.value.length==8)
			{
				if ((dtDateValue.charAt(1) != dtDSep) || (dtDateValue.charAt(3) != dtDSep))
				{
				   if (FindChar(dtDateValue,dtDSep)==true)
				   {
					ctr.focus();
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
				           nummonth = dtDateValue.charAt(0) + dtDateValue.charAt(1);
				           numday = dtDateValue.charAt(2) + dtDateValue.charAt(3);
				           numyear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
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
				        nummonth = dtDateValue.charAt(0);
				        numday = dtDateValue.charAt(2);
				        numyear = dtDateValue.charAt(4) + dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7);
				    }
				}
			}
			if(dtDateValue.length==9)
			{
				if (((dtDateValue.charAt(1) != dtDSep) || (dtDateValue.charAt(4) != dtDSep)) &&((dtDateValue.charAt(2) != dtDSep) || (dtDateValue.charAt(4) != dtDSep)))
				{
				   ctr.focus();
				   return false;
				}
				if (dtDateValue.charAt(1) == dtDSep) {

				    //Added for Multilingual Support
				    if (sCultureDateFormat == 'DD/MM/YYYY') {
				        numday = dtDateValue.charAt(0);
				        nummonth = dtDateValue.charAt(2) + dtDateValue.charAt(3);
				        numyear = dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8);
				    }
				    else {
				        nummonth = dtDateValue.charAt(0);
				        numday = dtDateValue.charAt(2) + dtDateValue.charAt(3);
				        numyear = dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8);
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
				        nummonth = dtDateValue.charAt(0) + dtDateValue.charAt(1);
				        numday = dtDateValue.charAt(3);
				        numyear = dtDateValue.charAt(5) + dtDateValue.charAt(6) + dtDateValue.charAt(7) + dtDateValue.charAt(8);
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
			        nummonth = dtDateValue.charAt(0);
			        numday = dtDateValue.charAt(1);
			        numyear = dtDateValue.charAt(2) + dtDateValue.charAt(3) + dtDateValue.charAt(4) + dtDateValue.charAt(5);
			    }
			}			
				
			if (numyear.length < 4)
			{
				//alert("Year format is YYYY");
				return false;
			}

			if (iLangID == 2) {
			    if (!IsVal("Dia valor na data", numday)) {
			        return false;
			    }
			    if (!IsVal("M" + String.fromCharCode(234) + "s o valor na data", nummonth)) {
			        return false;
			    }
			    if (!IsVal("valor do ano em data", numyear)) {
			        return false;
			    }
			}
			else {
			    if (!IsVal("Day value in date", numday)) {
			        return false;
			    }
			    if (!IsVal("Month value in date", nummonth)) {
			        return false;
			    }
			    if (!IsVal("Year value in date", numyear)) {
			        return false;
			    }
			}

			day = parseInt(numday,10);
			month = parseInt(nummonth,10);
			year = parseInt(numyear, 10);
			
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

			if (day > 31 ) {

			    if (iLangID == 2) {
			        alert("Dia n" + String.fromCharCode(227) + "o pode ser superior a 31");
			    }
			    else {
			        alert("Day cannot be greater than 31");
			    }
			      ctr.focus();
			     return false;
			}

			if ((month==4)||(month==6)||(month==9)||(month==11))
			{
				if (day > 30 ) {

				    if (iLangID == 2) {
				        alert("Dia n" + String.fromCharCode(227) + "o pode ser superior a 30");
				    }
				    else {
				        alert("Day cannot be greater than 30");
				    }
			        ctr.focus();
			        return false;
			    }
			}
			
			if (month==2)
			{
				if  ((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0) ) ) 
			    {
					if (day > 29) {

					    if (iLangID == 2) {
					        alert("Dia n" + String.fromCharCode(227) + "o pode ser maior que 29 para um ano bissexto");
					    }
					    else {
					        alert("Day cannot be greater than 29 for a Leap Year");
					    }
						 ctr.focus();
			            return false;
			        }					               
			    }  
			    else     
			    {
			        if (day > 28) {

			            if (iLangID == 2) {
			                alert("O ano n" + String.fromCharCode(227) + "o " + String.fromCharCode(233) + " bissexto. Data n" + String.fromCharCode(227) + "o pode ser superior a 28 de fevereiro."); //By sneha or iTrack 1407    //"Dia n" + String.fromCharCode(227) + "o pode ser maior que 28 para um ano bissexto n" + String.fromCharCode(227) + "o"
			            }
			            else
			            {
						    alert("Day cannot be greater than 28 for a non-leap year");
						}
						ctr.focus();
			            return false;
			        } 
			    }    
			}  
		return true;
		}
		
		/*******************************************************************
		'*	Function Name	:	IsVal() 
		'*	Parameters		:	msg,val
		'*	Author			:	Ashwani
		'*	Purpose			:   To check whether entered value is 
		'*						numeric or not.
		'*	Creation Date	:   01 April,2005		
		'*******************************************************************/
		function IsVal(msg,val)	
		{
			if (isNaN(val))
			{
			    var write;
			    if (iLangID == 2) {
			        write = msg + " tem de ser num" + String.fromCharCode(233) + "rico";			        
			    }
			    else {
			        write = msg + " has to be Numeric";
			    }
				//alert(write);
				return false;
			}
			return true;
		}
		
		/*******************************************************************
		'*	Function Name	:	FindChar() 
		'*	Parameters		:	strValue,strChar
		'*	Author			:	Ashwani
		'*	Purpose			:   To find whether there is '/' character present in the date value.
		'*	Creation Date	:   01 April,2005		
		'*******************************************************************/
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
		
		// Replaces text with by in string
		function replaceAll(string,text,by) 
		{
			var strLength = string.length, txtLength = text.length;
			if ((strLength == 0) || (txtLength == 0)) return string;

			var i = string.indexOf(text);
			if ((!i) && (text != string.substring(0,txtLength))) return string;
			if (i == -1) return string;

			var newstr = string.substring(0,i) + by;

			if (i+txtLength < strLength)
				newstr += replaceAll(string.substring(i+txtLength,strLength),text,by);

			return newstr;
		}
		
		/*******************************************************************
		'*	Function Name	:	HideTabControlTab() 
		'*	Parameters		:	tabNum
		'*	Author			:	Vijay
		'*	Purpose			:   To Hide any tab of tab control, tab will hide next time tab created
		'*	Creation Date	:   12 Dec 2005		
		'*******************************************************************/
		function HideTabControlTab(frame,tabNum)
		{
			var num = (tabNum -1)*4;
			
			//Setting the hide flag to Y so that tab control will not 
			//make this tab 
			frame.arrMainTab[num + 3] =	'Y';
		}		
		
		/*******************************************************************
		'*	Function Name	:	DrawTab() 
		'*	Parameters		:	tabNum,frame,TabName,Url
		'*	Author			:	Ashwani
		'*	Purpose			:   To add the tab 
		'*	Creation Date	:   20 April,2005
		'*	modified by 	:   Pravesh K Chandel
		'*	Creation Date	:   6 July,2010
        '*	Purpose			:   Implement ScreenId of Tab's screen
		'*******************************************************************/
	
		function DrawTab(tabNum, frame, TabName, Url, ScreenId,activeTab,notLoad, hideStatus)
		{
			if(activeTab==null)
				activeTab = 0;
				
			var num = (tabNum -1)*4;
			frame.arrMainTab[num] = TabName;
			frame.arrMainTab[num + 1] = Url;
			frame.arrMainTab[num + 2] = 0;
			
			if (hideStatus == null)
				frame.arrMainTab[num + 3] = '';
			else
			    frame.arrMainTab[num + 3] = hideStatus;
			if ((frame.arrScreenIds[tabNum - 1] == null || frame.arrScreenIds[tabNum - 1] == "undefined" || frame.arrScreenIds[tabNum - 1] == "") && ScreenId != null)
			    frame.arrScreenIds[tabNum - 1] = ScreenId;
			if (notLoad == true || notLoad == null || notLoad == 'undefined')
			{
			    frame.changeTab(0,activeTab,1,1); 	    //Passing the forth argument, for suppressing the save message
			}
			else
			    frame.changeTab(0,activeTab,null,1);    //Passing the forth argument, for suppressing the save message			
		}
			
		/*******************************************************************
		'*	Function Name	:	RemoveTab() 
		'*	Parameters		:	tabNum,frame
		'*	Author			:	Ashwani
		'*	Purpose			:   Remove the tab
		'*	Creation Date	:   20 April,2005		
		'*******************************************************************/
		function RemoveTab(tabNum,frame)
		{
			newArrMainTab = new Array();			
			var ctr,arrCtr=0;
			for(ctr=1;ctr<tabNum;ctr++)
			{
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4];
				arrCtr++;
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4+1];
				arrCtr++;
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4+2];
				arrCtr++;
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4+3];
				arrCtr++;
			}
			
			var tabCount = (frame.arrMainTab.length) / 4;
			
			for(ctr=tabNum+1;ctr <= tabCount ;ctr++)
			{
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4];
				arrCtr++;
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4+1];
				arrCtr++;
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4+2];
				arrCtr++;
				newArrMainTab[arrCtr] = frame.arrMainTab[(ctr -1)*4+3];
				arrCtr++;
			}
			
			frame.arrMainTab = null;
			frame.arrMainTab = newArrMainTab;
			frame.changeTab(0,0,1,1);	
		}
		
		/*******************************************************************
		'*	Function Name	:	PullCustomerAddress() 
		'*	Parameters		:	add1,add2,city,country,state,zip[Basically address controls id]
		'*	Author			:	Vijay
		'*	Purpose			:   To pull the client addres from hiCustAddXml hidden control
		'*	Creation Date	:   27 May 2005		 
		'*******************************************************************/
		function PullCustomerAddress(add1, add2, city, country, state, zip, county, number, dist, territory)
		{
			if (document.forms[0].hidCustAddXml.value == "")
				return false;
			//alert(top.frames[0].strLobId);
			var tempXML = document.forms[0].hidCustAddXml.value;
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('address')[0]);
	
			var i=0;
			for(i=0;i<tree.childNodes.length;i++)
			{
				if(!tree.childNodes[i].firstChild) continue;
				
				var nodeName = tree.childNodes[i].nodeName;
				var nodeValue = tree.childNodes[i].firstChild.text;
				var ctrl;
				
				ctrl = null;
				switch(nodeName)
				{
					case "address1":
						ctrl = add1;
						break;
					case "address2":
						ctrl = add2;
						break;
					case "city":
						ctrl = city;
						break;
					case "country":
						ctrl = country;
						break;
					case "state":
						ctrl = state;
						break;
					case "zip":
						ctrl = zip;
						break;
				    case "NUMBER":
				        ctrl = number;
				        break;
				    case "DISTRICT":
				        ctrl = dist;
				        break;
				}
				
				if (ctrl != null)
				{
				    if (nodeName == "state" || nodeName == "country") {
				        if (nodeName == "country") {
				            SelectComboOption(ctrl.id, nodeValue);
				        } else if (nodeName == "state") {
				            try {
				                if (document.getElementById('hidSTATE_ID')) {
				                    document.getElementById('hidSTATE_ID').value = nodeValue;
				                }
				                fillstateFromCountry();
				            } catch (err) { }

				        }
				    }
				    else {
				        //if (ctrl.value == "")//Done for Itrack Issue 5820 on 21 May 2009
				        //{
				        ctrl.value = nodeValue;
				        //}
				    }
				}				
			}
			if (document.forms[0].hidCustAddXml.value == "")
				return false;
			if(county!=null ||territory!=null)
			ShowCounty(county,territory);
			DisableValidators();
			ChangeColor();
			return false;
		}
		
		/*******************************************************************
		'*	Function Name	:	ShowCounty() 
		'*	Parameters		:	county
		'*	Author			:	Gaurav
		'*	Purpose			:   To pull the client addres from hiCustAddXml hidden control
		'*	Creation Date	:   27 May 2005		  
		'*******************************************************************/
		function ShowCounty(county,territory)
		{
			if (document.forms[0].hidCountyXML.value == "")
				return false;
			
			var tempXML = document.forms[0].hidCountyXML.value;
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('COUNTYXML')[0]);
			var i=0;
			
			for(i=0;i<tree.childNodes.length;i++)
			{
				if(!tree.childNodes[i].firstChild) continue;
				
				var nodeName = tree.childNodes[i].nodeName;
				var lob = tree.childNodes[i].getAttribute('LOBID');
				
				//alert(lob + "  =  " + top.frames[0].strLobId);
				
				if (lob == top.frames[0].strLobId)
				{
				//alert(county.value);
					if(county!=null && county.value=="")
						if (tree.childNodes[i].childNodes.length > 0)	
							if (tree.childNodes[i].childNodes[0].firstChild != null)
								county.value = tree.childNodes[i].childNodes[0].firstChild.text;
					//alert(territory.value);
					if(territory!=null && territory.value=="")
						if (tree.childNodes[i].childNodes.length > 0)
							if (tree.childNodes[i].childNodes[0].firstChild != null)
								territory.value = tree.childNodes[i].childNodes[1].firstChild.text;					
					
				}
			}
			DisableValidators();
			ChangeColor();
			return false;
		}		
		
		/*******************************************************************
		'*	Function Name	:	OpenLookupWindow 
		'*	Parameters		:	add1,add2,city,country,state,zip[Basically address controls id]
		'*	Author			:	Pradeep
		'*	Purpose			:   To show Look up window
		'*	Creation Date	:   27 May 2005		
		'*******************************************************************/
		function OpenLookupWindow(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Arg1,Arg2,Arg3,Arg4)
		{			
			var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
								DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
								'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
								'&Arg1=' + Arg1 + '&Arg2=' + Arg2 + '&Arg3=' + Arg3 + '&Arg4=' + Arg4,
								'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' );
			//win.document.title = Title;			
		}
		
		var lookupTitle;		
			/*******************************************************************
		'*	Function Name	:	OpenLookupWindow 
		'*	Parameters		:	add1,add2,city,country,state,zip[Basically address controls id]
		'*	Author			:	Pradeep
		'*	Purpose			:   To show Lokk up window
		'*	Creation Date	:   27 May 2005
		'*  Modified  Date	:					 
		'*	Returns			:   
		"Args" to be passed as @CustID=1;@CustName='pradeep' and so on
		'*******************************************************************/
		function OpenLookup(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args)
		{		
			lookupTitle = Title;
			var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
								DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
								'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
								'&Args=' + Args,
								'review','height=500, width=500,status= no, resizable= no, scrollbars=no, toolbar=no,location=no,menubar=no' );
			
			//win.document.title = Title;			
		}
		
			/*******************************************************************
		'*	Function Name	:	OpenLookupWindow 
		'*	Parameters		:	add1,add2,city,country,state,zip[Basically address controls id]
		'*	Author			:	Praveen kasana
		'*	Purpose			:   To show Lokk up window in Center Screen*/
		
		function OpenLookupCenterScreen(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args)
		{
			lookupTitle = Title;
			
		a1 = 1;			
		wWidth=500;wHeight=500;
		relWid=(a1?screen.width:window.innerWidth)/2; relHgh=(a1?
		screen.height:window.innerHeight)/2
		wLeft=(relWid>wWidth/2)?relWid-wWidth/2:screenX
		wTop=(relHgh>wHeight/2)?relHgh-wHeight/2:screenY+10
		window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
								DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
								'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
								'&Args=' + Args,
								'review',"left="+wLeft+",top="+wTop	+",width="+wWidth+",height="+wHeight)
					
		}
		
		//Specifies the title of the lookup window	
		
function OpenLookupWithFunction(Url,DataValueField,DataTextField,DataValueFieldID,DataTextFieldID,LookupCode,Title,Args,JSFunction) {
   
	lookupTitle = Title;
	var win = window.open(Url + '?DataValueField=' + DataValueField + '&DataTextField=' + 
						DataTextField + '&DataValueFieldID=' + DataValueFieldID + 
						'&DataTextFieldID=' + DataTextFieldID + '&LookupCode=' + LookupCode + 
						'&Args=' + Args + '&JSFunction=' + JSFunction,
						'review','height=600, width=600,status= no, resizable=yes, scrollbars=no, toolbar=no,location=no,menubar=no' );
}
		
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
/*****************IsAnyActionPerformedOnPage***************
common method to fill controls from xml provided
Parameters:
			xml: Xml to parse
			objForm: Form object of the page.
Notes:
	1. The name of nodes of xml and ids of form controls must be same apart from the prefixes of the controls
	2. The Id of radio buttons must be in the format: rdb+XmlNodeName+NodeValue 
	3. Prefixes assumed: txt,cmb,rdb,chk,lbl,hid,lst

*/
function IsAnyActionPerformedOnPage()
{
	if(document.getElementById('hidOldData'))
		var xml = document.getElementById('hidOldData').value;
	else
		return false;
		
	//alert(xml);
	
	if(xml=="" || xml=="0" || xml=="<NewDataSet />")
		return false;
	var flag=false;
		if(xml!="")
		{
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(xml).getElementsByTagName('Table')[0]);
			
			var i=0;
			for(i=0;i<tree.childNodes.length;i++)
			{
						
				var nodeName = tree.childNodes[i].nodeName;
				var nodeValue ;
				
				if ( tree.childNodes[i].firstChild == 'undefined' || tree.childNodes[i].firstChild == null )
				{
					nodeValue = '';
				}
				else
				{
					nodeValue = tree.childNodes[i].firstChild.text;
				}
				
				//**** Setting Textfield value *********
				if(document.getElementById("txt"+nodeName))
				{
					
					if(document.getElementById("txt"+nodeName).value != nodeValue)
					{
						flag=true;
						break;
					}
				}				
				
			/*//**** Setting hidden field value *********
				if(document.getElementById("hid"+nodeName))
				{
					if(document.getElementById("hid"+nodeName).value != nodeValue)
					{
						flag=true;
						break;
					}
				}*/
				//**** Setting drop down list value *********
				if(document.getElementById("cmb"+nodeName) && document.getElementById("cmb"+nodeName).selectedIndex>-1)
				{
					var currentSelectedValue = document.getElementById("cmb"+nodeName).options[document.getElementById("cmb"+nodeName).selectedIndex].value;
					if(currentSelectedValue!=nodeValue)
					{
						flag=true;
						break;
					}
				}
				//**** Setting list Box value *********
				if(document.getElementById("lst"+nodeName))
				{
					if(IsAnyActionPerformedOnListBox("lst"+nodeName,nodeValue.split(',')))
					{
						flag=true;
						break;
					}
				}
				//**** Setting label value *********
				if(document.getElementById("lbl"+nodeName))
				{
					if(document.getElementById("lbl"+nodeName).innerText != nodeValue)
					{
						flag=true;
						break;
					}
				}
				//**** Setting checkbox value *********
				if(document.getElementById("chk"+nodeName))
				{
					if(nodeValue=="Y" || nodeValue=="1" || nodeValue==true)
					{
						if(document.getElementById("chk"+nodeName).checked != true)
						{
							flag=true;
							break;
						}
					}
					else 
					{
						if(document.getElementById("chk"+nodeName).checked != false)
						{
							flag=true;
							break;
						}
					}
				}
				//**** Setting radio button value *********
				if(document.getElementById("rdb"+nodeName+nodeValue))
				{
					if(document.getElementById("rdb"+nodeName+nodeValue).checked != true)
					{
						flag=true;
						break;
					}
				}
			}
		return flag;
		
		}		
}
/****************** IsAnyActionPerformedOnListBox ****************
Created by : Ajit Singh Chahal, 22/6/2005
Description: Is useded to select multiple options of a listbox.
Parameters:  listId:- javascript Id of listbox,
			 selectedValues array of values to be selected in list box.
Note: All options whose value equal to any of the elements of the selectedValues array get selected.
*/
function IsAnyActionPerformedOnListBox(listId,selectedValues)
{
var flag=false;
	for(var j=0; j<document.getElementById(listId).options.length; j++)
	{
	for(var i=0;i<selectedValues.length;i++)
	{
		if(document.getElementById(listId).options[j].value == selectedValues[i])
		{
			if(document.getElementById(listId).options[j].selected == false)
			{
				return true;
			}
			else
				break;
		}
	}
	}
}
//added by Ajit
function RemoveAllOccrancesOfChar(Text,Char)
{
	Text = Text+"";//convertin to string for string operation.
	while(Text.indexOf(',')>-1)
				Text = Text.replace(Char,"")
	return Text;
}

function ChkTextAreaLength250(source, arguments)
{
	var txtArea = arguments.Value;
	if(txtArea.length > 250 ) 
	{
		arguments.IsValid = false;
		return;   // invalid userName
	}
}

// Look up Layer Code Start here -- Added by Anshuman on Aug 31, 2005
var globalLookupLayerId		=	'lookupLayer';
var globalLookupControlId;
var globalLookupLayerWidth = 220;
if (iLangID == 2) {
    var globalLookupMessage = 'Para adicionar valores neste campo, favor contatar o seu Administrador do Sistema.';//'Por favor, administrador de contato para adicionar um valor de pesquisa com a descri' + String.fromCharCode(231) + String.fromCharCode(227) + 'o desse c' + String.fromCharCode(243) + 'digo de categoria information.The para o valor ' + String.fromCharCode(233) + ' ';
}
else {
    var globalLookupMessage = 'Please contact administrator to add a lookup value with description for this information.The category code for the value is ';
}
var globalLookupMessageCode	=	'';
function showLookupLayer(controlId,messageText)
{
	globalLookupControlId	=	controlId;
	hideLookupLayer();
	if(messageText == '')
	{
	    lookupCompleteMessage = globalLookupMessage;// + globalLookupMessageCode; // By sneha for iTrack 1407
	}
	else
	{
		globalLookupMessageCode	=	messageText;
		lookupCompleteMessage = globalLookupMessage; // + messageText; // By sneha for iTrack 1407
	}
	document.getElementById("LookUpMsg").innerHTML	=	lookupCompleteMessage;
	setPositionLookupLayer();
	// show Lookup layer
	showFinalLookupLayer();
}
function hideLookupLayer()
{
	document.getElementById("lookupLayerFrame").style.display	=	'none';
	document.getElementById('lookupLayerIFrame').style.display	=	'none';
	document.getElementById(globalLookupLayerId).style.visibility = "hidden";
}
function setPositionLookupLayer()
{
	var leftPos, topPos;
	document.getElementById(globalLookupLayerId).style.width	=	globalLookupLayerWidth;
	document.getElementById(globalLookupLayerId).style.height	=	document.getElementById('LookUpMsg').offsetHeight + 20;
	
	// Set Left Position
	leftPos	=	getObjectOffsetLeft(document.getElementById(globalLookupControlId));
	if(document.body.offsetWidth < parseInt(leftPos) + globalLookupLayerWidth)
	{
		leftPos	=	document.body.offsetWidth - globalLookupLayerWidth
	}
	// Set Top Position
	topPos	=	getObjectOffsetTop(document.getElementById(globalLookupControlId)) + document.getElementById(globalLookupControlId).offsetHeight;
	
	if(top.botframe && top.botframe.document && top.botframe.document.getElementById('bodyHeight'))
	{
		if(top.botframe.document.getElementById('tabLayer'))
		{
			if((top.botframe.document.getElementById('bodyHeight').offsetHeight + top.botframe.document.getElementById('bodyHeight').scrollTop) - getObjectOffsetTop(top.botframe.document.getElementById('tabLayer')) < (topPos + document.getElementById(globalLookupLayerId).offsetHeight))
			{
				topPos	=	getObjectOffsetTop(document.getElementById(globalLookupControlId)) - document.getElementById(globalLookupLayerId).offsetHeight;
			}
		}
		else
		{
			if((top.botframe.document.getElementById('bodyHeight').offsetHeight + top.botframe.document.getElementById('bodyHeight').scrollTop) < (topPos + document.getElementById(globalLookupLayerId).offsetHeight))
			{
				topPos	=	getObjectOffsetTop(document.getElementById(globalLookupControlId)) - document.getElementById(globalLookupLayerId).offsetHeight;
			}
		}
	}
	// Set this left, top, width and height;
	document.getElementById(globalLookupLayerId).style.left		=	leftPos;
	document.getElementById(globalLookupLayerId).style.top		=	topPos;
	
	document.getElementById("lookupLayerFrame").style.left		=	document.getElementById(globalLookupLayerId).style.left;
	document.getElementById("lookupLayerFrame").style.top		=	document.getElementById(globalLookupLayerId).style.top;
	document.getElementById("lookupLayerFrame").style.width		=	document.getElementById(globalLookupLayerId).style.width;
	document.getElementById("lookupLayerFrame").style.height	=	document.getElementById(globalLookupLayerId).style.height;
	
	document.getElementById("lookupLayerIFrame").left			=	document.getElementById(globalLookupLayerId).style.left;
	document.getElementById("lookupLayerIFrame").top			=	document.getElementById(globalLookupLayerId).style.top;
	document.getElementById("lookupLayerIFrame").width			=	document.getElementById(globalLookupLayerId).style.width;
	document.getElementById("lookupLayerIFrame").height			=	document.getElementById(globalLookupLayerId).style.height;
	//alert(top.botframe.document.getElementById('bodyHeight').offsetHeight +','+ top.botframe.document.getElementById('bodyHeight').scrollTop +','+ topPos +','+ document.getElementById(globalLookupLayerId).style.height);
}
function showFinalLookupLayer()
{
	document.getElementById("lookupLayerFrame").style.display	=	'inline';
	document.getElementById('lookupLayerIFrame').style.display	=	'inline';
	document.getElementById(globalLookupLayerId).style.visibility	=	"visible";
}
function refreshLookupLayer()
{
	showLookupLayer(globalLookupControlId,'');
}
// Look up Layer Code End here

// Get object offset left -- Added by Anshuman on Aug 31, 2005
function getObjectOffsetLeft(el) 
{
	var x;
	// Return the x coordinate of an element relative to the page.
	x = el.offsetLeft;
	if (el.offsetParent != null)
		x += getObjectOffsetLeft(el.offsetParent);

	return x;
}
// Get object offset left ends here
// Get object offset top -- Added by Anshuman on Aug 31, 2005
function getObjectOffsetTop(el) 
{
	var y;
	// Return the x coordinate of an element relative to the page.
	y = el.offsetTop;
	if (el.offsetParent != null)
	{
		y += getObjectOffsetTop(el.offsetParent);
	}
	
	return y;
}
// Get object offset top ends here
//Following function is return to calculate difference in years between any given two dates
//Takes two dates and returns difference in years
function DateDiffernce(_firstDate,_secondDate) 
{
	FirstDate = new Date(_firstDate)
		
	dd = parseInt(FirstDate.getDate());
	mm = parseInt(FirstDate.getMonth()) + 1;
	yy = parseInt(FirstDate.getFullYear());	
	
	SecondDate = new Date(_secondDate);	
	gdate = SecondDate.getDate();
	gmonth = SecondDate.getMonth();
	gyear = SecondDate.getFullYear();
	if (gyear < 2000) gyear += 1900;
	age = gyear - yy;
	if ((mm == (gmonth + 1)) && (dd <= parseInt(gdate))) 
		{
		age = age; 
		} 
	else 
		{
		if (mm <= (gmonth)) 
			{
			age = age;
			} 
		else 
			{
			age = age - 1; 
   			}
		}
		
	if (age == 0) age = age;	
	return age;
}

/*******************************************************************
'*	Function Name	:	InsertDecimal() 
'*	Parameters		:	Amount (decimal value) to be formated
'*	Author			:	Vijay Joshi
'*	Purpose			:   To format entered amount( e.g. xxxxxx into xxxxxx.xx)
'*	Returns			:   formatted amount
'*******************************************************************/
//Used to format currency values
//Converts xxxxxx into xxxx.xx
function InsertDecimal(AmtValues) {

    //AmtValues = ReplaceAll(AmtValues, ".", "");
    AmtValues = ReplaceAll(AmtValues, sDecimalSep, "");
    
	DollarPart = AmtValues.substring(0, AmtValues.length - 2);
	CentPart = AmtValues.substring(AmtValues.length - 2);
	//tmp = formatCurrency( DollarPart) + "." + CentPart;
	tmp = formatCurrency(DollarPart) + sDecimalSep + CentPart;
	return tmp;	
}

		/*******************************************************************
		'*	Function Name	:	PullCustomerAddressPhone() 
		'*	Parameters		:	add1,add2,city,country,state,zip,phone
		'*	Author			:	Swastika
		'*	Purpose			:   To pull the client address,Phone No.  from hiCustAddXml hidden control
		'*	Creation Date	:   21 March 2006
		'*******************************************************************/
		function PullCustomerAddressPhone(add1,add2,city,country,state,zip,phone,email,county,territory)
		{ 
			if (document.forms[0].hidCustAddXml.value == "")
				return false;
			//alert(top.frames[0].strLobId);
			var tempXML = document.forms[0].hidCustAddXml.value;
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('address')[0]);
	
			var i=0;
			for(i=0;i<tree.childNodes.length;i++)
			{
				if(!tree.childNodes[i].firstChild) continue;
				
				var nodeName = tree.childNodes[i].nodeName;
				var nodeValue = tree.childNodes[i].firstChild.text;
				var ctrl;
				
				ctrl = null;
				switch(nodeName)
				{
					case "address1":
						ctrl = add1;
						break;
					case "address2":
						ctrl = add2;
						break;
					case "city":
						ctrl = city;
						break;
					case "country":
						ctrl = country;
						break;
					case "state":
						ctrl = state;
						break;
					case "zip":
						ctrl = zip;
						break;
					case "phone":
						ctrl = phone;
						break;
					case "email":
						ctrl = email;
						break;				
				}
				
				if (ctrl != null)
				{
			
					if (nodeName == "state" || nodeName == "country")
					{	
						//if (ctrl.value == "")
					//	{
							SelectComboOption(ctrl.id,nodeValue);
					//	}	
					}
					else
					{
						//if (ctrl.value == "")//Done for Itrack Issue 5820 on 21 May 2009
						//{
							ctrl.value = nodeValue;
						//}
					}
				}				
			}
			if (document.forms[0].hidCustAddXml.value == "")
				return false;
			if(county!=null ||territory!=null)
			ShowCounty(county,territory);
			DisableValidators();
			ChangeColor();
			return false;
		}
		
		/*******************************************************************
		'*	Function Name	:	PullCustomerAddressPhone() 
		'*	Parameters		:	add1,add2,city,country,state,zip,phone,email,mobile,ext
		'*	Author			:	Neeraj Singh
		'*	Purpose			:   To pull the client address,Phone No.  from hiCustAddXml hidden control
		'*	Creation Date	:   21 March 2006
		'*******************************************************************/
		function PullCustomerAddressPhoneMobile(add1,add2,city,country,state,zip,phone,email,mobile,businessPhone,ext)
		{ 
			//alert('cust');
			
			if (document.forms[0].hidCustAddXml.value == "")
				return false;
			//alert(top.frames[0].strLobId);
			var tempXML = document.forms[0].hidCustAddXml.value;
			var objXmlHandler = new XMLHandler();
			var tree = (objXmlHandler.quickParseXML(tempXML).getElementsByTagName('address')[0]);
	
			var i=0;
			for(i=0;i<tree.childNodes.length;i++)
			{
				if(!tree.childNodes[i].firstChild) continue;
				
				var nodeName = tree.childNodes[i].nodeName;
				var nodeValue = tree.childNodes[i].firstChild.text;
				var ctrl;
				ctrl = null;
				switch(nodeName)
				{
					case "address1":
						ctrl = add1;
						break;
					case "address2":
						ctrl = add2;
						break;
					case "city":
						ctrl = city;
						break;
					case "country":
						ctrl = country;
						break;
					case "state":
						ctrl = state;
						break;
					case "zip":
						ctrl = zip;
						break;
					case "phone":
						ctrl = phone;
						break;
					case "email":
						ctrl = email;
						break;
					case "mobile":
						ctrl = mobile;
						break;
					case "CUSTOMER_BUSINESS_PHONE":
						ctrl = businessPhone;
						break;
					case "CUSTOMER_EXT":
						ctrl = ext;
						break;	
					case "EMPLOYER_HOMEPHONE":
						ctrl = businessPhone;
						break;
					case "EMP_EXT":
						ctrl = ext;
						break;						
				}
				
				if (ctrl != null)
				{			
					if (nodeName == "state" || nodeName == "country")
					{	
						//if (ctrl.value == "")
					//	{
							SelectComboOption(ctrl.id,nodeValue);
							if (nodeName == "country")
							{
								if(document.forms[0].name=="CLT_APPLICANT_LIST")
								{
									//doPost('CountryChanged','0');
									fillstateFromCountry();
								}
							}
							if (nodeName == "state")
							{
								if(document.forms[0].name=="CLT_APPLICANT_LIST")
								{
									SelectComboOption(ctrl.id,nodeValue);
									document.getElementById('hidSTATE_ID_OLD').value=nodeValue;
									setStateID();
								}
							}
							
					//	}	
					}
					else
					{
					//alert('cust'+ nodeName + 'value=' + nodeValue);
						//if (ctrl.value == "")//Done for Itrack Issue 5820 on 21 May 2009
						//{
							ctrl.value = nodeValue;
						//}
					}
				}				
			}
			if (document.forms[0].hidCustAddXml.value == "")
				return false;
			//if(county!=null ||territory!=null)
			//ShowCounty(county,territory);
			DisableValidators();
			ChangeColor();
			return false;
		}		
		
	/*To be called for verifying the address*/	
	function VerifyAddress(add1, add2, city, state, zip)
	{	     
		if(
		   (add1.value !="" && city.value!="" && state.value!="" && zip.value!="")
		   ||
		   (add1.value !="" && city.value!="" && state.options[state.options.selectedIndex].text!="" && zip.value!="")
		  )
		{
			var stateName = '';
			
			top.topframe.add1 =  add1;
			top.topframe.add2 =  add2;		
			top.topframe.city = city;
			top.topframe.state = state;
			top.topframe.zip = zip;
			
			if (state.selectedIndex > -1)
				state = state.options[state.selectedIndex].text;			
				
			AddressDetails.GetAddressDetails( add1.value + ' ' + add2.value
											, city.value
											, state
											, zip
											, GetUser_callback);			        
		}		else {

		    if (iLangID == 2) {
		        alert("Insira um CEP v" + String.fromCharCode(225) + "lido para localizar endere" + String.fromCharCode(231) + "o, bairro, estado e pa" + String.fromCharCode(237) + "s");
		    }
		    else {
		       alert("Please fill Address 1,city,state and Zip fields to verify address."); 
		    }
		}
	}
	function VerifyAddressDetailsForBR(add1, District , city, state, zip) {
	    
	    if ((add1.value != "" && District.value != "" && city.value != "" && state.value != "" && zip.value != "")||(add1.value != "" && city.value != "" && state.options[state.options.selectedIndex].text != "" && zip.value != "")) 
	    {
	        var stateName = '';

	        top.topframe.add1 = add1;
	        top.topframe.District = District;
	        top.topframe.city = city;
	        top.topframe.state = state;
	        top.topframe.zip = zip;
                
	        if (state.selectedIndex > -1)
	            state = state.options[state.selectedIndex].text;
	         
	      AddressDetails.GetAddressDetailsBR(add1.value,District.value, city.value, state, zip.value, GetUser_callbackBR);
	    }
	    else {

	        if (iLangID == 2) {
	           
	            alert("Insira um CEP v"+ String.fromCharCode(225) + "lido para localizar endere" + String.fromCharCode(231) + "o, bairro, estado e pa" + String.fromCharCode(237) + "s");
	        }
	        else {
	            alert("Please fill Address 1,District,city,state and Zip fields to verify address.");
	        }
	    }
	}
	function GetUser_callbackBR(response) {
	    
	    if (response != null && response.value != null) {
	        var user = response.value;

	        if (typeof (user) == "object") {
	            //RP - 28 Aug 2006
	            var status = response.value.StatusDesc.toUpperCase();
	            if (status == 'RETURN ADDRESS FIXED') {
	                if (iLangID == 2) {
	                    top.topframe.LIST_ADDRESS_MATCH_STATUS = 'Para corrigir Address - Coloque o cursor sobre o endereço de escolha';
	                }
	                else
	                { top.topframe.LIST_ADDRESS_MATCH_STATUS = 'To fix Address - Put Cursor on address of choice'; }
	            }
	            else
	                top.topframe.LIST_ADDRESS_MATCH_STATUS = response.value.StatusDesc;

	            top.topframe.VerifyAddressListArray = user.AddressProperty;
	            ShowPopup("/cms/cmsweb/maintenance/verifiedaddresslist.aspx", "Address", 400, 200);
	        }
	    }
	}
	
	function GetUser_callback(response)
	{ 
		if (response != null && response.value != null)
		{
			var user = response.value;
			
			if (typeof(user) == "object")
			{   
				//RP - 28 Aug 2006
				var status = response.value.StatusDesc.toUpperCase();
				if (status == 'RETURN ADDRESS FIXED') {
				    if (iLangID == 2) {
				        top.topframe.LIST_ADDRESS_MATCH_STATUS = 'Para corrigir Address - Coloque o cursor sobre o endereço de escolha';
				    }
				    else {
				        top.topframe.LIST_ADDRESS_MATCH_STATUS = 'To fix Address - Put Cursor on address of choice';
				    }
				}
				else
				    top.topframe.LIST_ADDRESS_MATCH_STATUS = response.value.StatusDesc;				
				
				top.topframe.VerifyAddressListArray = user.AddressProperty;				
				ShowPopup("/cms/cmsweb/maintenance/verifiedaddresslist.aspx", "Address", 400, 200);
			}
		}
	}