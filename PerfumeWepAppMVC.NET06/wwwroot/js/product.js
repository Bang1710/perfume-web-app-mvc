document.getElementById("form-filter-id").addEventListener("submit", function () {
    var gender = document.getElementsByName('gender');
    gender.forEach(g => {
        localStorage.setItem(g.id, g.checked);
    })

    var priceSortOrder = document.getElementsByName("priceSortOrder");
    priceSortOrder.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    var brand = document.getElementsByName("brand");
    brand.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    var capacity = document.getElementsByName("capacity");
    capacity.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    var original = document.getElementsByName("original");
    original.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    var searchString = document.getElementById('searchString');
    localStorage.setItem('searchStringValue', searchHistory.value);

    var searchHistory = document.getElementById('searchHistory');
    localStorage.setItem('searchHistoryValue', searchHistory.value);
});

window.addEventListener("load", function () {
    var gender = document.getElementsByName('gender');
    var priceSortOrder = document.getElementsByName("priceSortOrder");
    var brand = document.getElementsByName("brand");
    var capacity = document.getElementsByName("capacity");
    var original = document.getElementsByName("original");
    var searchHistory = document.getElementsByName("searchHistory");
    var searchString = document.getElementById('searchString');

    gender.forEach(g => {
        g.checked = localStorage.getItem(g.id) === "true";
    })
    priceSortOrder.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    brand.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    capacity.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    original.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    searchHistory.value = localStorage.getItem('searchHistoryValue');

    searchString.value = localStorage.getItem('searchStringValue');

});


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
    contentFilter.style.boxShadow = 'rgb(96 95 95) 5px -1px 5px 0px';
    contentFilter.style.border = '1px solid #131212';
})

closeBtnFilter.addEventListener('click', function () {
    contentFilter.style.width = 0;
    contentFilter.style.padding = 0;
    contentFilter.style.boxShadow = none;
    contentFilter.style.border = none;
})


