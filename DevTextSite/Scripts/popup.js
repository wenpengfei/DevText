function G(id) { return document.getElementById(id); }
function showPop() {
	var objSha = G("shadowDiv");
	objSha.style.height = document.body.scrollHeight + "px";
	objSha.style.width = document.body.scrollWidth + "px";
	objSha.style.left = 0;
	objSha.style.top = 0;
	ToChooseLogo();
	var objPop = G("popDiv");
	var sClientWidth = document.body.clientWidth;
	var sClientHeight = document.body.clientHeight;
	var sScrollTop = document.body.scrollTop;
	var sleft = (document.body.clientWidth / 2) - 0;
	var iTop = -80 + (sClientHeight / 2 + sScrollTop) - 0;
	var sTop = iTop > 0 ? iTop : (sClientHeight / 2 + sScrollTop) - 375;
	if (sTop < 1) sTop = "20px";
	if (sleft < 1) sleft = "20px";

	if (screen.width < 801) {

		objPop.style.left = "20px";
		objPop.style.top = "20px";
		objPop.style.height = "350px";

	} else if (screen.width > 1024) {

		objPop.style.left = "300px";
		objPop.style.top = "170px";
		objPop.style.height = "550px";

	} else {

		objPop.style.left = "180px";
		objPop.style.top = "120px";
		objPop.style.height = "500px";
	}

	objSha.style.display = "";
	objPop.style.display = "";

}
function hidePop() {

	G("popDiv").style.display = "none";
	G("shadowDiv").style.display = "none";


}
function ToChooseLogo() {
	$.ajax({
		type: "POST",
		url: "/home/news/getallnewstopics",
		dataType: 'json',
		success: function (data) {
			$("#poppic_list").html("");
			$.each(data, function (i) {
				$("#poppic_list").append("<li><a onclick=\"ChooseLogo('" + data[i].ID + "');\"> <img src=" + data[i].FilePath + " id=\"img" + data[i].ID + "\" class=\"fl\" /></a></li>");
			});

		}
	});
}
function ChooseLogo(id) {
	$("#result").html("");
	$("#img" + id).clone(true).appendTo($("#result"));
	$("#result").show();
	$("#logo").val(id);
	hidePop();
}

function ClearTopic() {
	$("#result").html("");
	$("#logo").val(0);
}