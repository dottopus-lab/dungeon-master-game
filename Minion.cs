using System;

// MİRAS ALMA (INHERITANCE): İki nokta (:) kullanarak Minion'un aslında bir Enemy olduğunu söylüyoruz.
// Bu sayede Minion'un içine tekrar tekrar "string Name, Attack() TakeDamage()" yazmıyoruz. 
// Klasik genetiği Enemy'den çekiyor.
public class Minion : Enemy
{
    // YAPICI METOT (Constructor) -> "ctor" + TAB + TAB
    public Minion(string name, int hp, int attackPower)
    {
        Name = name; // Name özelliğini Enemy'den miras olarak beleş kullandık
        HP = hp;
        AttackPower = attackPower;
    }

    // Minion'a özel, babasında (Enemy'de) olmayan ekstra özellik (Hocanın kuralı)
    public void Roar()
    {
        Console.WriteLine($"{Name} korkutucu bir şekilde kükredi ama kafası karıştı, hiçbir şey yapamadı!");
    }
}
