//include共用頁面
$(function ($) {
	$.include = function (url) {
		$.ajax({
			url: url,
			async: false,
			success: function (result) {
				document.write(result);
			}
		});
	};
}(jQuery));


//清除header style 高度, 需於頁面最後執行
$(function () {
	$("#header").removeAttr("style"); 
	$(".header-logo").removeAttr("style"); 
});


//輪播圖左告按鈕加title
window.onload=function(){
	$(".owl-prev").attr("title","prev").attr("tabindex","0");
	$(".owl-next").attr("title","next").attr("tabindex","0");
    $(".tp-leftarrow").attr("title","prev");
	$(".tp-rightarrow").attr("title","next");
}


//font-size
function size_set(mysize){
    if(mysize=="font_l"){
        document.body.className="font_l";
    }
    if(mysize=="font_m"){
        document.body.className="font_m";
    }
    if(mysize=="font_s"){
        document.body.className="font_s";
    }
}


//滾動效果
$(function($) {
	'use strict';
	window.sr= new scrollReveal({
	  reset: false,
	  move: '50px',
	  mobile: false
	});
});

//智能客服smart robot
//彈跳視窗
function Popup(status) {
	var selobj = document.getElementsByTagName("select");
	for (var i = 0; i < selobj.length; i++) {
		if (status == "open") { selobj[i].disabled = true; }
		else { selobj[i].disabled = false; }
	}

	var divsub = document.getElementById("SubForm");

	if (status == "open") {
		divsub.style.display = "block";
	}
	else {
		divsub.style.display = "none";
	}	
	
}
function ccc() {
	document.getElementById("SubForm").style.display = 'none'
}


//開啟廣告
function ad_open() {
    document.getElementById("SubForm").style.display = "block";
}

//關閉廣告
$('.close_ad').click(function(){
	document.getElementById("SubForm").style.display = 'none'
});

//ESC關閉廣告
$( document ).on( 'keydown', function ( e ) {
    if ( e.keyCode === 27 ) { // ESC
        document.getElementById("SubForm").style.display = 'none'
    }
});


