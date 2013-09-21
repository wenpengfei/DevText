// JavaScript Document
$(document).ready(function () {
	var sw = 1;
	var count = $("#pf_pic li").size();
	//alert(count);
	var n = 0, str = '';
	for (n = 0; n < count; n++) {
		str += '<a href="#nogo" id="bn_' + n + '" style="background-position:0px -' + n * 13 + 'px">&nbsp;</a>';
	}
	$("#pf_words").html(str);
	$('#pf_words a:first').addClass("hover");
	$("#pf_pic li:first").show();
	$(".pf_title li:first").show();
	$("#pf_words a").mouseover(function () {
		sw = $("#pf_words a").index(this);
		//alert(sw);
		myShow(sw);
	});
	function myShow(i) {
		$("#pf_words a").eq(i).addClass("hover").siblings("a").removeClass("hover");
		$("#pf_pic li").eq(i).stop(true, true).fadeIn(500).siblings("li").fadeOut(500);
		$(".pf_title li").eq(i).stop(true, true).fadeIn(500).siblings("li").fadeOut(500);
	}
	//滑入停止动画，滑出开始动画
	$("#pf_pic li").hover(function () {
		if (myTime) {
			clearInterval(myTime);
		}
	}, function () {
		myTime = setInterval(function () {
			myShow(sw)
			sw++;
			if (sw == count) { sw = 0; }
		}, 5000);
	});
	//自动开始
	var myTime = setInterval(function () {
		myShow(sw)
		sw++;
		if (sw == count) { sw = 0; }
	}, 5000);
})
