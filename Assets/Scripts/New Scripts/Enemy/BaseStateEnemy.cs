using System.Collections;
using UnityEngine;

namespace Assets.Scripts.New_Scripts
{
    public abstract class BaseStateEnemy : ScriptableObject
    {
        [HideInInspector]public Enemy enemyRef;
        [HideInInspector]public IEnemyStateSwitcher stateSwitcherRef;
        public string nameState;
        protected bool _isActive;
        protected BaseStateEnemy(Enemy enemy, IEnemyStateSwitcher stateSwitcher)
        {
            enemyRef = enemy;
            stateSwitcherRef = stateSwitcher;
        }
        public abstract void Start();
        public abstract void Stop();

        public abstract void MoveEnemy(Vector2 movement);
        public abstract void TakeDamage();
        public abstract void DieEnemy();

        public abstract void RebornEnemy();

        public virtual void SpriteDirection(float horizontalMovement)
        {
            if (horizontalMovement > 0)
            {
                enemyRef.spriteRenderer.flipX = !enemyRef.spriteLookRight;
            }
            else if (horizontalMovement < 0)
            {
                enemyRef.spriteRenderer.flipX = enemyRef.spriteLookRight;
            }
        }

        protected virtual void Move(float horizontalMovement)
        {
            float movementForce = enemyRef.transform.right.x
                * enemyRef.movementSpeed
                * horizontalMovement;
            float horizontalVelocity = movementForce;
            float verticalVelocity = enemyRef.enemyRigidbody.velocity.y;
            enemyRef.enemyRigidbody.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        }

        public IEnumerator TimeOutToState<T>(float timeout) where T : BaseStateEnemy
        {
            float time = 0.0f;
            while (time < timeout)
            {
                yield return null;
                time += Time.deltaTime;
            }
            if(_isActive) stateSwitcherRef.SwitchState<T>();
            yield break;
        }

    }
}