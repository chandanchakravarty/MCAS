function lib_bwcheck(){ //Browsercheck (needed)
	this.ver=navigator.appVersion; this.agent=navigator.userAgent
	this.dom=document.getElementById?1:0
	this.ie5=(this.ver.indexOf("MSIE 5")>-1 && this.dom)?1:0;
	this.ie6=(this.ver.indexOf("MSIE 6")>-1 && this.dom)?1:0;
	this.ie7=(this.ver.indexOf("MSIE 7")>-1 && this.dom)?1:0;
	this.ie4=(document.all && !this.dom)?1:0;
	this.ie=this.ie4||this.ie5||this.ie6||this.ie7
	this.mac=this.agent.indexOf("Mac")>-1
	this.opera5=this.agent.indexOf("Opera 5")>-1
	this.ns6=(this.dom && parseInt(this.ver) >= 5) ?1:0; 
	this.ns4=(document.layers && !this.dom)?1:0;
	this.bw=(this.ie7 || this.ie6 || this.ie5 || this.ie4 || this.ns4 || this.ns6 || this.opera5 || this.dom)
	return this
}

var bw=new lib_bwcheck()
var mDebugging=2
oCMenu=new makeCoolMenu("oCMenu")
oCMenu.useframes=0 
oCMenu.frame="frmMain"
oCMenu.useclick=0
oCMenu.useNS4links=1
oCMenu.NS4padding=2
oCMenu.checkselect=0
oCMenu.pagecheck=1  
oCMenu.checkscroll=0
oCMenu.resizecheck=1
oCMenu.wait=500

//Background bar properties

// Changed from 1 to 0 by Charles on 3-Jun-09 for Itrack 5930
oCMenu.usebar=0

//oCMenu.barcolor="#ff0000"
oCMenu.barwidth="100%"
//oCMenu.barheight="menu"
oCMenu.barx=0
oCMenu.bary=0
oCMenu.barinheritborder=0

//chitr...............Placement properties.......of main menu 
oCMenu.width="100%"
oCMenu.rows=1
oCMenu.fromleft=0 //placing of menu from left
oCMenu.fromright=0 //placing of menu from right
oCMenu.fromtop=514 //placing of menu from top
//oCMenu.pxbetween=0

//chitr......space between links.................................
oCMenu.menuplacement=new Array(98,"210","341")



//TOP LEVEL PROPERTIES

//chitr: width of top menu
oCMenu.level[0]=new Array()
oCMenu.level[0].width="100%"
oCMenu.level[0].height=20
//oCMenu.level[0].bgcoloroff="#0066FF" 
//oCMenu.level[0].bgcoloron="#000000" 



//(chitr....top links >>>>color of text of links)
oCMenu.level[0].textcolor="#000000" //before mouse over
oCMenu.level[0].hovercolor="#FFffff" //on mouse over
oCMenu.level[0].style="padding:5px; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px; font-weight:bold;" 
oCMenu.level[0].border=0 
oCMenu.level[0].bordercolor="#0066FF" 
oCMenu.level[0].offsetX=0
oCMenu.level[0].offsetY=-1
oCMenu.level[0].NS4font="Verdana, Arial, Helvetica, sans-serif"
oCMenu.level[0].NS4fontSize="3"



/*New: Added animation features that can be controlled on each level.*/
oCMenu.level[0].clip=1 
oCMenu.level[0].clippx=25 
oCMenu.level[0].cliptim=100 //Speed of the effect
oCMenu.level[0].filter="progid:DXImageTransform.Microsoft.GradientWipe(duration=0.3,wipeStyle=1)"
oCMenu.level[0].align="top"

//SUB LEVEL[1] PROPERTIES
oCMenu.level[1]=new Array()
//oCMenu.level[1].width=oCMenu.level[0].width-2



//chitr...... changing ..................width, color, font of buttons
oCMenu.level[1].width="100%" //buttons width
oCMenu.level[1].height=25 //buttons height
oCMenu.level[1].bgcoloroff="#1E4B7B" //buttons color before mouse over
oCMenu.level[1].bgcoloron="#336699" //buttons color on mouse over
oCMenu.level[1].textcolor="#ffffff" //text color before mouse over
oCMenu.level[1].hovercolor="ffffff" //text color on mouse over
oCMenu.level[1].style="padding:5px; font-family:Verdana; font-size:11px; font-weight:normal"
oCMenu.level[1].align="bottom" 
oCMenu.level[1].offsetX=-(oCMenu.level[0].width-2)/2+20
oCMenu.level[1].offsetY=0
oCMenu.level[1].border=1 
oCMenu.level[1].bordercolor="#0D2948"

//SUB LEVEL[2] PROPERTIES
oCMenu.level[2]=new Array()
oCMenu.level[2].width="100%"
oCMenu.level[2].height=35
oCMenu.level[2].bgcoloroff="000000" 
oCMenu.level[2].bgcoloron="FFFFEB"
oCMenu.level[2].textcolor="#006600"
oCMenu.level[2].hovercolor="000000"
oCMenu.level[2].style="padding:5px; font-family:Verdana, Arial, Helvetica, sans-serif; font-size:11px"
oCMenu.level[2].align="bottom" 
oCMenu.level[2].offsetX=-(oCMenu.level[0].width-2)/2+20
oCMenu.level[2].offsetY=0
oCMenu.level[2].border=1 
oCMenu.level[2].bordercolor="9c8c52"




//chitr................menu.............Links
// MENU PARAMETERS


		
		
//oCMenu.makeMenu('top1','','&nbsp;Disclaimer  | &nbsp;&nbsp;&nbsp;','')
//Added by Praveen Kumar(30-12-2008):Itrack 5195
oCMenu.makeMenu('top1','','&nbsp;Disclaimer   &nbsp;&nbsp;&nbsp;','/cms/cmsweb/companyprofile/disclaimer.pdf','_blank')
//	oCMenu.makeMenu('sub10','top1','- General','#')



//Commented by praveen Kumar (30-12-2008):Itack 5195
/*oCMenu.makeMenu('top3','','Company Information  | ','')
	oCMenu.makeMenu('sub30','top3','- Annual Statement','/cms/cmsweb/companyprofile/annual_statement-1.pdf','_blank')
	oCMenu.makeMenu('sub31','top3','- Board of Directors','/cms/cmsweb/companyprofile/directors.pdf','_blank')
	oCMenu.makeMenu('sub32','top3','- Officers & Dept. heads','/cms/cmsweb/companyprofile/officers_heads.pdf','_blank')
	//oCMenu.makeMenu('sub33','top3','- Employee directory','/cms/cmsweb/companyprofile/employee_directory.pdf')
	

oCMenu.makeMenu('top5','','&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Contact Us  | ','/cms/cmsweb/companyprofile/employee_directory.pdf','_blank')
	
*/
oCMenu.makeStyle(); oCMenu.construct()			

