var $ = document.querySelector.bind(document)
var $$ = document.querySelectorAll.bind(document)

var slideIndex = 0;

showSlides()

function showSlides() {
    var slides = $$('.mySlide')
    //var dots = $$('.dot')
    slideIndex++;
    if (slideIndex > slides.length) { slideIndex = 1 }

    if (slideIndex < 1) { slideIndex = slides.length }

    slides.forEach(slide => {
        slide.style.display = 'none';
    });

    //dots.forEach(dot => {
    //    dot.className = dot.className.replace(' active', '');
    //});

    slides[slideIndex - 1].style.display = 'block';
    //dots[slideIndex - 1].className += ' active';
    setTimeout(showSlides, 2000)
}
var mainSideNavigation = document.getElementsByClassName('main-sidenav')[0];
var sideNavigations = document.getElementsByClassName('sidenav-btn')[0];
var closeButton = document.querySelector('.close');

sideNavigations.addEventListener('click', function (event) {
    mainSideNavigation.style.width = '300px';
});

closeButton.addEventListener('click', function () {
    mainSideNavigation.style.width = 0;
});

//document.addEventListener('DOMContentLoaded', function () {
//});



//var mainSideNavigation = document.getElementsByClassName('main-sidenav')[0];
//var sideNavigations = document.getElementsByClassName('sidenav-overplay')[0];

//console.log(document.querySelector('.sidenav-overplay'))
//console.log(document.querySelector('.main-sidenav'  ))

////document.querySelector('.sidenav-overplay').addEventListener('click', function () {
////    console.log(1)
////    mainSideNavigation.style.width = '300px';
////})

//function showToggle() {
//    mainSideNavigation.style.width = '300px';
//}

//document.querySelector('.close').addEventListener('click', function () {
//    // mainSideNavigation.style.display = 'none';
//    mainSideNavigation.style.width = 0;
//    //$('.container').style.marginLeft = 0;
//    //$('body').style.backgroundColor = 'white';
//})
//console.log(sideNavigations)
//console.log(mainSideNavigation)
////sideNavigations.forEach(function (sidenav) {
////    sidenav.addEventListener('click', function () {
////        $('button.active').classList.remove('active')
////        this.classList.add('active');
////    })
////})