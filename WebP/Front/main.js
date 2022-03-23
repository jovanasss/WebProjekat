import { AgencijaPrikaz } from "./AgencijaPrikaz.js";
import { Dan } from "./Dan.js";
import { Agencija } from "./Agencija.js";
import { Posao } from "./Posao.js";

var listaPoslova = []; //prazna lista ukju ce se dodavati poslovi
var listaAgencija = [];

fetch("https://localhost:5001/Agencija/PreuzmiAgencije")
.then(p=>{ //podaci su u json obliku
    p.json().then(agencije=>{  // ovo je lista poslova, ima ih vise
        agencije.forEach(agencija => {
            var p = new Agencija(agencija.id, agencija.naziv);
            listaAgencija.push(p); // dodajemo ovaj objekat Posao u listu
        })
    })
}) 

//sada se pisu pozivi (fetch)
fetch("https://localhost:5001/Posao/PreuzmiPoslove")
.then(p=>{ //podaci su u json obliku
    p.json().then(poslovi=>{  // ovo je lista poslova, ima ih vise
        poslovi.forEach(posao => {
            var p = new Posao(posao.id, posao.naziv);
            listaPoslova.push(p); // dodajemo ovaj objekat Posao u listu
        })


        // za dane------
var listaDana = [];
fetch("https://localhost:5001/Dan/DaniUNedelji")
.then(p=>{
    p.json().then(dani=>{
        dani.forEach(dan=>{
            var d = new Dan(dan.id, dan.naziv);
            listaDana.push(d);
        })
        // klasa za prikaz----
        var a = new AgencijaPrikaz(listaPoslova, listaDana, listaAgencija);
        a.crtaj(document.body);
    })
})

    })
})
console.log(listaPoslova);
