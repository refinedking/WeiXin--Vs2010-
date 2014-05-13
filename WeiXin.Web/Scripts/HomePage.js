/// <reference path="Shared/jquery-1.4.1.min.js" />

//翻页效果
(function ($) {
    var status = false;
    $.fn.scrollQ = function (options) {
        var defaults = {
            line: 3,
            scrollNum: 3,
            scrollTime: 2500
        }
        var options = jQuery.extend(defaults, options);
        var _self = this;
        return this.each(function () {
            $("li", this).each(function () {
                $(this).css("display", "none");
            })
            $("li:lt(" + options.line + ")", this).each(function () {
                $(this).css("display", "block");
            });
            function scroll() {
                for (i = 0; i < options.scrollNum; i++) {
                    var start = $("li:first", _self);
                    start.fadeOut(100);
                    start.css("display", "none");
                    start.appendTo(_self);
                    $("li:eq(" + (options.line - 1) + ")", _self).each(function () {
                        $(this).fadeIn(500);
                        $(this).css("display", "block");
                    });
                }
            }
            var timer;
            timer = setInterval(scroll, options.scrollTime);
            _self.bind("mouseover", function () {
                clearInterval(timer);
            });
            _self.bind("mouseout", function () {
                timer = setInterval(scroll, options.scrollTime);
            });

        });
    }
})(jQuery);


$(function () {
    $(".imgGift").click(function () {
        alert("功能完善中。。。");
    });
    //生日列表滚动
    if ($("#divBirthInfo li").length > 3) {
        $("#divBirthInfo ul").scrollQ();
    }
    //动态设置宽度（模拟table）
    $(window).resize(function () {
        var headTdList = $("#tableHead").find("td");
        $("#ulEmploymentList li").each(function (index, entity) {
            var itemTdList = $(this).find("td");
            for (var i = 0; i < $("#tableHead").find("td").length; i++) {
                itemTdList.eq(i).css("width", headTdList.eq(i).width());
            }
        });
    });
    $(window).resize().resize();
    //向上滚动效果
    var mar = new SimpleMarquee('ulNoticeList');   //getPrize 是HTML元素ID，其内容为要滚动的内容 
    mar.startMove();

    //公告详情展示
    $("#ulNoticeList").children("li").click(function () {
        window.location.href = $(this).attr("href");
    });

    var speed = 1600; //速度数值越大速度越慢
    function Marquee() {
        $("#ulEmploymentList").children().first().hide(speed / 4, function () {
            $(this).insertAfter($("#ulEmploymentList").children().last()).slideDown(speed / 4);
        });
    }
    var MyMar = setInterval(Marquee, speed);
    ulEmploymentList.onmouseover = function () { clearInterval(MyMar); }
    ulEmploymentList.onmouseout = function () { MyMar = setInterval(Marquee, speed) }

});