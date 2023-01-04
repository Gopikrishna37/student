var box1=[];
var box2=[];
var box3=[];
var box4=[];
var b1=[1,2,3];
var b2=[4,5,6];
var b3=[7,8,9];
var b4=[1,4,7];
var b5=[2,5,8];
var b6=[3,6,9];
var b7=[1,5,9];
var b8=[3,5,7];
let round=1;
let i=0;
let myTimeout ;
const date = new Date(); 
    var CSTTime = new Date(date.getTime());
    console.log("CST Date",CSTTime);
    var sec=date.setSeconds(date.getSeconds() + 30);
  
function myFunction(){
        timeout = setTimeout(alertFunc, 30000);
      
      
      function alertFunc() {
        alert("Game time over");
        myFunction();
      }
    p=1
    document.getElementById("1").innerHTML= " ";
    document.getElementById("2").innerHTML= " ";
    document.getElementById("3").innerHTML= " ";
    document.getElementById("4").innerHTML= " ";
    document.getElementById("5").innerHTML= " ";
    document.getElementById("6").innerHTML= " ";
    document.getElementById("7").innerHTML= " ";
    document.getElementById("8").innerHTML= " ";
    document.getElementById("9").innerHTML= " ";
    document.getElementById("p1r1").innerHTML="";
    document.getElementById("p2r1").innerHTML="";
    document.getElementById("p1r2").innerHTML="";
    document.getElementById("p2r2").innerHTML="";
    document.getElementById("p1").innerHTML = "";
    document.getElementById("p2").innerHTML = "";
    document.getElementById("p1f").innerHTML="";
    document.getElementById("p2f").innerHTML="";
document.getElementById("time").innerHTML = CSTTime;
document.getElementById("demo").innerHTML = "Game Started";
document.getElementById("gameboard").style.backgroundColor= "red";
while(p<3){
    if(p%2==1){
    var person1 = prompt("Please enter your name", "Enter name here...");
    if (person1 != null) {
      document.getElementById("p1").innerHTML = person1;
      p++;
    }
}
    else{
        var person2 = prompt("Please enter your name", "Enter name here...");
    if (person2 != null) {
      document.getElementById("p2").innerHTML = person2 ;
      p++;
    }

    }
}
}
function newboard(){
    document.getElementById("1").innerHTML= " ";
    document.getElementById("2").innerHTML= " ";
    document.getElementById("3").innerHTML= " ";
    document.getElementById("4").innerHTML= " ";
    document.getElementById("5").innerHTML= " ";
    document.getElementById("6").innerHTML= " ";
    document.getElementById("7").innerHTML= " ";
    document.getElementById("8").innerHTML= " ";
    document.getElementById("9").innerHTML= " ";
    
}

if(round<=3){
    
function xox(a){
    document.getElementById("round").innerHTML = "round"+round ;
    val(i,a);
    console.log(i);
    i++;
    console.log(round);
    
}
}


function val(i,a){
    
    if(i%2==0){
        document.getElementById(a).innerHTML="X";
        if(round==1){
        box1.push(a);
        console.log(b1);
        console.log(box1);
        if(arrayEquals(box1, b1)||arrayEquals(box1, b2)||arrayEquals(box1, b3)||arrayEquals(box1, b4)||arrayEquals(box1, b5)||
        arrayEquals(box1, b6)||arrayEquals(box1, b7)||arrayEquals(box1, b8))
        {
            
            document.getElementById("p1r1").innerHTML="won";
            document.getElementById("p2r1").innerHTML="Loss";
            if(round==1){
                newboard();
              return  round++;
            }
            
            
        }
    }
            if(round==2){
                box3.push(a);
                if(arrayEquals(box3, b1)||arrayEquals(box3, b2)||arrayEquals(box3, b3)||arrayEquals(box3, b4)||arrayEquals(box3, b5)||
                arrayEquals(box3, b6)||arrayEquals(box3, b7)||arrayEquals(box3, b8))
                {
            document.getElementById("p1r2").innerHTML="won";
            document.getElementById("p2r2").innerHTML="Loss";
            console.log("player1 is won");
            round++;
                }
        }
        }
    
    else{
        document.getElementById(a).innerHTML="O";
        if(round==1){
        box2.push(a);
        console.log(box2);
        if(arrayEquals(box2, b1)||arrayEquals(box2, b2)||arrayEquals(box2, b3)||arrayEquals(box2, b4)||arrayEquals(box2, b5)||
        arrayEquals(box2, b6)||arrayEquals(box2, b7)||arrayEquals(box2, b8))
        {
            
            document.getElementById("p2r1").innerHTML="won";

            document.getElementById("p1r1").innerHTML="Loss";
            if(round==1){
                newboard();
                return round++;
            }
        
          }
        }
            if(round==2){
                box4.push(a);
                if(arrayEquals(box4, b1)||arrayEquals(box4, b2)||arrayEquals(box4, b3)||arrayEquals(box4, b4)||arrayEquals(box4, b5)||
                arrayEquals(box4, b6)||arrayEquals(box4, b7)||arrayEquals(box4, b8))
                {
                document.getElementById("p2r2").innerHTML="won";
                document.getElementById("p1r2").innerHTML="Loss";
                console.log("player1 is won");
                    round++;
            }
             }
        }
        if(round==3){
            document.getElementById("p1f").innerHTML="Draw";
            document.getElementById("p2f").innerHTML="Draw";
            alert("Game is Draw");    
        }

    }



const arrayEquals= (x, y) => {
    if (x.length !== y.length){ return false;}
    const elements = new Set([...x, ...y]);
    for (const z of elements) {
        const count1 = x.filter(e => e === z).length;
        const count2 = y.filter(e => e === z).length;
        if (count1 !== count2) {return false;}
    }
    return true;
}
