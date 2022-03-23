export class Radnik{

    constructor(jmbg, ime, prezime, posao, dan, honorar){

        this.jmbg = jmbg;
        this.ime = ime;
        this.prezime = prezime;
        this.posao = posao;
        this.dan = dan;
        this.honorar = honorar;
    }

    crtaj(host){

        //Radnik treba da nacrta po jedan red u tabeli / ovo je samo kao red , a u agencija se crta tabela pa se ovi podaci dodaju u tabelu

        var tr = document.createElement("tr"); // ovo je red
        host.appendChild(tr);

        var el = document.createElement("td"); // ovo je polje
        el.innerHTML = this.jmbg;
        tr.appendChild(el); 

        el = document.createElement("td");  // za ime
        el.innerHTML = this.ime;
        tr.appendChild(el); 

        el = document.createElement("td"); //za prezime
        el.innerHTML = this.prezime;
        tr.appendChild(el); 

        el = document.createElement("td"); //za posao
        el.innerHTML = this.posao;
        tr.appendChild(el); 

        el = document.createElement("td"); //za dan
        el.innerHTML = this.dan;
        tr.appendChild(el); 

        el = document.createElement("td"); //za honorar
        el.innerHTML = this.honorar;
        tr.appendChild(el); 
    }
}