// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*
// ADAPTIVE SLDER

const images = document.querySelectorAll(".slider-wrap .slider img");
const slider = document.querySelector(".slider");

let count = 0;
let width;

function resize() {
    width = document.querySelector(".slider-wrap").offsetWidth;
    slider.style.width = width * images.length + "px";

    images.forEach(item => {
        item.style.width = width + "px";
        item.style.height = "auto";
        rollSlider();
    });
}


window.addEventListener("resize", resize);
resize();

document.querySelector(".slide-next").addEventListener("click", function () {
    count++;
    if (count >= images.length) {
        count = 0;
    }
    rollSlider();

});

document.querySelector(".slide-prev").addEventListener("click", function () {
    count--;
    if (count < 0) {
        count = images.length -1;
    }
    rollSlider();

});


function rollSlider() {

    slider.style.transform = "translate(-" + count * width + "px)"; 
}

let timer = setInterval(function () {
    count++;
    if (count >= images.length) {
        count = 0;
    }
    rollSlider();
}, 5000);


*/

/*
//Adptive slider 2

const screenWidth = window.screen.width;
(function () {
    var doc = document,
        index = 1;




    var Slider = function () {
        this.box = doc.querySelector(".carouselContainer");
        this.slidesBox = doc.querySelector(".carouselSlides");
        this.slides = doc.querySelectorAll('.slide');
        this.btns = doc.querySelectorAll(".butn");
        this.size = this.box.clientWidth;

        this.position();
        this.carousel();
    };
    Slider.prototype.position = function () {
        var size = this.size;
        this.slidesBox.style.transform = 'translateX(' + (-index * size) + 'px)';
    };

    Slider.prototype.carousel = function () {
        var i, max = this.btns.length,
            that = this;

        for (i = 0; i < max; i += 1) {
            that.btns[i].addEventListener('click', Slider[that.btns[i].id].bind(null, that));

        }
    }

    Slider.prev = function (box) {
        box.slidesBox.style.transition = "transform .3s ease-in-out";
        var size = box.size;
        index <= 0 ? false : index--;
        box.slidesBox.style.transform = 'translateX(' + (-index * size) + 'px)';
        box.jump();
    };
    Slider.next = function (box) {
        box.slidesBox.style.transition = "transform .3s ease-in-out";
        var max = box.slides.length;
        var size = box.size;

        index >= max - 1 ? false : index++;
        box.slidesBox.style.transform = 'translateX(' + (-index * size) + 'px)';
        box.jump();
    };


    Slider.prototype.jump = function () {
        var that = this;
        var size = this.size;
        this.slidesBox.addEventListener('transitionend', function () {
            that.slides[index].id === "firstClone" ? index = 1 : index;
            that.slides[index].id === "lastClone" ? index = that.slides.length - 2 : index;
            that.slidesBox.style.transition = "none";
            that.slidesBox.style.transform = 'translateX(' + (-index * size) + 'px)';
        });
    }
    
    function resize() {
        let width = document.querySelector(".carouselContainer").offsetWidth;
        doc.querySelector(".carouselContainer").style.width = width * slides.length + "px";

        slides.forEach(item => {
            item.style.width = width + "px";
            item.style.height = "auto";

        });
        console.log(screenWidth);
    }
    


    new Slider();
    window.addEventListener("resize", resize);
    resize();
})();
*/

let dec_btn = document.querySelectorAll('.decrement-btn');
let inc_btn = document.querySelectorAll('.increment-btn');


dec_btn.forEach(element => element.addEventListener("click", function dec() {
    var id = element.id.split('-')[2];
    var label = document.querySelector('#button-value-' + id);

    if (label.innerHTML <= 0) {
        label.innerHTML = 0;
        element.classList.add('isInactive')
    }
    else {
        element.classList.remove('isInactive')
        label.innerHTML--;
    }
}
));
inc_btn.forEach(element => element.addEventListener("click", function inc() {
    var id = element.id.split('-')[2];
    var label = document.querySelector('#button-value-' + id);
    label.innerHTML++;
}));