var followersButton = document.getElementsByClassName('-nal3 ')[1];
var timeoutID ='';
var followersListArea = document.getElementsByClassName('j6cq2');
var totalFollowersString = document.getElementsByClassName('g47SY')[1].getAttribute('title');
var numberFollowers = Number(totalFollowersString.replace(/ /g,''));
var countProccessingUsers = 1;
function start(){
    
	if(document.getElementsByClassName('NroHT').length >= numberFollowers)
		{
			clearTimeout(timeoutID);
			return;  
		}
		
		followersListArea[0].scrollTop = followersListArea[0].scrollHeight;
		
		run();
}

function run() {
	timeoutID = setTimeout(start, 100);
} 

start();

/*function inserButton()
{
    var divToInsert = document.getElementsByClassName('t48Bo fzjDT')[0];
    var buttonRun = document.createElement('a');
    buttonRun.setAttribute('style', 'color:green');
    buttonRun.innerText = 'RUN';
    buttonRun.addEventListener('click', start);
    divToInsert.appendChild(buttonRun);
}

followersButton.addEventListener('click', function(){ 
   setTimeout(inserButton,1000)
});*/