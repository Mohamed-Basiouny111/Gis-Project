// =============== Swiper Medicine Image Animation ===============
var swiper = new Swiper(".image-slider .mySwiper", {
    slidesPerView: 1,
    spaceBetween: 30,
    // autoplay: {
    //   delay: 5000,
    //   disableOnInteraction: false,
    // },
    slidesPerGroup: 1,
    loop: true,
    loopFillGroupWithBlank: true,
    pagination: {
        el: ".image-slider .swiper-pagination",
        clickable: true,
    },
    navigation: {
        nextEl: ".image-slider .swiper-button-next",
        prevEl: ".image-slider .swiper-button-prev",
    },
});

// =============== Favourite Medicine ===============
let favBtn = document.querySelectorAll(".description .fav");

favBtn.forEach((fav) => {
    fav.onclick = () => {
        if (fav.classList.contains("fa-regular")) {
            fav.classList.remove("fa-regular");
            fav.classList.add("fa-solid");
            fav.style.color = "#d80032";
        } else {
            fav.classList.remove("fa-solid");
            fav.classList.add("fa-regular");
            fav.style.color = `${getComputedStyle(document.body).getPropertyValue(
                "--header-color"
            )}`;
        }
    };
});
