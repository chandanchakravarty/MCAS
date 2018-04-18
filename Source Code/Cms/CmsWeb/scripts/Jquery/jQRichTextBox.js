/*
* jHtmlArea 0.7.0 - WYSIWYG Html Editor jQuery Plugin
* Copyright (c) 2009 Chris Pietschmann
* http://jhtmlarea.codeplex.com
* Licensed under the Microsoft Reciprocal License (Ms-RL)
* http://jhtmlarea.codeplex.com/license
*/
(function($) {
    $.fn.htmlarea = function(opts) {
        if (opts && typeof (opts) === "string") {
            var args = [];
            for (var i = 1; i < arguments.length; i++) { args.push(arguments[i]); }
            var htmlarea = jHtmlArea(this[0]);
            var f = htmlarea[opts];
            if (f) { return f.apply(htmlarea, args); }
        }
        return this.each(function() { jHtmlArea(this, opts); });
    };
    var jHtmlArea = window.jHtmlArea = function(elem, options) {
        if (elem.jquery) {
            return jHtmlArea(elem[0]);
        }
        if (elem.jhtmlareaObject) {
            return elem.jhtmlareaObject;
        } else {
            return new jHtmlArea.fn.init(elem, options);
        }
    };
    jHtmlArea.fn = jHtmlArea.prototype = {

        // The current version of jHtmlArea being used
        jhtmlarea: "0.7.0",

        init: function(elem, options) {
            if (elem.nodeName.toLowerCase() === "textarea") {
                var opts = $.extend({}, jHtmlArea.defaultOptions, options);
                elem.jhtmlareaObject = this;

                var textarea = this.textarea = $(elem);
                var container = this.container = $("<div/>").addClass("jHtmlArea").width(textarea.width()).insertAfter(textarea);

                var toolbar = this.toolbar = $("<div/>").addClass("ToolBar").appendTo(container);
                priv.initToolBar.call(this, opts);

                var iframe = this.iframe = $("<iframe id='RichHtmlArea'/>").height(textarea.height());
                iframe.width(textarea.width() - ($.browser.msie ? 0 : 4));
                var htmlarea = this.htmlarea = $("<div/>").append(iframe);

                container.append(htmlarea).append(textarea.hide());

                priv.initEditor.call(this, opts);
                priv.attachEditorEvents.call(this);

                // Fix total height to match TextArea
                iframe.height(iframe.height() - toolbar.height());
                toolbar.width(textarea.width() - 2);

                if (opts.loaded) { opts.loaded.call(this); }
            }
        },
        dispose: function() {
            this.textarea.show().insertAfter(this.container);
            this.container.remove();
            this.textarea[0].jhtmlareaObject = null;
        },
        execCommand: function(a, b, c) {
            this.iframe[0].contentWindow.focus();
            this.editor.execCommand(a, b || false, c || null);
            this.updateTextArea();
        },
        ec: function(a, b, c) {
            this.execCommand(a, b, c);
        },
        queryCommandValue: function(a) {
            this.iframe[0].contentWindow.focus();
            return this.editor.queryCommandValue(a);
        },
        qc: function(a) {
            return this.queryCommandValue(a);
        },
        getSelectedHTML: function() {
            if ($.browser.msie) {
                return this.getRange().htmlText;
            } else {
                var elem = this.getRange().cloneContents();
                return $("<p/>").append($(elem)).html();
            }
        },
        getSelection: function() {
            if ($.browser.msie) {
                //return (this.editor.parentWindow.getSelection) ? this.editor.parentWindow.getSelection() : this.editor.selection;
                return this.editor.selection;
            } else {
                return this.iframe[0].contentDocument.defaultView.getSelection();
            }
        },
        getRange: function() {
            var s = this.getSelection();
            if (!s) { return null; }
            //return (s.rangeCount > 0) ? s.getRangeAt(0) : s.createRange();
            return (s.getRangeAt) ? s.getRangeAt(0) : s.createRange();
        },
        html: function(v) {
            if (v) {
                this.pastHTML(v);
            } else {
                return toHtmlString();
            }
        },
        pasteHTML: function(html) {
            this.iframe[0].contentWindow.focus();
            var r = this.getRange();
            if ($.browser.msie) {
                r.pasteHTML(html);
            } else if ($.browser.mozilla) {
                r.deleteContents();
                r.insertNode($((html.indexOf("<") != 0) ? $("<span/>").append(html) : html)[0]);
            } else { // Safari
                r.deleteContents();
                r.insertNode($(this.iframe[0].contentWindow.document.createElement("span")).append($((html.indexOf("<") != 0) ? "<span>" + html + "</span>" : html))[0]);
            }
            r.collapse(false);
            r.select();
        },
        cut: function() {
            this.ec("cut");
        },
        copy: function() {
            this.ec("copy");
        },
        paste: function() {
            this.ec("paste");
        },
        bold: function() { this.ec("bold"); },
        italic: function() { this.ec("italic"); },
        underline: function() { this.ec("underline"); },
        strikeThrough: function() { this.ec("strikethrough"); },
        image: function(url) {
            if ($.browser.msie && !url) {
                this.ec("insertImage", true);
            } else {
                this.ec("insertImage", false, (url || prompt("Image URL:", "http://")));
            }
        },
        removeFormat: function() {
            this.ec("removeFormat", false, []);
            this.unlink();
        },
        link: function() {
            if ($.browser.msie) {
                this.ec("createLink", true);
            } else {
                this.ec("createLink", false, prompt("Link URL:", "http://"));
            }
        },
        unlink: function() { this.ec("unlink", false, []); },
        orderedList: function() { this.ec("insertorderedlist"); },
        unorderedList: function() { this.ec("insertunorderedlist"); },
        superscript: function() { this.ec("superscript"); },
        subscript: function() { this.ec("subscript"); },

        p: function() {
            this.formatBlock("<p>");
        },
        h1: function() {
            this.heading(1);
        },
        h2: function() {
            this.heading(2);
        },
        h3: function() {
            this.heading(3);
        },
        h4: function() {
            this.heading(4);
        },
        h5: function() {
            this.heading(5);
        },
        h6: function() {
            this.heading(6);
        },
        heading: function(h) {
            this.formatBlock($.browser.msie ? "Heading " + h : "h" + h);
        },

        indent: function() {
            this.ec("indent");
        },
        outdent: function() {
            this.ec("outdent");
        },

        insertHorizontalRule: function() {
            this.ec("insertHorizontalRule", false, "ht");
        },

        justifyLeft: function() {
            this.ec("justifyLeft");
        },
        justifyCenter: function() {
            this.ec("justifyCenter");
        },
        justifyRight: function() {
            this.ec("justifyRight");
        },

        increaseFontSize: function() {
            if ($.browser.msie) {
                this.ec("fontSize", false, this.qc("fontSize") + 1);
            } else if ($.browser.safari) {
                this.getRange().surroundContents($(this.iframe[0].contentWindow.document.createElement("span")).css("font-size", "larger")[0]);
            } else {
                this.ec("increaseFontSize", false, "big");
            }
        },
        decreaseFontSize: function() {
            if ($.browser.msie) {
                this.ec("fontSize", false, this.qc("fontSize") - 1);
            } else if ($.browser.safari) {
                this.getRange().surroundContents($(this.iframe[0].contentWindow.document.createElement("span")).css("font-size", "smaller")[0]);
            } else {
                this.ec("decreaseFontSize", false, "small");
            }
        },

        forecolor: function(c) {
            this.ec("foreColor", false, c || prompt("Enter HTML Color:", "#"));
        },

        formatBlock: function(v) {
            this.ec("formatblock", false, v || null);
        },

        showHTMLView: function() {
            this.updateTextArea();
            this.textarea.show();
            this.htmlarea.hide();
            $("ul li:not(li:has(a.html))", this.toolbar).hide();
            $("ul:not(:has(:visible))", this.toolbar).hide();
            $("ul li a.html", this.toolbar).addClass("highlighted");
        },
        hideHTMLView: function() {
            this.updateHtmlArea();
            this.textarea.hide();
            this.htmlarea.show();
            $("ul", this.toolbar).show();
            $("ul li", this.toolbar).show().find("a.html").removeClass("highlighted");
        },
        toggleHTMLView: function() {
            (this.textarea.is(":hidden")) ? this.showHTMLView() : this.hideHTMLView();
        },

        toHtmlString: function() {
            return this.editor.body.innerHTML;
        },
        toString: function() {
            return this.editor.body.innerText;
        },

        updateTextArea: function() {
            this.textarea.val(this.toHtmlString());
        },
        updateHtmlArea: function() {
            this.editor.body.innerHTML = this.textarea.val();
        }
    };
    jHtmlArea.fn.init.prototype = jHtmlArea.fn;

    jHtmlArea.defaultOptions = {
        toolbar: [
        ["html"], ["bold", "italic", "underline", "strikethrough", "|", "subscript", "superscript"],
        ["increasefontsize", "decreasefontsize"],
        ["orderedlist", "unorderedlist"],
        ["indent", "outdent"],
        ["justifyleft", "justifycenter", "justifyright"],
        ["link", "unlink", "image", "horizontalrule"],
        ["p", "h1", "h2", "h3", "h4", "h5", "h6"],
        ["cut", "copy", "paste"]
    ],
        css: null,
        toolbarText: {

        bold: iLangID == 2 ? "Negrito" : "bold", italic: iLangID == 2 ? "itálico" : "Italic", underline: iLangID == 2 ? "sublinhado" : "underline", strikethrough: iLangID == 2 ? "Tachado" : "Strike-Through        ", cut: iLangID == 2 ? "Recortar" : "Cut", copy: iLangID == 2 ? "Copiar" : "Copy", paste: iLangID == 2 ? "Colar" : "Paste", h1: iLangID == 2 ? "Header 1" : " Heading 1", h2: iLangID == 2 ? "Header 2 " : "Heading 2", h3: iLangID == 2 ? "Header 3" : "Heading 3", h4: iLangID == 2 ? "Header 4" : "Heading 4", h5: iLangID == 2 ? "Header 5" : "Heading 5", h6: iLangID == 2 ? "Header 6" : "Heading 6", p: iLangID == 2 ? "Parágrafo" : "Paragraph",
        indent: iLangID == 2 ? "Aumentar Recuo" : "Indent", outdent: iLangID == 2 ? "Diminuir Recuo" : "Outdent", horizontalrule: iLangID == 2 ? "Linha Horizontal" : "Insert Horizontal Rule", justifyleft: iLangID == 2 ? "Alinhar à esquerda" : "Left Justify", justifycenter: iLangID == 2 ? "Centralizar" : "Center Justify", justifyright: iLangID == 2 ? "Alinhar à direita" : "Right Justify", increasefontsize: iLangID == 2 ? "Aumentar Tamanho da Fonte" : "Increase Font Size", decreasefontsize: iLangID == 2 ? "Diminuir Tamanho da Fonte" : "Decrease Font Size", forecolor: iLangID == 2 ? "Cor do texto" : "Text Color",
        link: iLangID == 2 ? "Hiperlink" : "Insert Link", unlink: iLangID == 2 ? "Remover Link" : "Remove Link", image: iLangID == 2 ? "Remover Link" : "Insert Image", orderedlist: iLangID == 2 ? "Numeração" : "Insert Ordered List", unorderedlist: iLangID == 2 ? "Marcadores" : "Insert Unordered List", subscript: iLangID == 2 ? "Subscrito" : "Subscript", superscript: iLangID == 2 ? "Sobrescrito" : "Superscript", html: iLangID == 2 ? "Show / Hide Exibir fonte HTML" : "Show/Hide HTML Source View"
        
        }
    };
    var priv = {
        toolbarButtons: {
            strikethrough: "strikeThrough", orderedlist: "orderedList", unorderedlist: "unorderedList",
            horizontalrule: "insertHorizontalRule",
            justifyleft: "justifyLeft", justifycenter: "justifyCenter", justifyright: "justifyRight",
            increasefontsize: "increaseFontSize", decreasefontsize: "decreaseFontSize",
            html: function(btn) {
                this.toggleHTMLView();
            }
        },
        initEditor: function(options) {
            var edit = this.editor = this.iframe[0].contentWindow.document;
            edit.designMode = 'on';
            edit.open();
            edit.write(this.textarea.val());
            edit.close();
            if (options.css) {
                var e = edit.createElement('link'); e.rel = 'stylesheet'; e.type = 'text/css'; e.href = options.css; edit.getElementsByTagName('head')[0].appendChild(e);
            }
        },
        initToolBar: function(options) {
            var that = this;

            var menuItem = function(className, altText, action) {
                return $("<li/>").append($("<a href='javascript:void(0);'/>").addClass(className).attr("title", altText).click(function() { action.call(that, $(this)); }));
            };

            function addButtons(arr) {
                var ul = $("<ul/>").appendTo(that.toolbar);
                for (var i = 0; i < arr.length; i++) {
                    var e = arr[i];
                    if ((typeof (e)).toLowerCase() === "string") {
                        if (e === "|") {
                            ul.append($('<li class="separator"/>'));
                        } else {
                            var f = (function(e) {
                                // If button name exists in priv.toolbarButtons then call the "method" defined there, otherwise call the method with the same name
                                var m = priv.toolbarButtons[e] || e;
                                if ((typeof (m)).toLowerCase() === "function") {
                                    return function(btn) { m.call(this, btn); };
                                } else {
                                    return function() { this[m](); this.editor.body.focus(); };
                                }
                            })(e.toLowerCase());
                            var t = options.toolbarText[e.toLowerCase()];
                            ul.append(menuItem(e.toLowerCase(), t || e, f));
                        }
                    } else {
                        ul.append(menuItem(e.css, e.text, e.action));
                    }
                }
            };
            if (options.toolbar.length !== 0 && priv.isArray(options.toolbar[0])) {
                for (var i = 0; i < options.toolbar.length; i++) {
                    addButtons(options.toolbar[i]);
                }
            } else {
                addButtons(options.toolbar);
            }
        },
        attachEditorEvents: function() {
            var t = this;

            var fnHA = function() {
                t.updateHtmlArea();
            };

            this.textarea.click(fnHA).
                keyup(fnHA).
                keydown(fnHA).
                mousedown(fnHA).
                blur(fnHA);



            var fnTA = function() {
                t.updateTextArea();
            };

            $(this.editor.body).click(fnTA).
                keyup(fnTA).
                keydown(fnTA).
                mousedown(fnTA).
                blur(fnTA);

            $('form').submit(function() { t.toggleHTMLView(); t.toggleHTMLView(); });
            //$(this.textarea[0].form).submit(function() { //this.textarea.closest("form").submit(function() {


            // Fix for ASP.NET Postback Model
            if (window.__doPostBack) {
                var old__doPostBack = __doPostBack;
                window.__doPostBack = function() {
                    if (t) {
                        if (t.toggleHTMLView) {
                            t.toggleHTMLView();
                            t.toggleHTMLView();
                        }
                    }
                    return old__doPostBack.apply(window, arguments);
                };
            }

        },
        isArray: function(v) {
            return v && typeof v === 'object' && typeof v.length === 'number' && typeof v.splice === 'function' && !(v.propertyIsEnumerable('length'));
        }
    };
})(jQuery);


