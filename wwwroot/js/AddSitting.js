

const selectElement = document.getElementById("classrooms");
const capacityDisplay = document.getElementById("real-max-capacity");
const showButton = document.getElementById("select-btn");

showButton.addEventListener("click", function () {
  const selectedOption = selectElement.options[selectElement.selectedIndex];
  const capacity = selectedOption.getAttribute("data-capacity");

  if (capacity) {
    capacityDisplay.textContent = capacity;
  } else {
    capacityDisplay.textContent = "-";
  }
});
