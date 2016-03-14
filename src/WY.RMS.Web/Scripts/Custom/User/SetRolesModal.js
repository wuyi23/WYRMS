
var $setRolesModal = $("#setRolesModal");
/*******角色授权弹出表单*********/
function ShowModal_SetRoles(actionUrl, param, title) {
    //表单初始化
    $(".modal-title", $setRolesModal).html(title);
    $("#modal-content", $setRolesModal).attr("action", actionUrl);

    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        success: function (result) {
            $("#modal-content", $setRolesModal).html(result);
            $setRolesModal.modal('show');
            //RegisterForm();//通过Ajax加载返回的页面原有MVC属性验证将失效，需要重新注册验证脚本。
        }
    });
}


//“角色授权”模态框中保存

function SaveModal_SetRoles() {
    var actionUrl = $("#modal-content", $setRolesModal).attr("action");
    var $form = $("#modal-content", $setRolesModal);
    $.ajax({
        type: "POST",
        url: actionUrl,
        data: $form.serialize(),
        success: function (result) {
            if (result.ResultType === 0) {
                toastr.success(result.Message);
                $setRolesModal.modal('hide');
            }
            else {
                toastr.error(result.Message);
            }
        },
        error: function () {
            toastr.error('网络错误，请重新提交！');
        }
    });
}