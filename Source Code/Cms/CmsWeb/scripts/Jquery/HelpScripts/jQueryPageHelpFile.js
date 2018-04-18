/******************************************************************************************
<Author					: -  Pradeep Kushwaha 
<Start Date				: -	 01 - Oct - 2010
<End Date				: -	
<Description			: -  Help implementaion using Jquery
<Review Date			: - 
<Reviewed By			: - 	
 
Modification History
<Modified Date			: 
<Modified By			: 
<Purpose				: 
						
****************************************************************************************** */
$(document).ready(function() {

    $('body').bind('help', function() {
        return false;
    });
    $(window).load(function() {});
    $(function() {
        $(document).keydown(function(e) {

            var key = (e.keyCode ? e.keyCode : e.charCode);
            
            switch (key) {
                case 112:
                    var SourceControlID = e.srcElement.id;
                    OpenHelpFile(SourceControlID);
                    break;
                default: ;
            }
        });
    });


});
 
function OpenHelpFile(SourceControlID) {
    
    var Url = "http://" + window.location.host + "/Cms/CmsHelp/Cmshelpfile.htm?PageControlId=" + SourceControlID + "&CultureType=" + iLangID;// "pt-BR";

    window.open(Url, "mywindow", 'left=50,top=20,width=700,scrollbars=1,height=500,toolbar=0,resizable=1');
    
}

function init() {

   // debugger;    
    // confirm browser supports needed features and load .xml file
    var PageControlId = $.query.get('PageControlId');
    var CultureType = $.query.get('CultureType');
    
    switch (CultureType) {
        case 2:
            CultureType = "pt-BR";
            break;
        default:
            CultureType = "en-US";
            break;
    }
        
    var xFile = "http://" + document.location.host + "/Cms/CmsHelp/CmsHelpXmlFile/Client/Aspx/AddCustomers.xml";

    $.get(xFile, function(d) {

       // debugger;

        var CultureXMLNodes = {};

        if (d.childNodes[1].childNodes[0].childNodes[1].attributes[0].nodeTypedValue == CultureType)  
            CultureXMLNodes = d.childNodes[1].childNodes[0].childNodes[1];
        else  
            CultureXMLNodes = d.childNodes[1].childNodes[0].childNodes[0];

        $('body').append('<h1 id=Headerid> Customer help  </h1>');
        $('body').append('<dl>');

        $(CultureXMLNodes).find('FieldName').each(function() {

            var $FieldName = $(this);
            var FieldNameArribute = $FieldName.attr("Name");
            var title = $FieldName.find('Title').text();
            var Paragraph = $FieldName.find('Paragraph').text();

            var html = '<div id=\"new' + FieldNameArribute + '\" > <P class="msg_head"><img id=\"' + FieldNameArribute + '\" src=/Cms/CmsWeb/Images/nextoff.gif >' + " " + title + ' </P>';
            html += '<div id=\"para' + FieldNameArribute + '\"  class="msg_body"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' + Paragraph + '<div align=right ><a href=#Headerid>top</a></div></div></div>';
            html += '</dd>';

            $('dl').append($(html));

        });

        //hide the all of the element with class msg_body
        //debugger;
        $(".msg_body").hide();

        //toggle the componenet with class msg_body
        $(".msg_head").click(function() {
            $(this).next(".msg_body").slideToggle(200);
            //debugger;
            //alert($(this).next(".msg_body").context.all[0].id);
            var divid = $(this).next(".msg_body").context.all[0].id;
            
            if (document.getElementById(divid).src == "http://"+ document.location.host + "/Cms/CmsWeb/Images/nextoff.gif") {
                $('#' + divid).attr("src", "/Cms/CmsWeb/Images/prevHelp.gif");
            }
            else {
                $('#' + divid).attr("src", "/Cms/CmsWeb/Images/nextoff.gif");
            }

        });
       
        if ($('#' + PageControlId).attr('src') == "/Cms/CmsWeb/Images/nextoff.gif")
            $('#' + PageControlId).attr("src", "/Cms/CmsWeb/Images/prevHelp.gif");
        else
            $('#' + PageControlId).attr("src", "/Cms/CmsWeb/Images/nextoff.gif");

        $('#para' + PageControlId).show();
        window.location.hash = '#' + PageControlId;

    });

}
 
