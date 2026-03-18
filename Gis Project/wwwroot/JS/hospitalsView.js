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
