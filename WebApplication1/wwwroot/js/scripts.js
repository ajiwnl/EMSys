//Main Circle Click Animation
let isClick = false;
function formClick() {
    if(isClick == false) {
        //Gets circle id which is the main form
    isClick = true;
        var mainform = document.getElementById("mainform");
        var navhome = document.getElementById("navhome");

        //Removes animation beat and set animation from right to left
        mainform.classList.remove('beatanimation');
        mainform.className = 'srightanimation';
        navhome.classList.remove('hide-navbar');
        registerAnimationIn();
        subjectAnimationIn();
        schedAnimationIn();
        enrollAnimationIn();
        audioQueue();
        optionSelected('darkenedbg');

    //Slide animation for 4 buttons

    setTimeout(function () {
        //Reset beat animation after slide animation
        mainform.classList.remove('srightanimation');
        mainform.classList.add('beatanimation');

        setTimeout(function () {
            //Slide animation from left and 4 buttons fade out with the delay of 20 secs
            mainform.classList.add('sleftanimation');
            registerAnimationOut();
            subjectAnimationOut();
            schedAnimationOut();
            enrollAnimationOut();
            navhome.classList.add('hide-navbar');
            optionSelected('bg');
        }, 20000);

        //after .9 secs resets circle centric position and make the circle clickable again
        setTimeout(function () {
            mainform.classList.remove('sleftanimation');
            mainform.classList.add('circleObj');
            isClick = false;
        }, 20900); //This delay must be above than the first delay for it countsdown at the same time eg func 1 
                 //is 5 secs and func 2 has 5.9 secs.
    }, 200); //Transition delay for the beat and remove right animation
    }
    else { 
        //Do nothing//
     }
}

//Animation For Options in Main Circle
function registerAnimationIn() {
    var register = document.getElementById("register");
    register.className = 'regAnimationIn';
    register.classList.remove('options');
}

function registerAnimationOut() {
    var register = document.getElementById("register");
    register.classList.remove('regAnimationIn');
    register.className = 'regAnimationOut';
    register.classList.add('options');
}

function subjectAnimationIn() {
    var subject = document.getElementById("subjects");
    subject.className = 'subAnimationIn';
    subject.classList.remove('options');
}

function subjectAnimationOut() {
    var subject = document.getElementById("subjects");
    subject.classList.remove('subAnimationIn');
    subject.className = 'subAnimationOut';
    subject.classList.add('options');
}

function schedAnimationIn() {
    var sched = document.getElementById("sched");
    sched.className = 'schedAnimationIn';
    sched.classList.remove('options');
}

function schedAnimationOut() {
    var sched = document.getElementById("sched");
    sched.classList.remove('schedAnimationIn');
    sched.className = 'schedAnimationOut';
    sched.classList.add('options');
}

function enrollAnimationIn() {
    var enroll = document.getElementById("enrollment");
    enroll.className = 'enrollAnimationIn';
    enroll.classList.remove('options');
}

function enrollAnimationOut() {
    var enroll = document.getElementById("enrollment");
    enroll.classList.remove('enrollAnimationIn');
    enroll.className = 'enrollAnimationOut';
    enroll.classList.add('options');
}
//Background Changes
let optionsSelect = false;
function optionSelected(backgroundState) {
    var selected = document.getElementById("background");
    audioFourB();
    if (backgroundState === 'darkenedbg') {
        selected.classList.remove("bg");
        selected.classList.add('darkenedbg');
        localStorage.setItem('backgroundState', 'darkenedbg');
        optionsSelect = true;
    } else {
        selected.classList.remove('darkenedbg');
        selected.classList.add('bg');
        localStorage.setItem('backgroundState', 'bg');
        optionsSelect = false;
    }
}

document.addEventListener('DOMContentLoaded', function () {
    var selected = document.getElementById("background");
    var backgroundState = localStorage.getItem('backgroundState');

    if (backgroundState === 'darkenedbg') {
        selected.classList.add('darkenedbg');
        optionsSelect = true;
    } else {
        selected.classList.add('bg');
        optionsSelect = false;
    }
});

