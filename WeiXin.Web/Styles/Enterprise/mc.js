$(function () {
    $('#applyBtn').click(function () {
        var sessionid = $("#sessionid").val();
        var id = $("#id").val();
        var userid = $("#userid").val(); 
        $.ajax({
            type: "POST",
            data: { 'sessionid': sessionid, 'userid': userid, 'id': id },
            url: "/Web/MemberCard", 
            success: function (info) {
                if (info == "ok") {
                    window.location = window.location;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                Zuxia.Alert(errorThrown);
            }
        });
    });

});