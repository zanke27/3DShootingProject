using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using static Define;

public class BanditAI : MonoBehaviour
{
    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.IDLE;

    public float traceDistance = 10.0f;
    public float attackDistance = 2.0f;

    public bool isDie = false;

    private Transform _enemyTrm;
    private Transform _playerTrm;
    private NavMeshAgent _agent;
    private Animator _animator;

    private readonly int hashPunch = Animator.StringToHash("Punch");
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDeath = Animator.StringToHash("Death");
    private readonly int hashSprint = Animator.StringToHash("Sprint");

    public UnityEvent GetAttack;
    public UnityEvent GetHit;
    public UnityEvent GetDie;

    public GameObject hitEffect;

    public float Speed = 5f;
    public int MaxHp = 100;
    public int Hp = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyTrm = GetComponent<Transform>();
        _playerTrm = Player.GetComponent<Transform>();

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
    }

    private void OnEnable()
    {
        state = State.IDLE;
        Hp = MaxHp;
        isDie = false;
        GetComponent<CapsuleCollider>().enabled = true;
        SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();
        foreach (SphereCollider sphere in spheres)
        {
            sphere.enabled = true;
        }

        StartCoroutine(CheckEnemyState());

        StartCoroutine(EnemyAction());
    }

    private void Update()
    {
        if (isDie) return;
        if (_agent.remainingDistance >= 2.0f)
        {           
            Vector3 dir = _playerTrm.position - transform.position;

            Quaternion rot = Quaternion.LookRotation(dir);

            _enemyTrm.rotation = Quaternion.Slerp(_enemyTrm.rotation, rot, Time.deltaTime * 10);
        }
    }

    public void Attack()
    {
        GetAttack?.Invoke();
    }

    IEnumerator CheckEnemyState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.25f);

            if (state == State.DIE) yield break;

            float distance = Vector3.Distance(_enemyTrm.position, _playerTrm.position);

            if (distance <= attackDistance)
                state = State.ATTACK;
            else if (distance <= traceDistance)
                state = State.TRACE;
            else
                state = State.IDLE;

        }
    }

    IEnumerator EnemyAction()
    {
        while(!isDie)
        {
            switch (state)
            {
                case State.IDLE:
                    _agent.isStopped = true;
                    _animator.SetBool(hashSprint, false);
                    break;
                case State.TRACE:
                    _agent.SetDestination(_playerTrm.position); // 방향
                    _agent.isStopped = false;
                    _animator.SetBool(hashSprint, true);
                    break;
                case State.ATTACK:
                    _animator.SetBool(hashSprint, false);
                    _animator.SetTrigger(hashPunch);
                    break;
                case State.DIE:
                    GetDie?.Invoke();
                    isDie = true;
                    _agent.isStopped = true;
                    _animator.SetTrigger(hashDeath);
                    GetComponent<CapsuleCollider>().enabled = false;
                    SphereCollider[] spheres = GetComponentsInChildren<SphereCollider>();
                    foreach (SphereCollider sphere in spheres)
                        sphere.enabled = false;
                    yield return new WaitForSeconds(3.0f);
                    Destroy(this.gameObject);
                    break;
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Destroy(collision.collider.gameObject);

            _animator.SetTrigger(hashHit);

            Vector3 pos = collision.GetContact(0).point;

            Quaternion rot = Quaternion.LookRotation(-collision.GetContact(0).normal);

            // 이펙트 소환

            GameObject blood = Instantiate(hitEffect, pos, rot, transform);
            Destroy(blood, 1f);

            Hp -= 10;

            GetHit?.Invoke();

            if (Hp <= 0)
            {
                state = State.DIE;
            }
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (gameObject.activeSelf == true)
        {
            if (state == State.TRACE)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(_enemyTrm.position, traceDistance);
            }
            else if (state == State.ATTACK)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_enemyTrm.position, attackDistance);
            }
        }
    }
#endif
}
