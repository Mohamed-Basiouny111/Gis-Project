// <!---------- SideBar Toggle ---------->
let burgerList = document.querySelector(".sidebar-buger");
let sidebar = document.querySelector(".sidebar");
let sideSpan1 = document.querySelector(".sidebar-buger span:nth-of-type(1)");
let sideSpan2 = document.querySelector(".sidebar-buger span:nth-of-type(2)");
let sideSpan3 = document.querySelector(".sidebar-buger span:nth-of-type(3)");
let sideHeader = document.querySelector(".sidebar .header");
let sideHeaderText = document.querySelector(".sidebar .header > h4");
let adminLink = document.querySelector(".sidebar .sidebar-links .admin-page a");
let subMenuLinks = document.querySelectorAll(".sidebar .sub-lists li a");
let sidebarLinks = document.querySelectorAll(".sidebar .sidebar-links li");
let sidebarTexts = document.querySelectorAll(".sidebar .sidebar-links li span");

window.onload = function () {
  if (innerWidth < 850) {
    sideSpan1.style.cssText = "width: 100%";
    sideSpan3.style.cssText = "width: 100%";
    sidebar.style.cssText = "width: 80px";
    sideHeaderText.style.cssText = "max-width: 0;";
    sideHeader.style.cssText = "justify-content: center;";
    adminLink.style.cssText = "justify-content: center;";
    sidebarTexts.forEach((text) => {
      text.style.cssText = "max-width: 0; margin: 0;";
    });
    sidebarLinks.forEach((link) => {
      link.style.cssText = "justify-content: center;";
    });
    subMenuLinks.forEach((link) => {
      link.style.cssText = "justify-content: center;";
    });
  }
};

let sideBurger = () => {
  if (sidebar.style.width === "80px") {
    sideSpan1.style.cssText = "width: 50%";
    sideSpan3.style.cssText = "width: 50%";
    sidebar.style.cssText = "width: 250px";
    sideHeaderText.style.cssText = "max-width: 100px;";
    sideHeader.style.cssText = "justify-content: space-between";
    adminLink.style.cssText = "justify-content: space-between;";
    sidebarTexts.forEach((text) => {
      text.style.cssText = "max-width: 120px;";
    });
    sidebarLinks.forEach((link) => {
      link.style.cssText = "justify-content: space-between;";
    });
    subMenuLinks.forEach((link) => {
      link.style.cssText = "justify-content: flex-start;";
    });
  } else {
    sideSpan1.style.cssText = "width: 100%";
    sideSpan3.style.cssText = "width: 100%";
    sidebar.style.cssText = "width: 80px";
    sideHeaderText.style.cssText = "max-width: 0;";
    sideHeader.style.cssText = "justify-content: center;";
    adminLink.style.cssText = "justify-content: center;";
    sidebarTexts.forEach((text) => {
      text.style.cssText = "max-width: 0; margin: 0;";
    });
    sidebarLinks.forEach((link) => {
      link.style.cssText = "justify-content: center;";
    });
    subMenuLinks.forEach((link) => {
      link.style.cssText = "justify-content: center;";
    });
  }
};
burgerList.onclick = function () {
  if (innerWidth > 850) {
    sideBurger();
  }
};

// =============== Menu Sub Lists Toggle ===============
let menuLists = document.querySelectorAll("#main-list");

menuLists.forEach((list) => {
  list.onclick = () => {
    if (getComputedStyle(list.nextElementSibling).height === "0px") {
      list.nextElementSibling.style.cssText = `max-height: ${
        parseFloat(
          getComputedStyle(list.nextElementSibling.children[0]).height
        ) * list.nextElementSibling.children.length
      }px;`;
      list.lastElementChild.style.cssText = "transform: rotateZ(-90deg);";
    } else {
      list.nextElementSibling.style.cssText = "max-height: 0px;";
      list.lastElementChild.style.cssText = "transform: rotateZ(0);";
    }
  };
});

// --new--active
function Active(active) {
    const items = document.querySelectorAll('.sidebar .sidebar-links .admin-page');
    items.forEach(item => item.classList.remove('active'));
    active.classList.add('active');
}

// =============== Search Bar Animation ===============
let search_icon = document.querySelector(".searcher i");
let search_input = document.querySelector(".searcher input");
// let search_btn = document.querySelector(".searcher a");

search_icon.onclick = function () {
  if (window.innerWidth >= 768) {
    if (search_input.style.width === "0px" || search_input.style.width === "") {
      search_input.style.cssText =
        "width: 350px;padding: 0 1rem;border-right: 1px solid var(--light-blue-color);";
    } else {
      search_input.style.cssText = "width: 0;padding: 0;border-right: 0;";
    }
  }
};
// =============== Deleting Alert Box ===============
let addOverlay = document.querySelector(".alert-overlay");
let dltAlert = document.querySelector(".dlt-alert");
let deleteBtns = document.querySelectorAll("table .delete-btn");
let exitBtnDltAlert = document.querySelector(".dlt-alert .exit-btn");
let clsBtn = document.querySelector(".dlt-alert .dlt-btns .cls");

let transformAddAlertShow = (alert) => {
  document.body.style.overflowY = "hidden";
  alert.style.cssText = "left: 50%";
  addOverlay.style.cssText = "display:block";
};
let transformAddAlertClose = (alert) => {
  document.body.style.overflowY = "auto";
  alert.style.cssText = "left: -100%";
  addOverlay.style.cssText = "display:none";
};
deleteBtns.forEach((btn) => {
  btn.onclick = () => transformAddAlertShow(dltAlert);
});
exitBtnDltAlert.onclick = () => transformAddAlertClose(dltAlert);
clsBtn.onclick = () => transformAddAlertClose(dltAlert);

// =============== DataTable Searcher ===============
new DataTable("#example");
const table = document.getElementById("example");
const searchInput = document.getElementById("searchInput");

// Add event listener for search input
searchInput.addEventListener("input", function () {
  const searchTerm = searchInput.value.toLowerCase();

  for (const row of table.tBodies[0].rows) {
    let rowText = "";
    for (const cell of row.cells) {
      rowText += cell.textContent.toLowerCase() + " ";
    }

    if (rowText.includes(searchTerm)) {
      row.style.display = "";
    } else {
      row.style.display = "none";
    }
  }
});

