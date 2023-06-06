
const inputNumber = document.querySelector(".input-number input");
const plusBtns = document.querySelectorAll(".input-number .plus");
const minusBtns = document.querySelectorAll(".input-number .minus");

plusBtns.forEach((plusBtn) => {
    plusBtn.addEventListener("click", (event) => {
        event.preventDefault();
        inputNumber.value = parseInt(inputNumber.value) + 1;
        console.log(inputNumber.value);
    });
});

minusBtns.forEach((minusBtn) => {
    minusBtn.addEventListener("click", (event) => {
        event.preventDefault();
        if (parseInt(inputNumber.value) - 1 >= 1) {
            inputNumber.value = parseInt(inputNumber.value) - 1;
        }
    });
});


//var inputProductPrice = document.querySelectorAll('.product_price_hiden');
//var totalPrice = document.querySelectorAll('.hiden-mobie.total_price');
//var inputQuantitys = document.querySelectorAll('.input_quantity');
//var inputQuantityUpdates = document.querySelectorAll('.input_quantity_update');


//console.log(inputQuantitys)
//console.log(inputQuantityUpdates)


//inputQuantitys.forEach((inputQuantity, i) => { // Thêm biến i vào phạm vi của hàm nặc danh
//    inputQuantity.addEventListener('change', function (event) {
//        var newQuantity = parseInt(event.target.value);
//        console.log(newQuantity)
//        if (inputQuantityUpdates[i] && inputQuantityUpdates[i].hasAttribute('value')) {
//            inputQuantityUpdates[i].value = newQuantity;
//            console.log(inputQuantityUpdates[i].value);
//        }

//        inputQuantity.setAttribute('value', newQuantity); // Cập nhật giá trị của input number
//    });
//});

var inputQuantitys = document.querySelectorAll('.input_quantity');
var inputQuantityUpdates = document.querySelectorAll('.input_quantity_update');
var totalPriceElement = document.querySelector('.TotalPrice');

var inputPrices = document.querySelectorAll('.product_price_hiden');


console.log(inputQuantitys)
console.log(inputQuantityUpdates)

inputQuantitys.forEach((inputQuantity, i) => {
    inputQuantity.addEventListener('change', function (event) {
        var newQuantity = parseInt(event.target.value);
        console.log(newQuantity)
        if (inputQuantityUpdates[i] && inputQuantityUpdates[i].hasAttribute('value')) {
            inputQuantityUpdates[i].value = newQuantity;
            console.log(inputQuantityUpdates[i].value);
        }
        inputQuantity.setAttribute('value', newQuantity); // Cập nhật giá trị của input number

        // Tính toán tổng giá trị thành tiền
    });
});

// Hàm tính toán tổng giá trị
function calculateTotalPrice() {
    var total = 0;
    inputQuantityUpdates.forEach((inputQuantityUpdate, i) => {
        if (inputQuantityUpdate && inputQuantityUpdate.hasAttribute('value')) {
            var quantity = parseFloat(inputQuantityUpdate.value);
            var price = parseInt(inputPrices[i].value);
            if (!isNaN(price) && !isNaN(quantity)){
                total += price * quantity;
            }
        }
    });
    return total;
}

//// Hàm định dạng giá trị tiền tệ
function formatCurrency(value) {
    return value.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
}







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

// Mã JavaScript
//var inputQuantity = document.getElementById('input_quantity');
//var initialQuantity = parseInt(inputQuantity.value);
//console.log(initialQuantity);

//// Cập nhật giá trị cho trường input ẩn ban đầu
//var inputQuantityUpdate = document.getElementById('input_quantity_update');
//inputQuantityUpdate.value = initialQuantity;

//console.log(inputQuantityUpdate.value);

//// Lắng nghe sự thay đổi trong phần tử input number
//inputQuantity.addEventListener('change', function () {
//    var newQuantity = parseInt(inputQuantity.value);

//    // Cập nhật giá trị cho trường input ẩn
//    inputQuantityUpdate.value = newQuantity;
//});
//document.addEventListener('DOMContentLoaded', function () {
//    // Lấy giá trị ban đầu của phần tử input number
//});

    //var productPrice = parseInt(inputQuantity.dataset.productPrice);
    //var totalElement = document.getElementById('total_price');

    //// Lấy giá trị ban đầu của phần tử input number và tính tổng giá ban đầu
    //var initialQuantity = parseInt(inputQuantity.value);
    //var initialTotalPrice = productPrice * initialQuantity;
    //totalElement.innerText = initialTotalPrice.ToString("C0").Replace(",", ".").Substring(1);

    //// Lắng nghe sự thay đổi trong phần tử input number
    //inputQuantity.addEventListener('change', function () {
    //    var newQuantity = parseInt(inputQuantity.value);

    //    // Cập nhật giá trị cho trường input ẩn
    //    var newTotalPrice = productPrice * newQuantity;
    //    totalElement.innerText = newTotalPrice;
    //});
//// Mã JavaScript
//document.addEventListener('DOMContentLoaded', function () {
//    var inputQuantity = document.getElementById('input_quantity');
//});



