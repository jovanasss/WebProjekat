import { Radnik } from "./Radnik.js";

export class AgencijaPrikaz{

    constructor(listaPoslova, listaDana, listaAgencija){
        
        this.listaPoslova = listaPoslova;
        this.listaDana = listaDana;
        this.listaAgencija = listaAgencija;

        this.kont = null; //pamti se kontejner po kome se crta
    }

  

    crtaj(host){

        let agencijaCount = this.listaAgencija.length;

        this.listaAgencija.forEach(el =>{

            console.log(agencijaCount);
            this.kont = document.createElement("div");
            this.kont.className = "GlavniKontejner";
            this.kont.value=el.naziv;
            host.appendChild(this.kont);

            //------------------------
            let agencija = document.createElement("div");
            agencija.value=el.id;
            agencija.className='Agencija';
            agencija.style.visibility='hidden';
            this.kont.appendChild(agencija);     
            //-----------------------
            
            let kontForma = document.createElement("div");
            kontForma.className = "Forma";
            this.kont.appendChild(kontForma);

            this.crtajFormu(kontForma, el.naziv, this.kont);
            //el.naziv je naziv Agencije
            this.crtajPrikaz(this.kont);

        
        console.log(this.listaAgencija);
        });

    }

    crtajPrikaz(host){ // ovo je za tabelu

        let kontPrikaz = document.createElement("div");
        kontPrikaz.className = "Prikaz";
        host.appendChild(kontPrikaz);

        var tab = document.createElement("table"); // kreiranje tabele
        tab.className = "tabela";
        kontPrikaz.appendChild(tab);

        //tabela ima head i body
        var tabhead = document.createElement("thead");
        tab.appendChild(tabhead);

        // red
        var tr = document.createElement("tr");
        tabhead.appendChild(tr);

        var tabBody = document.createElement("tbody");
        tabBody.className = "TabelaPodaci";
        tab.appendChild(tabBody); // na tebelu se appenduje body/podaci

        //HEADER za tabelu
        let th;
        var zag=["JMBG", "Ime", "Prezime", "Posao", "Dan", "Honorar"]; // ovo je zaglavlje, sluzi da nebi pisao nekoliko puta identican kod
        zag.forEach(el=>{
            th = document.createElement("th");
            th.innerHTML = el; // pisace ovo gore Ime , Prezime...
            tr.appendChild(th); // dodaje se u red
        })
    }

    crtajRed(host){ // ovo je metoda koja sluzi da smesti u redove one elemente : dugmici, input polja, checkbox i td

        let red = document.createElement("div");
        red.className = "red";
        host.appendChild(red);
        return red;
    }

