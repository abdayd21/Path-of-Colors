using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlatformController sınıfı, bir platformun hareket ve çökme davranışlarını kontrol eder
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class PlatformController : MonoBehaviour {

    public PlatformWaypoint currentWaypoint; // Platformun şu anki hedef noktasını belirler
    public float maxSpeed; // Platformun maksimum hızı
    public float accelerationDistance; // Platformun hızlanmaya başladığı mesafe
    public float decelerationDistance; // Platformun yavaşlamaya başladığı mesafe
    public float waitTime; // Platformun hedef noktada beklediği süre
    public float crumbleTime; // Platformun çökme süresi
    public float restoreTime; // Platformun yeniden oluşma süresi
    public bool onlyPlayerCrumble; // Sadece oyuncunun platformu çökertip çökertemeyeceğini belirler

    [SerializeField]
    private Vector2 speed = Vector2.zero; // Platformun hızı
    private float currentWaitTime = 0; // Platformun beklediği süreyi izlemek için
    private float currentCrumbleTime = 0; // Platformun çökme süresini izlemek için
    private float currentRestoreTime = 0; // Platformun yeniden oluşma süresini izlemek için
    private bool crumbled = false; // Platformun çökmüş olup olmadığını izlemek için
    private List<ObjectController2D> objs = new List<ObjectController2D>(); // Platformla temas eden objelerin listesi
    private Animator animator; // Platformun animatör bileşeni
    private Collider2D myCollider; // Platformun collider bileşeni
    private PhysicsConfig pConfig; // Fizik konfigürasyonları

    private static readonly string ANIMATION_CRUMBLING = "crumbling"; // Çökme animasyonu tetikleyicisi
    private static readonly string ANIMATION_CRUMBLE = "crumble"; // Çökme animasyonu tetikleyicisi
    private static readonly string ANIMATION_RESTORE = "restore"; // Yeniden oluşma animasyonu tetikleyicisi

    // Start metodu, oyun nesnesi etkinleştirildiğinde bir kez çağrılır.
    void Start() {
        animator = GetComponent<Animator>(); // Animator bileşenini alır
        myCollider = GetComponent<Collider2D>(); // Collider2D bileşenini alır
        pConfig = GameObject.FindObjectOfType<PhysicsConfig>(); // Scene'deki PhysicsConfig nesnesini bulur
        if (!pConfig) {
            // Eğer PhysicsConfig nesnesi yoksa yeni bir tane oluşturur ve uyarı mesajı verir.
            pConfig = (PhysicsConfig) new GameObject().AddComponent(typeof(PhysicsConfig));
            pConfig.gameObject.name = "Physics Config";
            Debug.LogWarning("PhysicsConfig not found on the scene! Using default config.");
        }
    }

    // FixedUpdate metodu, belirli zaman aralıklarıyla sürekli olarak çağrılır. Fizik işlemleri burada yapılır.
    void FixedUpdate() {
        if (crumbled) {
            if (currentRestoreTime > 0) {
                currentRestoreTime -= Time.fixedDeltaTime; // Restore zamanını azaltır
                if (currentRestoreTime <= 0) {
                    Restore(); // Zaman dolduğunda platformu yeniden oluşturur
                }
            }
        } else {
            if (currentCrumbleTime > 0) {
                currentCrumbleTime -= Time.fixedDeltaTime; // Çökme zamanını azaltır
                if (currentCrumbleTime <= 0) {
                    crumbled = true;
                    animator.SetTrigger(ANIMATION_CRUMBLE); // Çökme animasyonunu tetikler
                    myCollider.enabled = false; // Collider'ı devre dışı bırakır
                    if (restoreTime > 0) {
                        currentRestoreTime = restoreTime; // Yeniden oluşma zamanını ayarlar
                    }
                }
            }
            if (currentWaypoint) {
                if (currentWaitTime > 0) {
                    currentWaitTime -= Time.fixedDeltaTime; // Bekleme zamanını azaltır
                    return;
                }
                Vector2 distance = currentWaypoint.transform.position - transform.position; // Hedef noktaya olan mesafeyi hesaplar
                if (distance.magnitude <= decelerationDistance) {
                    if (distance.magnitude > 0) {
                        speed -= Time.fixedDeltaTime * distance.normalized * maxSpeed * maxSpeed / (2 * decelerationDistance); // Yavaşlama hesaplamaları
                    } else {
                        speed = Vector2.zero; // Mesafe sıfır olduğunda hızı sıfırlar
                    }
                } else if (speed.magnitude < maxSpeed) {
                    if (accelerationDistance > 0) {
                        speed += Time.fixedDeltaTime * distance.normalized * maxSpeed * maxSpeed / (2 * accelerationDistance); // Hızlanma hesaplamaları
                    }
                    if (speed.magnitude > maxSpeed || accelerationDistance <= 0) {
                        speed = distance.normalized * maxSpeed; // Maksimum hız ayarlamaları
                    }
                }
                Vector3 newPos = Vector2.MoveTowards(transform.position, currentWaypoint.transform.position, speed.magnitude * Time.fixedDeltaTime); // Yeni pozisyonu hesaplar
                Vector2 velocity = newPos - transform.position;
                if (speed.y > 0) {
                    MoveObjects(velocity); // Objeleri hareket ettirir
                    transform.position = newPos; // Platformun yeni pozisyonunu ayarlar
                } else {
                    transform.position = newPos; // Platformun yeni pozisyonunu ayarlar
                    MoveObjects(velocity); // Objeleri hareket ettirir
                }
                distance = currentWaypoint.transform.position - transform.position;
                if (distance.magnitude < 0.00001f) {
                    speed = Vector2.zero; // Hız sıfırlanır
                    currentWaypoint = currentWaypoint.nextWaipoint; // Bir sonraki hedef noktaya geçer
                    currentWaitTime = waitTime; // Bekleme zamanını ayarlar
                }
            }
        }
    }

    // Objeleri platformun hızında hareket ettirir
    private void MoveObjects(Vector2 velocity) {
        foreach (ObjectController2D obj in objs) {
            obj.Move(velocity);
        }
    }

    // Başka bir nesne platforma temas ettiğinde çağrılır
    void OnTriggerEnter2D(Collider2D other) {
        AttachObject(other);
    }

    // Başka bir nesne platformla temas halindeyken sürekli çağrılır
    void OnTriggerStay2D(Collider2D other) {
        AttachObject(other);
    }

    // Nesneyi platforma bağlamaya çalışır
    private void AttachObject(Collider2D other) {
        if (crumbled) {
            return;
        }
        ObjectController2D obj = other.GetComponent<ObjectController2D>();
        if (obj && !objs.Contains(obj)) {
            // Platformun bir yönlü olup olmadığını ve nesnenin altında olup olmadığını kontrol eder
            if (pConfig.owPlatformMask == (pConfig.owPlatformMask | (1 << gameObject.layer)) &&
                (obj.transform.position.y < transform.position.y || obj.TotalSpeed.y > 0)) {
                return;
            } else {
                objs.Add(obj); // Nesneyi listeye ekler
                if (crumbleTime > 0 && currentCrumbleTime <= 0) {
                    if (!onlyPlayerCrumble || obj.GetComponent<PlayerController>()) {
                        currentCrumbleTime = crumbleTime; // Çökme zamanını ayarlar
                        animator.SetTrigger(ANIMATION_CRUMBLING); // Çökme animasyonunu tetikler
                    }
                }
            }
        }
    }

    // Başka bir nesne platformdan ayrıldığında çağrılır
    void OnTriggerExit2D(Collider2D other) {
        ObjectController2D obj = other.GetComponent<ObjectController2D>();
        if (obj && objs.Contains(obj)) {
            objs.Remove(obj); // Nesneyi listeden çıkarır
            obj.ApplyForce(speed); // Nesneye hız uygular
        }
    }

    // Platformu yeniden oluşturur
    public void Restore() {
        crumbled = false; // Çökme durumunu sıfırlar
        myCollider.enabled = true; // Collider'ı etkinleştirir
        animator.SetTrigger(ANIMATION_RESTORE); // Yeniden oluşma animasyonunu tetikler
    }
}