//Audio Queue
function audioQueue(){
    var audio = new Audio("https://drive.google.com/uc?export=download&id=1p-NcuxbEZXX_IR4Kw-fYtg32csLSn7Ww");
    audio.volume = 0.2;
    audio.play();
    audio.currentTime = 0;
}

function audioFourB() {
    var audio = new Audio("https://drive.google.com/uc?export=download&id=1p-NcuxbEZXX_IR4Kw-fYtg32csLSn7Ww");
    audio.volume = 0.2;
    audio.play();
    audio.currentTime = 0;
}

//Dropdown Interaction
function selectedCourse(selection) {
    var courseddbtn = document.getElementById("courseddbtn");
    var course = document.getElementById("course");

    if (courseddbtn) {
        courseddbtn.textContent = selection;
        course.value = selection;
    }
}

function selectedYear(selection) {
    var yearddbtn = document.getElementById("yearddbtn");
    var year = document.getElementById("year");
    if (yearddbtn) {
        yearddbtn.textContent = selection;
        year.value = selection;
    }
}

function selectedRemarks(selection) {
    var remarksddbtn = document.getElementById("remarksddbtn");
    var remarks = document.getElementById("remarks");
    if (remarksddbtn) {
        remarksddbtn.textContent = selection;
        remarks.value = selection;
    }
}

function selectedOffering(selection) {
    var offeringddbtn = document.getElementById("offeringddbtn");
    var offering = document.getElementById("offering");
    if (offeringddbtn) {
        offeringddbtn.textContent = selection;
        offering.value = selection;
    }
}

function selectedCategory(selection) {
    var categoryddbtn = document.getElementById("categoryddbtn");
    var category = document.getElementById("category");
    if (categoryddbtn) {
        categoryddbtn.textContent = selection;
        category.value = selection;
    }
}

function selectedXM(selection) {
    var SSFXMDropdownBtn = document.getElementById("SSFXMDropdownBtn");
    var selectedSSFXM = document.getElementById("selectedSSFXM");
    if (SSFXMDropdownBtn) {
        SSFXMDropdownBtn.textContent = selection;
        selectedSSFXM.value = selection;
    }
}

function selectedIcon(selection) {
    var iconddbtn = document.getElementById("iconddbtn");
    var iconselect = document.getElementById("iconselect");
    var profile = document.getElementById("profile");
    if (iconddbtn) {
        iconddbtn.textContent = selection;
        iconselect.value = selection;
        if (selection == "1") {
            profile.src = "https://drive.google.com/uc?export=download&id=14HinNQuk2LdHo3jWXNZGxJ3iOMLMem13";
        }
        if (selection == "2") {
            profile.src = "https://drive.google.com/uc?export=download&id=1My-KhHFZsfHNRUSYCSjCBY1jxBNIrqhYg";
        }
        if (selection == "3") {
            profile.src = "https://drive.google.com/uc?export=download&id=1yb5XoVry7NGAaHdji9ze7rPaUEw08l7Y";
        }
    }
}

//Custom Required Error
function InvalidMsgCourse(course) {
    if (course.value == '') {
        course.setCustomValidity('Please Select a Course!');
    }
    else {
        course.setCustomValidity('');
    } 
        return true;
}

function InvalidMsgYear(year) {
    if (year.value == '') {
        year.setCustomValidity('Please Select a Year!');
    }
    else {
        year.setCustomValidity('');
    } 
    return true;
}

function InvalidMsgRemarks(remarks) {
    if (remarks.value == '') {
        remarks.setCustomValidity('Please Select a Remarks!');
    }
    else {
        remarks.setCustomValidity('');
    }
    return true;
}

function InvalidMsgOffering(offering) {
    if (offering.value == '') {
        offering.setCustomValidity('Please Select an Offering!');
    }
    else {
        offering.setCustomValidity('');
    }
    return true;
}

function InvalidMsgCategory(category) {
    if (category.value == '') {
        category.setCustomValidity('Please Select a Category!');
    }
    else {
        category.setCustomValidity('');
    }
    return true;
}

function InvalidMsgRequisite(selectedRequisite) {
    if (selectedRequisite.value == '') {
        selectedRequisite.setCustomValidity('Please Select a Requisite Information!');
    }
    else {
        selectedRequisite.setCustomValidity('');
    }
    return true;
}