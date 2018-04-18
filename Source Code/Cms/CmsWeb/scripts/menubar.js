/******************************************************************************
* Menu Bar                                                                    *
*                                                                             *
* Do not remove this notice.                                                  *
*                                                                             *
* Copyright 2000-2004 by Mike Hall                                            *
* Please see http://www.brainjar.com for terms of use.                        *
******************************************************************************/
var TimeToHideMenu = 1500 ;

//----------------------------------------------------------------------------
// Code to determine the browser and version.
//----------------------------------------------------------------------------
function Browser() {

  var ua, s, i;

  this.isIE    = false;  // Internet Explorer
  this.isOP    = false;  // Opera
  this.isNS    = false;  // Netscape
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

//----------------------------------------------------------------------------
// Code for handling the menu bar and active button.
//----------------------------------------------------------------------------

var activeButton = null;

// Capture mouse clicks on the page so any active button can be
// deactivated.

if (browser.isIE)
  document.onmousedown = pageMousedown;
else
  document.addEventListener("mousedown", pageMousedown, true);

function pageMousedown(event) {

  var el;

  // If there is no active button, exit.

  if (activeButton == null)
    return;

  // Find the element that was clicked on.

  if (browser.isIE)
    el = window.event.srcElement;
  else
    el = (event.target.tagName ? event.target : event.target.parentNode);

  // If the active button was clicked on, exit.

  if (el == activeButton)
    return;

  // If the element is not part of a menu, reset and clear the active
  // button.

  if (getContainerWith(el, "DIV", "menu") == null) {
    resetButton(activeButton);
    activeButton = null;
  }
}

function buttonClick(event, menuId) {
	top.topframe.main1.mousein=true;
	if(menuId == null) return;
	var button;

  // Get the target button element.

  if (browser.isIE)
    button = window.event.srcElement;
  else
    button = event.currentTarget;

  // Blur focus from the link to remove that annoying outline.

  button.blur();

  // Associate the named menu to this button if not already done.
  // Additionally, initialize menu display.

  if (button.menu == null) {
    button.menu = document.getElementById(menuId);
    if (button.menu.isInitialized == null)
      menuInit(button.menu);
  }
  
  // Reset the currently active button, if any.
  if (activeButton != null)
    resetButton(activeButton);
  
  // Activate this button, unless it was the currently active one.

  if (button != activeButton) {
    depressButton(button);
    activeButton = button;
  }
  else
    activeButton = null;

	
  return false;
}

function buttonMouseover(event, menuId) {

  var button;

  // Find the target button element.

  if (browser.isIE)
    button = window.event.srcElement;
  else
    button = event.currentTarget;

  // If any other button menu is active, make this one active instead.

  if (activeButton != null && activeButton != button)
    buttonClick(event, menuId);
}

function depressButton(button) {

  var x, y;

  // Update the button's style class to make it look like it's
  // depressed.

  button.className += " menuButtonActive";

  // Position the associated drop down menu under the button and
  // show it.

  x = getPageOffsetLeft(button);
  y = getPageOffsetTop(button) + button.offsetHeight + 5; //3px is padding, 2px for white space

  // For IE, adjust position.

  if (browser.isIE) {
    x += button.offsetParent.clientLeft;
    y += button.offsetParent.clientTop;
  }

  button.menu.style.left = x + "px";
  button.menu.style.top  = y + "px";
  button.menu.style.visibility = "visible";
}

function resetButton(button) {
	// if no active button then return
	if(button == null) return;
  // Restore the button's style class.
	
  removeClassName(button, "menuButtonActive");

  // Hide the button's menu, first closing any sub menus.

  if (button.menu != null) {
    closeSubMenu(button.menu);
    button.menu.style.visibility = "hidden";
    if((top.botframe.menuFrame != null) && (top.botframe.menuFrame != 'undefined'))
    {
		top.botframe.menuFrame.style.display = 'none';
		top.botframe.document.getElementById('menuIframe').style.display = 'none';
		if((top.botframe.menuFrame2 != null) && (top.botframe.menuFrame2 != 'undefined'))
		{
			top.botframe.menuFrame2.style.display = 'none';
			top.botframe.document.getElementById('menuIframe2').style.display = 'none';
		}
    }
  }
}

//----------------------------------------------------------------------------
// Code to handle the menus and sub menus.
//----------------------------------------------------------------------------

function menuMouseover(event) {

  var menu;

  // Find the target menu element.

  if (browser.isIE)
    menu = getContainerWith(window.event.srcElement, "DIV", "menu");
  else
    menu = event.currentTarget;

  // Close any active sub menu.

  if (menu.activeItem != null)
    closeSubMenu(menu);
}

function menuItemMouseover(event, menuId) {
top.topframe.main1.mousein=true;
  var item, menu, x, y;

  // Find the target item element and its parent menu element.

  if (browser.isIE)
    item = getContainerWith(window.event.srcElement, "A", "menuItem");
  else
    item = event.currentTarget;
  menu = getContainerWith(item, "DIV", "menu");

  // Close any active sub menu and mark this one as active.

  if (menu.activeItem != null)
    closeSubMenu(menu);
  menu.activeItem = item;

  // Highlight the item element.

  item.className += " menuItemHighlight";

  // Initialize the sub menu, if not already done.

  if (item.subMenu == null) {
    item.subMenu = document.getElementById(menuId);
    if (item.subMenu.isInitialized == null)
      menuInit(item.subMenu);
  }

  // Get position for submenu based on the menu item.

  x = getPageOffsetLeft(item) + item.offsetWidth;
  y = getPageOffsetTop(item);

  // Adjust position to fit in view.

  var maxX, maxY;

  if (browser.isIE) {
    maxX = Math.max(document.documentElement.scrollLeft, document.body.scrollLeft) +
      (document.documentElement.clientWidth != 0 ? document.documentElement.clientWidth : document.body.clientWidth);
    maxY = Math.max(document.documentElement.scrollTop, document.body.scrollTop) +
      (document.documentElement.clientHeight != 0 ? document.documentElement.clientHeight : document.body.clientHeight);
  }
  if (browser.isOP) {
    maxX = document.documentElement.scrollLeft + window.innerWidth;
    maxY = document.documentElement.scrollTop  + window.innerHeight;
  }
  if (browser.isNS) {
    maxX = window.scrollX + window.innerWidth;
    maxY = window.scrollY + window.innerHeight;
  }
  maxX -= item.subMenu.offsetWidth;
  maxY -= item.subMenu.offsetHeight;

  if (x > maxX)
    x = Math.max(0, x - item.offsetWidth - item.subMenu.offsetWidth
      + (menu.offsetWidth - item.offsetWidth));
  y = Math.max(0, Math.min(y, maxY));

  // Position and show it.
	top.topframe.main1.subMenuLeft = x;
	top.topframe.main1.subMenuTop = y;	
	top.topframe.main1.subMenuWidth = item.subMenu.offsetWidth;
	top.topframe.main1.subMenuHeight = item.subMenu.offsetHeight;
  item.subMenu.style.left = x + "px";
  item.subMenu.style.top  = y + "px";
  item.subMenu.style.visibility = "visible";
  	
  // Stop the event from bubbling.
  if (browser.isIE)
    window.event.cancelBubble = true;
  else
    event.stopPropagation();
}

function closeSubMenu(menu) {
  if (menu == null || menu.activeItem == null)
    return;

  // Recursively close any sub menus.

  if (menu.activeItem.subMenu != null) {
    closeSubMenu(menu.activeItem.subMenu);
    menu.activeItem.subMenu.style.visibility = "hidden";
    if((menu.activeItem.subMenu.id.length-4) == 4)hideSubSubMenu();
    if((menu.activeItem.subMenu.id.length-4) == 3)hideSubMenu();
    if((menu.activeItem.subMenu.id.length-4) == 2)hideDropMenu();
    menu.activeItem.subMenu = null;
  }
  removeClassName(menu.activeItem, "menuItemHighlight");
  menu.activeItem = null;
}

//----------------------------------------------------------------------------
// Code to initialize menus.
//----------------------------------------------------------------------------

function menuInit(menu) {

  var itemList, spanList;
  var textEl, arrowEl;
  var itemWidth;
  var w, dw;
  var i, j;

  // For IE, replace arrow characters.

  if (browser.isIE) {
    menu.style.lineHeight = "2.5ex";
    spanList = menu.getElementsByTagName("SPAN");
    for (i = 0; i < spanList.length; i++)
      if (hasClassName(spanList[i], "menuItemArrow")) {
        spanList[i].style.fontFamily = "Webdings";
        spanList[i].firstChild.nodeValue = "4";
      }
  }

  // Find the width of a menu item.

  itemList = menu.getElementsByTagName("A");
  if (itemList.length > 0)
    itemWidth = itemList[0].offsetWidth;
  else
    return;

  // For items with arrows, add padding to item text to make the
  // arrows flush right.

  for (i = 0; i < itemList.length; i++) {
    spanList = itemList[i].getElementsByTagName("SPAN");
    textEl  = null;
    arrowEl = null;
    for (j = 0; j < spanList.length; j++) {
      if (hasClassName(spanList[j], "menuItemText"))
        textEl = spanList[j];
      if (hasClassName(spanList[j], "menuItemArrow")) {
        arrowEl = spanList[j];
      }
    }
    if (textEl != null && arrowEl != null) {
      textEl.style.paddingRight = (itemWidth 
        - (textEl.offsetWidth + arrowEl.offsetWidth)) + "px";
      // For Opera, remove the negative right margin to fix a display bug.
      if (browser.isOP)
        arrowEl.style.marginRight = "0px";
    }
  }

  // Fix IE hover problem by setting an explicit width on first item of
  // the menu.

  if (browser.isIE) {
    w = itemList[0].offsetWidth;
    itemList[0].style.width = w + "px";
    dw = itemList[0].offsetWidth - w;
    w -= dw;
    itemList[0].style.width = w + "px";
  }

  // Mark menu as initialized.

  menu.isInitialized = true;
}

//----------------------------------------------------------------------------
// General utility functions.
//----------------------------------------------------------------------------

function getContainerWith(node, tagName, className) {

  // Starting with the given node, find the nearest containing element
  // with the specified tag name and style class.

  while (node != null) {
    if (node.tagName != null && node.tagName == tagName &&
        hasClassName(node, className))
      return node;
    node = node.parentNode;
  }

  return node;
}

function hasClassName(el, name) {

  var i, list;

  // Return true if the given element currently has the given class
  // name.

  list = el.className.split(" ");
  for (i = 0; i < list.length; i++)
    if (list[i] == name)
      return true;

  return false;
}

function removeClassName(el, name) {

  var i, curList, newList;

  if (el.className == null)
    return;

  // Remove the given class name from the element's className property.

  newList = new Array();
  curList = el.className.split(" ");
  for (i = 0; i < curList.length; i++)
    if (curList[i] != name)
      newList.push(curList[i]);
  el.className = newList.join(" ");
}

function getPageOffsetLeft(el) {

  var x;

  // Return the x coordinate of an element relative to the page.

  x = el.offsetLeft;
  if (el.offsetParent != null)
    x += getPageOffsetLeft(el.offsetParent);

  return x;
}

function getPageOffsetTop(el) {

  var y;

  // Return the x coordinate of an element relative to the page.

  y = el.offsetTop;
  if (el.offsetParent != null)
    y += getPageOffsetTop(el.offsetParent);

  return y;
}
function findClass(menuNumber)
{
	var arrMenu = menuNumber.split(',');
	if(top.topframe.main1.activeMenuBar == null || top.topframe.main1.activeMenuBar == 'undefined')
	{
		return 'menuButton';
	}
	else
	{
		var arrActiveMenu = top.topframe.main1.activeMenuBar.split(',');
		if(arrActiveMenu.length < arrMenu.length)
		{
			return 'menuButton';
		}
		for(var i=0; i<arrMenu.length; i++)
		{
			if(arrActiveMenu[i] == arrMenu[i])
			{
				return 'menuButtonDoActive';
			}
			else
			{
				break;
			}
		}
		return 'menuButton';	
	}
}
function styleMenu(menuNumber)
{
	top.topframe.main1.mousein = true;
	var arrMenu = menuNumber.split(',');
	if(arrMenu.length > 0)
	{
		activeImage = top.topframe.main1.activeImage[parseInt(menuNumber)].src; 
		('top.topframe.img' + arrMenu[0]).src =activeImage;
	}
	if(arrMenu.length > 1)
	{
		/*for(var i=0; i<top.topframe.main1.treeMenu[arrMenu[0]].childNodes.length; i++)
		{
			if(!((!top.topframe.main1.treeMenu[arrMenu[0]].childNodes[i].enabled)||(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[i].enabled == "false")))
				eval('top.botframe.menuLink' + arrMenu[0] + i).className = 'menuButton';
		}*/
		('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]).className = 'menuButtonDoActive';
		/*if(top.topframe.main1.activeMenuBar != null && top.topframe.main1.activeMenuBar != 'undefined')
		{
			arrActiveMenu = top.topframe.main1.activeMenuBar.split(',');
			if(arrActiveMenu.length > 1)
				if(eval('top.botframe.menuLink' + arrActiveMenu[0] + arrActiveMenu[1]))
					eval('top.botframe.menuLink' + arrActiveMenu[0] + arrActiveMenu[1]).className = 'menuButtonDoActive';
		}*/
	}
	if(arrMenu.length == 2)
	{
		if(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].childNodes.length > 0)
		{
			var dropMenu = eval('top.botframe.menu' + arrMenu[0] + arrMenu[1])
			if(dropMenu == 'undefined' || dropMenu == null) return;
			dropMenuList = dropMenu.getElementsByTagName("A")
			if(dropMenuList.length > 0)
			{
				var dropMenuW = dropMenuList[0].offsetWidth;
				var dropMenuH = dropMenuList.length*22;
				var dropMenuL = eval('top.botframe.subMenuLink')
				top.botframe.menuFrame.style.left		= eval('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]+'.offsetLeft');
				document.all.item('menuIFrame').left	= eval('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]+'.offsetLeft');
				top.botframe.menuFrame.style.width		= dropMenuW;
				document.all.item('menuIFrame').width	= dropMenuW;
				top.botframe.menuFrame.style.height		= dropMenuH;
				document.all.item('menuIFrame').height	= dropMenuH;
				if(('top.botframe.menu' + arrMenu[0] + arrMenu[1] + '.style.visibility') != 'hidden')
				{
					top.botframe.menuFrame.style.display = 'inline';
					top.botframe.document.getElementById('menuIframe').style.display = 'inline';
					//alert('1 -> ' + document.all.item('menuIFrame').width + ',' + document.all.item('menuIFrame').height + ',' + document.all.item('menuIFrame').left + ',' + document.all.item('menuIFrame').top + '\n' + document.all.item('menuIFrame2').width + ',' + document.all.item('menuIFrame2').height + ',' + document.all.item('menuIFrame2').left + ',' + document.all.item('menuIFrame2').top + '\n' + document.all.item('menuIFrame3').width + ',' + document.all.item('menuIFrame3').height + ',' + document.all.item('menuIFrame3').left + ',' + document.all.item('menuIFrame3').top);
				}
			}
		}
	}
	if(arrMenu.length == 3)
	{
		('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]).className = 'menuButtonDoActive';
		if(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].childNodes[arrMenu[2]].childNodes.length > 0)
		{
			var subMenu = eval('top.botframe.menu' + arrMenu[0] + arrMenu[1] + arrMenu[2])
			if(subMenu == 'undefined' || subMenu == null) return;
			subMenuList = subMenu.getElementsByTagName("A");
			
			if(subMenuList.length > 0)
			{
				var subMenuW = subMenuList[0].offsetWidth;
				var subMenuH = subMenuList.length*22;
				top.botframe.menuFrame2.style.left		= top.topframe.main1.subMenuLeft;
				document.all.item('menuIFrame2').left	= top.topframe.main1.subMenuLeft;
				top.botframe.menuFrame2.style.top		= top.topframe.main1.subMenuTop;
				document.all.item('menuIFrame2').top	= top.topframe.main1.subMenuTop;
				
				top.botframe.menuFrame2.style.width		= subMenuW;
				document.all.item('menuIFrame2').width	= subMenuW;
				top.botframe.menuFrame2.style.height	= subMenuH;
				document.all.item('menuIFrame2').height = subMenuH;
				if(eval('top.botframe.menu' + arrMenu[0] + arrMenu[1] + arrMenu[2] + '.style.visibility') != 'hidden')
				{
					top.botframe.menuFrame2.style.display = 'inline';
					top.botframe.document.getElementById('menuIframe2').style.display = 'inline';
					//alert('2 -> ' + document.all.item('menuIFrame2').width + ',' + document.all.item('menuIFrame2').height + ',' + document.all.item('menuIFrame2').left + ',' + document.all.item('menuIFrame2').top + '\n' + document.all.item('menuIFrame3').width + ',' + document.all.item('menuIFrame3').height + ',' + document.all.item('menuIFrame3').left + ',' + document.all.item('menuIFrame3').top);
				}
			}
		}
	}
	if(arrMenu.length == 4)
	{
		('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]).className = 'menuButtonDoActive';
		if(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].childNodes[arrMenu[2]].childNodes[arrMenu[3]].childNodes.length > 0)
		{
			var subSubMenu = eval('top.botframe.menu' + arrMenu[0] + arrMenu[1] + arrMenu[2] + arrMenu[3])
			if(subSubMenu == 'undefined' || subSubMenu == null) return;
			subSubMenuList = subSubMenu.getElementsByTagName("A");
			if(subSubMenuList.length > 0)
			{
				var subSubMenuW = subSubMenuList[0].offsetWidth;
				var subSubMenuH = subSubMenuList.length*22;
				top.botframe.menuFrame3.style.left		= top.topframe.main1.subMenuLeft;
				document.all.item('menuIFrame2').left	= top.topframe.main1.subMenuLeft;
				top.botframe.menuFrame3.style.top		= top.topframe.main1.subMenuTop;
				document.all.item('menuIFrame2').top	= top.topframe.main1.subMenuTop;
				top.botframe.menuFrame3.style.width		= subSubMenuW;
				document.all.item('menuIFrame3').width	= subSubMenuW;
				top.botframe.menuFrame3.style.height	= subSubMenuH;
				document.all.item('menuIFrame3').height = subSubMenuH;
				if(('top.botframe.menu' + arrMenu[0] + arrMenu[1] + arrMenu[2] + arrMenu[3] + '.style.visibility') != 'hidden')
				{
					top.botframe.menuFrame3.style.display = 'inline';
					top.botframe.document.getElementById('menuIframe3').style.display = 'inline';
					//alert('2 -> ' + document.all.item('menuIFrame2').width + ',' + document.all.item('menuIFrame2').height + ',' + document.all.item('menuIFrame2').left + ',' + document.all.item('menuIFrame2').top + '\n' + document.all.item('menuIFrame3').width + ',' + document.all.item('menuIFrame3').height + ',' + document.all.item('menuIFrame3').left + ',' + document.all.item('menuIFrame3').top);
				}
			}
		}
	}
	if(arrMenu.length == 5)
	{
		('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]).className = 'menuButtonDoActive';
	}
}
function showSubMenu(menuNumber)
{
	top.topframe.main1.mousein = true;
	var subMenu = top.botframe.document.getElementById(menuNumber);
	
	top.botframe.menuFrame2.style.width = subMenu.offsetWidth;
	document.all.item('menuIFrame2').width = subMenu.offsetWidth;
	top.botframe.menuFrame2.style.height = subMenu.offsetHeight;
	document.all.item('menuIFrame2').height = subMenu.offsetHeight;
	top.botframe.menuFrame2.style.left = subMenu.offsetLeft;
	document.all.item('menuIFrame2').left = subMenu.offsetLeft;
	top.botframe.menuFrame2.style.top = subMenu.offsetTop;
	document.all.item('menuIFrame2').top = subMenu.offsetTop;
	
	top.botframe.menuFrame2.style.display = 'inline';
	top.botframe.document.getElementById('menuIframe2').style.display = 'inline';
	subMenu.style.visibility = 'visible';
	//alert('3 -> ' + document.all.item('menuIFrame2').width + ',' + document.all.item('menuIFrame2').height + ',' + document.all.item('menuIFrame2').left + ',' + document.all.item('menuIFrame2').top + '\n' + document.all.item('menuIFrame3').width + ',' + document.all.item('menuIFrame3').height + ',' + document.all.item('menuIFrame3').left + ',' + document.all.item('menuIFrame3').top);
}
function hideDropMenu()
{
	if(top.botframe.menuFrame)
	{
		top.botframe.menuFrame.style.display = 'none';
		top.botframe.document.getElementById('menuIframe').style.display = 'none';
	}
	hideSubMenu();
}
function hideSubMenu()
{
	if(top.botframe.menuFrame2)
	{
		top.botframe.menuFrame2.style.display = 'none';
		top.botframe.document.getElementById('menuIframe2').style.display = 'none';
		/*
		top.botframe.menuFrame2.style.left = 0;
		document.all.item('menuIFrame2').left = 0;
		top.botframe.menuFrame2.style.top = 0;
		document.all.item('menuIFrame2').top = 0;
		
		top.botframe.menuFrame2.style.width = 0;
		document.all.item('menuIFrame2').width = 0
		top.botframe.menuFrame2.style.height = 0;
		document.all.item('menuIFrame2').height = 0;*/
	}
	hideSubSubMenu();
}
function showSubSubMenu(menuNumber)
{
	top.topframe.main1.mousein = true;
	var subSubMenu = top.botframe.document.getElementById(menuNumber);
	if(subSubMenu.style.visibility == 'visible')return;
	
	top.botframe.menuFrame3.style.width = subSubMenu.offsetWidth;
	document.all.item('menuIFrame3').width = subSubMenu.offsetWidth;
	top.botframe.menuFrame3.style.height = subSubMenu.offsetHeight;
	document.all.item('menuIFrame3').height = subSubMenu.offsetHeight;
	top.botframe.menuFrame3.style.left = subSubMenu.offsetLeft;
	document.all.item('menuIFrame3').left = subSubMenu.offsetLeft;
	top.botframe.menuFrame3.style.top = subSubMenu.offsetTop;
	document.all.item('menuIFrame3').top = subSubMenu.offsetTop;
	
	top.botframe.menuFrame3.style.display = 'inline';
	top.botframe.document.getElementById('menuIframe3').style.display = 'inline';
	
	subSubMenu.style.visibility = 'visible';
	//alert('4 -> ' + document.all.item('menuIFrame2').width + ',' + document.all.item('menuIFrame2').height + ',' + document.all.item('menuIFrame2').left + ',' + document.all.item('menuIFrame2').top + '\n' + document.all.item('menuIFrame3').width + ',' + document.all.item('menuIFrame3').height + ',' + document.all.item('menuIFrame3').left + ',' + document.all.item('menuIFrame3').top);
}
function hideSubSubMenu()
{
	if(top.botframe.menuFrame3)
	{
		top.botframe.menuFrame3.style.display = 'none';
		top.botframe.document.getElementById('menuIframe3').style.display = 'none';
		/*
		top.botframe.menuFrame3.style.left = 0;
		document.all.item('menuIFrame3').left = 0;
		top.botframe.menuFrame3.style.top = 0;
		document.all.item('menuIFrame3').top = 0;
		
		top.botframe.menuFrame3.style.width = 0;
		document.all.item('menuIFrame3').width = 0
		top.botframe.menuFrame3.style.height = 0;
		document.all.item('menuIFrame3').height = 0;*/
	}
}
function reSetMenu()
{
	if(top.topframe.main1.activeMenuBar == null || top.topframe.main1.activeMenuBar == 'undefined')return;
	var arrMenu = top.topframe.main1.activeMenuBar.split(',');
	if(arrMenu.length > 0)
	{
		top.topframe.main1.treeMenu[arrMenu[0]].classText = 'menuButton';
		('top.topframe.buttonLink' + arrMenu[0]).className = 'menuButton';
	}
	if(arrMenu.length > 1)
	{
		top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].classText = 'menuButton';
	}
}
function callItemMouseOut(menuNumber)
{
	top.topframe.main1.mousein=false;
	var arrActiveMenu = new Array('-1','-1','-1');
	var arrMenu = menuNumber.split(',');
	if(top.topframe.main1.activeMenuBar != null && top.topframe.main1.activeMenuBar != 'undefined')
	{
		arrActiveMenu = top.topframe.main1.activeMenuBar.split(',');
	}
	if(arrMenu.length > 0)
	{
		if(arrMenu.length == 1)
			return;
	}
	if(arrMenu.length > 1)
	{
		if(arrActiveMenu[1] != arrMenu[1])
			('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]).className = 'menuButton';
		else
			('top.botframe.menuLink' + arrMenu[0] + arrMenu[1]).className = 'menuButtonDoActive';
	}
}
//function added By Deepak to Show Popup directly from menu.
//14-July-2006
function ShowPopupWindow(strUrl) 
{
	var URLParam = strUrl.split(':');
	var MyURL = URLParam[0];
	var MyWindowName = URLParam[1];
	var MyWidth = URLParam[2];
	var MyHeight = URLParam[3];
	var MyScrollBars = 'Yes';
	var MyResizable = 'Yes';
	var MyMenuBar = 'No';
	var MyToolBar = 'No';
	var MyStatusBar = 'No';
	var dt;

	if (document.all)
		var xMax = screen.width, yMax = screen.height;
	else
		if (document.layers)
			var xMax = window.outerWidth, yMax = window.outerHeight;
		else
			var xMax = 640, yMax=480;

	var xOffset = (xMax - MyWidth)/2, yOffset = (yMax - MyHeight)/2;

    dt = new Date();
    MyWindowName = MyWindowName + dt.getYear() + dt.getMonth() + dt.getDate() + dt.getHours() + dt.getMinutes() + dt.getSeconds() + dt.getMilliseconds();
	MyWin = window.open(MyURL,MyWindowName,'width=' + MyWidth + ',height=' + MyHeight + ',screenX= ' + xOffset + ',screenY=' + yOffset + ',top=' + yOffset + ',left=' + xOffset + ',scrollbars=' + MyScrollBars + ',resizable=' + MyResizable + ',menubar=' + MyMenuBar + ',toolbar=' + MyToolBar + ',status=' + MyStatusBar + '' );
	MyWin.focus();
}
function callItemClicked(menuNumber,linkUrl)
{
	// if menu has child, then its click will fix the menu
	//alert(top.topframe.main1.menuXml);
	var arrMenu = menuNumber.split(',');
	if(arrMenu.length == 1)
	{
	
		if(top.topframe.main1.fixedButton != null)RestoreImage(top.topframe.main1.fixedButton);
		ChangeImage(menuNumber);
		if(!(top.topframe.main1.treeMenu[menuNumber].default_page == 'undefined' || top.topframe.main1.treeMenu[menuNumber].default_page == null || top.topframe.main1.treeMenu[menuNumber].default_page == ''))
		{
			linkUrl = top.topframe.main1.treeMenu[menuNumber].default_page;
		}
			//top.topframe.main1.fixedButton = null;
			//showVerticalMenu(menuNumber);
			//top.topframe.main1.fixedButton = menuNumber
			//if(menuNumber == '1')
			//{
			//	callItemClicked('1,0','/cms/client/aspx/CustomerManagerSearch.aspx');
			//}
			//return;
		//}
	}
	if(arrMenu.length == 2)
	{
		if(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].childNodes.length > 0) return;
	}
	if(arrMenu.length == 3)
	{
		if(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].childNodes[arrMenu[2]].childNodes.length > 0) return;
	}
	if(arrMenu.length == 4)
	{
		if(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].childNodes[arrMenu[2]].childNodes[arrMenu[3]].childNodes.length > 0) return;
	}
	// otherwise change activemenubar and follow the link
	reSetMenu();
	top.topframe.main1.activeMenuBar = menuNumber;
	top.topframe.main1.fixedButton = arrMenu[0];
	
	if (linkUrl.toUpperCase().indexOf('SHOWPOPUPWINDOW')<0)
		parent.botframe.location.href = linkUrl;
	else
		ShowPopupWindow(linkUrl);
	
}
function createActiveMenu()
{
	resetButton(activeButton);
	activeButton = null;
	if(top.topframe.main1.activeMenuBar == null || top.topframe.main1.activeMenuBar == 'undefined')return;
	var arrMenu = top.topframe.main1.activeMenuBar.split(',');
	top.topframe.showTopButton();
	if(arrMenu.length > 0)
	{
		ChangeImage(arrMenu[0]);
	}
	if(arrMenu.length > 1)
	{
		for(var i=0; i<top.topframe.main1.treeMenu[arrMenu[0]].childNodes.length; i++)
		{
			if(!((!top.topframe.main1.treeMenu[arrMenu[0]].childNodes[i].enabled)||(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[i].enabled == "false")))
				if(top.topframe.main1.treeMenu[arrMenu[0]].childNodes[i].classText)
				{
					top.topframe.main1.treeMenu[arrMenu[0]].childNodes[i].classText = 'menuButton';
				}
		}
		top.topframe.main1.treeMenu[arrMenu[0]].childNodes[arrMenu[1]].classText = 'menuButtonDoActive';
	}
	top.topframe.main1.fixedButton = null;
	top.topframe.showVerticalMenu(arrMenu[0]);
	top.topframe.main1.fixedButton = arrMenu[0];
}
function dummy()
{
	alert(document.all.item('menuIFrame3').width +','+ document.all.item('menuIFrame3').height +','+ document.all.item('menuIFrame3').left +','+ document.all.item('menuIFrame3').top + '\n' + document.all.item('menuIFrame2').width +','+ document.all.item('menuIFrame2').height +','+ document.all.item('menuIFrame2').left +','+ document.all.item('menuIFrame2').top + '\n' + document.all.item('menuIFrame').width +','+ document.all.item('menuIFrame').height +','+ document.all.item('menuIFrame').left +','+ document.all.item('menuIFrame').top);
}
function findMouseIn()
{
	if(!top.topframe.main1.mousein)
	{
		createActiveMenu();
		top.topframe.main1.mousein = true;
	}
	setTimeout('findMouseIn()',TimeToHideMenu);
}



function loadImage(btnXml)
{
	var objXmlHandler = new XMLHandler();
	var tree = (objXmlHandler.quickParseXML(btnXml).getElementsByTagName('root')[0]);
	var i = 0;
	var ImageLang = '';
	try {
	    if (langCulture != '' && langCulture != "en-US" && langCulture != "zh-SG")
	        ImageLang = '.' + langCulture;
	}
	catch (err) { } 
	for(i=0;i<tree.childNodes.length;i++)
	{
		//Start:Loading images into the browser cache
		top.topframe.main1.normalImage[i]		= new Image();
		top.topframe.main1.activeImage[i]		= new Image();
		top.topframe.main1.adjacentImage[i]		= new Image();

		top.topframe.main1.normalImage[i].src = '/cms/cmsweb/images' + colorScheme + '/' + tree.childNodes[i].getAttribute("image") + ImageLang + '.jpg';
		top.topframe.main1.activeImage[i].src = '/cms/cmsweb/images' + colorScheme + '/' + tree.childNodes[i].getAttribute("image") + '_a' + ImageLang + '.jpg';
		top.topframe.main1.adjacentImage[i].src = '/cms/cmsweb/images' + colorScheme + '/' + tree.childNodes[i].getAttribute("image") + '_h' + ImageLang + '.jpg';
		//End:Loading images into the browser cache
	}
}
function ChangeImage(menuNumber)
{
	// if any active image is there, then make it normal
	if(top.topframe.main1.activeMenuBar != null && top.topframe.main1.activeMenuBar != 'undefined')
	{
		var arrActiveMenu = top.topframe.main1.activeMenuBar.split(',');
		top.topframe.document.getElementById('img' + parseInt(arrActiveMenu[0])).src = top.topframe.main1.normalImage[arrActiveMenu[0]].src;
		
		//change adjacent image to normal as well
		if(parseInt(arrActiveMenu[0]) != top.topframe.main1.treeMenu.length-1)
		{
			top.topframe.document.getElementById('img' + (parseInt(arrActiveMenu[0])+1)).src = top.topframe.main1.normalImage[parseInt(arrActiveMenu[0]) + 1].src;;
		}
	}
	//change top image to active image
	top.topframe.document.getElementById('img' + menuNumber).src = top.topframe.main1.activeImage[menuNumber].src;
	//change adjacent image to incorporate
	if(menuNumber != top.topframe.main1.treeMenu.length-1)
	{
		top.topframe.document.getElementById('img' + (parseInt(menuNumber)+1)).src = top.topframe.main1.adjacentImage[parseInt(menuNumber)+1].src;
	}
}

function RestoreImage(menuNumber)
{
	top.topframe.document.getElementById('img' + parseInt(menuNumber)).src = top.topframe.main1.normalImage[parseInt(menuNumber)].src;
	// make adjacent image normal as well
	if(parseInt(menuNumber) != top.topframe.main1.treeMenu.length-1)
	{
		top.topframe.document.getElementById('img' + (parseInt(menuNumber)+1)).src = top.topframe.main1.normalImage[parseInt(menuNumber)+1].src;
	}
}
function returnFalse()
{
	return false;
}
//---------------------------------------------------------------------------------------------------
function createDropMenu1(buttonNumber,menuNumber)
{
	var dropMenuLength = top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes.length;
	var menuText		= '';
	var linkUrl			= '';
	var classText		= '';
	var clickText		= '';
	var mouseOutText	= '';
	var styleMenus		= '';
	var subMenus		= '';
	var mouseOverText	= '';
	
	//var strHTML = '<div id="menu' + buttonNumber + menuNumber + '" class="menu" onmouseover="menuMouseover(event)">';
	var strHTML = '';
	
	for(var k = 0; k < dropMenuLength; k++)
	{
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].classText == 'undefined' || top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].classText == null) top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].classText = 'menuItem';
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].enabled == 'undefined' || top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].enabled == null) top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].enabled = true;
		
		menuText		= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].name;
		linkUrl			= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].linkUrl;
		classText = top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].classText;
	
		clickText		= 'callItemClicked("' + buttonNumber + ',' + menuNumber + ',' + k + '","' + linkUrl + '")';
		mouseOutText	= 'callItemMouseOut("' + buttonNumber + ',' + menuNumber + ',' + k + '");';
		styleMenus		= 'styleMenu("' + buttonNumber + ',' + menuNumber + ',' + k + '");';
		mouseOverText	= 'createSubMenu1(' + buttonNumber + ',' + menuNumber + ',' + k + ');menuItemMouseover(event,"menu' + buttonNumber + menuNumber + k + '");';
		
		if((!top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].enabled)||(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].enabled == "false"))
		{
			mouseOverText	= 'top.topframe.main1.mousein=true;';
			mouseOutText	= 'top.topframe.main1.mousein=false;';
			clickText		= '';
			classText		= 'menuItemDisable';
			top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].classText = classText;
		}
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[k].childNodes.length > 0)
		{
			mouseOverText	+= styleMenus;
			strHTML			+= '<a id=subMenuLink' + buttonNumber + menuNumber + k + ' class=' + classText + ' href=\'javascript:' + clickText + '\' onmouseover=' + mouseOverText + ' onmouseout=' + mouseOutText + '>' + '<span class="menuItemText">' + menuText + '</span><span class="menuItemArrow">&#9654;</span></a>';
		}
		else
		{
			mouseOverText	= styleMenus + 'hideSubMenu();';
			strHTML			+= '<a id=subMenuLink' + buttonNumber + menuNumber + k + ' class=' + classText + ' href=\'JavaScript:' + clickText + '\' onmouseover=' + mouseOverText + ' onmouseout=' + mouseOutText + '>' + menuText + '</a>';
		}
	}
	//strHTML += '</div>';
    top.botframe.document.getElementById('menu' + buttonNumber + menuNumber).innerHTML = strHTML;
    menuInit(top.botframe.document.getElementById('menu' + buttonNumber + menuNumber))
	//menuInit(eval('top.botframe.menu' + buttonNumber + menuNumber));
	/*for(var l=0; l< dropMenuLength; l++)
	{
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[l].childNodes.length > 0)
		{
			createSubMenu(buttonNumber,menuNumber,l);
		}
	}*/
}
function createSubMenu1(buttonNumber,menuNumber,itemNumber)
{
	var subMenuLength	= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes.length;
	//var strHTML			= '<div id="menu' + buttonNumber + menuNumber + itemNumber + '" class="menu" onmouseover="menuMouseover(event)">';
	var strHTML			= '';
	var menuText		= '';
	var linkUrl			= '';
	var clickText		= '';
	var classText		= '';
	var mouseOverText	= '';
	var mouseOutText	= '';
	var styleMenus		= '';
	for(var t=0; t< subMenuLength; t++)
	{
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].classText == 'undefined' || top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].classText == null) top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].classText = 'menuItem';
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].enabled == 'undefined' || top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].enabled == null) top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].enabled = true;
		
		menuText		= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].name;
		linkUrl			= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].linkUrl;
		clickText		= 'callItemClicked("' + buttonNumber + ',' + menuNumber + ',' + itemNumber + ',' + t + '","' + linkUrl + '")';
		classText		= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].classText;
		styleMenus		= 'styleMenu("' + buttonNumber + ',' + menuNumber + ',' + itemNumber + ',' + t + '");';
		mouseOutText	= 'callItemMouseOut("' + buttonNumber + ',' + menuNumber + ',' + itemNumber + ',' + t + '");';
		mouseOverText	= 'createSubSubMenu1(' + buttonNumber + ',' + menuNumber + ',' + itemNumber + ',' + t + ');menuItemMouseover(event,"menu' + buttonNumber + menuNumber + itemNumber + t + '");';
		mouseOverText	+= styleMenus;
		
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].childNodes.length == 0)
		{
			mouseOverText = styleMenus + 'hideSubSubMenu();';
		}
		
		if((!top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].enabled)||(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].enabled == "false"))
		{
			clickText		= 'return false;';
			mouseOverText	= 'top.topframe.main1.mousein=true;';
			mouseOutText	= 'top.topframe.main1.mousein=false;';
			classText		= 'menuItemDisable';
			top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].classText = classText;
		}
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[t].childNodes.length > 0)
		{
			strHTML += '<a id=subSubMenu' + buttonNumber + menuNumber+ itemNumber + t + ' class=' + classText + ' href=\'JavaScript:' + clickText + '\' onmouseout=' + mouseOutText + ' onmouseover=' + mouseOverText + '>' + '<span class="menuItemText">' + menuText + '</span><span class="menuItemArrow" >&#9654;</span></a>';
		}
		else
		{
			strHTML += '<a id=subSubMenu' + buttonNumber + menuNumber+ itemNumber + t + ' class=' + classText + ' href=\'JavaScript:' + clickText + '\' onmouseout=' + mouseOutText + ' onmouseover=' + mouseOverText + '>' + menuText + '</a>';
		}
	}
	//strHTML += "</div>";
	//top.botframe.main1.innerHTML += strHTML;
	//eval('top.botframe.menu' + buttonNumber + menuNumber + itemNumber).innerHTML = strHTML
	//menuInit(eval('top.botframe.menu' + buttonNumber + menuNumber + itemNumber));
	top.botframe.document.getElementById('menu' + buttonNumber + menuNumber + itemNumber).innerHTML = strHTML
	menuInit(top.botframe.document.getElementById('menu' + buttonNumber + menuNumber + itemNumber));
	/*for(var m=0; m< subMenuLength; m++)
	{
		if(main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[m].childNodes.length > 0)
			createSubSubMenu(buttonNumber,menuNumber,itemNumber,m);
	}*/
}

