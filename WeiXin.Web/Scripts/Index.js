/// <reference path="Shared/jquery-1.4.1.min.js" />

/*

*/
/*标识符，是否需要显示系统列表*/
var needShowSystemList = false;
/*标识符，是否需要显示子级菜单列表*/
var needShowChildMenuItem = false;

/*注册Tab功能事件*/
function InitEvents() {
    //注销单击事件
    $(".divTabCtrl,.CloseTab").unbind("click");
    //注册tab单击事件
    $(".divTabCtrl").click(function () {
        if ($(this).parent().hasClass("cur")) {
            return false;
        } else {
            $(this).addClass("cur").siblings().removeClass("cur");
            $("#divTabItem").children("iframe").css("display", "none");
            $("#divTabItem").children("iframe[tabTag='" + $(this).attr("tabTag") + "']").css("display", "block");
        }
    });
    //注册tab关闭事件
    $(".CloseTab").click(function () {
        if ($(this).parent().hasClass("cur")) {
            if ($(this).parent().nextAll().length > 0) {
                $(this).parent().next().click();
            }
            else if ($(this).parent().prevAll().length > 0) {
                $(this).parent().prev().click();
            }
        }
        $("#divTabItem").children("iframe[tabTag = '" + $(this).parent().attr("tabTag") + "']").remove();
        $(this).parent().remove();
        if ($("#divTab").children().length == 0) {
            $("#ifrmOtherFrame").show();
        }
    });
}
/*初始化菜单项事件，在页面初次加载或更新系统后调用*/
function InitMenuEvents() {
    //设置所有子菜单的宽度与根菜单项的宽度一致
    $("#ulMenu").children().each(function (index, entity) {
        $(entity).find("li").width($(entity).width() - 2);
    })
    /*注册一级菜单项效果*/
    $("#ulMenu").find("li[class]").mouseover(function () {
        /*添加当前选中项状态样式*/
        $(this).addClass("currentLi").children("div").addClass("currentDiv");
        $(this).children("ul").show().children().first();
        needShowChildMenuItem = true;
    }).mouseleave(function () {
        $(this).removeClass("currentLi").children("div").removeClass("currentDiv");
        var $currentObj = $(this).children("ul");
        setTimeout(function () {
            needShowChildMenuItem = false;
            if (!needShowChildMenuItem) {
                $currentObj.hide();
            }
        }, 0);
    });
    /*注册子菜单项效果*/
    $("#ulMenu ul").find("li").mouseover(function () {
        //兼容IE6光棒效果
        $(this).css("background-color", "rgb(0, 94, 172)").css("color", "White");
        $(this).children("ul").show(0, function () {
            $(this).css("position", "absolute");
            var bro = $.browser;
            //专门为ie6单独设置菜单样式
            if (bro.msie && bro.version < 7) {
                $(this).css("left", 127).css("top", ($(this).parent().prevAll().length + 1) * $(this).parent().height());
            }
            else {
                $(this).css("left", 74).css("top", $(this).parent().prevAll().length * $(this).parent().height());
            }
        });
        needShowChildMenuItem = true;
    }).mouseleave(function () {
        //兼容IE6光棒效果
        $(this).css("background-color", "").css("color", "rgb(0, 50,152)");
        var $currentObj = $(this).children("ul");
        setTimeout(function () {
            needShowChildMenuItem = false;
            if (!needShowChildMenuItem) {
                $currentObj.hide();
            }
        }, 0);
    });
    /*注册菜单项单击事件*/
    $("#ulMenu ul li,#ulMenu li[class]").click(function () {
        //注册首页菜单单击事件(substring方法是为了防止ie9不兼容，ie9自动添加个空格）
        if ($(this).text().toString().substring(0, 2) == "首页") {
            $(".divTabCtrl").removeClass("cur");
            $("#divTabItem").children("iframe").hide();
            $("#ifrmOtherFrame").attr("src", $("#hidIndexUrl").val()).show();
            return false;
        }
        var tabTag = "tag" + $(this).attr("menuId"); //获取标签
        //判断目标页面是否已经打开
        if ($(".divTabCtrl[tabTag='" + tabTag + "']").length == 0) {
            var strSrc = $(this).attr("href"); //获取网址
            if (strSrc != null) {
                //添加tab选项卡
                $("#divTab").append("<div class='divTabCtrl' tabTag='" + tabTag + "' ><span>" + $(this).children("div").text() + "</span><div class='CloseTab'></div></div>");
                //设置tab层宽度，解决旧版本IE兼容问题
                $("#divTab").children().last().width($("#divTab").children().last().children().width() + 28);
                //添加tab选项卡内容(iframe)
                $("#divTabItem").append("<iframe tabTag='" + tabTag + "' frameborder='0' scrolling='yes' src='" + strSrc + "' style='display:none;'></iframe>");
                InitEvents(); //重新绑定纵向菜单项单击事件
            }
        }
        $(".divTabCtrl[tabTag='" + tabTag + "']").click(); //选中打开的项
        return false;
    });
}
/*初始化页面事件*/
$(function () {
    //IE升级提示
    var bro = $.browser;
    //专门为ie6单独设置菜单样式
    if (bro.msie && bro.version < 8) {
        alert("系统提示\n亲爱的用户，为了您能更好的体验本系统，请将您的IE升级为IE8.0以上的版本");
    }
    /*切换系统层隐藏效果*/
    if ($("#divSystemList").find("li").length > 0) {
        $("#divChangeSystem").mouseover(function () {
            needShowSystemList = true;
            $("#divSystemList").show();
            if (bro.msie && bro.version == 7) {
                $("#divSystemList").css("left", $(this).position().left - $(window).scrollLeft());
            }
        }).mouseleave(function () {
            needShowSystemList = false;
            setTimeout(function () {
                if (!needShowSystemList) {
                    $("#divSystemList").hide();
                }
            }, 300);
        });
        $("#divSystemList").mouseover(function () {
            needShowSystemList = true;
        }).mouseleave(function () {
            needShowSystemList = false;
            setTimeout(function () {
                if (!needShowSystemList) {
                    $("#divSystemList").hide();
                }
            }, 300);
        });
    }
    else {
        $("#divChangeSystem").hide();
    }
    //初始化菜单事件
    //    InitMenuEvents();
    //系统切换事件
    $("#divSystemList").find("li").click(function () {
        var strSysName = "";
        strSysName += $(this).text() + "<br />" + $(this).attr("enName");
        $("#divSysName").html(strSysName);
        //数据加载提示
        Zuxia.Loading.show("菜单信息加载中,请稍后...");
        $.ajax({
            type: "POST",
            url: "/Home/GetMenu/" + $(this).attr("sysId"),
            dataType: "JSON",
            success: function (data) {
                $("#divMenu").html(data);
                var allMenuWidth = 0;
                $("#divMenu").find("li").each(function (index, entity) {
                    allMenuWidth += $(entity).width();
                });
                $("#divMenu").width(allMenuWidth);
                InitMenuEvents();
                //关闭数据加载提示
                Zuxia.Loading.hide();
                if ($("#divSystemList").find("li").length == 1) {
                    $("#divMenu").css("margin-left", $("#divChangeSystem").width());
                    $("#divChangeSystem,#divSystemList").hide();
                }
            }
        });
        $(this).addClass("curSys").siblings().removeClass("curSys");
    });
    //页面加载时显示系统
    $("#divSystemList").find(".curSys").click();
    //意外关闭浏览器提示
    var isExit = true; //是否需要提示（用于解决IE的BUG）
    $(window).bind("beforeunload", function (event) {
        if ($("#divTab").children().length > 0 && isExit) {
            isExit = false;
            _t = setTimeout(function () {
                isExit = true;
                clearTimeout(_t); //用于解决部分浏览器网页关闭以后计时器还会继续运行的BUG
            }, 10);
            return "【注意】\n您已经打开了【" + $("#divTab").children().length + "】个工作区，离开此页您将会【丢失未保存的工作信息】!";
        }
    });
    //显示个人资料页面
    $("#spanUserName").click(function () {
        $("#divTabItem").children().hide();
        $("#ifrmOtherFrame").show().attr("src", $("#hidUserInfoUrl").val());
    });
    //纠正整体母版框架页面大小布局，让底部框架充满整个浏览器
    $(window).resize(function () {
        $("#divMiddle").height($(window).height() - $("#divTop").height() - $("#divPartingLine").height() - 2);
        $("#divTabItem").height($("#divMiddle").height() - $("#divTab").height() - 2);
        $("#divTabItem").children().height($("#divTabItem").height());
    });
    $(window).resize().resize();
});
function CreateTab(flag, tabText, strSrc) {
    var tabTag = "tag" + flag; //获取标签
    //判断目标页面是否已经打开
    if ($(".divTabCtrl[tabTag='" + tabTag + "']").length == 0) {
        if (strSrc != null) {
            //添加tab选项卡
            $("#divTab").append("<div class='divTabCtrl' tabTag='" + tabTag + "' ><span>" + tabText + "</span><div class='CloseTab'></div></div>");
            //添加tab选项卡内容(iframe)
            $("#divTabItem").append("<iframe tabTag='" + tabTag + "' frameborder='0' scrolling='yes' src='" + strSrc + "' style='display:none;'></iframe>");
            InitEvents(); //重新绑定纵向菜单项单击事件
        }
    }
    else {
        var oldSrc = $("#divTabItem").children("iframe[tabTag='" + tabTag + "']").attr("src");
        if (oldSrc != strSrc) {
            $("#divTabItem").children("iframe[tabTag='" + tabTag + "']").attr("src", strSrc);
        }
    }
    $(".divTabCtrl[tabTag='" + tabTag + "']").click(); //选中打开的项
}
function CloseCurTab() {
    $("#divTab").find(".cur").find(".CloseTab").click();
}