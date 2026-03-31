using System;
using System.Collections.Generic;

// 💡 İPUCU (VS SNIPPET KISAYOLU): "class" yazıp İKİ KERE TAB tuşuna basarsan "public class İsim { }" iskeletini anında fırlatır!
// Player sınıfı hem Saldırgan (IAttacker) hem de Hasar Alabilen (IDamageable) kurallarına uyuyor.
public class Player : IAttacker, IDamageable
{
    // Hoca get; set; göstermediyse demek ki sadece normal DEĞİŞKEN (Field) kullanacaksınız. 
    // Tüm get; set; maskelerini kaldırdık! Artık en ilkel ve sade halindeler.
    public string Name;
    public int HP;
    public int AttackPower;
    
    // Kalkanın açık olup olmadığını anlayan anahtar (Varsayılan olarak false'dur)
    public bool isDefending;

    // GERÇEK LİSTE (ENVERTAR) SİSTEMİ 🎒
    // Yeni isteğin üzerine çantayı artık List<string> koleksiyonuna dönüştürdük!
    public List<string> Inventory = new List<string>();

    private int maxHP;

    // CONSTRUCTOR (YAPICI METOT)
    // 💡 İPUCU (VS SNIPPET KISAYOLU): Sınıfın içine "ctor" yazıp İKİ KERE TAB'a basarsan
    // "public Player() { }" şeklinde yapıcı (kurucu) iskeletini otomatik fırlatır! Tek tek elle yazmaya yer yok.
    public Player(string name, int hp, int attackPower)
    {
        Name = name; 
        HP = hp;
        maxHP = hp; 
        AttackPower = attackPower;
        
        // Çantayı oyun başlarken eşyalarla doldur (For döngüsü kısaltması)
        for (int i = 0; i < 5; i++) { Inventory.Add("normal"); }
        for (int i = 0; i < 2; i++) { Inventory.Add("super"); }
        
        // Alan Hasarı İksiri eklendi (Senin isteğin)
        Inventory.Add("attack"); 
    }

    // IAttacker Sözleşmesinin Zorunlu Metodu
    public void Attack(IDamageable target)
    {
        // 💡 İPUCU (VS SNIPPET KISAYOLU): Ekrana yazdırmak için tek tek Console... yazmak ameleliktir. "cw" yazıp İKİ KERE TAB'a bas!
        // Artık IDamageable'ın içinde "Name" kuralı olmadığı için isim yazamıyoruz, "hedefe" dedik.
        Console.WriteLine($"{Name}, seçtiği hedefe güçlü bir şekilde saldırdı!");
        
        // Hedefe hasarı yolladık.
        target.TakeDamage(AttackPower); 
    }

    // IDamageable Sözleşmesinin Zorunlu Metodu
    public void TakeDamage(int damage)
    {
        // Eğer Defend tuşuna basılıp kalkan anahtarı açıldıysa
        if (isDefending)
        {
            damage = damage / 2; // Hasarı yarıya (matematikte) düşür
            Console.WriteLine($"{Name} KALKANLA hasarın yarısını blokladı!");
            isDefending = false; // Darbeyi emdikten sonra kalkanı tekrar kapat
        }

        HP -= damage; 
        Console.WriteLine($"{Name} {damage} hasar aldı! Kalan HP: {HP}");
    }

    // 💣 ALAN HASARI İÇİN İKSİR İÇME MANTIĞI GÜNCELLENDİ (Arena listesini içeri alıyoruz)
    public void UseItem(string potionType, List<Enemy> arenaDusmanlari)
    {
        // Öyle bir eşya Çantada cidden var mı?
        if (Inventory.Contains(potionType) == false)
        {
            Console.WriteLine("Çantanda bu eşyadan kalmamış! (Sıranı heba ettin)");
            return; // Metodu burada kes!
        }

        if (potionType == "normal")
        {
            int healAmount = (int)(maxHP * 0.30); 
            HP += healAmount; 
            if (HP > maxHP) 
            {
                HP = maxHP;
            }

            Inventory.Remove("normal"); // İçtiğimiz şişeyi Listededen 1 kere sildik
            Console.WriteLine($"{Name} Normal İksir içti! (+{healAmount} HP).");
        }
        else if (potionType == "super")
        {
            HP = maxHP; 
            Inventory.Remove("super"); // Süper şişeyi Liseden 1 kere sildik
            Console.WriteLine($"{Name} SÜPER İksir içti! Can TAMAMEN yenilendi!");
        }
        else if (potionType == "attack")
        {
            Console.WriteLine($"{Name} YERE ZEHİRLİ BİR BOMBA ATTI (Bölgesel Alan Hasarı - Herkese 20 Vurur)!!");
            Inventory.Remove("attack"); // Bombayı sepetten düş
            
            // Foreach ile arenadaki TÜM canlı düşmanlara aynı hasarı otomatik saniye saniye yedir!
            foreach (var enemy in arenaDusmanlari)
            {
                enemy.TakeDamage(20);
            }
        }
        else 
        {
            Console.WriteLine("Böyle bir eşya tanımlı değil!");
        }
    }

    public void Defend()
    {
        isDefending = true; // Kalkanı Aktif Et!
        Console.WriteLine($"{Name} kalkanını kaldırdı ve savunma pozisyonuna geçti!");
    }
}
