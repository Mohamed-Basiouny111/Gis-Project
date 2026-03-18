// =============== Side Bar Links Animation ===============
let ele = document.querySelector(".container .sidebar ul li a span");

window.onload = function () {
  if (window.innerWidth <= 768) {
    ele.style.height = "100%";
  } else {
    ele.style.width = "100%";
  }
};

// <!---------- Alert Contact Section ---------->
let emailInput = document.querySelector("input[type='email']");
let passInput = document.querySelector("input[type='password']");
let btnSubmit = document.querySelector("input[type='submit']");
let alert = document.querySelector(".alert");
let alertTimer = document.querySelector(".alert span");
let alertExit = document.querySelectorAll(".alert .exit");
let sendBtn = document.querySelector("#send");

function show(alert, alertTimer) {
  alertTimer.style.transition = "width 5s linear";
  alertTimer.style.width = 0;
  alert.style.transform = "translateX(0)";
}
function close(alert, alertTimer) {
  alertTimer.style.transition = "width 0s linear";
  alertTimer.style.width = "100%";
  alert.style.transform = "translateX(calc(350px + 1rem))";
}

btnSubmit.addEventListener("click", (e) => {
  if (
    passInput.value.length !== 0 &&
    emailInput.value.includes("@") &&
    emailInput.value.slice(emailInput.value.indexOf("@"), -1).length >= 1
  ) {
    setTimeout(() => {
      close(alert, alertTimer);
    }, 5000);
    show(alert, alertTimer);
  } else {
    e.preventDefault();
    setTimeout(() => {
      close(
        document.querySelector(".alert.error"),
        document.querySelector(".alert.error span")
      );
    }, 5000);
    show(
      document.querySelector(".alert.error"),
      document.querySelector(".alert.error span")
    );
  }
});
alertExit[0].addEventListener("click", () => {
  close(
    document.querySelector(".alert.success"),
    document.querySelector(".alert.success span")
  );
});
alertExit[1].addEventListener("click", () => {
  close(
    document.querySelector(".alert.error"),
    document.querySelector(".alert.error span")
  );
});

// =============== Code Login Inputs ===============
const inputs = document.querySelectorAll("#code-input"),
  btn = document.querySelector(".btns input");

// Focus the first input when load
window.addEventListener("load", () => inputs[0].focus());

// Iterate over all inputs
inputs.forEach((input, index1) => {
  input.addEventListener("keyup", (e) => {
    const currentInput = input,
      nextInput = input.nextElementSibling,
      prevInput = input.previousElementSibling;

    if (currentInput.value.lenght > 1) {
      currentInput.value = "";
      return;
    }
    if (
      nextInput &&
      nextInput.hasAttribute("disabled") &&
      currentInput.value !== ""
    ) {
      nextInput.removeAttribute("disabled");
      nextInput.focus();
    }
    if (e.key === "Backspace") {
      inputs.forEach((input, index2) => {
        if (index1 <= index2 && prevInput) {
          input.setAttribute("disabled", true);
          currentInput.value = "";
          prevInput.focus();
        }
      });
    }
    if (!inputs[3].disabled && inputs[3].value !== "") {
      btn.classList.add("active");
      btn.removeAttribute("disabled");
      return;
    } else {
      btn.classList.remove("active");
      btn.setAttribute("disabled", true);
    }
  });
});
