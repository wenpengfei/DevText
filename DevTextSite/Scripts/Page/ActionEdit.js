/// <reference path="../jquery-1.4.4-vsdoc.js" />
/// <reference path="../jquery-ui-1.8.14.custom.min.js" />

$(document).ready(function () {
	$("#dataGrid").bindTable("/sysadmin/action/getactionlist", function (data) {
		$("#dataGrid > tbody > tr:even").addClass("even");
		$("#dataGrid > tbody > tr:odd").addClass("odd");
	});
});

function CreateAction() {
	$("#modeldialogdiv").dialog({ title: "创建动作", modal: true, width: 460, height: 330, open: $("#modeldialogdiv").load("/sysadmin/action/create") });
}

function EditAction(id) {
	$("#modeldialogdiv").dialog({ title: "编辑动作", modal: true, width: 460, height: 330, open: $("#modeldialogdiv").load("/sysadmin/action/edit?id=" + id, function (data) {
		if (data) {
			$("#hidActionID").val(id);
		}
	})
	});
}

function Cancel() {
	$("#modeldialogdiv").dialog('close');
}

function Submit() {
	if ($("#txtActionName").val() == "" || $("#txtActionValue").val() == "") {
		alert("动作名称和动作值为必填");
		return;
	}
	var action = {
		id: $("#hidActionID").val(),
		channelid: $("#ddlChannel").val(),
		actionname: $("#txtActionName").val(),
		actionvalue: $("#txtActionValue").val(),
		pageurl: $("#txtPageUrl").val()
	};
	$.post("/sysadmin/action/submitcreate", { json: JSON.stringify(action) }, function (data) {
		if (data.result) {
			alert(data.message);
			if ($("#cbCreateContinue").attr("checked")) {
				$("#modeldialogdiv").dialog({ title: "创建动作", modal: true, width: 460, height: 330, open: $("#modeldialogdiv").load("/sysadmin/action/create") });
			} else {
				location.replace("/sysadmin/action/list");
			}
		} else {
			alert(data.message);
		}
	}, "json");
}