function createSubSubMenu1(buttonNumber,menuNumber,itemNumber,subItemNumber)
{
	var subSubMenuLength	= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes.length;
	//var strHTML				= '<div id="menu' + buttonNumber + menuNumber + itemNumber + subItemNumber + '" class="menu">';
	var strHTML				= '';
	var menuText			= '';
	var linkUrl				= '';
	var clickText			= '';
	var classText			= '';
	var mouseOverText		= '';
	var mouseOutText		= '';
	
	for(var n=0; n< subSubMenuLength; n++)
	{
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].classText == 'undefined' || top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].classText == null) top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].classText = 'menuItem';
		if(top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].enabled == 'undefined' || top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].enabled == null) top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].enabled = true;
		
		menuText		= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].name;
		linkUrl			= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].linkUrl;
		clickText		= 'callItemClicked("' + buttonNumber + ',' + menuNumber + ',' + itemNumber + ',' + subItemNumber + ',' + n + '","' + linkUrl + '")';
		classText		= top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].classText;
		
		mouseOverText	= 'styleMenu("' + buttonNumber + ',' + menuNumber + ',' + itemNumber + ',' + subItemNumber + ',' + n + '");';
		mouseOutText	= 'top.topframe.main1.mousein=false;';
		
		if(!top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].enabled)
		{
			mouseOverText	= 'top.topframe.main1.mousein=true;';
			mouseOutText	= 'top.topframe.main1.mousein=false;';
			clickText	= 'return false;';
			classText	= 'menuItemDisable';
			top.topframe.main1.treeMenu[buttonNumber].childNodes[menuNumber].childNodes[itemNumber].childNodes[subItemNumber].childNodes[n].classText = classText;
		}
		
		strHTML += '<a id=sub2SubMenu' + buttonNumber + menuNumber+ itemNumber + subItemNumber + n + ' class=' + classText + ' href=\'JavaScript:' + clickText + '\' onmouseout=' + mouseOutText + ' onmouseover=' + mouseOverText + '>' + menuText + '</a>';
	}
	//strHTML += "</div>";
	//top.botframe.main1.innerHTML += strHTML;
	//eval('top.botframe.menu' + buttonNumber + menuNumber + itemNumber + subItemNumber).innerHTML = strHTML;
	//menuInit(eval('top.botframe.menu' + buttonNumber + menuNumber + itemNumber + subItemNumber));
	top.botframe.document.getElementById('menu' + buttonNumber + menuNumber + itemNumber + subItemNumber).innerHTML = strHTML
	menuInit(top.botframe.document.getElementById('menu' + buttonNumber + menuNumber + itemNumber + subItemNumber));
}
