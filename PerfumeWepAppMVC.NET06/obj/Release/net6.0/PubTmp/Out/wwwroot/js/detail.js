const inputNumber = document.querySelector(".input-number input");
const plusBtn = document.querySelector(".input-number .plus");
const minusBtn = document.querySelector(".input-number .minus");

plusBtn.addEventListener("click", (event) => {
    event.preventDefault();
    inputNumber.value = parseInt(inputNumber.value) + 1;
    console.log(inputNumber.value);
});


minusBtn.addEventListener("click", (event) => {
    event.preventDefault();
    if (parseInt(inputNumber.value) - 1 >= 0) {
        inputNumber.value = parseInt(inputNumber.value) - 1;
    }
});

const tabs = $$('.tab-item')
const tabPanes = $$('.tab-pane')
const line = $('.tabs .line')


tabs.forEach(function (tab, index) {
    tab.addEventListener('click', function () {
        const pane = tabPanes[index]
        $('.tab-item.active').classList.remove('active');
        $('.tab-pane.active').classList.remove('active')

        line.style.left = this.offsetLeft + "px";
        line.style.width = this.offsetWidth + "px";

        this.classList.add('active')
        pane.classList.add('active')
    })
})



var listStar = document.querySelectorAll('.star');
var listStarAnswer = document.querySelectorAll('.star-answer');
console.log(listStar);

listStar.forEach(function (star, index) {
    star.addEventListener('click', function () {
        var value = star.getAttribute('data-value');
        listStar.forEach(function (s, i) {
            if (i < index + 1) {
                s.classList.add('selected');
            } else {
                s.classList.remove('selected');
            }
        });
        document.querySelector('#rating').value = value;
    });
});

listStarAnswer.forEach(function (star, index) {
    var value = document.querySelector('#rating-answer').value;
    var valueStar = star.getAttribute('data-value');
    if (valueStar <= value) {
        star.classList.add('selected');
    } else {
        star.classList.remove('selected');
    }
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