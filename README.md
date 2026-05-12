# IntensHRPlatform

## Pokretanje

### Backend
1. Klonirati repozitorijum
2. Promeniti connection string u appsettings.json
3. dotnet ef database update
4. Pokrenuti projekat

### Frontend
1. `cd gr-frontend`
2. `npm install`
3. `npm run dev`

> Frontend koristi `http://localhost:5173`, backend mora biti pokrenut na `https://localhost:44311`

## API Endpointi
| Metoda | URL | Opis |
|--------|-----|------|
| GET | /api/candidates | Vraća sve kandidate sa njihovim skillovima |
| GET | /api/candidates/{id} | Vraća jednog kandidata |
| POST | /api/candidates | Dodaje novog kandidata |
| PUT | /api/candidates/{id}/skills/{skillId} | Dodaje skill kandidatu |
| DELETE | /api/candidates/{id}/skills/{skillId} | Uklanja skill od kandidata |
| DELETE | /api/candidates/{id} | Briše kandidata |
| GET | /api/candidates/search?name=&skillIds= | Pretraga po imenu i/ili skillovima |
| GET | /api/skills | Vraća sve skillove |
| POST | /api/skills | Dodaje novi skill |

## Najzahtjevniji deo

Najveći izazov za mene bio je implementacija pretrage kandidata po imenu i/ili skillu. Trebalo je da upit bude fleksibilan, ako se ne prosledi nijedan parametar vraćaju se svi kandidati, a ako se prosledi jedan ili oba filtrira se po njima. Koristio sam AsQueryable() kako bih dinamički gradio upit i dodavao filtere samo kada su prosleđeni, što mi je oduzelo dosta vremena dok nisam sve složio kako treba.

Dosta pažnje sam posvetio i tome da ne dođe do duplih podataka. Za email sam koristio AnyAsync sa poređenjem na ToLower(), isti princip sam primenio i za nazive skillova, "C# Programming" i "c# programming" treba da budu isti skill. Zbog toga sam morao da normalizujem unos pre provere u bazi umesto da se oslanjam samo na database constraint.

Ove odluke su mi pomogle da podaci ostanu konzistentni, a logika validacije bude na pravom mestu u servisnom sloju.
