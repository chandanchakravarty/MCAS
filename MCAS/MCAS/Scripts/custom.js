var $border_color = "#efefef";
var $grid_color = "#ddd";
var $default_black = "#666";
var $primary = "#575348";
var $secondary = "#8FBB6C";
var $orange = "#F38733";

// SparkLine Bar
//$(function () {
//  $('#currentSale').sparkline([23213, 63216, 82234, 29341, 61789, 88732, 11234, 54328, 33245], {
//    type: 'bar',
//    barColor: [$orange, $secondary],
//    barWidth: 6,
//    height: 18
//  });

//  $('#currentBalance').sparkline([33251, 98123, 54312, 88763, 12341, 55672, 87904, 23412, 17632], {
//    type: 'bar',
//    barColor: [$secondary, $primary],
//    barWidth: 6,
//    height: 18
//  });
//  fkeypress
//});

// Date Range Picker

(function ($, h, c) { var a = $([]), e = $.resize = $.extend($.resize, {}), i, k = "setTimeout", j = "resize", d = j + "-special-event", b = "delay", f = "throttleWindow"; e[b] = 250; e[f] = true; $.event.special[j] = { setup: function () { if (!e[f] && this[k]) { return false } var l = $(this); a = a.add(l); $.data(this, d, { w: l.width(), h: l.height() }); if (a.length === 1) { g() } }, teardown: function () { if (!e[f] && this[k]) { return false } var l = $(this); a = a.not(l); l.removeData(d); if (!a.length) { clearTimeout(i) } }, add: function (l) { if (!e[f] && this[k]) { return false } var n; function m(s, o, p) { var q = $(this), r = $.data(this, d); r.w = o !== c ? o : q.width(); r.h = p !== c ? p : q.height(); n.apply(this, arguments) } if ($.isFunction(l)) { n = l; return m } else { n = l.handler; l.handler = m } } }; function g() { i = h[k](function () { a.each(function () { var n = $(this), m = n.width(), l = n.height(), o = $.data(this, d); if (m !== o.w || l !== o.h) { n.trigger(j, [o.w = m, o.h = l]) } }); g() }, e[b]) } })(jQuery, this);




//Sortable
$(function () {
    $(".sortable").sortable();
    $(".sortable").disableSelection();
});

//Datepicker
$(function () {
    $("#datepicker").datepicker({
        dateFormat: "dd/mm/yy",
        showStatus: true,
        showWeeks: true,
        currentText: 'Now',
        autoSize: true,
        gotoCurrent: true,
        showAnim: 'blind',
        highlightWeek: true,
        yearRange: "1951:2050",
        changeMonth: !0,
        changeYear: !0
    });
});

//Timer for tiles info
var x = 3395, y = 5578;

function incrementX() {
    x++;
    // document.getElementById('sales-x').innerHTML = x;
}
setInterval('incrementX()', 5000);

function incrementY() {
    y++;
    //document.getElementById('income-y').innerHTML = "$"+y;
}
setInterval('incrementY()', 3000);

//Dropdown Menu




//Sidebar
$('.arrow-box').click(function () {
    "0" == $("#sidebar").attr("left") ? ($("#sidebar").css("left", "-220px"), $(".dashboard-wrapper").css("margin-left", "0px"), $(".logo").css("background", "transparent")) : ($("#sidebar").css("left", "0"), $(".dashboard-wrapper").css("margin-left", "220px"), $(".logo").css("background", ""));
    $(".Systemmenu").css("display", "none");

});

//Tooltip
$('a').tooltip();

//Popover
$('button').popover();




$(function () {
    if ($("#divLoading").length == 0) {
        $("BODY").append('<div id="divLoading" class="loading"><p><br>Please wait...</br><img src="../Images/ajax-loader.gif" alt="Loading.."></p></div>');
    }
    showLoading();
    $("#btnSave").click(function () {
        if (!PageChanged())
            return false;
        else
            return true;
    });
});

$(window).load(function () {
    hideLoading();
});
//$(window).unload(function () {
//    showLoading();
//});
window.onbeforeunload = function () { showLoading(); }

