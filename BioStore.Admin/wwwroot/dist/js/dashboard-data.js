/*Dashboard3 Init*/
 
"use strict"; 
$(document).ready(function() {
	/*Toaster Alert*/

});

/*****E-Charts function start*****/
var echartsConfig = function() { 
	
	
	
	
}
/*****E-Charts function end*****/

var sparklineLogin = function() { 
	if( $('#sparkline_1').length > 0 ){
		$("#sparkline_1").sparkline([2,4,4,6,8,5,6,4,8,6,6,2 ], {
			type: 'line',
			width: '100',
			height: '34',
			resize: true,
			lineWidth: '1',
			lineColor: '#7a5449',
			fillColor: '#f6f3f2',
			spotColor:'#7a5449',
			spotRadius:'0',
			minSpotColor: '#7a5449',
			maxSpotColor: '#7a5449',
			highlightLineColor: 'rgba(0, 0, 0, 0)',
			highlightSpotColor: '#7a5449'
		});
	}	
	if( $('#sparkline_2').length > 0 ){
		$("#sparkline_2").sparkline([2,7,7,5,8,5,4,4,3,4,6,1 ], {
			type: 'line',
			width: '100',
			height: '34',
			resize: true,
			lineWidth: '1',
			lineColor: '#7a5449',
			fillColor: '#f6f3f2',
			spotColor:'#7a5449',
			spotRadius:'0',
			minSpotColor: '#7a5449',
			maxSpotColor: '#7a5449',
			highlightLineColor: 'rgba(0, 0, 0, 0)',
			highlightSpotColor: '#7a5449'
		});
	}	
	if( $('#sparkline_3').length > 0 ){
		$("#sparkline_3").sparkline([9,3,3,2,8,6,4,3,3,2,6,1 ], {
			type: 'line',
			width: '100',
			height: '34',
			resize: true,
			lineWidth: '1',
			lineColor: '#7a5449',
			fillColor: '#f6f3f2',
			spotColor:'#7a5449',
			spotRadius:'0',
			minSpotColor: '#7a5449',
			maxSpotColor: '#7a5449',
			highlightLineColor: 'rgba(0, 0, 0, 0)',
			highlightSpotColor: '#7a5449'
		});
	}
	if( $('#sparkline_4').length > 0 ){
		$("#sparkline_4").sparkline([5,3,3,2,1,6,2,3,5,2,2,1 ], {
			type: 'line',
			width: '100',
			height: '34',
			resize: true,
			lineWidth: '1',
			lineColor: '#7a5449',
			fillColor: '#f6f3f2',
			spotColor:'#7a5449',
			spotRadius:'0',
			minSpotColor: '#7a5449',
			maxSpotColor: '#7a5449',
			highlightLineColor: 'rgba(0, 0, 0, 0)',
			highlightSpotColor: '#7a5449'
		});
	}
}
sparklineLogin();

/*****Resize function start*****/
var sparkResize,echartResize;
$(window).on("resize", function () {
	/*Sparkline Resize*/
	clearTimeout(sparkResize);
	sparkResize = setTimeout(sparklineLogin, 200);
	
	/*E-Chart Resize*/
	clearTimeout(echartResize);
	echartResize = setTimeout(echartsConfig, 200);
}).resize(); 
/*****Resize function end*****/

/*****Function Call start*****/
echartsConfig();
/*****Function Call end*****/