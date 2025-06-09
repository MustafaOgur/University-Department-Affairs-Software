

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

const names = [
"Musa Taş",
"Halil İbrahim Can",
"Yakup Kurt",
"Hüseyin Kaya",
"Adem Yıldırım",
"Recep Işık",
"Furkan Yıldırım",
"Mehmet Demir",
"Mehmet Ali Çelik",
"Kemal Kılıç",
"Hasan Kılıç",
"İbrahim Keskin",
"Onur Şahin",
"Yaşar Ateş",
"Abdullah Özkan",
"Arif Özer",
"Osman Ünal",
"Onur Doğan",
"Emre Arslan",
"Yakup Aktaş",
"Mehmet Keskin",
"Serkan Özkan",
"Kemal Şen",
"Recep Köse",
"Halil İbrahim Turan",
"Mustafa Işık",
"Ramazan Şimşek",
"Hakan Keskin",
"Yaşar Çakır",
"Musa Kılıç",
"Şükrü Şimşek",
"Mehmet Avcı",
"Fatih Yıldırım",
"İsmail Aktaş",
"Orhan Yıldırım",
"Bekir Polat",
"Kemal Aktaş",
"Mehmet Güneş",
"Serkan Özcan",
"Musa Gül",
"Osman Avcı",
"Ömer Çakır",
"Yılmaz Şen",
"Cemal Özer",
"Kemal Tekin",
"Yılmaz Acar",
"Mustafa Çakır",
"Muhammet Yüksel",
"Osman Özkan",
"Cemal Çetin",
"Burak Keskin",
"Hasan Kaya",
"Musa Yıldırım",
"Hüseyin Koç",
"Murat Ünal",
"Bekir Kara",
"Serkan Ünal",
"Musa Işık",
"Enes Gül",
"Yaşar Öztürk",
"Orhan Özer",
"Kemal Özdemir",
"Arif Kaya",
"Metin Yalçın",
"Muhammet Aksoy",
"Arif Polat",
"Hüseyin Özdemir",
"Furkan Özcan",
"Mehmet Ali Çetin",
"Süleyman Kaplan",
"Furkan Gül",
"Yasin Polat",
"Metin Özdemir",
"Metin Güler",
"Cemal Yavuz",
"Halil İbrahim Çakır",
"Burak Erdoğan",
"Halil Aktaş",
"Muhammed Öztürk",
"Muhammed Yalçın",
"Muhammet Aksoy",
"Onur Özcan",
"Bekir Işık",
"Muhammet Taş",
"Kemal Keskin",
"Halil İbrahim Korkmaz",
"Onur Yıldız",
"Furkan Öztürk",
"Salih Ateş",
"Hasan Erdoğan",
"Mustafa Yavuz",
"Salih Şimşek",
"Şaban Şen",
"Ahmet Ünal",
"Metin Çakır",
"Hüseyin Korkmaz",
"Mehmet Korkmaz",
"Burak Tekin",
"Ahmet Yıldırım",
"Şaban Şen",
];

const selectedNames = names.slice(0, 50);

const div = document.getElementById("listbox");

selectedNames.forEach(name => {
  const p = document.createElement("p");
  p.textContent = name;
  div.appendChild(p);
});