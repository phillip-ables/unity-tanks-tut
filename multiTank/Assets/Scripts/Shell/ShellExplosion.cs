using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] _colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        for(int i = 0; i < _colliders.Length; i++)
        {
            Rigidbody _targetRigidBody = _colliders[i].GetComponent<Rigidbody>();
            if (!_targetRigidBody)
                continue;
            _targetRigidBody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            TankHealth _targetHealth = _targetRigidBody.GetComponent<TankHealth>();
            if (!_targetHealth)
                continue;
            float _damage = CalculateDamage(_targetRigidBody.position);
            _targetHealth.TakeDamage(_damage);
        }

        m_ExplosionParticles.transform.parent = null;
        m_ExplosionParticles.Play();

        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 _explosionToTarget = targetPosition - transform.position;

        float _explosionDistance = _explosionToTarget.magnitude;

        float _relativeDistance = (m_ExplosionRadius - _explosionDistance) / m_ExplosionRadius;

        float _damage = _relativeDistance * m_MaxDamage;
        _damage = Mathf.Max(0f, _damage);
        return _damage;
    }
}