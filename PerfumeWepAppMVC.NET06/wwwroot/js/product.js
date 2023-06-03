
var mainSideNavigation = document.getElementsByClassName('main-sidenav')[0];
var sideNavigations = document.getElementsByClassName('sidenav-btn')[0];
var closeButton = document.querySelector('.close');

sideNavigations.addEventListener('click', function (event) {
    mainSideNavigation.style.width = '300px';
});

closeButton.addEventListener('click', function () {
    mainSideNavigation.style.width = 0;
});


var btnFilterToggle = document.getElementsByClassName('btn-filter');
var contentFilter = document.getElementsByClassName('content-filter')[0];
var closeBtnFilter = document.querySelectorAll('.fa-solid.fa-xmark.btn-close-filter');

console.log(btnFilterToggle)
console.log(contentFilter)
console.log(closeBtnFilter)


for (var i = 0; i < btnFilterToggle.length; i++) {
    btnFilterToggle[i].addEventListener('click', function () {
        contentFilter.style.width = '325px';
        contentFilter.style.padding = '20px';
        contentFilter.style.border = '1px solid #131212';
    });
}

for (var i = 0; i < closeBtnFilter.length; i++) {
    closeBtnFilter[i].addEventListener('click', function () {
        contentFilter.style.width = 0;
        contentFilter.style.padding = 0;
        contentFilter.style.border = 'none';
    });
}

//var btnFilterToggle = document.querySelectorAll('.btn.btn-outline-dark.btn-filter');
//var contentFilter = document.querySelector('.content-filter');

//console.log(btnFilterToggle);
//console.log(contentFilter);
//console.log(closeBtnFilter);

//// Lặp qua từng nút bấm và gán sự kiện click


//// Gán sự kiện click cho nút đóng
//closeBtnFilter.addEventListener('click', function () {
//    contentFilter.style.width = '0';
//    contentFilter.style.padding = '0';
//    contentFilter.style.border = 'none';
//});


// Lấy form và nút reset
var form = document.getElementById('form-filter-id');



// Đặt sự kiện click cho nút reset
// Lấy tất cả các nút resetButton
var resetButtons = document.querySelectorAll('.btn-reset');

// Lặp qua tất cả các nút resetButton và đặt sự kiện click cho từng nút
resetButtons.forEach(function (resetButton) {
    resetButton.addEventListener('click', function () {
        // Lấy tất cả các trường checkbox và radio button trong form
        var checkboxes = resetButton.closest('form').querySelectorAll('input[type="checkbox"]');
        var radios = resetButton.closest('form').querySelectorAll('input[type="radio"]');


        // Lặp qua các checkbox và đặt thuộc tính defaultChecked thành true/false
        checkboxes.forEach(function (checkbox) {
            checkbox.defaultChecked = false;
        });

        // Lặp qua các radio button và đặt thuộc tính defaultChecked thành true/false
        radios.forEach(function (radio) {
            radio.defaultChecked = false;
        });
    });
});
