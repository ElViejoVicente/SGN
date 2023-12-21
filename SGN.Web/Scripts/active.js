$(function(){
	
	var note = $('#note'),
		ts = new Date(2017, 0, 1),
		newYear = true;
	
	if((new Date()) > ts){
		ts = (new Date()).getTime() + 11*24*60*60*1000;
		newYear = false;
	}
		
	$('#countdown').countdown({
		timestamp	: ts,
		callback	: function(days, hours, minutes, seconds){
			
			var message = "";
			
			message += days + " day" + ( days==1 ? '':'s' ) + ", ";
			message += hours + " hour" + ( hours==1 ? '':'s' ) + ", ";
			message += minutes + " minute" + ( minutes==1 ? '':'s' ) + " and ";
			message += seconds + " second" + ( seconds==1 ? '':'s' ) + " <br />";
			
			if(newYear){
				message += "Hasta el proximo año nuevo!";
			}
			else {
				message += "en los proximos 10 dias apartir de hoy!";
			}
			
			note.html(message);
		}
	});
	
});