/*=========================================================================
* Autocomplete - jQuery plugin added by Pradeep Kushwaha- 23-Sep-2011
*==========================================================================
*/

; (function ($) {

    $.fn.extend({
        autocomplete: function (urlOrData, options) {
            var isUrl = typeof urlOrData == "string";
            options = $.extend({}, $.Autocompleter.defaults, {
                url: isUrl ? urlOrData : null,
                data: isUrl ? null : urlOrData,
                delay: isUrl ? $.Autocompleter.defaults.delay : 10,
                max: options && !options.scroll ? 10 : 150
            }, options);

            // if highlight is set to false, replace it with a do-nothing function
            options.highlight = options.highlight || function (value) { return value; };

            // if the formatMatch option is not specified, then use formatItem for backwards compatibility
            options.formatMatch = options.formatMatch || options.formatItem;

            return this.each(function () {
                new $.Autocompleter(this, options);
            });
        },
        result: function (handler) {
            return this.bind("result", handler);
        },
        search: function (handler) {
            return this.trigger("search", [handler]);
        },
        flushCache: function () {
            return this.trigger("flushCache");
        },
        setOptions: function (options) {
            return this.trigger("setOptions", [options]);
        },
        unautocomplete: function () {
            return this.trigger("unautocomplete");
        }
    });

    $.Autocompleter = function (input, options) {

        var KEY = {
            UP: 38,
            DOWN: 40,
            DEL: 46,
            TAB: 9,
            RETURN: 13,
            ESC: 27,
            COMMA: 188,
            PAGEUP: 33,
            PAGEDOWN: 34,
            BACKSPACE: 8
        };

        // Create $ object for input element
        var $input = $(input).attr("autocomplete", "off").addClass(options.inputClass);

        var timeout;
        var previousValue = "";
        var cache = $.Autocompleter.Cache(options);
        var hasFocus = 0;
        var lastKeyPressCode;
        var config = {
            mouseDownOnSelect: false
        };
        var select = $.Autocompleter.Select(options, input, selectCurrent, config);

        var blockSubmit;

        // prevent form submit in opera when selecting with return key
        $.browser.opera && $(input.form).bind("submit.autocomplete", function () {
            if (blockSubmit) {
                blockSubmit = false;
                return false;
            }
        });

        // only opera doesn't trigger keydown multiple times while pressed, others don't work with keypress at all
        $input.bind(($.browser.opera ? "keypress" : "keydown") + ".autocomplete", function (event) {
            // track last key pressed
            lastKeyPressCode = event.keyCode;
            switch (event.keyCode) {

                case KEY.UP:
                    event.preventDefault();
                    if (select.visible()) {
                        select.prev();
                    } else {
                        onChange(0, true);
                    }
                    break;

                case KEY.DOWN:
                    event.preventDefault();
                    if (select.visible()) {
                        select.next();
                    } else {
                        onChange(0, true);
                    }
                    break;

                case KEY.PAGEUP:
                    event.preventDefault();
                    if (select.visible()) {
                        select.pageUp();
                    } else {
                        onChange(0, true);
                    }
                    break;

                case KEY.PAGEDOWN:
                    event.preventDefault();
                    if (select.visible()) {
                        select.pageDown();
                    } else {
                        onChange(0, true);
                    }
                    break;

                // matches also semicolon  
                case options.multiple && $.trim(options.multipleSeparator) == "," && KEY.COMMA:
                case KEY.TAB:
                case KEY.RETURN:
                    if (selectCurrent()) {
                        // stop default to prevent a form submit, Opera needs special handling
                        event.preventDefault();
                        blockSubmit = true;
                        return false;
                    }
                    break;

                case KEY.ESC:
                    select.hide();
                    break;

                default:
                    clearTimeout(timeout);
                    timeout = setTimeout(onChange, options.delay);
                    break;
            }
        }).focus(function () {
            // track whether the field has focus, we shouldn't process any
            // results if the field no longer has focus
            hasFocus++;
        }).blur(function () {
            hasFocus = 0;
            if (!config.mouseDownOnSelect) {
                hideResults();
            }
        }).click(function () {
            // show select when clicking in a focused field
            if (hasFocus++ > 1 && !select.visible()) {
                onChange(0, true);
            }
        }).bind("search", function () {
            // TODO why not just specifying both arguments?
            var fn = (arguments.length > 1) ? arguments[1] : null;
            function findValueCallback(q, data) {
                var result;
                if (data && data.length) {
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].result.toLowerCase() == q.toLowerCase()) {
                            result = data[i];
                            break;
                        }
                    }
                }
                if (typeof fn == "function") fn(result);
                else $input.trigger("result", result && [result.data, result.value]);
            }
            $.each(trimWords($input.val()), function (i, value) {
                request(value, findValueCallback, findValueCallback);
            });
        }).bind("flushCache", function () {
            cache.flush();
        }).bind("setOptions", function () {
            $.extend(options, arguments[1]);
            // if we've updated the data, repopulate
            if ("data" in arguments[1])
                cache.populate();
        }).bind("unautocomplete", function () {
            select.unbind();
            $input.unbind();
            $(input.form).unbind(".autocomplete");
        });


        function selectCurrent() {
            var selected = select.selected();
            if (!selected)
                return false;

            var v = selected.result;
            previousValue = v;

            if (options.multiple) {
                var words = trimWords($input.val());
                if (words.length > 1) {
                    v = words.slice(0, words.length - 1).join(options.multipleSeparator) + options.multipleSeparator + v;
                }
                v += options.multipleSeparator;
            }

            $input.val(v);
            hideResultsNow();
            $input.trigger("result", [selected.data, selected.value]);
            return true;
        }

        function onChange(crap, skipPrevCheck) {
            if (lastKeyPressCode == KEY.DEL) {
                select.hide();
                return;
            }

            var currentValue = $input.val();

            if (!skipPrevCheck && currentValue == previousValue)
                return;

            previousValue = currentValue;

            currentValue = lastWord(currentValue);
            if (currentValue.length >= options.minChars) {
                $input.addClass(options.loadingClass);
                if (!options.matchCase)
                    currentValue = currentValue.toLowerCase();
                request(currentValue, receiveData, hideResultsNow);
            } else {
                stopLoading();
                select.hide();
            }
        };

        function trimWords(value) {
            if (!value) {
                return [""];
            }
            var words = value.split(options.multipleSeparator);
            var result = [];
            $.each(words, function (i, value) {
                if ($.trim(value))
                    result[i] = $.trim(value);
            });
            return result;
        }

        function lastWord(value) {
            if (!options.multiple)
                return value;
            var words = trimWords(value);
            return words[words.length - 1];
        }

        // fills in the input box w/the first match (assumed to be the best match)
        // q: the term entered
        // sValue: the first matching result
        function autoFill(q, sValue) {
            // autofill in the complete box w/the first match as long as the user hasn't entered in more data
            // if the last user key pressed was backspace, don't autofill
            if (options.autoFill && (lastWord($input.val()).toLowerCase() == q.toLowerCase()) && lastKeyPressCode != KEY.BACKSPACE) {
                // fill in the value (keep the case the user has typed)
                $input.val($input.val() + sValue.substring(lastWord(previousValue).length));
                // select the portion of the value not typed by the user (so the next character will erase)
                $.Autocompleter.Selection(input, previousValue.length, previousValue.length + sValue.length);
            }
        };

        function hideResults() {
            clearTimeout(timeout);
            timeout = setTimeout(hideResultsNow, 200);
        };

        function hideResultsNow() {
            var wasVisible = select.visible();
            select.hide();
            clearTimeout(timeout);
            stopLoading();
            if (options.mustMatch) {
                // call search and run callback
                $input.search(
				function (result) {
				    // if no value found, clear the input box
				    if (!result) {
				        if (options.multiple) {
				            var words = trimWords($input.val()).slice(0, -1);
				            $input.val(words.join(options.multipleSeparator) + (words.length ? options.multipleSeparator : ""));
				        }
				        else
				            $input.val("");
				    }
				}
			);
            }
            if (wasVisible)
            // position cursor at end of input field
                $.Autocompleter.Selection(input, input.value.length, input.value.length);
        };

        function receiveData(q, data) {
            if (data && data.length && hasFocus) {
                stopLoading();
                select.display(data, q);
                autoFill(q, data[0].value);
                select.show();
            } else {
                hideResultsNow();
            }
        };

        function request(term, success, failure) {
            if (!options.matchCase)
                term = term.toLowerCase();
            var data = cache.load(term);
            // recieve the cached data
            if (data && data.length) {
                success(term, data);
                // if an AJAX url has been supplied, try loading the data now
            } else if ((typeof options.url == "string") && (options.url.length > 0)) {

                var extraParams = {
                    timestamp: +new Date()
                };
                $.each(options.extraParams, function (key, param) {
                    extraParams[key] = typeof param == "function" ? param() : param;
                });

                $.ajax({
                    // try to leverage ajaxQueue plugin to abort previous requests
                    mode: "abort",
                    // limit abortion to this input
                    port: "autocomplete" + input.name,
                    dataType: options.dataType,
                    url: options.url,
                    data: $.extend({
                        q: lastWord(term),
                        limit: options.max
                    }, extraParams),
                    success: function (data) {
                        var parsed = options.parse && options.parse(data) || parse(data);
                        cache.add(term, parsed);
                        success(term, parsed);
                    }
                });
            } else {
                // if we have a failure, we need to empty the list -- this prevents the the [TAB] key from selecting the last successful match
                select.emptyList();
                failure(term);
            }
        };

        function parse(data) {
            var parsed = [];
            var rows = data.split("\n");
            for (var i = 0; i < rows.length; i++) {
                var row = $.trim(rows[i]);
                if (row) {
                    row = row.split("|");
                    parsed[parsed.length] = {
                        data: row,
                        value: row[0],
                        result: options.formatResult && options.formatResult(row, row[0]) || row[0]
                    };
                }
            }
            return parsed;
        };

        function stopLoading() {
            $input.removeClass(options.loadingClass);
        };

    };

    $.Autocompleter.defaults = {
        inputClass: "ac_input",
        resultsClass: "ac_results",
        loadingClass: "ac_loading",
        minChars: 1,
        delay: 400,
        matchCase: false,
        matchSubset: true,
        matchContains: false,
        cacheLength: 10,
        max: 100,
        mustMatch: false,
        extraParams: {},
        selectFirst: true,
        formatItem: function (row) { return row[0]; },
        formatMatch: null,
        autoFill: false,
        width: 0,
        multiple: false,
        multipleSeparator: ", ",
        highlight: function (value, term) {
            return value.replace(new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + term.replace(/([\^\$\(\)\[\]\{\}\*\.\+\?\|\\])/gi, "\\$1") + ")(?![^<>]*>)(?![^&;]+;)", "gi"), "<strong>$1</strong>");
        },
        scroll: true,
        scrollHeight: 180
    };

    $.Autocompleter.Cache = function (options) {

        var data = {};
        var length = 0;

        function matchSubset(s, sub) {
            if (!options.matchCase)
                s = s.toLowerCase();
            var i = s.indexOf(sub);
            if (options.matchContains == "word") {
                i = s.toLowerCase().search("\\b" + sub.toLowerCase());
            }
            if (i == -1) return false;
            return i == 0 || options.matchContains;
        };

        function add(q, value) {
            if (length > options.cacheLength) {
                flush();
            }
            if (!data[q]) {
                length++;
            }
            data[q] = value;
        }

        function populate() {
            if (!options.data) return false;
            // track the matches
            var stMatchSets = {},
			nullData = 0;

            // no url was specified, we need to adjust the cache length to make sure it fits the local data store
            if (!options.url) options.cacheLength = 1;

            // track all options for minChars = 0
            stMatchSets[""] = [];

            // loop through the array and create a lookup structure
            for (var i = 0, ol = options.data.length; i < ol; i++) {
                var rawValue = options.data[i];
                // if rawValue is a string, make an array otherwise just reference the array
                rawValue = (typeof rawValue == "string") ? [rawValue] : rawValue;

                var value = options.formatMatch(rawValue, i + 1, options.data.length);
                if (value === false)
                    continue;

                var firstChar = value.charAt(0).toLowerCase();
                // if no lookup array for this character exists, look it up now
                if (!stMatchSets[firstChar])
                    stMatchSets[firstChar] = [];

                // if the match is a string
                var row = {
                    value: value,
                    data: rawValue,
                    result: options.formatResult && options.formatResult(rawValue) || value
                };

                // push the current match into the set list
                stMatchSets[firstChar].push(row);

                // keep track of minChars zero items
                if (nullData++ < options.max) {
                    stMatchSets[""].push(row);
                }
            };

            // add the data items to the cache
            $.each(stMatchSets, function (i, value) {
                // increase the cache size
                options.cacheLength++;
                // add to the cache
                add(i, value);
            });
        }

        // populate any existing data
        setTimeout(populate, 25);

        function flush() {
            data = {};
            length = 0;
        }

        return {
            flush: flush,
            add: add,
            populate: populate,
            load: function (q) {
                if (!options.cacheLength || !length)
                    return null;
                /* 
                * if dealing w/local data and matchContains than we must make sure
                * to loop through all the data collections looking for matches
                */
                if (!options.url && options.matchContains) {
                    // track all matches
                    var csub = [];
                    // loop through all the data grids for matches
                    for (var k in data) {
                        // don't search through the stMatchSets[""] (minChars: 0) cache
                        // this prevents duplicates
                        if (k.length > 0) {
                            var c = data[k];
                            $.each(c, function (i, x) {
                                // if we've got a match, add it to the array
                                if (matchSubset(x.value, q)) {
                                    csub.push(x);
                                }
                            });
                        }
                    }
                    return csub;
                } else
                // if the exact item exists, use it
                    if (data[q]) {
                        return data[q];
                    } else
                        if (options.matchSubset) {
                            for (var i = q.length - 1; i >= options.minChars; i--) {
                                var c = data[q.substr(0, i)];
                                if (c) {
                                    var csub = [];
                                    $.each(c, function (i, x) {
                                        if (matchSubset(x.value, q)) {
                                            csub[csub.length] = x;
                                        }
                                    });
                                    return csub;
                                }
                            }
                        }
                return null;
            }
        };
    };

    $.Autocompleter.Select = function (options, input, select, config) {
        var CLASSES = {
            ACTIVE: "ac_over"
        };

        var listItems,
		active = -1,
		data,
		term = "",
		needsInit = true,
		element,
		list;

        // Create results
        function init() {
            if (!needsInit)
                return;
            element = $("<div/>")
		.hide()
		.addClass(options.resultsClass)
		.css("position", "absolute")
		.appendTo(document.body);

            list = $("<ul/>").appendTo(element).mouseover(function (event) {
                if (target(event).nodeName && target(event).nodeName.toUpperCase() == 'LI') {
                    active = $("li", list).removeClass(CLASSES.ACTIVE).index(target(event));
                    $(target(event)).addClass(CLASSES.ACTIVE);
                }
            }).click(function (event) {
                $(target(event)).addClass(CLASSES.ACTIVE);
                select();
                // TODO provide option to avoid setting focus again after selection? useful for cleanup-on-focus
                input.focus();
                return false;
            }).mousedown(function () {
                config.mouseDownOnSelect = true;
            }).mouseup(function () {
                config.mouseDownOnSelect = false;
            });

            if (options.width > 0)
                element.css("width", options.width);

            needsInit = false;
        }

        function target(event) {
            var element = event.target;
            while (element && element.tagName != "LI")
                element = element.parentNode;
            // more fun with IE, sometimes event.target is empty, just ignore it then
            if (!element)
                return [];
            return element;
        }

        function moveSelect(step) {
            listItems.slice(active, active + 1).removeClass(CLASSES.ACTIVE);
            movePosition(step);
            var activeItem = listItems.slice(active, active + 1).addClass(CLASSES.ACTIVE);
            if (options.scroll) {
                var offset = 0;
                listItems.slice(0, active).each(function () {
                    offset += this.offsetHeight;
                });
                if ((offset + activeItem[0].offsetHeight - list.scrollTop()) > list[0].clientHeight) {
                    list.scrollTop(offset + activeItem[0].offsetHeight - list.innerHeight());
                } else if (offset < list.scrollTop()) {
                    list.scrollTop(offset);
                }
            }
        };

        function movePosition(step) {
            active += step;
            if (active < 0) {
                active = listItems.size() - 1;
            } else if (active >= listItems.size()) {
                active = 0;
            }
        }

        function limitNumberOfItems(available) {
            return options.max && options.max < available
			? options.max
			: available;
        }

        function fillList() {
            list.empty();
            var max = limitNumberOfItems(data.length);
            for (var i = 0; i < max; i++) {
                if (!data[i])
                    continue;
                var formatted = options.formatItem(data[i].data, i + 1, max, data[i].value, term);
                if (formatted === false)
                    continue;
                var li = $("<li/>").html(options.highlight(formatted, term)).addClass(i % 2 == 0 ? "ac_even" : "ac_odd").appendTo(list)[0];
                $.data(li, "ac_data", data[i]);
            }
            listItems = list.find("li");
            if (options.selectFirst) {
                listItems.slice(0, 1).addClass(CLASSES.ACTIVE);
                active = 0;
            }
            // apply bgiframe if available
            if ($.fn.bgiframe)
                list.bgiframe();
        }

        return {
            display: function (d, q) {
                init();
                data = d;
                term = q;
                fillList();
            },
            next: function () {
                moveSelect(1);
            },
            prev: function () {
                moveSelect(-1);
            },
            pageUp: function () {
                if (active != 0 && active - 8 < 0) {
                    moveSelect(-active);
                } else {
                    moveSelect(-8);
                }
            },
            pageDown: function () {
                if (active != listItems.size() - 1 && active + 8 > listItems.size()) {
                    moveSelect(listItems.size() - 1 - active);
                } else {
                    moveSelect(8);
                }
            },
            hide: function () {
                element && element.hide();
                listItems && listItems.removeClass(CLASSES.ACTIVE);
                active = -1;
            },
            visible: function () {
                return element && element.is(":visible");
            },
            current: function () {
                return this.visible() && (listItems.filter("." + CLASSES.ACTIVE)[0] || options.selectFirst && listItems[0]);
            },
            show: function () {
                var offset = $(input).offset();
                element.css({
                    width: typeof options.width == "string" || options.width > 0 ? options.width : $(input).width(),
                    top: offset.top + input.offsetHeight,
                    left: offset.left
                }).show();
                if (options.scroll) {
                    list.scrollTop(0);
                    list.css({
                        maxHeight: options.scrollHeight,
                        overflow: 'auto'
                    });

                    if ($.browser.msie && typeof document.body.style.maxHeight === "undefined") {
                        var listHeight = 0;
                        listItems.each(function () {
                            listHeight += this.offsetHeight;
                        });
                        var scrollbarsVisible = listHeight > options.scrollHeight;
                        list.css('height', scrollbarsVisible ? options.scrollHeight : listHeight);
                        if (!scrollbarsVisible) {
                            // IE doesn't recalculate width when scrollbar disappears
                            listItems.width(list.width() - parseInt(listItems.css("padding-left")) - parseInt(listItems.css("padding-right")));
                        }
                    }

                }
            },
            selected: function () {
                var selected = listItems && listItems.filter("." + CLASSES.ACTIVE).removeClass(CLASSES.ACTIVE);
                return selected && selected.length && $.data(selected[0], "ac_data");
            },
            emptyList: function () {
                list && list.empty();
            },
            unbind: function () {
                element && element.remove();
            }
        };
    };

    $.Autocompleter.Selection = function (field, start, end) {
        if (field.createTextRange) {
            var selRange = field.createTextRange();
            selRange.collapse(true);
            selRange.moveStart("character", start);
            selRange.moveEnd("character", end);
            selRange.select();
        } else if (field.setSelectionRange) {
            field.setSelectionRange(start, end);
        } else {
            if (field.selectionStart) {
                field.selectionStart = start;
                field.selectionEnd = end;
            }
        }
        field.focus();
    };

})(jQuery);


/*=========================================================================
*Added Autocomplete till here - jQuery plugin added by Pradeep- 23-Sep-2011
*==========================================================================
*/