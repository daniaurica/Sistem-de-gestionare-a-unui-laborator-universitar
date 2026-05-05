# Sistem de gestionare a unui laborator universitar

## Descriere
Acest proiect reprezintă o aplicație realizată în C# pentru gestionarea echipamentelor dintr-un laborator universitar și a împrumuturilor realizate de studenți.

Scopul aplicației este de a ține evidența echipamentelor disponibile, a persoanelor care le împrumută și a perioadelor de utilizare, precum și de a implementa reguli care să prevină depășirea limitelor stabilite.

---

## Funcționalități
- Adăugare și afișare echipamente  
- Adăugare și afișare studenți  
- Gestionarea împrumuturilor  
- Căutare echipamente după denumire  
- Căutare studenți după nume  

Fiecare echipament poate exista în mai multe exemplare.

Pentru un anumit echipament se poate afișa:
- Numărul total de exemplare  
- Numărul de exemplare disponibile  

Pentru un anumit student se poate afișa:
- Lista împrumuturilor active  

Dacă un student depășește numărul maxim de echipamente permise, aplicația va afișa un mesaj de avertizare și nu va permite realizarea unui nou împrumut.

---

## Reguli implementate
- Nu se poate realiza un împrumut dacă nu există exemplare disponibile.  
- Un student nu poate avea mai multe împrumuturi active decât limita stabilită.  
- La returnarea unui echipament, numărul de exemplare disponibile este actualizat automat.  
- Fiecare împrumut conține data de început și termenul de returnare.  

---

## Structura aplicației
Aplicația este organizată pe mai multe clase:
- Echipament  
- Student  
- Imprumut  

---

## Tehnologii utilizate
- C#  
- Programare orientată pe obiect (OOP)  
