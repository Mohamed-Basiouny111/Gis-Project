// <!---------- Dark Mode ---------->
let darkMode = document.querySelector(".dark-mode");
let logoItems = document.querySelectorAll("img.logo");
let waves = document.querySelectorAll(".section-wave");
let getMode = localStorage.getItem("mode");
let wcol = localStorage.getItem("mode");


if (getMode === "dark") {
    document.body.classList.toggle("dark-mode-active");
    logoItems.forEach((logo) => {
        logo.setAttribute("src", "Images/LogoDark.png");
    });
    if (waves.length > 0) {
        waves[0].setAttribute("src", "Images/Waves/WaveDark.svg");
        waves[1].setAttribute("src", "Images/Waves/WaveLight.svg");
        waves[2].setAttribute("src", "Images/Waves/WaveDark.svg");
        waves[1].style.opacity = "0.03";
    }
} else {
    logoItems.forEach((logo) => {
        logo.setAttribute("src", "Images/Logo.png");
    });
    if (waves.length > 0) {
        waves[0].setAttribute("src", "Images/Waves/WaveLight.svg");
        waves[1].setAttribute("src", "Images/Waves/WaveBlue.svg");
        waves[2].setAttribute("src", "Images/Waves/WaveLight.svg");
        waves[1].style.opacity = "0.051";
    }
}
darkMode.addEventListener("click", function () {
    document.body.classList.toggle("dark-mode-active");

    if (document.body.classList.contains("dark-mode-active")) {
        logoItems.forEach((logo) => {
            logo.setAttribute("src", "Images/LogoDark.png");
        });
        if (waves.length > 0) {
            waves[0].setAttribute("src", "Images/Waves/WaveDark.svg");
            waves[1].setAttribute("src", "Images/Waves/WaveLight.svg");
            waves[2].setAttribute("src", "Images/Waves/WaveDark.svg");
            waves[1].style.opacity = "0.03";
        }
    } else {
        logoItems.forEach((logo) => {
            logo.setAttribute("src", "Images/Logo.png");
        });
        if (waves.length > 0) {
            waves[0].setAttribute("src", "Images/Waves/WaveLight.svg");
            waves[1].setAttribute("src", "Images/Waves/WaveBlue.svg");
            waves[2].setAttribute("src", "Images/Waves/WaveLight.svg");
            waves[1].style.opacity = "0.051";
        }
    }

    if (!document.body.classList.contains("dark-mode-active")) {
        return localStorage.setItem("mode", "light");
    }
    localStorage.setItem("mode", "dark");
});

// <!---------- Mobile Menu Bar ---------->
let burger = document.querySelector("header .burger-list");
let nav = document.querySelector(".menu-bar");
let navSpan1 = document.querySelector(
    "header .burger-list span:nth-of-type(1)"
);
let navSpan2 = document.querySelector(
    "header .burger-list span:nth-of-type(2)"
);
let navSpan3 = document.querySelector(
    "header .burger-list span:nth-of-type(3)"
);

let navBurger = () => {
    // navSpan1.classList.toggle("close");
    navSpan1.classList.toggle("close1");
    navSpan2.classList.toggle("close2");
    // navSpan3.classList.toggle("close");
    navSpan3.classList.toggle("close3");
    if (nav.style.visibility === "hidden" || nav.style.visibility === "") {
        nav.style.cssText =
            "padding: 2rem 0;visibility: visible; max-height: 500px;";
    } else {
        nav.style.cssText = "padding: 0;visibility: hidden; max-height: 0;";
    }
};
burger.onclick = function (e) {
    navBurger();
};

// =============== User Login Menu ===============
let user_clicker = document.querySelector("header .user");
let menu = document.querySelector("header .user ul");
let arrow = document.querySelector("header .user .user-info i");

user_clicker.onclick = function () {
    if (menu.style.display === "none" || menu.style.display === "") {
        menu.style.display = "flex";
        arrow.style.transform = "translateY(15px)";
    } else {
        menu.style.display = "none";
        arrow.style.transform = "translateY(0)";
    }
};

// <!---------- Scroll To Top ---------->
let scrollBtn = document.querySelector(".scroll-top");
let header = document.querySelector("header");

window.onscroll = function () {
    if (this.scrollY >= 120) {
        scrollBtn.classList.add("show-scroll");
        header.classList.add("background");
    } else {
        scrollBtn.classList.remove("show-scroll");
        header.classList.remove("background");
    }
};
scrollBtn.onclick = function () {
    window.scrollTo({
        top: 0,
        behavior: "smooth",
    });
};
