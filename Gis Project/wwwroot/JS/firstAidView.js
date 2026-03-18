// <!---------- BurgerList Mobile SideBar ---------->
let burgerSide = document.querySelector(".burger-sidebar");
let sideBar = document.querySelector(".side");
let burgerSpan1 = document.querySelector(".burger-sidebar span:nth-of-type(1)");
let burgerSpan2 = document.querySelector(".burger-sidebar span:nth-of-type(2)");
let burgerSpan3 = document.querySelector(".burger-sidebar span:nth-of-type(3)");

let sideShow = () => {
  burgerSpan1.classList.toggle("close1");
  burgerSpan2.classList.toggle("close2");
  burgerSpan3.classList.toggle("close3");
  sideBar.classList.toggle("right-side-bar");
};
burgerSide.onclick = function (e) {
  sideShow();
};

// <!---------- BurgerList Mobile SideBar ---------->
let options = document.querySelectorAll(".helpfull .helpfull-options");

options.forEach((option) => {
  option.onclick = () => {
    options.forEach((option1) => {
      option1.style.cssText =
        "background-color: var(--background-color); color: var(--header-color);";
      option.style.cssText =
        "background-color: var(--light-blue-color); color: var(--background-color);";
    });
  };
});
