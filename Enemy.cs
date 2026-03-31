using System;

// ABSTRACT SINIF
public abstract class Enemy : IAttacker, IDamageable
{
    // Dümdüz değişkenler (get; set; kaldırıldı)
    public string Name;
    public int HP;
    public int AttackPower;
    public bool isDefending; // Kalkan Anahtarı

    public virtual void Defend() 
    {
        isDefending = true;
        Console.WriteLine($"{Name} standart bir şekilde eğildi ve savunmaya geçti!");
    }

    public void Attack(IDamageable target)
    {
        Console.WriteLine($"{Name}, acımasızca hedefine saldırdı!");
        target.TakeDamage(AttackPower);
    }

    public void TakeDamage(int damage)
    {
        if (isDefending)
        {
            damage = damage / 2;
            Console.WriteLine($"{Name} kalkanıyla hasarın yarısını absorbe etti!");
            isDefending = false; 
        }

        HP -= damage;
        Console.WriteLine($"{Name} {damage} hasar yedi! Kalan Canı: {HP}");
    }
}
