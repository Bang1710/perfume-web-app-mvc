
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