    //funkcija za crtanje
    crtajFormu(host, nazivAgencije, kont){

        let red1 = this.crtajRed(host);
        red1.className = 'agencijaRed';

        let lab = document.createElement("label");
        lab.innerHTML = nazivAgencije;
        red1.appendChild(lab);

        let red = this.crtajRed(host);
        //ovo je za labelu za poslove
        let l = document.createElement("label");
        l.innerHTML = "Posao:";
        red.appendChild(l);

        //za select element
        let se = document.createElement("select");
        se.className = "se";
        red.appendChild(se);

        //sada elementi koje se nalaze u ovom select-u
        let op; //op skraceno od opcija
        this.listaPoslova.forEach(p => {
            op = document.createElement("option");
            op.innerHTML = p.naziv;  //korisnik ce videti naziv poslova
            op.value = p.id; //id elementa smestamo u value , value je vrednost koju ta opcija ima
            se.appendChild(op);
        })

        red = this.crtajRed(host);
        //sada crtanje dana--------------------------------------------------------

        l = document.createElement("label");
        l.innerHTML = "Dan";
        red.appendChild(l);

        let cbbox = document.createElement("div");
        cbbox.className = "cbbox";
        red.appendChild(cbbox);

        //sada su potreban svaki checkbox
        let cb;
        this.listaDana.forEach(d=>{

            let redBox = document.createElement("div");
            redBox.className = "redBox";
            cbbox.appendChild(redBox);

            cb = document.createElement("input");
            cb.type = "checkbox";
            cb.value = d.id; //id dana
            redBox.appendChild(cb);

            //sada labele pored svakog checkbox-a
            l = document.createElement("label");
            l.innerHTML = d.naziv; // naziv ce biti napisan u labeli
            redBox.appendChild(l);
        })

        red = this.crtajRed(host);
        // sada dugme za pretragu
        let btnNadji = document.createElement("button");

        btnNadji.onclick=(ev)=>this.nadjiRadnike(kont);       
        btnNadji.innerHTML = "Prikazi";
        btnNadji.className = "btnNadji";
        red.appendChild(btnNadji);
//-----------------------------------------------------------------deo za upis honorara-------------------------------------------------------------

        red = this.crtajRed(host);
//ovo je input za unos jmbg
        l = document.createElement("label");
        l.innerHTML = "JMBG:";
        red.appendChild(l);        

        var brojJMBG = document.createElement("input");
        brojJMBG.type = "number";
        brojJMBG.className = "BrojJMBG";
        red.appendChild(brojJMBG);

        red = this.crtajRed(host);
//ovo je input za unos honorara
        l = document.createElement("label");
        l.innerHTML = "Honorar:";
        red.appendChild(l);        

        var poljeHonorar = document.createElement("input");
        poljeHonorar.type = "number";
        poljeHonorar.className = "Honorar";
        red.appendChild(poljeHonorar);

        red = this.crtajRed(host);
//sada dugme za upis
        let btnUpisi = document.createElement("button");
        btnUpisi.onclick=(ev)=>this.upisi(brojJMBG.value, poljeHonorar.value, kont); // ova funkcija ce da upise honorar radniku
        btnUpisi.innerHTML = "Upisi honorar";
        btnUpisi.className = "btnUpisi";
        red.appendChild(btnUpisi);

        //------------------------------------------------deo za dodavanje radnika-------------------------------------------------------------------------------
        red = this.crtajRed(host);
//ovo je input za unos imena
        l = document.createElement("label");
        l.innerHTML = "Ime:";
        red.appendChild(l);        

        var ImeRadnika = document.createElement("input");
        ImeRadnika.className = "ImeRadnika";
        red.appendChild(ImeRadnika);

        red = this.crtajRed(host);
//ovo je input za unos prezimena
        l = document.createElement("label");
        l.innerHTML = "Prezime:";
        red.appendChild(l);        

        var PrezimeRadnika = document.createElement("input");
       // poljeHonorar.type = "number";
       PrezimeRadnika.className = "PrezimeRadnika";
        red.appendChild(PrezimeRadnika);

        red = this.crtajRed(host);
        //sada dugme za dodavanje radnika
        let btnDodaj = document.createElement("button");
        btnDodaj.onclick=(ev)=>this.dodaj(brojJMBG.value, ImeRadnika.value, PrezimeRadnika.value, kont); // ova funkcija ce da upise honorar radniku
        btnDodaj.innerHTML = "Dodaj radnika";
        btnDodaj.className = "btnDodaj";
        red.appendChild(btnDodaj);

        //deo za azuriranje radnika
        red = this.crtajRed(host);
        //sada dugme za azuriranje radnika
        let btnAzuriraj = document.createElement("button");
        btnAzuriraj.onclick=(ev)=>this.azuriraj(brojJMBG.value, ImeRadnika.value,  PrezimeRadnika.value, kont); // ova funkcija ce da upise honorar radniku
        btnAzuriraj.innerHTML = "Azuriraj radnika";
        btnAzuriraj.className = "btnAzuriraj";
        red.appendChild(btnAzuriraj);

    //deo za brisanje radnika
        red = this.crtajRed(host);
        //sada dugme za azuriranje radnika
        let btnObrisi = document.createElement("button");
        btnObrisi.onclick=(ev)=>this.obrisi(brojJMBG.value, kont); // ova funkcija ce da upise honorar radniku
        btnObrisi.innerHTML = "Obrisi radnika";
        btnObrisi.className = "btnObrisi";
        red.appendChild(btnObrisi);
    }

