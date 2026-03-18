// =============== Favourite Medicine ===============
let favBtn = document.querySelectorAll(".medicine-card .fav");

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

// =============== Swiper Hospital Animation ===============
if (innerWidth < 768) {
  var swiper = new Swiper(".hospitals .mySwiper", {
    slidesPerView: 1,
    spaceBetween: 30,
    autoplay: {
      delay: 5000,
      disableOnInteraction: false,
    },
    slidesPerGroup: 1,
    loop: true,
    loopFillGroupWithBlank: true,
    navigation: {
      nextEl: ".hospitals .swiper-button-next",
      prevEl: ".hospitals .swiper-button-prev",
    },
  });
} else if (innerWidth < 992) {
  var swiper = new Swiper(".hospitals .mySwiper", {
    slidesPerView: 2,
    spaceBetween: 30,
    autoplay: {
      delay: 5000,
      disableOnInteraction: false,
    },
    slidesPerGroup: 1,
    loop: true,
    loopFillGroupWithBlank: true,
    navigation: {
      nextEl: ".hospitals .swiper-button-next",
      prevEl: ".hospitals .swiper-button-prev",
    },
  });
} else if (innerWidth >= 992) {
  var swiper = new Swiper(".hospitals .mySwiper", {
    slidesPerView: 3,
    spaceBetween: 30,
    autoplay: {
      delay: 5000,
      disableOnInteraction: false,
    },
    slidesPerGroup: 1,
    loop: true,
    loopFillGroupWithBlank: true,
    navigation: {
      nextEl: ".hospitals .swiper-button-next",
      prevEl: ".hospitals .swiper-button-prev",
    },
  });
}
// =============== Search Bar Animation ===============
let searcher_icon = document.querySelector(".searcher i");
let searcher_input = document.querySelector(".searcher input");
let searcher_btn = document.querySelector(".searcher button");

searcher_icon.onclick = function () {
  if (window.innerWidth >= 768) {
    if (
      searcher_input.style.width === "0px" ||
      searcher_input.style.width === ""
    ) {
      searcher_input.style.cssText =
        "width: 250px;padding: 0 1rem;border-right: 1px solid var(--light-blue-color);";
      searcher_btn.style.cssText = "display: block;";
    } else {
      searcher_input.style.cssText = "width: 0;padding: 0;border-right: 0;";
      searcher_btn.style.cssText = "display: none;";
    }
  }
};
