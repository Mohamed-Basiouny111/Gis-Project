// =============== Adding Alert Choosing Image ===============
var inputs = document.querySelectorAll(".phone-input .file-input");

for (var i = 0, len = inputs.length; i < len; i++) {
  customInput(inputs[i]);
}

function customInput(el) {
  const fileInput = el.querySelector('[type="file"]');
  const label = el.querySelector("[data-js-label]");

  fileInput.onchange = fileInput.onmouseout = function () {
    if (!fileInput.value) return;

    var value = fileInput.value.replace(/^.*[\\\/]/, "");
    el.className += " -chosen";
    label.innerText = value;
  };
}

// =============== Textarea Editor Design ===============
tinymce.init({
  selector: "#basic-conf",
  width: 600,
  height: 300,
  plugins: [
    "advlist",
    "autolink",
    "link",
    "image",
    "lists",
    "charmap",
    "preview",
    "anchor",
    "pagebreak",
    "searchreplace",
    "wordcount",
    "visualblocks",
    "code",
    "fullscreen",
    "insertdatetime",
    "media",
    "table",
    "emoticons",
    "template",
    "help",
  ],
  toolbar:
    "undo redo | styles | bold italic | alignleft aligncenter alignright alignjustify | " +
    "bullist numlist outdent indent | link image | print preview media fullscreen | " +
    "forecolor backcolor emoticons | help",
  menu: {
    favs: {
      title: "My Favorites",
      items: "code visualaid | searchreplace | emoticons",
    },
  },
  menubar: "favs file edit view insert format tools table help",
  content_style:
    "body { font-family:Helvetica,Arial,sans-serif; font-size:16px }",
});

// Function to set price in the input text

function calculateTotal() {
    const quantity = document.getElementById("quantity").value;

    const seatPrice = document.querySelector('input[name="BookTicket.TablePrice"]:checked');

    if (!seatPrice) {
        document.getElementById("BookTicket.totalPrice").value = "";
        return;
    }
    const total = seatPrice.value * quantity;
    document.getElementById("BookTicket.totalPrice").value = total;
}

const availableTickets = parseInt(document.getElementById('ticketCount').value, 10);
const ticketQuantityInput = document.getElementById('quantity');

ticketQuantityInput.max = availableTickets;

ticketQuantityInput.addEventListener('input', () => {
    let value = parseInt(ticketQuantityInput.value);

    if (value < 1) {
        ticketQuantityInput.value = 1;
    }
    else if (value > 5 && value <= availableTickets) {
        ticketQuantityInput.value = 5;
    }
    else if (value >= availableTickets && 5 >= availableTickets) {
        ticketQuantityInput.value = availableTickets;
    }
});

function check() {

    document.getElementById("online").checked = false;
    document.getElementById("cash").checked = false;
    document.getElementById("online").required = false;
    document.getElementById("cash").required = false;
    document.getElementById("vodafone").checked = false;
    document.getElementById("E&").checked = false;
    document.getElementById("Orange").checked = false;
    document.getElementById("instapay").checked = false;
    document.getElementById('offlineDetails').style.display = 'block';
    document.getElementById('cashDetails').style.display = 'none';
    document.getElementById('onlineDetails').style.display = 'none';
    document.getElementById('phoneInput').style.display = 'none';
}

function showDetails(paymentMethod) {

    // إخفاء جميع التفاصيل أولاً
    document.getElementById('cashDetails').style.display = 'none';
    document.getElementById('onlineDetails').style.display = 'none';
    document.getElementById('offlineDetails').style.display = 'none';
    document.getElementById('phoneInput').style.display = 'none';

    // إظهار تفاصيل الخيار المختار فقط
    document.getElementById(paymentMethod + 'Details').style.display = 'block';

    if (document.getElementById('online').checked || document.getElementById('cash').checked) {
        document.getElementById("offline").checked = false;
        document.getElementById("vodafone").checked = false;
        document.getElementById("instapay").checked = false;
        document.getElementById("vodafone").required = false;
        document.getElementById("E&").checked = false;
        document.getElementById("Orange").checked = false;
        document.getElementById("instapay").required = false;
        document.getElementById("mnum").required = false;
        document.getElementById("screenshot").required = false;
    }


}

function showPhoneInput() {
    document.getElementById('phoneInput').style.display = 'none';
    document.getElementById('phoneInput').style.display = 'block';

    if (document.getElementById('vodafone').checked || document.getElementById('instapay').checked) {
        document.getElementById("offline").checked = true;
        document.getElementById("offline").required = false;
        document.getElementById("mnum").required = true;
        document.getElementById("screenshot").required = true;
    }


}