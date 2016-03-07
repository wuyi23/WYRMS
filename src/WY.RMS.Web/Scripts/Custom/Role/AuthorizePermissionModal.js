//权限授权模态框操作js函数







/*******弹出表单*********/
function ShowModal_Authorize(actionUrl, param, title) {
    var $modal = $("#authorizeModal");
    //表单初始化
    $(".modal-title", $modal).html(title);
    $("#modal-content", $modal).attr("action", actionUrl);
    $("#roleId", $modal).val(param.id);
    var treeSetting = {
        view: {
            selectedMulti: false
        },
        check: {
            enable: true
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        edit: {
            enable: false
        },
        callback: {
            // onCheck: onCheck
        }
    };
    $.ajax({
        type: "GET",
        url: actionUrl,
        data: param,
        beforeSend: function () {
            //
        },
        success: function (zNodes) {
            $.fn.zTree.init($("#treePermission", $modal), treeSetting, zNodes);
            $modal.modal('show');
        },
        error: function () {
            //
        },
        complete: function () {
            //
        }
    });
}