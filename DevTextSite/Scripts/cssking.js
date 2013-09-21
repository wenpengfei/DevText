$(function () {
	$(".search-keyword").focus(function () {
		$this_text = $(this).val();
		if ($this_text == this.defaultValue) {
			$(this).val("");
		}
	});
	$(".search-keyword").blur(function () {
		$this_text = $(this).val();
		if ($this_text == "") {
			$(this).val(this.defaultValue);
		}

	});
}); //顶部Search效果;

$(function () {
	var new_list_menu_li = $(".new_list_menu li");
	$(".news_list_box div:gt(1)").hide();
	new_list_menu_li.click(function () {
		$(this).addClass('selected')
		       .siblings().removeClass('selected');
		var new_list_menu_index = new_list_menu_li.index(this);
		$(".news_list_box div").eq(new_list_menu_index).show()
		                       .siblings().hide();
	});
});
$(function () {
	var tab_menu_li = $('.tab_menu li');
	$('.tab_box_ul div:gt(0)').hide();

	tab_menu_li.click(function () {
		$(this).addClass('selected')
			   .siblings().removeClass('selected');

		var tab_menu_li_index = tab_menu_li.index(this);
		$('.tab_box_ul div').eq(tab_menu_li_index).show()
					 .siblings().hide();
	}).hover(function () {
		$(this).addClass('hover');
	}, function () {
		$(this).removeClass('hover');
	});
});



$(function () {
	var flash_menu_li = $('.flash_menu li');

	flash_menu_li.mouseover(function () {
		$(this).addClass('selected')
			   .siblings().removeClass('selected');

		var index = flash_menu_li.index(this);
		$('.flash_content div').eq(index).show()
						 		.siblings().hide();
	});
});
