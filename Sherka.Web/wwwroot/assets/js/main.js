let toggleMenu = () => {
  var menu = document.getElementById("myLinks");
  if (menu.style.display === "block") {
    menu.style.display = "none";
  } else {
    menu.style.display = "block";
  }
};
$("#close__btn").on("click", () => {
  $("#myLinks").hide();
});

$(document).ready(function () {
    var body = $('.js-render-body')
    body.html(body.text());
}