    //funkcija za brisanje
    obrisi(jmbg, host){
        
        if(jmbg===null || jmbg===undefined || jmbg===""){
            alert("JMBG radnika nije unet!");
            return;
        }

        console.log("jmbg "+jmbg);

        let agencijaID = host.querySelector(".Agencija").value; 


        fetch("https://localhost:5001/Radnik/ObrisiRadnika/"+jmbg+"/"+agencijaID,
        {
            method:"DELETE"
        }).then(s=>{
            if(s.ok){
                alert("Radnik je obrisan!");
                s.text().then(data=>{
 
                    console.log(data);
                })
            }
            else{alert("Radnik nije pronadjen!");}
        })

    }
    //funkcija za azuriranje
    azuriraj(jmbg, ime, prezime, host){

        if(jmbg===null || jmbg===undefined || jmbg===""){
            alert("JMBG radnika nije unet!");
            return;
        }

        if(ime===""){
            alert("Ime nije uneto!");
            return;
        }

        if(prezime===""){
            alert("Prezime nije uneto!");
            return;
        }

        //preuzimanje id agencije u kojoj je radnik zaposlen
        let agencijaID = host.querySelector(".Agencija").value; 
        console.log(agencijaID);

        console.log("ime "+ime);
        console.log("jmbg "+jmbg);
        console.log("prezime "+prezime); 

        fetch("https://localhost:5001/Radnik/PromenitiRadnika/"+jmbg+"/"+ime+"/"+prezime+"/"+agencijaID,
        {
            method:"PUT"
        }).then(s=>{
            if(s.ok){
                alert("Radnik je azuriran!");
                s.json().then(data=>{
 
                    console.log(data);
                    data.forEach(r=>{
                        const radnik = new Radnik(r.jmbg, r.ime, r.prezime, r.posao, r.dan, r.honorar);
                    })
                })
            }
            else{alert("Radnik nije pronadjen!");}
        })

    }
//za dodavanje fja
    dodaj(jmbg, ime, prezime, host){



        if(jmbg===null || jmbg===undefined || jmbg===""){
            alert("JMBG radnika nije unet!");
            return;
        }

        if(ime===""){
            alert("Ime nije uneto!");
            return;
        }

        if(prezime===""){
            alert("Prezime nije uneto!");
            return;
        }

        //preuzimanje id agencije u kojoj je radnik zaposlen
        let agencijaID = host.querySelector(".Agencija").value; 

        console.log("ime "+ime);
        console.log("jmbg "+jmbg);
        console.log("prezime "+prezime); 

        fetch("https://localhost:5001/Radnik/DodavanjeRadnika/"+jmbg+"/"+ime+"/"+prezime+"/"+agencijaID,
        {
            method:"POST"
        }).then(s=>{
            if(s.ok){
                console.log(s);
                alert("Radnik je dodat!");
                s.json().then(data=>{
 
                    console.log(data);
                    data.forEach(r=>{
                        const radnik = new Radnik(r.jmbg, r.ime,r.prezime, r.posao, r.dan);
                    })
                })
            }
            else{alert("JMBG, ime ili prezime nisu dobro uneti");}
        })
    }

    //fja za upis
    upisi(jmbg, honorar, host){

        if(jmbg===null || jmbg===undefined || jmbg===""){
            alert("JMBG radnika nije unet!");
            return;
        }
        if(honorar===""){
            alert("Honorar nije unet!");
            return;
        }
        else{
            let honorarPars = parseInt(honorar);
            if(honorar < 200 || honorar > 150000){
                alert("Honorar je van opsega!");
                return;
            }
        }
        let dani = host.querySelectorAll("input[type='checkbox']:checked"); // pokupljanje dana CHECKBOXOVA


        //nece biti dozvoljen upis honorara za vise razlicith dana
        
        if(dani[0] === undefined){
            alert("Izaberite jedan dan");
            return;
        }

        if(dani.length!=1){
            alert("Moguce je izabrati samo jedan dan");
            return;
        }


        //sada pokuplam poslove iz SELECT
        let optionEl = host.querySelector("select");  // vraca ID tog posla pa ga uisujemo u sledecem redu u posaoID
        var posaoID = optionEl.options[optionEl.selectedIndex].value;
        
        console.log("honorar "+honorar);
        console.log("jmbg "+jmbg);
        console.log("posao "+posaoID);
        console.log("dan "+dani[0].value);

        //sada pisme fetch koji sluzi za upis
        fetch("https://localhost:5001/Dan/DodajOdradjeniPosao/"+jmbg+"/"+posaoID+"/"+dani[0].value+"/"+honorar,
        {
            method:"POST"
        }).then(s=>{
            if(s.ok){

                //sada oept prvo brisemo prethodnu tabelu i prikazujemo novu / zovem onu funkciju koja to radi
                var teloTab = this.obrisiPredhodniBodyTabele(host);
                s.json().then(data=>{
                    //sada samo da iskoristim podatke koje sam dobila , proverila sam ih sa 
                    console.log(data);
                    data.forEach(r=>{
                        const radnik = new Radnik(r.jmbg, r.ime, r.prezime, r.posao, r.dan, r.honorar);
                        radnik.crtaj(teloTab);
                    })
                })
            }
            else{alert("JMBG ili honorar nisu dobro uneti!");}
        })
    }

