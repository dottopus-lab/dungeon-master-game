// INTERFACE (ARAYÜZ): Sınıflar için bir "sözleşme" veya "kurallar bütünüdür".
// Eğer Hoca derste "get; set;" göstermediyse, arayüzlerin içine asla Değişken Tipi yazılamaz.
// Bu yüzden Arayüzleri sadece Eylemler (Metotlar) üzerinden sınırlandırırız.

public interface IAttacker
{
    // KURAL: Saldırganın "Attack" adında bir yeteneği olmalı.
    void Attack(IDamageable target);
}

public interface IDamageable
{
    // KURAL: Hasar alabilen herkesin "TakeDamage" adında bir metodu olmalı.
    void TakeDamage(int damage);
}
