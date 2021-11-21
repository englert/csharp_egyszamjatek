/*
Az egyszámjáték Mérő László matematikus találmánya. A játék nagyon egyszerű.
Mindenkinek, aki a játék egy fordulójában részt kíván venni, tippelnie kell egy számra 1 és
99 között. A játékot az nyeri, aki a legkisebb olyan számra tippelt, amelyre csak ő tippelt
egyedül, ha nincs ilyen szám, akkor a fordulónak nincs nyertese. 
http://www.infojegyzet.hu/erettsegi/informatika-ismeretek/kozep-prog-2017okt/
egyszamjatek.txt:
3 12 1 8 5 8 1 2 1 4 Marci
3 1 2 3 6 1 1 2 3 4 Lili
1 2 3 4 5 2 3 1 1 2 Andi
1 3 2 3 2 1 1 2 2 3 Tibi
4 5 1 1 2 4 1 7 8 2 Bence
3 1 2 6 3 4 7 8 2 3 Anna
1 2 1 2 1 2 1 2 1 2 Mari
3 2 6 7 2 4 8 11 1 2 Rita
4 7 5 6 13 9 5 4 1 2 Gabi
*/
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

class Egyszam
{
    public string    eredetisor  { get; set; }
    public string[]  tippek      { get; set; }
    public string    nev         { get; set; }
    public List<int> tippek_int  { get; set; }

    public Egyszam(string sor)
    {
        eredetisor      = sor;
        var s = sor.Split();

        tippek_int = new List<int>();
        nev       =  s[^1];
        tippek    =  s[0..^1];
        foreach(var tipp in tippek) tippek_int.Add(int.Parse(tipp));       
    }
}

class Program 
{
    public static void Main (string[] args) 
    {
/* 
2. Olvassa be az egyszamjatek.txt állományban lévő adatokat és tárolja el egy olyan
adatszerkezetben, ami a további feladatok megoldására alkalmas! 
*/       
        var lista = new List<Egyszam>();
        var fr    = new StreamReader("egyszamjatek.txt");
        while(!fr.EndOfStream)
        {   
            var sor = fr.ReadLine().Trim();
            lista.Add( new Egyszam(sor) ); 
        }
        fr.Close();
/* 
3. Határozza meg és írja ki a képernyőre a minta szerint, hogy a játékban hány játékos vett
részt!
minta: 
3. feladat: A játékosok száma: 9
*/
        Console.WriteLine($"3. feladat: A játékosok száma: {lista.Count()}");
/* 
4. Határozza meg és írja ki a képernyőre a minta szerint, hogy a játékban hány fordulót
játszottak a játékosok! Feltételezheti, hogy minden játékos minden fordulóban részt vett. 
minta:
4. feladat: A fordulók száma: 10
*/
        Console.WriteLine( $"4. feladat: A fordulók száma: {lista[0].tippek.Count()}" ); 
/* 
5. Döntse el és írja ki a képernyőre a minták szerint, hogy az első fordulóban tippelt-e valaki az 1-es számra! 
minta:
5. feladat: Az első fordulóban volt egyes tipp!
*/ 
        var elso = 
        (
            from sor in lista
            where sor.tippek[0] == "1"
            select sor.nev
        );
        if (elso.Any()) Console.WriteLine($"5. feladat: Az első fordulóban volt egyes tipp!");
        else            Console.WriteLine($"5. feladat: Az első fordulóban nem volt egyes tipp!");
/* 
6. Határozza meg és írja ki a minta szerint, hogy a fordulók során melyik volt a legnagyobb tipp!
minta:
6. feladat: A legnagyobb tipp a fordulók során: 13
*/
        var maxi =
        (
            from sor in lista
            select sor.tippek_int.Max()
        ).Max();
        Console.WriteLine($"6. feladat: A legnagyobb tipp a fordulók során: {maxi}");
/* 
7. Kérje be egy forduló sorszámát! Az adatbevitel előtt jelenjen meg a lehetséges legkisebb
és legnagyobb fordulószám értéke! Például: „7. feladat: Kérem a forduló
sorszámát [1−10]:” (Ebben az esetben 10 db forduló volt az egyszamjatek.txt
állományban.) Ha a beadott sorszám nem felel meg a lehetséges értékeknek, akkor az
1. fordulóval dolgozzon a következő feladatokban!
minta: 
7. feladat: Kérem a forduló sorszámát[1-10]: 5
*/
        var fordulo = 0;
        Console.Write("7. feladat: Kérem a forduló sorszámát[1-10]: ");
        int.TryParse(Console.ReadLine(), out fordulo);
        if ( fordulo < 1 || 11 < fordulo ) fordulo = 1;
/* 
8. Az előző feladatban bekért fordulóban határozza meg és írja ki a minta szerint a nyertes tipp értékét! Ha nem volt nyertes tipp a vizsgált fordulóban, akkor a „Nem volt egyedi tipp a megadott fordulóban!” szöveget jelenítse meg! 
minta:
8. feladat: A nyertes tipp a megadott fordulóban: 3
*/
        var nyertes_tippek = 
        (
            from sor in lista
            group sor by sor.tippek_int[fordulo-1]
            into tippek
            where tippek.Count() == 1
            select tippek.Key
        );

        if (nyertes_tippek.Any()) Console.WriteLine($"8. feladat: A nyertes tipp a megadott fordulóban: {nyertes_tippek.Min()} ");
        else                      Console.WriteLine($"8. feladat: Nem volt egyedi tipp a megadott fordulóban!");
/* 
9. A 7. feladatban bekért fordulóban határozza meg és írja ki a minta szerint a nyertes játékos
nevét! Ha nem volt nyertes a megadott fordulóban, akkor a „Nem volt nyertes a megadott 
fordulóban!” szöveget jelenítse meg! 
minta:
9. feladat: A megadott forduló nyertese: Andi
*/
    string nyertes_neve = "";
    if (nyertes_tippek.Any())
        {
            var nyertes = 
            (
                from sor in lista
                where sor.tippek_int[fordulo-1] == nyertes_tippek.Min()
                select sor.nev
            );
            nyertes_neve = nyertes.First();
            Console.WriteLine($"9. feladat: A megadott forduló nyertese: {nyertes_neve}");
        }
        else Console.WriteLine($"9. feladat: Nem volt nyertes a megadott fordulóban!");
/* 
10. Ha volt nyertes a 7. feladatban megadott fordulóban, akkor a minta szerint írja ki a nyertes
forduló adatait a nyertes.txt állományba! 
Minta a nyertes.txt állományhoz:
Forduló sorszáma: 2.
Nyertes tipp: 3
Nyertes játékos: Tibi
*/
        if (nyertes_tippek.Any())
        {
            var fw = new StreamWriter("nyertes.txt");
            fw.WriteLine($"Forduló sorszáma: {fordulo}");
            fw.WriteLine($"Nyertes tipp: {nyertes_tippek.Min()}");
            fw.WriteLine($"Nyertes játékos: {nyertes_neve}");
            fw.Close();
        }
    } //--------------------- Main vége -------------------------------------------------
}