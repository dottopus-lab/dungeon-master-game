using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    // NEDEN "static void Main(string[] args)" YAZIYORUZ?
    // Main: Bilgisayarın (Windows/Mac) programı okumaya başladığı ve içine girdiği BAŞLAMA NOKTASIDIR.
    // 💡 İPUCU (VS KISAYOLU): Sınıfın içine "svm" yazıp İKİ KERE TAB'a basarak otomatik Main metodunu oluşturabilirsin.
    static void Main(string[] args)
    {
        // Türü (Player) olan ve "player" isminde yeni bir Kahraman isimli nesne canlandırıyoruz.
        // Canı 100, Saldırı Gücü 15.
        Player player = new Player("Kahraman", 100, 15);
        
        // C#'da birden fazla karakteri yan yana dizmek için (Vagonlara bindirmek için) List kullanılır.
        // Yeni C# sürümünde "new List<Enemy>()" yazmak yerine en sona sadece "new()" yazman yeterlidir!
        List<Enemy> enemies = new List<Enemy>
        {
            // Vagonun 0. sırasına bir minyon koyduk
            new Minion("Goblin1", 30, 5),   
            // Vagonun 1. sırasına ikinci minyonu koyduk
            new Minion("Goblin2", 30, 5),   
            // Vagonun 2. sırasına Boss'u (Patronu) koyduk
            new Boss("Kral_Ejderha", 150, 20) 
        };

        // Oyunun kaçıncı turda olduğunu sayacak bir tamsayı (int) atıyoruz.
        int turnCount = 0;
        
        // 💡 İPUCU: "cw" yazıp İKİ KERE TAB tuşuna basarak Console.WriteLine() komutunu otomatik yazdırabilirsin!
        Console.WriteLine("=== SAVAŞ BAŞLADI ===");

        // 💡 İPUCU: "while" yazıp İKİ KERE TAB tuşuna basarak döngü iskeletini oluşturabilirsin!
        // WHILE DÖNGÜSÜ KURALI: Kahraman yaşıyorken (HP'si 0'dan büyük) VE Karşıda düşman vagonu (Count) 0'dan çokken dön dur!
        while (player.HP > 0 && enemies.Count > 0)
        {
            // Yeni turun başında Kahramanın eski kalkanı parçalanır ve süresi dolar!
            player.isDefending = false;

            // Tur sayacını her döngünün başında 1 arttırır. (turnCount = turnCount + 1; demekle aynıdır)
            turnCount++;
            
            // 🚨 HORDE (DALGA/YIĞIN) SİSTEMİ: Belirli turlarda Listeye (.Add) yeni düşmanlar ekler!
            if (turnCount == 3)
            {
                Console.WriteLine("\n🚨 HORDE: Ormanın derinliklerinden 'Geciken_Goblin' koşa koşa savaşa katıldı! 🚨");
                // Listeye yeni eleman eklemek işte bu kadar kolaydır:
                enemies.Add(new Minion("Geciken_Goblin", 30, 5));
            }
            else if (turnCount == 6)
            {
                Console.WriteLine("\n🚨 KABUS HORDE: Kan kokusunu alan 'Şövalye_Boss' arenaya iniş yaptı! 🚨");
                enemies.Add(new Boss("Şövalye_Boss", 100, 15));
            }

            // Dolar işareti ($), yazdığımız yazının içine süslü parantez {} açıp değişken sayılarını eklememizi sağlar.
            Console.WriteLine($"\n--- TUR {turnCount} ---");
            Console.WriteLine($"Senin Canın: {player.HP} | Kalan Düşman: {enemies.Count}");
            
            // Oyuncunun göreceği ana 1-2-3 Seçenekli Menü yazıları
            Console.WriteLine("Ne yapmak istersin?");
            Console.WriteLine("1 - Saldır (Attack)");
            Console.WriteLine("2 - Savun (Defend)");
            Console.WriteLine("3 - Eşya Kullan (Item)");
            
            // 💡 İPUCU: "try" yazıp İKİ KERE TAB tuşuna basarak Try-Catch (Hata Önleme Odası) iskeletini anında kurabilirsin!
            // Kullanıcıların kasten yanlış harfe/tuşa basıp oyunu çökertmesini önlemek için kodumuzu bu odanın (try) içine alıyoruz.
            try
            {
                // Console.ReadLine() ekranda imleci durdurup senin klavyeden bişey yazıp Enter'a basmanı bekler.
                // Oyuncu "1" veya başka bir şey yazınca bunu eylemSecim değişkeninin (Kutucuğunun) içine koyar.
                string eylemSecim = Console.ReadLine(); 

                // 💡 İPUCU: "sw" yazıp İKİ KERE TAB tuşuna basarak Switch-Case iskeletini anında kurabilirsin! 
                // İf-Else komutuna göre Menü (Karar Ağaçları) yapmak için çok daha kolaydır.
                switch (eylemSecim)
                {
                    // Oyuncu menüden 1'i seçerse (Saldırı Emri):
                    case "1":
                        Console.WriteLine("\nKime saldıracaksın?");
                        
                        // 🌟 YENİ DİNAMİK HORDE (DALGA) MENÜSÜ 🌟
                        // Oyuna sonradan 4. 5. veya 100. düşman katılsa bile For döngüsü hepsini otomatik numaralandırır!
                        for (int i = 0; i < enemies.Count; i++)
                        {
                            // Eğer yaşıyorsa ekranda Sıra Numarasıyla beraber göster (Bilgisayar 0'dan başlar o yüzden i + 1 dedik)
                            if (enemies[i].HP > 0) 
                            {
                                Console.WriteLine($"{i + 1} - {enemies[i].Name} (HP: {enemies[i].HP})");
                            }
                        }
                        
                        // "Hangi numaraya saldıracağının" sorusunu Konsoldan bekliyoruz.
                        string hedefSecim = Console.ReadLine(); 

                        // Try-Catch ile "int.Parse" işlemini güvenceye aldık. Adam tuşa yanlış basıp "1a" girerse oyun çökmesin diye.
                        try 
                        {
                            // Çeviri (Parse): Girilen "1" yazısını gerçek matematik 1 sayısına çevirdik
                            // Ve Listeler (Vagonlar) 0'dan başladığı için sayıyı 1 azalttık (Hedef: 0. İndeks = hedefVagon)
                            int hedefVagon = int.Parse(hedefSecim) - 1;

                            // Belirtilen (Örn: 0.) Vagon gerçekten listede var mı? VE Vagonun içindeki adam yaşıyor mu?
                            if (hedefVagon >= 0 && hedefVagon < enemies.Count && enemies[hedefVagon].HP > 0)
                            {
                                player.Attack(enemies[hedefVagon]); // Bulduk, Mıhladık!
                            }
                            else 
                            {
                                Console.WriteLine("Lobi boş veya kılıcı havaya salladın! (Sıranı heba ettin)");
                            }
                        }
                        catch 
                        {
                            Console.WriteLine("Menüyü şaşırdın sadece o adamların rakamına basman lazımdı! (Pas geçtin)");
                        }
                        break; // "case 1" yani Saldırı Komutu tamamen BİTİŞ

                    // Oyuncu ana menüden 2'yi seçerse (Savunma Emri):
                    case "2":
                        // Player içindeki Defend metodunu çalıştır
                        player.Defend();
                        break;

                    // Oyuncu ana menüden 3'ü seçerse (İksir Emri):
                    case "3":
                        Console.WriteLine("Hangi Eşya? (1- Normal, 2- Süper, 3- Alan Hasarı Bombası)");
                        
                        // İksirin numarasını yine konsoldan bir metin (string) olarak bekle
                        string iksirSecim = Console.ReadLine();
                        
                        // Eğer 1 derse Player'ın içine "normal" bilgisini VE arenadaki "düşmanları (enemies)" yolla!
                        if (iksirSecim == "1") 
                        {
                            player.UseItem("normal", enemies);
                        }
                        // Eğer 2 derse "super" bilgisini yolla
                        else if (iksirSecim == "2") 
                        {
                            player.UseItem("super", enemies);
                        }
                        // 🌟 Yeni İstediğin Alan Hasarı Bomba Mantığı
                        else if (iksirSecim == "3") 
                        {
                            player.UseItem("attack", enemies);
                        }
                        else 
                        {
                            Console.WriteLine("Öyle bir iksir çantanda veya menüde yok!");
                        }
                        break;

                    // Oyuncu ana menüde cidden 1, 2, 3 dışında kafasına göre bir tuşa bastıysa:
                    default:
                        Console.WriteLine("Menüde böyle bir numara yok! (Sıranı heba edip pas geçtin)");
                        break;
                }
            }
            // Yukarıdaki "try" odasında yazan kodların bir yerinde patlama / çökme olursa...
            // "catch" yakalayıcısı o hatayı anında havada tutar ("ex" isminde) ve programın camdan atlamasını engeller.
            catch (Exception ex)
            {
                // Hatayı biz dostane bir metne dönüştürüp ekrana basarız, böylece oyun kaldığı yerden oynamaya devam eder.
                Console.WriteLine("Klavyeden hatalı bir tuş veya metin girdiniz (Exception fırlattı): " + ex.Message);
            }

            // ÖLEN DÜŞMANLARI LİSTEDEN SİLME (FOR DÖNGÜSÜ)
            // 💡 İPUCU: "for" yazıp İKİ KERE TAB basarsan standart for iskeletini yazar!
            // i-- dememizin (Geri Geri saymamızın) çok kritik bir sebebi vardır: 1. vagonu silersen kalan herkes başa 1 vagon kayar.
            // İndexler kayıp hata vermesin diye her zaman sondan başa doğru arama ve silme işlemi yapılır.
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                // Canı 0 ve altıysa silme/kaldırma görevlisi (RemoveAt) metodu ile Onu sistemden yok et!
                if (enemies[i].HP <= 0)
                {
                    Console.WriteLine($"{enemies[i].Name} küle döndü ve listeden silindi!");
                    enemies.RemoveAt(i);
                }
            }

            // Temizlik bitti ama ya herkes öldüyse? Listede kimse (Count == 0) kalmadıysa?
            // "break" komutu döngüyü acımasızca tamamen sonlandırır ve While{} bloğunun en alt satırına indirir kodu.
            if (enemies.Count == 0) 
            {
                break; 
            }

            // ================== DÜŞMANLARIN YAPAY ZEKASI (ZAR ATMA) ==================
            Console.WriteLine("\n-- Düşmanların Sırası --");
            
            // Random: Hocanın verdiği "Şans/Zar (Randomized)" kuralını sağlamak için çalışan şans motorudur.
            Random rnd = new Random();
            
            // FOREACH DÖNGÜSÜ: Canlı kalmayı başarmış bütün düşmanları listeden sırayla alıp birer tur oynatmaya yarar.
            // 💡 İPUCU: "foreach" yazıp İKİ KERE TAB basarsan kendisi bu şablonu şıp diye tamamlar!
            foreach (var enemy in enemies)
            {
                if (enemy.HP > 0) // Eğer hayatta kalmışsa zarlar onun için atılmaya başlasın
                {
                    // Düşmanın sırası geldiğinde geçen tur kaldırdığı ve kırılamamış kalkanının süresi biter!
                    enemy.isDefending = false;

                    // Pattern Matching (Arama): Baktığımız genel düşman aslında "Minion" genetiğinden mi geliyor? (if enemy is Minion)
                    // Eğer Minionsa asilMinion isminde yeni minik geçici bir adama çevir!
                    if (enemy is Minion asilMinion) 
                    {
                        // 1, 2 veya 3 sayısını rastgele tut (.Next 4 dediğimiz zaman 4 hiçbir zaman dahil olmaz)
                        int minionZari = rnd.Next(1, 4); 
                        
                        // Zarına göre saldırt, savundur veya boş komut çalıştırt (Roar)
                        if (minionZari == 1) 
                        {
                            asilMinion.Attack(player);
                        }
                        else if (minionZari == 2) 
                        {
                            asilMinion.Defend();
                        }
                        else 
                        {
                            asilMinion.Roar();
                        }
                    }
                    // Baktığımız düşman "Boss" sınıfından doğmuşsa: Onu geçici asilBoss kelimesine devret
                    else if (enemy is Boss asilBoss) 
                    {
                        // Boss, saldırma zarı, savunma zarı veya efsanevi yetenek zarını (3) atar
                        int bossZari = rnd.Next(1, 4); 
                        
                        if (bossZari == 1) 
                        {
                            asilBoss.Attack(player);
                        }
                        else if (bossZari == 2) 
                        {
                            asilBoss.Defend();
                        }
                        else 
                        {
                            // 3 (Efsanevi) ZAR GELDİYSE: Boss iyileştirme mi yapacak kendi arkadaşına mı vuracak?
                            // Bunun için de kendi içinde havaya yazı tura (1, 2 zarı) atıyor
                            int delirmeZari = rnd.Next(1, 3); // 1 veya 2 zar atılır
                            if (delirmeZari == 1) 
                            {
                                asilBoss.HealRandomTarget(enemy); // 1 gelirse kendisini veya arkadaşlarını iyileştir
                            }
                            else 
                            {
                                // 2 gelirse gözünü kan bürüyüp Kendi Arkadaşına (Ally) vurma ihtimali doğuyor!
                                // enemies.Count > 1 : Arkadaşı var mı diye listede adam sayısı kontrolü. (Tek kalmamış olmalı)
                                // enemies[0] != enemy : İlk bulduğumuz arkadaş bizzat kendisinin aynada yansıması olmasın diye.
                                if (enemies.Count > 1 && enemies[0] != enemy) 
                                {
                                    asilBoss.HitAlly(enemies[0]); 
                                }
                                else 
                                {
                                    asilBoss.Attack(player); // Tura geldi ama arkadaşı kalmamış, şansımıza mecburen bize vurur :)
                                }
                            }
                        }
                    }
                }
            } // Tüm yaşayanların sırası Foreach boyunca tur tur işletilir ve Foreach Biter
        } // Ve oyun böylece 1 elde bir kez dönmüş olur, While Biter. Kahraman ölene veya karşı taraf ölene kadar...

        // --- SAVAŞ BİTİŞ EKRANI VE DOSYA İŞLEMLERİ (FILE IO) ---
        Console.WriteLine("\n=== SAVAŞ BİTTİ ===");
        
        if (player.HP > 0) 
        {
            Console.WriteLine("TEBRİKLER KAHRAMAN, KAZANDIN!");
        }
        else 
        {
            Console.WriteLine("MAALESEF YENİLDİN, GÖZLERİNİ KAPATTIN...");
        }

        // ETAP 4: Hocanın istediği DOSYAYA KAYDETME KISMI (Tekrar Aktif Edildi!)
        try
        {
            string yazilacakMetin = $"Savas Sonu:\nTur Sayisi: {turnCount}\nOyuncunun Kalan Canı: {player.HP}";
            File.WriteAllText("sonuclar.txt", yazilacakMetin); // Oluşturduğumuz yazıyı sonuclar.txt adında bas!
            Console.WriteLine("\nSonuçların 'sonuclar.txt' dosyasina güvenli bir şekilde kaydedildi!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Dosyaya kaydederken disk hatası/izin hatası oluştu: " + ex.Message);
        }

        Console.ReadLine(); // Konsol penceresinin oyun bitince hemen pat diye kapanmasını engellemek için bekletme tuşu
    }
}
