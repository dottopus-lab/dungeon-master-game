using System;

// Boss da Enemy sınıfından miras alıyor.
public class Boss : Enemy
{
    // "ctor" + TAB + TAB kısa yolu
    public Boss(string name, int hp, int attackPower)
    {
        Name = name;
        HP = hp;
        AttackPower = attackPower;
    }

    // Boss'a özel (Hocanın kuralı: Rastgele birini iyileştirebilir)
    // Sınavda Interface içinde HP kuralı (get; set;) koymadığımız için buraya IDamageable yerine Enemy yazdık.
    public void HealRandomTarget(Enemy target)
    {
        Console.WriteLine($"{Name} garip bir büyü yaptı ve hedefini (+20 HP) iyileştirdi!");
        target.HP += 20; 
    }

    // Boss'a özel (Hocanın kuralı: Çıldırıp kendi adamına (Müttefik-Ally) vurabilir)
    public void HitAlly(Enemy ally)
    {
        Console.WriteLine($"{Name} GÖZÜNÜ KAN BÜRÜDÜ ve kendi arkadaşı {ally.Name}'i parçaladı!");
        ally.TakeDamage(AttackPower);
    }
}
