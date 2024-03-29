/*
Copyright (c) 2009 Ronnie Garcia, Travis Nickels

This file is part of Uploadify v1.6.2

Permission is hereby granted, free of charge, to any person obtaining a copy
of Uploadify and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

UPLOADIFY IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

var flashVer = -1;
if (navigator.plugins != null && navigator.plugins.length > 0) {
    if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) {
        var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : "";
        var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description;
        var descArray = flashDescription.split(" ");
        var tempArrayMajor = descArray[2].split(".");
        var versionMajor = tempArrayMajor[0];
        var versionMinor = tempArrayMajor[1];
        var versionRevision = descArray[3];
        if (versionRevision == "") {
            versionRevision = descArray[4]
        }
        if (versionRevision[0] == "d") {
            versionRevision = versionRevision.substring(1)
        } else {
            if (versionRevision[0] == "r") {
                ersionRevision = versionRevision.substring(1);
                if (versionRevision.indexOf("d") > 0) {
                    versionRevision = versionRevision.substring(0, versionRevision.indexOf("d"))
                }
            }
        }
        var flashVer = versionMajor + "." + versionMinor + "." + versionRevision
    }
} else {
    if ($.browser.msie) {
        var version;
        var axo;
        var e;
        try {
            axo = new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");
            version = axo.GetVariable("$version")
        } catch (e) { }
        flashVer = version.replace("WIN ", "").replace(",", ".")
    }
}
flashVer = flashVer.split(".")[0];
if (jQuery) {
    (function(a) {
        a.extend(a.fn, {
            fileUpload: function(b) {
                if (flashVer >= 9) {
                    a(this).each(function() {
                        settings = a.extend({
                            uploader: "uploader.swf",
                            script: "uploader.php",
                            folder: "",
                            height: 30,
                            width: 110,
                            cancelImg: "cancel.png",
                            wmode: "opaque",
                            scriptAccess: "sameDomain",
                            fileDataName: "Filedata",
                            displayData: "percentage",
                            onInit: function() { },
                            onSelect: function() { },
                            onCheck: function() { },
                            onCancel: function() { },
                            onError: function() { },
                            onProgress: function() { },
                            onComplete: function() { }
                        },
                        b);
                        var d = location.pathname;
                        d = d.split("/");
                        d.pop();
                        d = d.join("/") + "/";
                        var f = "&pagepath=" + d;
                        if (settings.buttonImg) {
                            f += "&buttonImg=" + escape(settings.buttonImg)
                        }
                        if (settings.buttonText) {
                            f += "&buttonText=" + escape(settings.buttonText)
                        }
                        if (settings.rollover) {
                            f += "&rollover=true"
                        }
                        f += "&script=" + settings.script;
                        f += "&folder=" + escape(settings.folder);
                        if (settings.scriptData) {
                            var g = "";
                            for (var c in settings.scriptData) {
                                g += "&" + c + "=" + settings.scriptData[c]
                            }
                            f += "&scriptData=" + escape(g)
                        }
                        f += "&btnWidth=" + settings.width;
                        f += "&btnHeight=" + settings.height;
                        f += "&wmode=" + settings.wmode;
                        if (settings.hideButton) {
                            f += "&hideButton=true"
                        }
                        if (settings.fileDesc) {
                            f += "&fileDesc=" + settings.fileDesc + "&fileExt=" + settings.fileExt
                        }
                        if (settings.multi) {
                            f += "&multi=true"
                        }
                        if (settings.auto) {
                            f += "&auto=true"
                        }
                        if (settings.sizeLimit) {
                            f += "&sizeLimit=" + settings.sizeLimit
                        }
                        if (settings.simUploadLimit) {
                            f += "&simUploadLimit=" + settings.simUploadLimit
                        }
                        if (settings.checkScript) {
                            f += "&checkScript=" + settings.checkScript
                        }
                        if (settings.fileDataName) {
                            f += "&fileDataName=" + settings.fileDataName
                        }
                        if (a.browser.msie) {
                            flashElement = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="' + settings.width + '" height="' + settings.height + '" id="' + a(this).attr("id") + 'Uploader" class="fileUploaderBtn"><param name="movie" value="' + settings.uploader + "?fileUploadID=" + a(this).attr("id") + f + '" /><param name="quality" value="high" /><param name="wmode" value="' + settings.wmode + '" /><param name="allowScriptAccess" value="' + settings.scriptAccess + '"><param name="swfversion" value="9.0.0.0" /></object>'
                        } else {
                            flashElement = '<embed src="' + settings.uploader + "?fileUploadID=" + a(this).attr("id") + f + '" quality="high" width="' + settings.width + '" height="' + settings.height + '" id="' + a(this).attr("id") + 'Uploader" class="fileUploaderBtn" name="' + a(this).attr("id") + 'Uploader" allowScriptAccess="' + settings.scriptAccess + '" wmode="' + settings.wmode + '" type="application/x-shockwave-flash" />'
                        }
                        if (settings.onInit() !== false) {
                            a(this).css("display", "none");
                            if (a.browser.msie) {
                                a(this).after('<div id="' + a(this).attr("id") + 'Uploader"></div>');
                                document.getElementById(a(this).attr("id") + "Uploader").outerHTML = flashElement
                            } else {
                                a(this).after(flashElement)
                            }
                            a("#" + a(this).attr("id") + "Uploader").after('<div id="' + a(this).attr("id") + 'Queue" class="fileUploadQueue"></div>')
                        }
                        a(this).bind("rfuSelect", {
                            action: settings.onSelect
                        },
                        function(j, h, i) {
                            if (j.data.action(j, h, i) !== false) {
                                var k = Math.round(i.size / 1024 * 100) * 0.01;
                                var l = "KB";
                                if (k > 1000) {
                                    k = Math.round(k * 0.001 * 100) * 0.01;
                                    l = "MB"
                                }
                                var m = k.toString().split(".");
                                if (m.length > 1) {
                                    k = m[0] + "." + m[1].substr(0, 2)
                                } else {
                                    k = m[0]
                                }
                                if (i.name.length > 20) {
                                    fileName = i.name.substr(0, 20) + "..."
                                } else {
                                    fileName = i.name
                                }
                                a("#" + a(this).attr("id") + "Queue").append('<div id="' + a(this).attr("id") + h + '" class="fileUploadQueueItem"><div class="cancel"><a href="javascript:$(\'#' + a(this).attr("id") + "').fileUploadCancel('" + h + '\')"><img src="' + settings.cancelImg + '" border="0" /></a></div><span class="fileName">' + fileName + " (" + k + l + ')</span><span class="percentage">&nbsp;</span><div class="fileUploadProgress" style="width: 100%;"><div id="' + a(this).attr("id") + h + 'ProgressBar" class="fileUploadProgressBar" style="width: 1px; height: 3px;"></div></div></div>')
                            }
                        });
                        if (typeof (settings.onSelectOnce) == "function") {
                            a(this).bind("rfuSelectOnce", settings.onSelectOnce)
                        }
                        a(this).bind("rfuCheckExist", {
                            action: settings.onCheck
                        },
                        function(m, l, j, k, o) {
                            var i = new Object();
                            i.folder = d + k;
                            for (var h in j) {
                                i[h] = j[h];
                                if (o) {
                                    var n = h
                                }
                            }
                            a.post(l, i,
                            function(r) {
                                for (var p in r) {
                                    if (m.data.action(m, l, j, k, o) !== false) {
                                        var q = confirm("Do you want to replace the file '" + r[p] + "'?");
                                        if (!q) {
                                            document.getElementById(a(m.target).attr("id") + "Uploader").cancelFileUpload(p)
                                        }
                                    }
                                }
                                if (o) {
                                    document.getElementById(a(m.target).attr("id") + "Uploader").startFileUpload(n, true)
                                } else {
                                    document.getElementById(a(m.target).attr("id") + "Uploader").startFileUpload(null, true)
                                }
                            },
                            "json")
                        });
                        a(this).bind("rfuCancel", {
                            action: settings.onCancel
                        },
                        function(j, h, i, k) {
                            if (j.data.action(j, h, i, k) !== false) {
                                a("#" + a(this).attr("id") + h).fadeOut(250,
                                function() {
                                    a("#" + a(this).attr("id") + h).remove()
                                })
                            }
                        });
                        a(this).bind("rfuClearQueue", {
                            action: settings.onClearQueue
                        },
                        function() {
                            if (event.data.action() !== false) {
                                a("#" + a(this).attr("id") + "Queue").contents().fadeOut(250,
                                function() {
                                    a("#" + a(this).attr("id") + "Queue").empty()
                                })
                            }
                        });
                        a(this).bind("rfuError", {
                            action: settings.onError
                        },
                        function(k, h, j, i) {
                            if (k.data.action(k, h, j, i) !== false) {
                                a("#" + a(this).attr("id") + h + " .fileName").text(i.type + " Error - " + j.name);
                                a("#" + a(this).attr("id") + h).css({
                                    border: "1px solid #FBCBBC",
                                    "background-color": "#FDE5DD"
                                })
                            }
                        });
                        a(this).bind("rfuProgress", {
                            action: settings.onProgress,
                            toDisplay: settings.displayData
                        },
                        function(j, h, i, k) {
                            if (j.data.action(j, h, i, k) !== false) {
                                a("#" + a(this).attr("id") + h + "ProgressBar").css("width", k.percentage + "%");
                                if (j.data.toDisplay == "percentage") {
                                    displayData = " - " + k.percentage + "%"
                                }
                                if (j.data.toDisplay == "speed") {
                                    displayData = " - " + k.speed + "KB/s"
                                }
                                if (j.data.toDisplay == null) {
                                    displayData = " "
                                }
                                a("#" + a(this).attr("id") + h + " .percentage").text(displayData)
                            }
                        });
                        a(this).bind("rfuComplete", {
                            action: settings.onComplete
                        },
                        function(k, h, j, i, l) {
                            if (k.data.action(k, h, j, unescape(i), l) !== false) {
                                a("#" + a(this).attr("id") + h).fadeOut(250,
                                function() {
                                    a("#" + a(this).attr("id") + h).remove()
                                });
                                a("#" + a(this).attr("id") + h + " .percentage").text(" - Completed")
                            }
                        });
                        if (typeof (settings.onAllComplete) == "function") {
                            a(this).bind("rfuAllComplete", settings.onAllComplete)
                        }
                    })
                }
            },
            fileUploadSettings: function(b, c) {
                a(this).each(function() {
                    document.getElementById(a(this).attr("id") + "Uploader").updateSettings(b, c)
                })
            },
            fileUploadStart: function(b) {
                a(this).each(function() {
                    document.getElementById(a(this).attr("id") + "Uploader").startFileUpload(b, false)
                })
            },
            fileUploadCancel: function(b) {
                a(this).each(function() {
                    document.getElementById(a(this).attr("id") + "Uploader").cancelFileUpload(b)
                })
            },
            fileUploadClearQueue: function() {
                a(this).each(function() {
                    document.getElementById(a(this).attr("id") + "Uploader").clearFileUploadQueue()
                })
            }
        })
    })(jQuery)
};