document.getElementById("btn-artikl-pretraga").addEventListener("click", function () {
    const artiklNazivTxtBox = document.getElementById("artkil-naziv-textbox");
    const artiklNaziv = artiklNazivTxtBox.value;

    let url = "/api/artikli/" + artiklNaziv;

    fetch(url)
        .then(response => {
            if (response.ok) {
                return response.json();
            }

            return response.json().then(data => {
                throw new Error(data.error);
            });
        })
        .then(data => {

            const tableBody = document.getElementById("artikli-tablica").getElementsByTagName("tbody")[0];
            
            // isprazni tablicu
            $("#artikli-tablica tbody tr").remove(); 

            data.forEach(function (item) {
                item.artikli.forEach(function (artikl) {
                    let row = document.createElement("tr");

                    let idCell = document.createElement("td");
                    idCell.id = "td-artikl-id";
                    idCell.className = "td-artikl";
                    idCell.textContent = artikl.id;
                    row.appendChild(idCell);

                    let articleCell = document.createElement("td");
                    articleCell.id = "td-artikl";
                    articleCell.className = "td-artikl";
                    articleCell.textContent = artikl.artikl;
                    row.appendChild(articleCell);

                    let nameCell = document.createElement("td");
                    nameCell.id = "td-artikl-naziv";
                    nameCell.className = "td-artikl";
                    nameCell.textContent = artikl.naziv;
                    row.appendChild(nameCell);

                    tableBody.appendChild(row);
                });

            });
        })
        .catch(error => {
            alert(error.message);
        });
});

document.getElementById("btn-datum-obracuna-pretraga").addEventListener("click", function () {
    const poslovnaJedinicaTxtBox = document.getElementById("poslovna-jedinica-textbox");
    const poslovnaJedinica = poslovnaJedinicaTxtBox.value;

    const datumObracunaOdTxtBox = document.getElementById("obracun-od-datuma");
    const datumObracunaOd = datumObracunaOdTxtBox.value;

    const datumObracunaDoTxtBox = document.getElementById("obracun-do-datuma");
    const datumObracunaDo = datumObracunaDoTxtBox.value;

    const obracunPrometaDropdown = document.getElementById("obracun-prometa-dropdown");
    const obracunPrometaValue = obracunPrometaDropdown.value;

    if (poslovnaJedinica == "") {
        alert("Poslovna jedinica je obavezan podatak!");
        return;

    } else if (datumObracunaOd == "") {
        alert("Datum obračuna je obavezan podatak!");
        return;
    }

    let url = getBaseUrl(obracunPrometaValue);

    url = url + poslovnaJedinica + "/" + datumObracunaOd;

    if (datumObracunaDo != "") {
        url = url + "/" + datumObracunaDo
    }

    fetch(url)
        .then(response => {
            if (response.ok) {
                return response.json();
            }

            return response.json().then(data => {
                throw new Error(data.error);
            });
        })
        .then(data => {
            // isprazni tablicu
            $("#datum-obracuna-tablica tr").remove();


            // table head
            if (obracunPrometaValue == "vrstaplacanja") {
                tableHeadSetUp("Vrsta plaćanja UID", "Naziv", "Iznos", "Nadgrupa plaćanja UID", "Nadgrupa plaćanja naziv");

            } else if (obracunPrometaValue == "artikli") {
                tableHeadSetUp("Artikl ID", "Naziv artikla", "Količina", "Iznos", "Usluga");

            }

            // table body
            tableBodySetUp(data, obracunPrometaValue)
        })
        .catch(error => {
            alert(error.message);
        });
});

function getBaseUrl(obracunPlacanjaValue) {
    let url = "";

    if (obracunPlacanjaValue == "vrstaplacanja") {
        url = "/api/obracuni-placanja/";
    } else if (obracunPlacanjaValue == "artikli") {
        url = "/api/obracuni-artikli/";
    }

    return url;
}

function tableHeadSetUp(tHeadValue1, tHeadValue2, tHeadValue3, tHeadValue4, tHeadValue5) {
    // table head
    const tableHead = document.getElementById("datum-obracuna-tablica").getElementsByTagName("thead")[0];
    const thRow = document.createElement("tr");

    let th1 = document.createElement("th");
    th1.textContent = tHeadValue1;
    thRow.appendChild(th1);

    let th2 = document.createElement("th");
    th2.textContent = tHeadValue2;
    thRow.appendChild(th2);

    let th3 = document.createElement("th");
    th3.textContent = tHeadValue3;
    thRow.appendChild(th3);

    let th4 = document.createElement("th");
    th4.textContent = tHeadValue4;
    thRow.appendChild(th4);

    let th5 = document.createElement("th");
    th5.textContent = tHeadValue5;
    thRow.appendChild(th5);

    tableHead.appendChild(thRow);
}

function tableBodySetUp(data, obracunPrometaValue) {
    if (data != null) {
        const tableBody = document.getElementById("datum-obracuna-tablica").getElementsByTagName("tbody")[0];

        if (obracunPrometaValue == "vrstaplacanja") {
            data.forEach(function (item) {
                item.obracunPlacanja.forEach(function (obracunPlacanja) {

                    const tbRow = document.createElement("tr");

                    let vrstaPlacanjaCell = document.createElement("td");
                    vrstaPlacanjaCell.textContent = obracunPlacanja.vrstaPlacnjaUid;
                    tbRow.appendChild(vrstaPlacanjaCell);

                    let nazivCell = document.createElement("td");
                    nazivCell.textContent = obracunPlacanja.naziv;
                    tbRow.appendChild(nazivCell);

                    let iznosCell = document.createElement("td");
                    iznosCell.textContent = obracunPlacanja.iznos;
                    tbRow.appendChild(iznosCell);

                    let nadgrupaPlacanjaUidCell = document.createElement("td");
                    nadgrupaPlacanjaUidCell.textContent = obracunPlacanja.nadgrupaPlacanjaUid;
                    tbRow.appendChild(nadgrupaPlacanjaUidCell);

                    let nadgrupaPlacanjaNazivCell = document.createElement("td");
                    nadgrupaPlacanjaNazivCell.textContent = obracunPlacanja.nadgrupaPlacanjaNaziv;
                    tbRow.appendChild(nadgrupaPlacanjaNazivCell);

                    tableBody.appendChild(tbRow);
                });

            });

        } else if (obracunPrometaValue == "artikli") {
            data.forEach(function (item) {
                item.obracunArtikli.forEach(function (artikl) {

                    const tbRow = document.createElement("tr");

                    let artiklCell = document.createElement("td");
                    artiklCell.textContent = artikl.artiklUid;
                    tbRow.appendChild(artiklCell);

                    let nazivCell = document.createElement("td");
                    nazivCell.textContent = artikl.nazivArtikla;
                    tbRow.appendChild(nazivCell);

                    let kolicinaCell = document.createElement("td");
                    kolicinaCell.textContent = artikl.kolicina;
                    tbRow.appendChild(kolicinaCell);

                    let iznosCell = document.createElement("td");
                    iznosCell.textContent = artikl.iznos;
                    tbRow.appendChild(iznosCell);

                    let uslugaCell = document.createElement("td");
                    uslugaCell.textContent = artikl.usluga;
                    tbRow.appendChild(uslugaCell);

                    tableBody.appendChild(tbRow);
                });

            });
        }
    }
}