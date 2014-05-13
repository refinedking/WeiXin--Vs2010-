/* File Created: 八月 22, 2012 */
/// <reference path="../Shared/jquery-1.4.1.js" />

var IsVaild = 0;
$(function () {
    //构建JSON
    var json = { PerId: 0, PerName: "", PerSex: 0, PerPosition: "", PerPhone1: "", PerPhone2: "", PerQQ: "", PerEmail: "", PerRemark: "", PerContractTime: new Date().format("yyyy-MM-dd"), PerState: 0 };
    //执行操作(添加、编辑、详细)
    $("#add,#edit,#details").click(function () {
        //获取当前点击的的ID
        var clickId = $(this).attr("id");
        //如果为编辑或者详细查询实体数据
        if (clickId != "add") {
            $.ajax({
                url: "/CmpPersonManager/GetCmpPersonInfo",
                data: { id: $(this).parent().siblings().find("input[type=checkbox]").val() },
                type: "POST",
                datatype: "json",
                async: false,
                success: function (data) {
                    json.PerId = data.PerId
                    json.PerName = data.PerName;
                    json.PerSex = data.PerSex;
                    json.PerPosition = data.PerPosition;
                    json.PerPhone1 = data.PerPhone1;
                    json.PerPhone2 = data.PerPhone2;
                    json.PerQQ = data.PerQQ;
                    json.PerEmail = data.PerEmail;
                    json.PerRemark = data.PerRemark;
                    json.PerContractTime = data.PerContractTime;
                    json.PerState = data.PerState;
                }
            });
        }
        //设定默认值
        $("#PerId").val(json.PerId);
        $("#PerName").val(json.PerName);
        $("#PerPosition").val(json.PerPosition);
        $("#PerPhone1").val(json.PerPhone1);
        $("#PerPhone2").val(json.PerPhone2);
        $("#PerQQ").val(json.PerQQ);
        $("#PerEmail").val(json.PerEmail);
        $("#PerRemark").val(json.PerRemark);
        $("#PerContractTime").val(json.PerContractTime);
        //设置默认选择项
        $("input[name='PerSex']").each(function (index, item) {
            $(this).removeAttr("checked");
            if ($(this).val() == json.PerSex) {
                $(this).attr("checked", "checked");
            };
        });
        $("input[name='PerState']").each(function (index, item) {
            $(this).removeAttr("checked");
            if ($(this).val() == json.PerState) {
                $(this).attr("checked", "checked");
            };
        });
        //设定是否允许编辑
        if (clickId == "details") {
            $("#PerName").attr("disabled", "false");
            $("#PerPosition").attr("disabled", "false");
            $("#PerPhone1").attr("disabled", "false");
            $("#PerPhone2").attr("disabled", "false");
            $("#PerQQ").attr("disabled", "false");
            $("#PerEmail").attr("disabled", "false");
            $("#PerRemark").attr("disabled", "false");
            $("#PerContractTime").attr("disabled", "false");
            $(".divFormCtrl").children().css("display", "none");
            $("input[name='PerState']").attr("disabled", "false");
            $("input[name='PerSex']").attr("disabled", "false");
        }
        else {
            $(".tblForm input,textarea").removeAttr("disabled");
            $(".divFormCtrl").children().css("display", "block");
        }
        //设置是否显示
        $("fieldset").hide();
        $("fieldset").fadeIn("slow");
    });
    //关闭
    $("#hidden").click(function () {
        $("fieldset").hide();
    })
    //验证
    validation();
});
function submitForm() {
    var regexPerName = /^[\u4e00-\u9fa5]{2,10}$/;
    var regexPerPosition = /^[\u4e00-\u9fa5]{0,10}$/;
    var regexPerPhone = /^(13|15)[0-9]{9}$/;
    var regexQQ = /^[1-9]*[1-9][0-9]*$/;
    var regex = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
    var regexPerContractTime = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
    validationData("PerName", regexPerName, "请输入2-5汉字！");
    validationData("PerPosition", regexPerPosition, "请输入10个汉字以内的职务！");
    if ($("#PerPhone1").val().length != 0) {
        validationData("PerPhone1", regexPerPhone, "请正确的手机号码！");
    }
    if ($("#PerPhone2").val().length != 0) {
        validationData("PerPhone2", regexPerPhone, "请正确的手机号码！");
    }
    if ($("#PerQQ").val().length != 0) {
        validationData("PerQQ", regexQQ, "请输入正确的QQ号码！");
    }
    if ($("#PerEmail").val().length != 0) {
        validationData("PerEmail", regex, "请输入正确的邮箱地址！");
    }
    validationData("PerContractTime", regexPerContractTime, "请正确的时间！");
    if ($("#fieldestAdd input[type=text]").hasClass("txtBox")) {
        alert("存在数据错误！移动鼠标到红线处查看错误信息！");
        return false;
    }
    //检测数据是否在合法范围
//    if (IsVaild != 0) {
//        alert("存在数据错误！移动鼠标到红线处查看错误信息！");
//        return false;
//    }
 }
//验证数据
function validation() {
    //验证联系人
    $("#PerName").blur(function () {
        var regexPerName = /^[\u4e00-\u9fa5]{2,10}$/;
        validationData("PerName", regexPerName, "请输入2-5汉字！");
    });
    //验证职务
    $("#PerPosition").blur(function () {
        var regexPerPosition = /^[\u4e00-\u9fa5]{0,10}$/;
        validationData("PerPosition", regexPerPosition, "请输入10个汉字以内的职务！");
    });
    //验证手机一
    $("#PerPhone1").blur(function () {
        var regexPerPhone = /^(13|15)[0-9]{9}$/;
        if ($(this).val().length != 0) {
            validationData("PerPhone1", regexPerPhone, "请正确的手机号码！");
        }
    });
    //验证手机二
    $("#PerPhone2").blur(function () {
        var regexPerPhone = /^(13|15)[0-9]{9}$/;
        if ($(this).val().length != 0) {
            validationData("PerPhone2", regexPerPhone, "请正确的手机号码！");
        }
    });
    //验证QQ
    $("#PerQQ").blur(function () {
        var regexQQ = /^[1-9]*[1-9][0-9]*$/;
        if ($(this).val().length != 0) {
            validationData("PerQQ", regexQQ, "请输入正确的QQ号码！");
        }
    });
    //验证邮箱
    $("#PerEmail").blur(function () {
        var regex = /^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
        if ($(this).val().length != 0) {
            validationData("PerEmail", regex, "请输入正确的邮箱地址！");
        }
    });
    //验证时间
    $("#PerContractTime").blur(function () {
        var regexPerContractTime = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
        validationData("PerContractTime", regexPerContractTime, "请正确的时间！");
    });
};
function validationData(property, regex, msg) {
    var jqProperty = "#" + property;
    if (!regex.test($(jqProperty).val())) {
        $(jqProperty).addClass("txtBox");
        $(jqProperty).attr("title", msg);
    } else {
        $(jqProperty).removeAttr("class");
        $(jqProperty).removeAttr("title");
    };
};