// =============== Home Page Slider ===============
document.addEventListener("DOMContentLoaded", function () {
  const layers = document.querySelectorAll(".layer");
  const maxIndex = layers.length;
  let currentIndex = 0;

  setInterval(function () {
    layers.forEach((layer, index) => {
      layer.style.animation = "none"; // Reset animation
      void layer.offsetWidth; // Trigger reflow
      layer.style.animation = null; // Remove inline style to restart animation
      layer.style.zIndex = ((currentIndex + index) % maxIndex) + 1;
    });
    currentIndex = (currentIndex + 1) % maxIndex;
  }, 5000); // Change z-index Every 5 Secends
});

// =============== Search Bar Animation ===============
let search_icon = document.querySelector(".search-bar i");
let search_input = document.querySelector(".search-bar input");
let search_btn = document.querySelector(".search-bar button");

search_icon.onclick = function () {
  if (window.innerWidth >= 768) {
    if (search_input.style.width === "0px" || search_input.style.width === "") {
      search_input.style.cssText =
        "width: 450px;padding: 0 1rem;border-right: 1px solid var(--light-blue-color);";
      search_btn.style.cssText = "display: block;";
    } else {
      search_input.style.cssText = "width: 0;padding: 0;border-right: 0;";
      search_btn.style.cssText = "display: none;";
    }
  }
};
