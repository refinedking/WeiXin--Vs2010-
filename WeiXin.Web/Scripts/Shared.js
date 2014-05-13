/// <reference path="Shared/jquery-1.4.1.min.js" />

//公共脚本
$(function () {
    /*详细编辑页面公共脚本*/
    /*ymPrompt插件自定义事件*/
    //禁止选择
    $(".disSelect,.divTitleCtrl").bind("selectstart", function () {
        return false;
    }).bind("contextmenu", function () {
        return false;
    }).keydown(function () {
        return key(arguments[0])
    });
    //页面加载提示
    $(".pageLoading").click(function () {
        //数据加载提示
        Zuxia.Loading.show("页面加载中,请稍后...");
    });
    //数据加载提示
    $(".dataLoading").click(function () {
        //数据加载提示
        Zuxia.Loading.show("数据加载中,请稍后...");
    });
    //去掉虚线
    $("a,:radio,:checkbox").attr("hidefocus", "true");
});
//AutoCompelte
function autoComp(id, url, action) {
    if (action == null) {
        $("#" + id + "").autocomplete(url, { minChars: 1, max: 100, autoFill: true, mustMatch: true, matchContains: true, selectFirst: true, matchCase: true, cacheLength: 20, autoComplete: 20 });
    }
    else {
        $("#" + id + "").autocomplete("" + url + "?action=" + action + "", { minChars: 1, max: 100, autoFill: true, mustMatch: true, matchContains: true, selectFirst: true, matchCase: true, cacheLength: 20, autoComplete: 20 });
    }
}


//按键时提示警告

function key(e) {
    var keynum;

    if (window.event) {
        keynum = e.keyCode; // IE
    }

    else if (e.which) {
        keynum = e.which; // Netscape/Firefox/Opera
    }

    if (keynum == 17) {
        return false;
    }
}

//新建一个tab标签，并在新的框架中打开页面
function CreateTab(flag, tabText, strSrc, parentsCount) {
    var document = window.parent;
    for (var i = 1; i < parentsCount; i++) {
        document = document.parent;
    }
    document.CreateTab(flag, tabText, strSrc);
}

function CloseWindow() {
    window.close();
}

function CloseTab(parentsCount) {
    var document = window.parent;
    for (var i = 1; i < parentsCount; i++) {
        document = document.parent;
    }
    document.CloseCurTab();
}
//计算时间差
function dateDiff(interval, date1, date2) {
    var objInterval = { 'D': 1000 * 60 * 60 * 24, 'H': 1000 * 60 * 60, 'M': 1000 * 60, 'S': 1000, 'T': 1 };
    interval = interval.toUpperCase();
    var dt1 = new Date(Date.parse(date1.replace(/-/g, '/')));
    var dt2 = new Date(Date.parse(date2.replace(/-/g, '/')));
    try {
        //alert(dt2.getTime() - dt1.getTime());
        //alert(eval_r('objInterval.'+interval));
        //alert((dt2.getTime() - dt1.getTime()) / eval_r('objInterval.'+interval));
        return Math.round((dt2.getTime() - dt1.getTime()) / eval('objInterval.' + interval));
    }
    catch (e) {
        return e.message;
    }
}