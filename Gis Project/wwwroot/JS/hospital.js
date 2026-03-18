// const swiper = new Swiper(".swiper", {
//   // Optional parameters
//   loop: true,

//   // If we need pagination
//   pagination: {
//     el: ".swiper-pagination",
//   },

//   // Navigation arrows
//   navigation: {
//     nextEl: ".swiper-button-next",
//     prevEl: ".swiper-button-prev",
//   },

//   // And if we need scrollbar
//   scrollbar: {
//     el: ".swiper-scrollbar",
//   },
// });

// =============== Swiper Medicine Image Animation ===============
var swiper = new Swiper(".swiper", {
  slidesPerView: 1,
  spaceBetween: 30,
  // autoplay: {
  //   delay: 5000,
  //   disableOnInteraction: false,
  // },
  slidesPerGroup: 1,
  loop: true,
  loopFillGroupWithBlank: true,
  // pagination: {
  //   el: ".swiper-pagination",
  //   clickable: true,
  // },
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev",
  },
});

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