// by Pravesh K Chandel to make Page Read Only mode and finding dirty page
$(document).ready(function () {
    $(window).resize(function () {
        if ($(window).width() > 768) {
            $("aside#sidebar").css("left", "0");
            $('.Claimmenu').css("display", "block");
            $('.Systemmenu').css("display", "none");
            $(".dashboard-wrapper").css("margin-left", "220px");
            $(".logo").css("background", "");
            $('.custom-search').css("display", "block");
            $('#wizard').css("display", "block");
            $("#top").css("top", parseInt($("#header").height()) + parseInt($("#reg2").height())).css("width", (parseInt($("#reg").width()) - 220));
            $("#testdiv").css("height", (parseInt($("div#top").height()) + parseInt($("div#header").is(":visible") ? $("div#header").height() : "0")));
        }
        else {
            $("aside#sidebar").css("left", "-220px");
            $(".dashboard-wrapper").css("margin-left", "0px");
            $(".logo").css("background", "transparent");
            $('.custom-search').css("display", "none");
            $('#wizard').css("display", "none");
            $("#top").css("width", $(".container").width());
            $("#testdiv").css("height", ($("div#Hormenuwizard").offset().top.toString()));
        }
    });




    $("#Systemadminanchor").bind("click", function () {
        "0px" == $("aside#sidebar").css("left") ? ($("aside#sidebar").css("left", "-220px"), $(".dashboard-wrapper").css("margin-left", "0px"), $(".logo").css("background", "transparent")) : ($("aside#sidebar").css("left", "0"), $(".dashboard-wrapper").css("margin-left", "220px"), $(".logo").css("background", ""));
        $(".Claimmenu").css("display", "none");
        $(".Systemmenu").css("display", "block");
    });
    $('#mob-nav,#mini-nav').bind("click", function () {
        "0px" == $("aside#sidebar").css("left") ? ($("aside#sidebar").css("left", "-220px"), $(".dashboard-wrapper").css("margin-left", "0px"), $(".logo").css("background", "transparent")) : ($("aside#sidebar").css("left", "0"), $(".dashboard-wrapper").css("margin-left", "220px"), $(".logo").css("background", ""));
        $("#toprow").css("width", "auto");
        $(".Claimmenu").css("display", "block");
        $(".Systemmenu").css("display", "none");
    });

    $("#aHormenu").bind("click", function () {
        $("#wizard").is(":visible") ? $('#wizard').css("display", "none") : $('#wizard').css("display", "block");
    });

    showLoading();

    ($("#ReadOnly").val() == "True" || $("#ViewMode").val() == "Read") && !$('form').hasClass("DonotDiableControl") && $("form").find(":input:not([id=Next]):not([id=Previous]):not([id=btnDialogeOK]):not(.DonotDiableControl)").each(function () {
        switch (this.type) {
            case "text":
                $(this).attr('disabled', 'disabled')
            case "textarea":
                $(this).attr('disabled', 'disabled')
            case "checkbox":
                $(this).attr('disabled', 'disabled')
            case "radio":
                $(this).attr('disabled', 'disabled')
            case "button":
                $(this).attr('disabled', 'disabled')
            case "file":
                $(this).attr('disabled', 'disabled')

        }
    }) && $('select:not(.DonotDiableControl)').attr('disabled', 'disabled') && $('button:not([id=btnDialogeOK]):not(.DonotDiableControl)').attr('disabled', 'disabled') && $('#btnSave,#btnReset').attr('disabled', 'disabled');

    $("form").submit(function (e) {
        var isvalid = true;
        $("form").each(function (e, t) {
            $(this).validate();
            if (!$(this).valid()) isvalid = false;
        });
        if (isvalid) {
            showLoading();
        }
    });

    $(".DonotDiableControl").removeAttr("disabled");
    hideLoading();
    try {
        $(".datepicker").mask("99/99/9999", { placeholder: "DD/MM/YYYY" });
        $(".apply_datemask").mask("99/99/9999", { placeholder: "DD/MM/YYYY" });
        $(".apply_timemask").mask("99:99", { placeholder: "__:__" });
    } catch (e) { }
    $("input[type=text]").each(function () {
        if (void 0 !== $(this).attr("data-val-date") && "The field " == $(this).attr("data-val-date").substring(0, 10)) {
            var a = $(this).attr("data-val-date").substring(10, $(this).attr("data-val-date").length).replace("must be a date.", vDateFormat);
            $(this).removeAttr("data-val-date");
            $(this).attr("data-val-date", a)
        }
    });

    $('td').each(function () {
        if ($(this).hasClass("format")) {
            if ($(this).html().trim() != "") {
                var b = "-1" == $(this).html().trim().toString().indexOf("-") ? "" : "-", a = $(this).html().trim().toString().replace(/,/g, "");
                "" == a || isNaN(a) || $(this).html(b + CurrencyFormat($(this).html().trim().replace(/,/g, "")));
            }
        };
    });

    //------------------------------------------------Keypress-Start-----------------------------------------------------------------------------------
    $("input[type=text], textarea").keypress(function (e) {
        var evt = e || window.event, a = evt.which ? evt.which : evt.keyCode, keytoallow = [], keytoallow = ["45", "8", "46", "118"], arrowkey = [], arrowkey = ["ArrowDown", "ArrowUp", "ArrowLeft", "ArrowRight", "Enter", "Escape"], maxlen = parseInt($(this).attr("maxlength")), noofcomma = (parseInt((maxlen - 3) / 4, 10)), ctext = $(this).val().replace(/,/g, "");

        if (1 == evt.ctrlKey && ("118" == a || "86" == a || "99" == a)) return !0;
        //-----------------------Negative Number Keypress-Start-----------------------
        if ($(this).hasClass("Number") && $(this).hasClass("format") && $(this).hasClass("NegativeAllow")) { var b = /\./.test(ctext) ? maxlen - noofcomma : maxlen - (noofcomma + 1), b = 1 < ctext.toString().split("-").length || 45 == a ? b + 1 : b; if (45 != a && 46 != a && 31 < a && (48 > a || 57 < a) || !isTextSelected(document.getElementById($(this).context.id))) return 46 == a && !parseInt(ctext.length).between(1, b - 2) || 46 == a && /\./.test(ctext) || 45 == a && 0 != document.getElementById($(this).context.id).selectionStart || 46 != a && parseInt(ctext.length) == b - 2 && 1 == ctext.toString().split(".").length || 45 != a && 46 != a && 31 < a && (48 > a || 57 < a) ? ($("#" + ($(this).id || $(this).context.id)).focus(), !1) : !0; if (isTextSelected(document.getElementById($(this).context.id)) && b > parseInt(ctext.length)) return 45 != a ? !0 : isTextSelected(document.getElementById($(this).context.id)) && 0 == document.getElementById($(this).context.id).selectionStart ? !0 : !1 }

        if ($(this).hasClass("Number") && !$(this).hasClass("format") && $(this).hasClass("NegativeAllow")) return b = $(this).val().length, c = maxlen - 3, c == parseInt(b) && isTextSelected(document.getElementById($(this).context.id)) || c > parseInt(b) ? (b = "#" + this.id, 45 == a && 0 != $(this).val().length || 45 != a && 31 < a && (48 > a || 57 < a) ? ($(b).focus(), !1) : !0) : !1;
        //-----------------------Negative Number Keypress-End-----------------------


        //----------- Number Keypress-Start-----------------------
        if ($(this).hasClass("Number") && $(this).hasClass("format") && !$(this).hasClass("NegativeAllow")) return b = ctext == "" ? 0 : ctext.length, c = /\./.test(ctext) ? maxlen - noofcomma : maxlen - (noofcomma + 1), 46 != a && 31 < a && (48 > a || 57 < a) || !isTextSelected(document.getElementById($(this).context.id)) ? isTextSelected(document.getElementById($(this).context.id)) || c > parseInt(ctext.length) ? (46 == a && !parseInt(ctext.length).between(1, c - 2) || /\./.test(ctext) && 46 == a || 46 != a && parseInt(ctext.length) == c - 2 && 1 == ctext.toString().split(".").length || 46 != a && 31 < a && (48 > a || 57 < a) ? ($("#" + ($(this).id || $(this).context.id)).focus(), !1) : !0) : !1 : !0;



        if ($(this).hasClass("Number") && !$(this).hasClass("format") && !$(this).hasClass("NegativeAllow")) {
            var c = maxlen, b = $(this).val().length;
            return ((-1 < $.inArray(a.toString(), keytoallow)) || (-1 < $.inArray(typeof (e.key) != "undefined" ? e.key.toString() : "", arrowkey))) ? !0 : isTextSelected(document.getElementById($(this).context.id)) || c >= parseInt(b) ? 31 < a && (48 > a || 57 < a) ? (b = "#" + this.id, $(b).focus(), !1) : !0 : !1
        }
        //-----------  Number Keypress-End-----------------------
        if ($(this).hasClass("alpha-only")) {
            if (((e.which < 97 || e.which > 122) && (e.which < 65 || e.which > 90)) && e.which != 32 && e.which != 8 && e.which != 0) {
                e.preventDefault();
            }
        }

        if ($(this).hasClass("alphanumeric")) {
            var c = parseInt($(this).attr("maxlength")), b = $(this).val().length;
            return ((-1 < $.inArray(a.toString(), keytoallow)) || (-1 < $.inArray(typeof (e.key) != "undefined" ? e.key.toString() : "", arrowkey))) ? !0 : isTextSelected(document.getElementById($(this).context.id)) || c > parseInt(b) ? 32 == a ? !0 : /^[a-z0-9]+$/i.test(String.fromCharCode(a)) ? !0 : !1 : !1
        }

        if (void 0 !== $(this).attr("strlen")) {
            var c = parseInt($(this).attr("strlen")), b = $(this).val().length;
            return ((-1 < $.inArray(a.toString(), keytoallow)) || (-1 < $.inArray(typeof (e.key) != "undefined" ? e.key.toString() : "", arrowkey))) ? !0 : isTextSelected(document.getElementById($(this).context.id)) || c > parseInt(b) ? 32 == a ? !0 : /[a-z]+$/i.test(String.fromCharCode(a)) ? !0 : !1 : !1
        }

        if (void 0 !== $(this).attr("maxlength")) return b = $(this).val().length, c = parseInt($(this).attr("maxlength")), ((-1 < $.inArray(a.toString(), keytoallow)) || (-1 < $.inArray(typeof (e.key) != "undefined" ? e.key.toString() : "", arrowkey))) ? !0 : c == parseInt(b) && isTextSelected(document.getElementById($(this).context.id)) || c > parseInt(b) ? !0 : !1;
        $(this).valid();
    });
    //------------------------------------------------Keypress-End-----------------------------------------------------------------------------------
    $("input[type=text]").blur(function () {
        if ($(this).hasClass("format") && "" != $(this).val()) {
            var b = "-" == $(this).val().charAt(0) ? "-" : "", a = $(this).val().replace(/,/g, "");
            "" == a || isNaN(a) || $(this).val(b + CurrencyFormat($(this).val().replace(/,/g, "")));
            $(this).valid()
        }
        $(this).hasClass("format") && "" == $(this).val() && ($(this).val("0.00"), $(this).valid());
    }).each(function () {
        $(this).hasClass("format") && $(this).addClass("rightJustified")
    });

    $("input[type=text], textarea").on("paste", function (a) {

        //-----------  Get Text To Be Paste-Start-----------  
        var maxlen = parseInt($(this).attr("maxlength")), noofcomma = (parseInt((maxlen - 3) / 4, 10));
        var c, pastedText, c = ("" == $(this).val() || "0.00" == $(this).val()) ? 0 : (!isTextSelected(document.getElementById($(this).context.id)) ? $(this).val().length : ($(this).val().length - Math.abs((document.getElementById($(this).context.id).selectionEnd - document.getElementById($(this).context.id).selectionStart))));
        pastedText = ((a.originalEvent || a).clipboardData !== undefined) ? (a.originalEvent || a).clipboardData.getData("text/plain") : window.clipboardData.getData("Text");
        pastedText = pastedText.replace(/,/g, "");
        //-----------  Get Text To Be Paste-End-----------  


        //-----------  For Negative Number Validation-Start-----------  
        if ($(this).hasClass("Number") && $(this).hasClass("format") && $(this).hasClass("NegativeAllow")) if (a = /^\s*-?[1-9]\d*(\.\d{1,2})?\s*$/.test(pastedText)) {
            a = parseInt(pastedText.length) + c; var b = /\./.test(pastedText) ? (maxlen - 2) : (maxlen - 3) - noofcomma, b = "-" == pastedText.toString().charAt(0) ? b + 1 : b; if (/\./.test(pastedText) && pastedText.split(".")[0].length > (b - 3)) return alert("Maximum length before decimal is " + ("-" == pastedText.toString().charAt(0) ? b - 4 : b - 3) + ",so cannot be paste. "),
!1; if (a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are allowed."), !1;
            if (!/^\s*-?[1-9]\d*(\.\d{1,2})?\s*$/.test((($(this).val().substring(0, $(this)[0].selectionStart) + pastedText + $(this).val().substring($(this)[0].selectionEnd, $(this).val().length)).toString().substring(0, b)))) return alert("Invalid decimal number, so cannot be paste."), !1;
        } else return alert("Invalid decimal number, so cannot be paste."), !1

        if ($(this).hasClass("Number") && !$(this).hasClass("format") && $(this).hasClass("NegativeAllow")) if (a = /^-?\d+$/.test(pastedText)) {
            a = parseInt(pastedText.length) + c;
            var b = maxlen;
            if (a != b && a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are allowed."), !1
        } else return alert("String contains non numeric digit, so cannot be paste."), !1;

        //-----------  For Negative Number Validation-End-----------  


        //-----------  For Number Validation-Start-----------  
        if (void 0 !== $(this).attr("numlen")) if (a = /^\d+$/.test(pastedText)) {
            a = parseInt(pastedText.length) + c;
            var b = parseInt($(this).attr("numlen"));
            if (a != b && a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are allowed."), !1
        } else return alert("String contains non numeric digit, so cannot be paste."), !1;

        if ($(this).hasClass("Number") && $(this).hasClass("format") && !$(this).hasClass("NegativeAllow")) if (a = /^\s*-?[1-9]\d*(\.\d{1,2})?\s*$/.test(pastedText)) {
            a = parseInt(pastedText.length) + c;
            var b = /\./.test(pastedText) ? (maxlen - 2) : (maxlen - 3) - noofcomma;
            if ("-" == pastedText.toString().charAt(0)) return alert("Negative number not allowed"), !1;
            if (/\./.test(pastedText) && pastedText.split(".")[0].length > b - 3) return alert("Maximum length before decimal is " + (b - 3) + ",so cannot be paste. "), !1;
            if (a != b && a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are allowed."), !1;
            if (!/^\s*-?[1-9]\d*(\.\d{1,2})?\s*$/.test((($(this).val().substring(0, $(this)[0].selectionStart) + pastedText + $(this).val().substring($(this)[0].selectionEnd, $(this).val().length)).toString().substring(0, b)))) return alert("Invalid decimal number, so cannot be paste."), !1;
        } else return alert("Invalid decimal number, so cannot be paste."), !1




        if ($(this).hasClass("Number") && !$(this).hasClass("format") && !$(this).hasClass("NegativeAllow")) if (a = /^\d+$/.test(pastedText)) {
            var b = maxlen;
            if (a != b && a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are allowed."), !1
        } else return alert("String contains non numeric digit, so cannot be paste."), !1;

        //-----------  For Number Validation-End----------- 
        //        if (culture_ == "en-US") {
        //            if ($(this).hasClass("alpha-only")) if (a = /^[a-zA-Z ]*$/.test(pastedText)) {
        var b = maxlen;
        if (a != b && a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are allowed."), !1
        //            } else return alert("String contains numeric digit, so cannot be paste."), !1;

        //            if (void 0 !== $(this).attr("strlen")) if (a = /^[a-zA-Z ]*$/.test(pastedText)) {
        if (a = parseInt(pastedText.length) + c, b = parseInt($(this).attr("strlen")), a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are allowed."), !1
        //            } else return alert("String contains numeric digit, so cannot be paste."), !1;


        //            if ($(this).hasClass("alphanumeric")) if (a = /^[a-z0-9]+$/i.test(pastedText)) {
        //                var b = maxlen;
        //                if (a != b && a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are paste."), !1
        //            } else return alert("String contains special character, so cannot be paste."), !1;


        //        }
        //        else {
        //            if ($(this).hasClass("alpha-only")) if (a = /^[0-9-]*$/.test(pastedText)) {
        //                alert("String contains numeric digit, so cannot be paste."), !1;
        //            }
        //            else {
        //                var b = maxlen;
        //                if (a != b && a > b) return $(this).val((pastedText + $(this).val()).toString().substring(0, b)), alert("Only " + b + " characters are paste."), !1
        //            }
        //        }
        if (void 0 !== $(this).attr("maxlength")) {
            a = parseInt(pastedText.length) + c,
          b = parseInt($(this).attr("maxlength"));
            if (a != b && a > b) {
                alert("Only " + b + " characters are allowed.");
                return $(this).val().toString().substring(0, b);
            }
            //return alert("Only " + b + " characters are paste.");// !1;
            else
                $(this).valid()
        }

    });




    $('input[type=text]').each(function () {
        if ($(this).hasClass("format")) {
            if ($(this).val() != "") {
                var b = "-" == $(this).val().charAt(0) ? "-" : "";
                $(this).val(b + CurrencyFormat($(this).val()));
            }
        };
    });
});

function isTextSelected(input) {
    if (typeof input.selectionStart == "number") {
        return input.selectionStart - input.selectionEnd != 0;
    }
};
function CurrencyFormat(number) {
    var decimalplaces = 2;
    var decimalcharacter = ".";
    var thousandseparater = ",";
    number = parseFloat(number);
    var sign = number < 0 ? "-" : "";
    var formatted = new String(number.toFixed(decimalplaces));
    if (decimalcharacter.length && decimalcharacter != ".") { formatted = formatted.replace(/\./, decimalcharacter); }
    var integer = "";
    var fraction = "";
    var strnumber = new String(formatted);
    var dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
    if (dotpos > -1) {
        if (dotpos) { integer = strnumber.substr(0, dotpos); }
        fraction = strnumber.substr(dotpos + 1);
    }
    else { integer = strnumber; }
    if (integer) { integer = String(Math.abs(integer)); }
    while (fraction.length < decimalplaces) { fraction += "0"; }
    temparray = new Array();
    while (integer.length > 3) {
        temparray.unshift(integer.substr(-3));
        integer = integer.substr(0, integer.length - 3);
    }
    temparray.unshift(integer);
    integer = temparray.join(thousandseparater);
    return integer + decimalcharacter + fraction;
}
Number.prototype.between = function (e, t) {
    return e < t ? this >= e && this <= t : this >= t && this <= e
}

function showLoading() {
    $("#divLoading").show();
}
function hideLoading() {
    $("#divLoading").hide();
}
var skipDirtyChecking = false;

/*$(window).unload(function() {
if (!skipDirtyChecking && IsPageChanged() == true) {
var response = confirm('Would you like to save your changes before exiting?'); 
if (response) $(window).submit();
}    
});
*/
PageChanged = function () {
    if (!IsPageChanged()) {
        /*var response = confirm('There is no change on the page. Would you like to save this page?');
        if (response) {
        $(window).submit();
        return true;
        }
        else
        */
        //alert('No Changes Have Been Made!');
        alert(vNoChange);

        return false;
    }
    return true;
};

function IsPageChanged() {
    var IsChanges = false;
    $(":input:not(:button):not([type=hidden])").each(function () {
        if ((this.type == "text" || this.type == "textarea" || this.type == "hidden" || this.type == "file" || this.type == "password")) {
            if (!($(this).hasClass("format") && "" == this.defaultValue && "0.00" == this.value || $(this).hasClass("format") && "" != this.defaultValue && this.value == CurrencyFormat(this.defaultValue.replace(/,/g, "")) || this.defaultValue == this.value)) {
                if ($.isNumeric(this.value.replace(/,/g, ""))) {
                    if (parseFloat(this.value.replace(/,/g, "")) == parseFloat(this.defaultValue.replace(/,/g, ""))) {

                    }
                    else {
                        return IsChanges = !0
                    }
                }
                else {
                    if ($(this).attr("readonly")) {
                    }
                    else {
                        if (!$(this).is(":visible")) {

                        }
                        else {
                            if (this.defaultValue == "0.00") {
                                if (parseFloat(this.value.replace(/,/g, "")) == parseFloat(this.defaultValue.replace(/,/g, ""))) {
                                }
                            }
                            else {
                                if (!($(this).hasClass("ignoreAlert"))) {
                                    return IsChanges = !0
                                }
                            }
                        }
                    }
                }
            }
        }
        else {
            if ((this.type == "radio" || this.type == "checkbox") && this.defaultChecked != this.checked && !($(this).hasClass("ignoreAlert"))) {
                IsChanges = true;
                return true;
            }
            else {
                if ((this.type == "select-one" || this.type == "select-multiple")) {
                    //console.log($(this).find(":selected").text());
                    //console.log($(this).find(":selected").value);
                    if (!($(this).find(":selected").text().contains("Select")) && !this.disabled && $(this).is(":visible") && !($(this).hasClass("ignoreAlert"))) {
                        IsChanges = true;
                        return true;
                    } 
                    for (var x = 0; x < this.length; x++) {
                        if ((this.options[x].selected != this.options[x].defaultSelected) && this.options[x].outerHTML.contains("selected=") && $(this).is(":visible") && !($(this).hasClass("ignoreAlert"))) {
                            IsChanges = true;
                            return true;
                        }
                       
                    }
                }
            }
        }
    });
    return IsChanges;
}

var showSearchDiv = true;
$("#expanddiv").click(function () {
    $('#searchdiv').toggle('slow');
    var cname = $("#expanddiv").attr('class')
    if (cname == "SearchCriteriaClose") {
        $("#expanddiv").removeClass("SearchCriteriaClose").addClass("SearchCriteriaExpand");
    }
    else {
        $("#expanddiv").removeClass("SearchCriteriaExpand").addClass("SearchCriteriaClose");
    }
});

window.alert = function (message, obj) {
    $('<div />').text(message).dialog({
        modal: true,
        title: 'CRIS+',
        buttons: [{
            id: 'btnDialogeOK',
            text: 'Ok',
            class: 'DonotDiableControl',
            click: function () {

                $(this).dialog('close');
                if (obj) {
                    setTimeout(function () { document.getElementById(obj).focus(); }, 10);
                    $("#" + obj).focus();
                }
            }
        }]
        ,
        close: function () { $(this).dialog('destroy').remove(); }
    });

};

function CreateDialog(TexttoDisplay, okText, cancelText, okCallback, cancelCallback) {
    $("<div id='CreateDialog_id'></div>").text(TexttoDisplay).dialog({
        modal: true,
        title: 'CRIS+',
        draggable: false,
        buttons: [{
            id: 'btnDialogeOK',
            text: okText,
            click: function () {
                $(this).dialog("close");
                typeof (okCallback) == "function" && okCallback();
            }
        }, {
            id: 'btnDialogeCancel',
            text: cancelText,
            click: function () {
                $(this).dialog("close");
                typeof (cancelCallback) == "function" && cancelCallback();
            }
        }]
    }).height('auto').attr('id', 'dialogId');
    $("#btnDialogeOK").closest("div").css({
        "display": "block",
        "margin": "0 auto",
        "text-align": "center",
        "float": "none !important"
    });
};

function MobileNumberLengthCheck(ControlId, Msg) {
    if ($.trim($("#" + ControlId).val()).length < 3 || $.trim($("#" + ControlId).val()).length > 15) {
        alert(Msg, ControlId);
        return false;
    }
};
function FormatValue() {
    $("input[type=text]").each(function () {
        if ($(this).hasClass("format") && "" != $(this).val()) {
            var a = $(this).context.id, b = $(this).val().replace(/,/g, "");
            $("#" + a).val(b.replace("NaN", "0"))
        }
    });
};
function checkKeyCode(evt) {
    var evt = evt || window.event, a = evt.which ? evt.which : evt.keyCode
    if (a == 116) {
        evt.keyCode = 0;
        if ($('#reload').attr("href") != "" && $('#reload').attr("href") != undefined) {
            evt.preventDefault();
            window.location.href = $('#reload').attr("href");
            return false;
        }
    }
};
document.onkeydown = checkKeyCode;
String.prototype.contains = function (a) {
    return -1 != this.indexOf(a)
};