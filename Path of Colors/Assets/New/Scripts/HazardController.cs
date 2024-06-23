using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for environmental hazards that cause harm to characters
/// Çevresel tehlikelerin karakterlere zarar vermesi için kullanılır.
/// </summary>
public class HazardController : MonoBehaviour
{

    public float damage = 1f; // Verilen hasar miktarı
    public float knockbackForce; // Geri itme kuvveti
    public float stunDuration; // Sersemletme süresi
    public float invulnerableDuration; // Karakterin zarar görmez hale geldiği süre
    public bool airStagger; // Havada sersemletme etkin mi?
    public bool playerOnly; // Sadece oyuncuya mı etki edecek?
    public bool softRespawn; // Yumuşak yeniden doğma etkin mi?
    public bool instantKill; // Anında öldürme etkin mi?

    private PhysicsConfig pConfig; // Fizik konfigürasyonu

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// Bir script etkinleştirildiğinde, Update metodlarının ilk defa çağrılmasından hemen önce çağrılır.
    /// </summary>
    void Start()
    {
        pConfig = GameObject.FindObjectOfType<PhysicsConfig>(); // Fizik konfigürasyonunu sahnede bul
        if (!pConfig)
        { // Eğer fizik konfigürasyonu bulunamazsa
            pConfig = (PhysicsConfig)new GameObject().AddComponent(typeof(PhysicsConfig)); // Yeni bir fizik konfigürasyonu oluştur
            pConfig.gameObject.name = "Physics Config"; // Oluşturulan nesneye isim ver
            Debug.LogWarning("PhysicsConfig not found on the scene! Using default config."); // Uyarı mesajı göster
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// Bu nesneye bağlı bir tetikleyiciye başka bir nesne girdiğinde gönderilir (sadece 2D fizik).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.
    /// Bu çarpışmaya dahil olan diğer Collider2D.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        // Eğer sadece oyuncuya etki edecekse ve çarpışan nesne karakter maskesine sahip değilse
        if (playerOnly && pConfig.characterMask != LayerMask.GetMask(LayerMask.LayerToName(other.gameObject.layer)))
        {
            return; // İşlemi sonlandır
        }

        CharacterController2D character = other.GetComponent<CharacterController2D>(); // Çarpışan nesnenin karakter olup olmadığını kontrol et
        if (character && !character.Invulnerable)
        { // Eğer karakterse ve invulnerable değilse
            Health health = other.GetComponent<Health>(); // Çarpışan nesnenin sağlık bileşenini al
            if (health)
            {
                health.TakeDamage(damage); // Sağlık bileşenine hasar uygula
            }
            if (knockbackForce > 0)
            { // Eğer geri itme kuvveti belirlenmişse
                Vector2 force = character.TotalSpeed.normalized * -1 * knockbackForce; // Geri itme kuvvetini hesapla
                character.Knockback(force, stunDuration); // Karakteri geri it ve sersemlet
            }
            if (invulnerableDuration > 0)
            { // Eğer invulnerable süresi belirlenmişse
                character.setInvunerable(invulnerableDuration); // Karakteri invulnerable yap
            }
            if (airStagger)
            { // Eğer havada sersemletme etkinse
                character.SetAirStagger(stunDuration); // Karakteri havada sersemlet
            }
            PlayerControllers player = other.GetComponent<PlayerControllers>(); // Çarpışan nesnenin oyuncu olup olmadığını kontrol et
            if (softRespawn && player)
            { // Eğer yumuşak yeniden doğma etkinse ve çarpışan nesne oyuncuysa
                player.SoftRespawn(); // Oyuncuyu yumuşak yeniden doğur
            }
        }
    }
}
