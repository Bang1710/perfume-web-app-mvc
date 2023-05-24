
var mainSideNavigation = document.getElementsByClassName('main-sidenav')[0];
var sideNavigations = document.getElementsByClassName('sidenav-btn')[0];
var closeButton = document.querySelector('.close');

sideNavigations.addEventListener('click', function (event) {
    mainSideNavigation.style.width = '300px';
});

closeButton.addEventListener('click', function () {
    mainSideNavigation.style.width = 0;
});


var btnFilterToggle = document.getElementsByClassName('btn-filter')[0];
var contentFilter = document.getElementsByClassName('content-filter')[0];
var closeBtnFilter = document.querySelector('.btn-close-filter');

console.log(btnFilterToggle)
console.log(contentFilter)
console.log(closeBtnFilter)

btnFilterToggle.addEventListener('click', function () {
    contentFilter.style.width = '325px';
    contentFilter.style.padding = '20px';
    contentFilter.style.border = '1px solid #131212';
})

closeBtnFilter.addEventListener('click', function () {
    contentFilter.style.width = 0;
    contentFilter.style.padding = 0;
    contentFilter.style.border = none;
})


