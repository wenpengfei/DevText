/// <reference path="jquery-1.4.4.min.js" />
var request = {
	QueryString: function (name, url) {
		url = url || location.href;
		var r = new RegExp("(\\?|#|&)" + name + "=([^&#]*)(&|#|$)", "i");
		var m = url.match(r);
		return (!m ? "" : m[2]);
	},
	Form: function (name) {
		return $(name).val();
	}
};

function CheckLogin() {
	var username = $.trim($("#txtLoginName").val());
	var password = $.trim($("#txtPassword").val());
	var loginurl = request.QueryString("returnurl");
	if (username == null || username == "" || password == null || password == "") {
		alert("请输入用户名和密码");
		return;
	} else {
		$.post("/site/checklogin", { username: username, password: password }, function (data) {
			if (data.result == true) {
				$("#messageinfo").text(data.message);
				loginurl = loginurl || "/home/event";
				location.replace(loginurl);
			} else {
				alert(data.message);
			}
		}, "json");
	}
}

function CheckLoginHome() {
	var username = $.trim($("#txtLoginName2").val());
	var password = $.trim($("#txtPassword2").val());
	var loginurl = request.QueryString("returnurl");
	if (username == null || username == "" || password == null || password == "") {
		$("#messageinfo").text("请输入用户名和密码");
		return;
	} else {
		$.post("/site/checklogin", { username: username, password: password }, function (data) {
			if (data.result == true) {
				$("#messageinfo").text(data.message);
				loginurl = loginurl || "/home/event";
				location.replace(loginurl);
			} else {
				$("#messageinfo").text(data.message);
			}
		}, "json");
	}
}

function checkemail(strMail) {
	if (strMail.length == 0) return false;
	var objReg = new RegExp("[A-Za-z0-9-_]+@[A-Za-z0-9-_]+[\.][A-Za-z0-9-_]")
	var IsRightFmt = objReg.test(strMail)
	var objRegErrChar = new RegExp("[^a-z0-9-._@]", "ig")
	var IsRightChar = (strMail.search(objRegErrChar) == -1)
	var IsRightLength = strMail.length <= 60
	var IsRightPos = (strMail.indexOf("@", 0) != 0 && strMail.indexOf(".", 0) != 0 && strMail.lastIndexOf("@") + 1 != strMail.length && strMail.lastIndexOf(".") + 1 != strMail.length)
	var IsNoDupChar = (strMail.indexOf("@", 0) == strMail.lastIndexOf("@"))
	if (!(IsRightFmt && IsRightChar && IsRightLength && IsRightPos && IsNoDupChar)) {
		alert("E-mail 地址无效，请提供真实Email，以便找回密码和系统通知所用。");
		return false;
	}
	return true;
}



function convertdate(strdate) {
	strdate = strdate.replace(/-/ig, '/');
	var d = new Date(strdate);
	var now = new Date();
	var result;

	if (d.getYear() == now.getYear() && d.getMonth() == now.getMonth()) {
		var xday = now.getDate() - d.getDate();

		switch (xday) {
			case 0:
				result = "今天 " + d.getHours() + " : " + d.getMinutes();
				break;
			case 1:
				result = "昨天 " + d.getHours() + " : " + d.getMinutes();
				break;
			case 2:
				result = "前天 " + d.getHours() + " : " + d.getMinutes();
				break;
			default:
				result = d.format("MM.dd hh:mm");
				break;
		}
	}
	else {
		result = d.format("MM.dd hh:mm");
	}

	return result;
}


/*
方法名称: imgautosize 
方法说明: 按高/宽(宽/高)比例显示图片方法
参数说明:
imgobj : img 元素对象
maxwidth 设置图片宽度界限
maxheight 设置图片高度界限
*/
function imgautosize(imgobj, maxwidth, maxheight) {
	var heightwidthrate = imgobj.offsetHeight / imgobj.offsetWidth; //设置高宽比
	var widthheightrate = imgobj.offsetWidth / imgobj.offsetHeight; //设置宽高比

	if (is_ie && imgobj.readyState != 'complete') {  //确保图片完全加载
		//alert(imgobj.offsetHeight+ ' '+imgobj.fileSize);
		return false;
	}

	if (imgobj.offsetHeight > maxheight) {
		imgobj.height = maxheight;
		imgobj.width = maxheight * widthheightrate;
	}
	if (imgobj.offsetWidth > maxwidth) {
		imgobj.width = maxwidth;
		imgobj.height = maxwidth * heightwidthrate;
	}
}
