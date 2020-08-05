var $window = $(window);
var windowHeight = $window.height();
var windowWidth = $window.width();
var pos = $window.scrollTop();
var wx;
var hx;

//save quick_box
var q1 = window.localStorage.getItem('QuickKey');
if( q1 == 'off'){
	$('.quick_box').removeClass('on');
	$('.quick_box_btn').addClass('on');
}else{
	$('.quick_box_btn').removeClass('on');
	$('.quick_box').addClass('on');
}

//save news_box list
var q2 = window.localStorage.getItem('ListKey');
//console.log(q2);
if( q2 == 'large'){
	$('.tool_bar .large').addClass('on');
	$('.tool_bar .list').removeClass('on');
	$('.news_box .inn').removeClass('list');
	$('.news_box .inn').addClass('large');
}else if( q2 == 'list'){
	$('.tool_bar .large').removeClass('on');
	$('.tool_bar .list').addClass('on');
	$('.news_box .inn').removeClass('large');
	$('.news_box .inn').addClass('list');
}

if (typeof console == "undefined") { window.console = { log: function () { } } } // Console IE fix

(function($){

	////pho gallery
	//$.get(root+"inc/pswp_box.html",function(data){ 
	//	$("#pswp_box").html(data);
	//}); 
	$(document).ready(function(e) {
		$('.pho-gallery').contents().filter(function() {
			return this.nodeType === 3;
		}).remove();
	});

	//載圖
	'use strict';
	var loadn = 0;
	var allimgs = $('img').length;

	function imgLoad(){

		loadn++;
		var iw = (loadn / allimgs) * 100;
		$('.loadbar').width(iw+'%');
		if(iw === 100){
			setTimeout("$('.loadbar').addClass('ed');",400);
			setTimeout("$('.loading').fadeOut(1000);",400);	
			setTimeout("$('.loadbar_bg').addClass('ed');",400);	
			$('.peload').addClass('ed');	
		}
	}

	$('img').on('load', function() {
		var $this = $(this);

		//取得原始寬高
		var pnw = $this.prop("naturalWidth");
		var pnh = $this.prop("naturalHeight");
		
		//for PhotoSwipe 
		if( $this.parent().parent().hasClass('pho-gallery') ){
			$this.parent('a').attr('data-size',pnw+'x'+pnh);
		}
		
		imgLoad();
	}).each(function() {
		var $this = $(this);

		//取得原始寬高
		var pnw = $this.prop("naturalWidth");
		var pnh = $this.prop("naturalHeight");
		
		//for PhotoSwipe 
		if( $this.parent().parent().hasClass('pho-gallery') ){
			$this.parent('a').attr('data-size',pnw+'x'+pnh);
		}

		$this.parent('.imgLiquidFill').imgLiquid();
		$this.parent('.imgLiquidCenter').imgLiquid({
			fill: false,
			verticalAlign: 'center', 
			horizontalAlign:  'center'
		});
		if(this.complete){
			imgLoad();
		}
	});

	
	//開始時
	$(document).ready(function(){
		pos =  $window.scrollTop();
		
		//mobile menu icon
		$('.menu_btn, .top_menu_mask').on('click',function(){
			if( $(this).hasClass('on') ){
				$('.menu_btn').removeClass('on');
				$('.top_menu, .top_menu_mask, .submenuhead, .search_box').removeClass('on');
				$("html").attr({style:"overflow: visible !important; background-attachment: scroll !important;"});
			}else{
				$('.menu_btn').addClass('on');
				$('.top_menu, .top_menu_mask').addClass('on');
				$("html").attr({style:"overflow: hidden !important; background-attachment: fixed !important;"});
			}
			return false;
		});

		$('.inx_service_box .menu>li').on('mouseenter',function(){
			var sn = $(this).index();
			//console.log(sn);
			$('.inx_service_box .inner>li').removeClass('on');
			$('.inx_service_box .inner>li').eq(sn).addClass('on');
			return false;
		});
		
		//submenu
		if( windowWidth > 1024 ){
			$('.submenuhead>a').on('mouseenter click', function() {
				$('.submenuhead').removeClass('on');
				$(this).parent().addClass('on');
				
			});
			$('.submenuhead>a').on('focus', function() {
				$('.submenuhead').removeClass('on');
				$('.submenuhead2').removeClass('on');
			});
		
			$('.submenuhead').on('mouseleave', function() {
				$('.submenuhead').removeClass('on');
			});
			
		}else{
			$('.submenuhead>a').on('click', function() {
				if( $(this).parent().hasClass('on')){
					$(this).parent().removeClass('on');
					
				}else{
					$('.submenuhead').removeClass('on');
					$(this).parent().addClass('on');
				}
				return false;
			});
		}
		if( windowWidth > 1024 ){
			$('.submenuhead2>a').on('mouseenter click', function() {
				$('.submenuhead2').removeClass('on');
				$(this).parent().addClass('on');
				
				return false;
			});
			$('.submenuhead2>a').on('focus', function() {
				$('.submenuhead2').removeClass('on');
			});
		
			$('.submenuhead2').on('mouseleave', function() {
				$('.submenuhead2').removeClass('on');
			});
			
		}else{
			$('.submenuhead2>a').on('click', function() {
				if( $(this).parent().hasClass('on')){
					$(this).parent().removeClass('on');
					
				}else{
					$('.submenuhead2').removeClass('on');
					$(this).parent().addClass('on');
				}
				return false;
			});
		}

		$('.search_bar').on('click touchstart', function() {
			if( $(this).hasClass('on')){
				$(this).removeClass('on');	
				$('.search_box').removeClass('on');
			}else{
				$(this).addClass('on');
				$('.search_box').addClass('on');
				$('.submenuhead').removeClass('on');
			}
			return false;
		});
	
	});
	//READY END
	
	//載入後
	$window.on('load', function (e) {
		windowHeight = $window.height();
	
		$('.quick_box .switch').on('click touchstart', function() {
			$('.quick_box').removeClass('on');
			
			$('.quick_box_btn').addClass('on');
			window.localStorage.setItem('QuickKey', 'off');
			return false;
		});

		$('.quick_box_btn').on('click touchstart', function() {
			$('.quick_box_btn').removeClass('on');
			$('.quick_box').addClass('on');
			window.localStorage.setItem('QuickKey', 'on');
			return false;
		});

		$('.tool_bar .large').on('click touchstart', function() {
			$('.tool_bar .large').addClass('on');
			$('.tool_bar .list').removeClass('on');
			$('.news_box .inn').removeClass('list');
			$('.news_box .inn').addClass('large');
			window.localStorage.setItem('ListKey', 'large');
			return false;
		});

		$('.tool_bar .list').on('click touchstart', function() {
			$('.tool_bar .large').removeClass('on');
			$('.tool_bar .list').addClass('on');
			$('.news_box .inn').removeClass('large');
			$('.news_box .inn').addClass('list');
			window.localStorage.setItem('ListKey', 'list');
			return false;
		});

		$('.service_box.sec li .item').on('touchend', function() {
			if( $(this).hasClass('hover')){
				$(this).removeClass('hover');
			}else{
				$('.service_box.sec li .item').removeClass('hover');
				$(this).addClass('hover');
			}
		});

		if( pos >= 80){
			$('#header, #footer, #path').addClass('ed');
		}else{
			$('#header, #footer, #path').removeClass('ed');
		}
		
		
		//SCROLLMAGIC
		var controller = new ScrollMagic.Controller();
		var scene = new ScrollMagic.Scene({ duration: 900, offset: 0 })
			.setTween(".top_banner .pic", 1, { transform: 'translateY(200px)'})
			.addTo(controller);

		var scene = new ScrollMagic.Scene({ duration: 1500, offset: 0 })
			.setTween(".page_banner .pic", 1, { top: 300 })
			.addTo(controller);



		var scene = new ScrollMagic.Scene({ triggerElement: ".inx_news_box2" })
			// trigger animation by adding a css class
			.setClassToggle(".inx_news_box2", "on")
			//.addIndicators({ name: "1 - add a class" }) // add indicators (requires plugin)
			.addTo(controller);


		function isMobile(width) {
			if(width == undefined){
				width = 719;
			}
			if(window.innerWidth <= width) {
				return true;
			} else {
				return false;
			}
		}
		
		//OWL
		var owl1 = $('.top_banner .owl-carousel').owlCarousel({
			loop: true,
			dots: true,
			autoplay: true,
			nav: false,
			lazyLoad: true,
			items: 1,
			animateOut: 'fadeOut',
			animateIn: 'fadeIn',
			autoplayTimeout: 5000,
			autoplaySpeed: 2000
		});

		var setInterval_v;
		owl1.on('translated.owl.carousel',function(e){
			//console.log('translated');
			$('.top_banner .owl-item.active .video').each(function(){
				var attr = $(this).attr('data-videosrc');
				if (typeof attr !== typeof undefined && attr !== false) {
					var videosrc = $(this).attr('data-videosrc');
					//console.log($('.top_banner .owl-item.active .video video').length); 
					
					if( $('.top_banner .owl-item.active .video video').length == 0){
						//未載入影片
						//console.log('add video');

						$(this).prepend('<video muted autoplay playsinline><source src="'+videosrc+'" type="video/mp4"></video>');
						//addvideosn = 1;

						var v1 = $('.top_banner .owl-item.active video').get(0);
						if( v1 ) {
							
							owl1.trigger('stop.owl.autoplay');
		
							v1.oncanplay = function() {
								v1.play();
								var allTime =  v1.duration;
								if( allTime < 6 ) allTime = 6;
								//console.log(allTime);
								$('.top_banner .owl-item.active video').addClass('on');
			
								clearInterval(setInterval_v);
								setInterval_v = setInterval(function(){  
									if( v1.currentTime < ( allTime - 6 )){
										//console.log(v1.currentTime);	
										
									}else{
										owl1.trigger('play.owl.autoplay',[5000]);
									}
								},1000);
							};
						}
					}else{
						//已載入影片
						//console.log('added');
						
						var v1 = $('.top_banner .owl-item.active video').get(0);
						if( v1 ) {
							
							owl1.trigger('stop.owl.autoplay');
		
							v1.currentTime = 0;
							v1.play();
							var allTime =  v1.duration;
							if( allTime < 6 ) allTime = 6;
							//console.log(allTime);
							$('.top_banner .owl-item.active video').addClass('on');
		
							clearInterval(setInterval_v);
							setInterval_v = setInterval(function(){  
								if( v1.currentTime < ( allTime - 5 )){
									//console.log(v1.currentTime);	
									
								}else{
									owl1.trigger('play.owl.autoplay',[5000]);
								}
							},1000);
						}
					}
				}
			});
		});
		
		owl1.on('translate.owl.carousel',function(e){
			//console.log('translate');
			$('.owl-item video').each(function(){
				clearInterval(setInterval_v);
				owl1.trigger('stop.owl.autoplay');
				owl1.trigger('play.owl.autoplay',[5000]);
				$('.top_banner .owl-item.active video').removeClass('on');
			});
		});
		

		var owl2 = $('.inx_spo_box .owl-carousel').owlCarousel({
			loop: true,
			dots: false,
			autoplay: true,
			nav: true,
			lazyLoad: true,
			margin: 50,
			navText: [ '', '' ],
			video:true,
			autoplayTimeout: 6000,
			autoplaySpeed: 1000,
			responsive:{
				0:{
					items:2,
				},
				640:{
					items:3,
				},
				1000:{
					items:4,
				},
				1200:{
					items:5,
				}
			}
		});

		var owl3 = $('.his_list.owl-carousel').owlCarousel({
			loop: false,
			dots: true,
			autoplay: false,
			nav: true,
			lazyLoad: true,
			margin: 20,
			navText: [ '', '' ],
			video:true,
			autoplayTimeout: 6000,
			autoplaySpeed: 1000,
			responsive:{
				0:{
					items:1,
				},
				640:{
					items:3,
				},
				1000:{
					items:4,
				},
				1200:{
					items:5,
				}
			}
		});
		

		var his_h = $('.his_list').height() - $('.his_list li:last-of-type').height() + 15;
		$('.his_list .line').height(his_h);

	});//LOAD END
	
	
	
	//變更視窗尺寸時
	$window.on('resize',function(){
		//目前尺寸
		wx = windowWidth;
		hx = windowHeight;
		//變動後尺寸
		windowWidth = $window.width();
		windowHeight = $window.height();
		
		var his_h = $('.his_list').height() - $('.his_list li:last-of-type').height() + 15;
		$('.his_list .line').height(his_h);
		
	});//RESIZE END
	
	//捲動視窗
	$window.bind('scroll',function(){
		//目前尺寸
		wx = windowWidth;
		hx = windowHeight;
		//變動後尺寸
		windowWidth = $window.width();
		windowHeight = $window.height();
		
		pos =  $window.scrollTop();
		
		if( pos >= 80){
			$('#header, #footer, #path').addClass('ed');
		}else{
			$('#header, #footer, #path').removeClass('ed');
		}
	});
	
})(window.jQuery);