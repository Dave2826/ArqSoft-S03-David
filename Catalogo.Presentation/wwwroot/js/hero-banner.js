document.addEventListener("DOMContentLoaded", function () {
    const slides = Array.from(document.querySelectorAll(".hero-slide"));

    if (slides.length === 0) {
        return;
    }

    let currentIndex = 0;

    slides[currentIndex].classList.add("hero-slide-active");

    if (slides.length === 1) {
        return;
    }

    window.setInterval(function () {
        slides[currentIndex].classList.remove("hero-slide-active");

        currentIndex = (currentIndex + 1) % slides.length;

        slides[currentIndex].classList.add("hero-slide-active");
    }, 5200);
});