    //funkcija za pronalazenja radnika
    nadjiRadnike(host){

        console.log(host);
        console.log();
        console.log("CONTAINER: " + host.value);

        //select trenutnog kontejnera
        //ovde poklupja vrednosti iz select
        let optionEl = host.querySelector("select");

        var posaoID = optionEl.options[optionEl.selectedIndex].value;   //value zato sto nam treba samo vrednost, odnostno samo ID iz baze
        console.log(posaoID);

        //drugi nacin
       //console.log(this.kont.querySelector('option:checked').value);   //querySlelectorAll vraca node listu, a samo querySelector vraca 1 element


        //sada pokuplja vrednosti iz checkbox-ova
        let dani = host.querySelectorAll("input[type='checkbox']:checked");   // ima vise checkboxova i trebaju nam samo cekirano
        console.log(dani);

        //provera da li je checkbox cekiran
        if(dani === null){
            alert("Dan mora biti cekiran");
            return;
        }

        //povezuje se u string sve
        let nizDana = "";
        for(let i = 0; i < dani.length; i++){
            nizDana = nizDana.concat(dani[i].value, "a");  // value su ID iz baze, brojevi ovo "a" ce biti deo URL
        }
        console.log(nizDana);

        //sada je potrebno ucitati radnika
        let agencijaID = host.querySelector('.Agencija').value;
        console.log("ID::" + agencijaID);
        this.ucitajRadnike(posaoID, nizDana, agencijaID, host);

    }
//funkcija za PRIKAZ radnika i njegovih poslova
    ucitajRadnike(posaoID, nizDana, agencijaID, host){

        console.log("POSAOID: " + posaoID);
        console.log("nizDana: " + nizDana);
        console.log("agencijaID: " + agencijaID);

        fetch("https://localhost:5001/Radnik/RadniciPretragaAgencija/"+nizDana+"/"+posaoID+"/"+agencijaID,
        {
            method:"GET"
        }).then(s=>{
            if(s.ok){  // provera da li je Ok posto sam u contoleru kroz Ok vratila podatke
                    var teloTab = this.obrisiPredhodniBodyTabele(host);
                s.json().then(data=>{ // data je niz radnika
                    //ovde ce za svakog radnika da crta ono iz klase radnik
                    data.forEach(s=>{
                        let rdn = new Radnik(s.jmbg, s.ime, s.prezime, s.posao, s.dan, s.honorar);
                        rdn.crtaj(teloTab);
                    })
                })
            }
            else{alert("Dani nisu izabrani!");}
        })
    }

    obrisiPredhodniBodyTabele(host)  // ova funkcija sluzi da se podaci u tabeli nebi ponavljali 
    {
        var teloTab = host.querySelector(".TabelaPodaci");  // izvucemo body tabele i prosledimo ga da u njemu crta, .TabelaPodaci je klasaname pa ima .
        //mora da se nadje roditelj ovog elementa
        var parent = teloTab.parentNode;
        parent.removeChild(teloTab);

        // sada je o brisan ali moramo ponovo da dodamo teloTab da bi moglo da se prikaze opet
        teloTab = document.createElement("tbody");  // ovde ce se dodavati radnici nakon onog forEach u funkciji iznad
        teloTab.className = "TabelaPodaci";
        parent.appendChild(teloTab);
        return teloTab;
    }
}