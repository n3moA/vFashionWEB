$('.chat-bubble').click(function(){
	console.log("Bubbling")
	$('.chat-box').toggleClass('hide');
	$('.chat-bubble').toggleClass('chat-bubble-hover');
})