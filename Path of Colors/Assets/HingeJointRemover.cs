using UnityEngine;

public class HingeJointRemover : MonoBehaviour
{
    // Kutuya temas eden bir obje ile çarpışma olduğunda çağrılır
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Temas eden objenin layer'ını kontrol et
        if (collision.gameObject.layer == LayerMask.NameToLayer("Kapak"))
        {
            // Bu GameObject'teki HingeJoint2D bileşenini al
            HingeJoint2D hingeJoint = GetComponent<HingeJoint2D>();

            // HingeJoint2D bileşeni varsa, kaldır
            if (hingeJoint != null)
            {
                Destroy(hingeJoint);
            }
        }
    }
}
