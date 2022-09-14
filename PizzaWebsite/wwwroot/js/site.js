// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


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