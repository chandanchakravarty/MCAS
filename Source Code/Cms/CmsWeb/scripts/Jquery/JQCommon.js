


$(document).ready(function() {

    var Result;
    var Error;

    var xx = '';
    var yy = '';

    var Parameters = "{'x':'" + xx + "','y':'" + yy + "'}"; //Dynamic Parameters
    $('.AJAX').click(function(e) {

        CallAJAX(this.ServerMethod, Parameters, this.SuccessMethod, this.ErrorMethod, '#' + this.TargetControl, this.ItemValue, this.ItemText);
        return false;
    }
    );



    $('.FillDD').change(function(e) {


        if (parseInt($('#' + this.id).val()) > 0) {
            var Parameters = "{'Param':'" + document.getElementById(this.id).value + "'}";
            CallAJAX(this.ServerMethod, Parameters, this.SuccessMethod, this.ErrorMethod, '#' + this.TargetControl, this.ItemValue, this.ItemText);
        }
        //   return false;
    }
    );

    function CallAJAX(ServerMethod, Parameters, SuccessMethod, ErrorMethod, ControlID, ItemValue, ItemText) {

        $.ajax({

            type: "POST",
            url: ServerMethod,
            data: Parameters,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function(xhr, status, errorThrown) {
                Error = xhr;
                eval(ErrorMethod + "()");
            },
            success: function(msg) {
                Result = msg;
                var rr = SuccessMethod + "('" + ControlID + "','" + ItemValue + "','" + ItemText + "');";
                eval(rr);
                //eval(SuccessMethod + "()");//without parameter
            }
        });
    }

    function outputDT(ControlID, ItemValue, ItemText) {
        BindDropdown(ControlID, ItemValue, ItemText);

    }

    function outputDTSUBLOB(ControlID, ItemValue, ItemText) {

        BindDropdownForSUBLOB(ControlID, ItemValue, ItemText);
    }

    function BindDropdown(ControlID, ItemValue, ItemText) {

        var opt = '';
        opt += '<option value=0>' + " " + '</option>';
        if (Result.d.Table.length > 0) {
            for (var row in Result.d.Table) {

                if (row >= 0) {
                    opt += '<option value="' + eval('Result.d.Table[row].' + ItemValue) + '">' + eval('Result.d.Table[row].' + ItemText) + '</option>';
                }
            }
        }

        $(ControlID).html(opt);

    }
    function BindDropdownForSUBLOB(ControlID, ItemValue, ItemText) {
        var Gen = '';
        if (iLangID == 2)
            Gen = 'Geral';
        else
            Gen = 'General';
             
        var opt = '';
        opt += '<option value=-1>' + "" + '</option>';
        opt += '<option value=0>' + Gen + '</option>';
        if (Result.d.Table.length > 0) {
            for (var row in Result.d.Table) {

                if (row >= 0) {

                    opt += '<option value="' + eval('Result.d.Table[row].' + ItemValue) + '">' + eval('Result.d.Table[row].' + ItemText) + '</option>';
                }
            }
        }

        $(ControlID).html(opt);

    }

    function ShowError() {
        alert(Error.responseText);
    }

});

  

 
 