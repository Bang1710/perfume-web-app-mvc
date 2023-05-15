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