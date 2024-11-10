# **Imtynių varžybų organizavimo sistema**

Ši sistema skirta **imtynių varžybų organizavimui**. 

### Pagrindiniai taikomosios srities objektai:
- **Varžybos** → **Varžybų svorio kategorija** → **Imtynininkas**

---

## **Pagrindinės sistemos naudotojų rolės:**

1. **Varžybų organizatorius**  
   Asmuo, atsakingas už varžybų ir jų svorio kategorijų kūrimą bei imtynininkų priskyrimą tam tikroms svorio kategorijoms.

2. **Treneris**  
   Treneris gali pridėti imtynininkus į jau sukurtų varžybų svorio kategorijas. Varžybų organizatorius turi patvirtinti pridėjimą.

3. **Svečias**  
   Naudotojas, kuris gali tik **peržiūrėti** informaciją apie renginius, svorio kategorijas ir imtynininkus. Neturi teisės kurti ar redaguoti informacijos, tačiau gali gauti išsamią informaciją apie vykstančias arba planuojamas varžybas bei juose dalyvaujančius imtynininkus.

4. **Administratorius**  
   Asmuo, turintis **visas teises** sistemoje, įskaitant varžybų, svorio kategorijų ir imtynininkų valdymą.

---

## **Naudojamos technologijos:**

- **Backend**: .NET Core
- **Frontend**: React
- **Duomenų bazė**: MSSQL
- **Azure servisai**: „Key Vault“ ir „Azure Storage“
- **Hostingas**: Azure
