Pri prvom pokretanju aplikacije, potrebno je :
1) Update database. To se moze uciniti iz Visual Studia klikom na Tools -> NuGet Packaga Manager -> Packaga Manager Console. U konzolu je potrebno uneti sledecu komandu:
Update-Database

2) Posle podizanja i podesavanja baze podataka, potrebno je pozvati metodu iz controller-a CurrenciesController, metodu Get() da bi se upisalo u bazu podataka svi podrzani simboli valuta
 koji se koriste u ostalim delovima programa, ukoliko se to ne ucini, aplikacija nece raditi.
