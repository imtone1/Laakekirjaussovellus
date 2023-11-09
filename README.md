# Lääkekirjaussovellus

Projektin inspiroivana lähtökohtana oli Blazor-kehitystä käsittelevät [YouTube-opetusvideot](https://youtu.be/eEyAKk4NeSg?si=d4NR7SPEKrVs2YEM ), jossa luotiin ehdotussivusto. Näiden videoiden antamalla tietämyksellä loin oman sovellukseni, joka on räätälöity lääkkeiden kirjaamisen ja seurannan tarpeisiin.

## Ongelma

Leikkausten jälkeinen aika on kriittinen jakso potilaan toipumisessa, ja lääkityksen asianmukainen hallinta on välttämätön osa toipumisprosessia. Yleinen ongelma potilaille on lääkkeiden ottamisen ajastaminen ja oireiden kirjaaminen, mikä voi olla haastavaa heikentyneessä tilassa.

## Ratkaisu

Ratkaisuna oli kehittää web-pohjainen sovellus, joka auttaa käyttäjiä tallentamaan ja laskemaan lääkeottoajat sekä seuraamaan heidän oireitaan.

## Teknologiat

- Ohjelmointikieli: C#
- Framework: Blazor
- Tietokanta: MongoDB
- Autentikaatio/Autorisaatio: Azure B2C
- Tyyli: HTML, CSS, Bootstrap 5

## Esittely

Lääkkeiden listaus.
Pystyy etsimään lääkeitä tekstikentästä, lajittelu kategorian perusteella.

<img width="688" alt="laakeAppLaakeList" src="https://github.com/imtone1/Laakekirjaussovellus/assets/88165529/167bbc60-efd5-4ae7-a669-8d82e4dc8aec">

----

Lääkkeen lisätiedot.
Näkee lääkkeen, kuvauksen, annostuksen, ottoajat.

Pystyy muokkaamaan lääkeiden nimiä ja annostelumääriä.

Pystyy laskemaan uudet lääkeottoajat. Lääkeottoaikojen laskemisessa jaetaan vuorokausi lääkeottokertojen määrään. Mikäli yötunnit on määritelty niin lasketaan jäljelle jäävistä tunneista.

<img width="616" alt="laakeAppLaakkeenLisatiedot" src="https://github.com/imtone1/Laakekirjaussovellus/assets/88165529/2c43bf73-5b3c-4034-baf7-2992d310af8f">

---

Lääkkeen lisääminen

Pystyy lisäämään uuden lääkkeen. Pystyy määrittämään yötunnit, joilta lääkettä ei oteta.

<img width="352" alt="laakeAppLisaaLaake" src="https://github.com/imtone1/Laakekirjaussovellus/assets/88165529/be24bc8d-3493-4b47-b36f-29bd483d0e79">

---

Oireen lisääminen

Pystyy lisäämään oireita. Jos aikaa tai päivä ei määritellä niin nykyinen aika tai pvm kirjautuu oireen ilmentymiseen.

<img width="607" alt="laakeAppLisaaOire" src="https://github.com/imtone1/Laakekirjaussovellus/assets/88165529/a0fd659c-4bed-4da7-b30b-b91c55f7c4f6">

---

Lääkeottoaikojen ja oireiden listaus.

Lääkkeet ja oireet listautuvat eri MongoDb kokoelmista perustuen aikaa.

<img width="604" alt="laakeAppOireListaus" src="https://github.com/imtone1/Laakekirjaussovellus/assets/88165529/25ced2db-6e18-404f-8891-22ab46b6f623">




