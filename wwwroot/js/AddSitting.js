
// Dinamik kapasite görüntüleme
const selectElement = document.getElementById("classrooms");
const capacityDisplays = document.querySelectorAll(".real-max-capacity");
const showButton = document.getElementById("select-btn");

// Burayı eski haline getirmen gerekebilir
showButton.addEventListener("click", function () {
  const selectedOption = selectElement.options[selectElement.selectedIndex];
  const capacity = selectedOption.getAttribute("data-capacity");
  
  if (capacity) {
    capacityDisplays.forEach( span => {
      span.textContent = capacity;
    });
  } else {
    capacityDisplays.forEach( span => {
      span.textContent = "-";
    });
  }

});

// Rastgele öğrenci isimleri
const studentNumberDisplayers = document.querySelectorAll(".student-number");

function changeStudentNumberText(newVal) {
  studentNumberDisplayers.forEach( span => {
    span.textContent = newVal;
  });
}

let currentIndex = 0;

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
  currentIndex++;
});

changeStudentNumberText(currentIndex);

// Öğrenci ekle ve sil
const addBtn = document.getElementById("add-btn");
const deleteBtn = document.getElementById("delete-btn");

addBtn.addEventListener("click", function () {

  if (currentIndex < names.length){
    const p = document.createElement("p");
    p.textContent = names[currentIndex];
    div.appendChild(p);

    selectedNames.push(names[currentIndex]);

    currentIndex++;
    changeStudentNumberText(currentIndex);
  } else {
    alert("Tüm öğrenciler zaten eklendi.");
  }
  
});

deleteBtn.addEventListener("click", function () {

  if (div.lastChild){
    div.removeChild(div.lastChild);
    
    selectedNames.pop();

    currentIndex--;
    changeStudentNumberText(currentIndex);
  } else {
    alert("Silinecek öğrenci yok!");
  }
  
});

// Sıralarla ilgli işlemler

const allSeats = document.querySelectorAll('input[type="checkbox"]');

const seatInfo = new Object();

allSeats.forEach( checkbox => {
  seatInfo[`${checkbox.id}`] = { status: false, owner: "(boş)" };
});

allSeats.forEach( checkbox => {
  checkbox.addEventListener("change", function () {
    if(checkbox.checked){
      seatInfo[`${checkbox.id}`].status = true;
    } else {
      seatInfo[`${checkbox.id}`].status = false;
    }
  });
});
// Burayı düzgün yap
const generateRandomSitting = document.getElementById("create-btn");

generateRandomSitting.addEventListener("click", function () {

  const tempNames = new Array();

  for( let i=0; i<selectedNames.length; i++) {
    tempNames[i] = selectedNames[i];
  }

  for( let id in seatInfo ){
    if( seatInfo[id].status ){
      const randomIndex = Math.floor(Math.random() * tempNames.length);
      seatInfo[id].owner = tempNames[randomIndex];

      changeStudentNameText("_" + id, tempNames[randomIndex]);
      saveSeatInfo("-" + id, tempNames[randomIndex]);

      tempNames.splice(randomIndex, 1);
    } else {
      seatInfo[id].owner = "(boş)"
      changeStudentNameText("_" + id, "(boş)");
      saveSeatInfo("-" + id, "(boş)");
    }
  }
  
});

const checkboxLabelSpans= document.querySelectorAll(".check-span");

function changeStudentNameText(id, newVal) {
  checkboxLabelSpans.forEach( span => {
    if(span.id == id){
      span.textContent = newVal;
    }
  });
}

// Veriyi kaydetmek için
const hiddenSeatNumber = document.querySelectorAll(".hidden-seat-number");
const hiddenSeatOwner = document.querySelectorAll(".hidden-seat-owner");

function saveSeatInfo(id, val) {
  hiddenSeatNumber.forEach( input => {
    if( input.id == id ){
      input.value = id.split("-")[1];
    }
  });

  hiddenSeatOwner.forEach( input => {
    if( input.id == id ){
      input.value = val;
    }
  });
}
