/*
author:David
If something goes wrong then focus on the form/input field can be using this function.
 */
function getFocus() {
    document.getElementById("inputForm").focus()
}

function countLetters(){
    let numberOfEntered =  //document.getElementById("inputForm").valueOf //value //?? doesn't work.
    console.log(numberOfEntered)
    document.getElementById("counter").innerHTML = numberOfEntered +" characters"
}

/*
//NEVER MIND - the card reader keyboards input ends with ENTER key. //when a defined numbers of characters have been entered into the form, submit is triggered automatically.
function countChars(){
    let maxLength = 10;
    let currentLength = obj.value.length;  //where does it get its value from?
    document.getElementById("counter").innerHTML = obj.value.length+" characters"
    //var x = document.getElementById("inputForm").value;
    //document.getElementById("demo").innerHTML = x;
    if(currentLength == maxLength) {
        document.getElementById("inputForm").submit();
    }
}
*/

function checkIfCodeInDB(){
    //stuff
}