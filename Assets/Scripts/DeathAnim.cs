using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAnim : MonoBehaviour
{
    [SerializeField] private HealthSystem targetHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private string sceneOnDeath;
    
    private void OnEnable()
    {
        targetHealth.OnHealthChanged += OnDeath;
    }

    private void OnDisable()
    {
        targetHealth.OnHealthChanged -= OnDeath;
    }

    void OnDeath(float current, float max)
    {
        if (current <= 0)
        {
            // TODO: set animator to dead
            animator?.SetBool("IsDead", true);
            if (sceneOnDeath != null)
            {
                SceneManager.LoadScene(sceneOnDeath);
            }
        }
    }
}
