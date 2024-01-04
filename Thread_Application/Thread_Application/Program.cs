using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading;

/*
 * Author: Muhammet Sait Yılmaz
 * Number: 211229018
 * Date: 04.01.2024
 * Description: This program a thread C# application.
 * Github: Muhammetyilmaz7
 */

class Program
{
    static void Main()
    {
        // Stopwatch ile zaman ölçümü başlatılır.
        Stopwatch stopwatch = Stopwatch.StartNew();
        // Nedir bu Thread?
        // Bir process'in birden fazla işi aynı anda gerçekleştirmesini sağlayan yapıya "thread" denir.
        // Bir process, içerisinde bir veya birden fazla thread barındırabilir.
        // Thread'ler, aynı anda sadece tek bir işi değil, aynı anda farklı işleri de gerçekleştirebilir.
        // App fonksiyonumuzu  çağırıyoruz ve işlemler başlıyor.
        App();
        // Stopwatch durdurulur ve süre konsola yazdırılır.
        stopwatch.Stop();
        Console.WriteLine($"Yaptığınız İşlemler Sonunda Kodunuzun Çalışma Süresi (1000 milisaniye = 1saniye ): {stopwatch.ElapsedMilliseconds} milisaniye");

    }
    static void App()
    {


        // 1'den 1000000'a kadar olan sayılardan oluşan bir List<int> oluşturuyoruz.
        List<int> sayilar = Enumerable.Range(1, 1000000).ToList();

        // Listeyi 4 eşit parçaya bölüyoruz.
        int size = sayilar.Count / 4;

        // Oluşturulan sayilar listesi, 4 eşit parçaya bölünüp listeler adlı bir liste içine eklenir.
        List<List<int>> listeler = new List<List<int>>();

        //  Her bir alt listeyi, GetRange metodu kullanılarak ana listeden alıyoruz.
        for (int i = 0; i < sayilar.Count; i += size)
        {
            List<int> liste = sayilar.GetRange(i, Math.Min(size, sayilar.Count - i));
            listeler.Add(liste);
        }

        // Ortak sonuç listelerini oluşturduk.
        ConcurrentBag<int> asalSayilar = new ConcurrentBag<int>();
        ConcurrentBag<int> ciftSayilar = new ConcurrentBag<int>();
        ConcurrentBag<int> tekSayilar = new ConcurrentBag<int>();

        // Thread'leri oluşturuyoruz.
        Thread thread1 = new Thread(() => ProcessNumbers(listeler[0], asalSayilar, ciftSayilar, tekSayilar));
        Thread thread2 = new Thread(() => ProcessNumbers(listeler[1], asalSayilar, ciftSayilar, tekSayilar));
        Thread thread3 = new Thread(() => ProcessNumbers(listeler[2], asalSayilar, ciftSayilar, tekSayilar));
        Thread thread4 = new Thread(() => ProcessNumbers(listeler[3], asalSayilar, ciftSayilar, tekSayilar));

        // Thread'leri başlatıyoruz.
        thread1.Start();
        thread2.Start();
        thread3.Start();
        thread4.Start();

        // Thread'lerin tamamlanmasını beklemek için bekleyici kullanıyoruz.
        thread1.Join();
        thread2.Join();
        thread3.Join();
        thread4.Join();

        // Listelerdeki sayıları düzenli gözükmesi için küçükten büyüğe sıralıyoruz.
        List<int> siraliAsalSayilarListesi = asalSayilar.ToList();
        siraliAsalSayilarListesi.Sort();

        List<int> siraliTekSayilarListesi = tekSayilar.ToList();
        siraliTekSayilarListesi.Sort();

        List<int> siraliCiftSayilarListesi = ciftSayilar.ToList();
        siraliCiftSayilarListesi.Sort();

        // Sonuçları ekrana yazdırıyoruz.
        Console.WriteLine("Asal Sayılar: " + string.Join(", ", siraliAsalSayilarListesi) + "\n");
        Console.WriteLine("Çift Sayılar: " + string.Join(", ", siraliCiftSayilarListesi) + "\n");
        Console.WriteLine("Tek Sayılar: " + string.Join(", ", siraliTekSayilarListesi) + "\n");

    }

    // Sayıları işleyip belirli kriterlere göre farklı listelere ekleyerek sayıları sınıflandırıyoruz.
    static void ProcessNumbers(List<int> sayilar, ConcurrentBag<int> asalSayilar, ConcurrentBag<int> ciftSayilar, ConcurrentBag<int> tekSayilar)
    {
        foreach (var item in sayilar)
        {
            if (IsPrime(item))
            {
                asalSayilar.Add(item);
            }

            if (item % 2 == 0)
            {
                ciftSayilar.Add(item);
            }
            else
            {
                tekSayilar.Add(item);
            }
        }
    }

    // Asal Sayıları bu fonksiyonda kontrol ediyoruz.
    static bool IsPrime(int sayi)
    {
        if (sayi < 2)
            return false;

        for (int i = 2; i <= Math.Sqrt(sayi); i++)
        {
            if (sayi % i == 0)
                return false;
        }

        return true;
    }
}