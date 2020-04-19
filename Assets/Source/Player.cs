using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Player
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        public CharacterController PlayerCharacterController { get; private set; }

        public Transform Hand;
        public Baloon Baloon;

        public List<ActiveBonus> Bonuses = new List<ActiveBonus>();

        public void Start()
        {
            PlayerCharacterController = GetComponent<CharacterController>();
        }

        public void Update()
        {
            UpdateActiveBonuses();
        }

        private void UpdateActiveBonuses()
        {
            List<ActiveBonus> bonusesToRemove = new List<ActiveBonus>();
            foreach (ActiveBonus bonus in Bonuses)
            {
                bonus.Duration -= Time.deltaTime;
                if (bonus.Duration < 0)
                {
                    bonusesToRemove.Add(bonus);
                }
            }

            foreach (ActiveBonus bonus in bonusesToRemove)
            {
                RemoveBonus(bonus);
                Bonuses.Remove(bonus);
            }
        }

        public void ApplyBonus(ActiveBonus bonus)
        {
            switch (bonus.BonusType)
            {
                case BonusType.BaloonSizeDown:
                    Baloon.transform.localScale *= 0.4F;
                    break;
                case BonusType.BaloonSizeUp:
                    Baloon.transform.localScale *= 1.5F;
                    break;
                case BonusType.Armour:
                    Baloon.ArmourUp();
                    break;
                case BonusType.Bomb:
                    foreach (Enemy enemy in FindObjectsOfType<Enemy>())
                    {
                        enemy.Die(false);
                    }
                    Camera.main.GetComponent<CameraController>().Shake(0.6F);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RemoveBonus(ActiveBonus bonus)
        {
            switch (bonus.BonusType)
            {
                case BonusType.BaloonSizeDown:
                    Baloon.transform.localScale /= 0.4F;
                    break;
                case BonusType.BaloonSizeUp:
                    Baloon.transform.localScale /= 1.5F;
                    break;
                case BonusType.Armour:
                    Baloon.ToNormal();
                    break;
                case BonusType.Bomb:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void AddBonus(ActiveBonus bonus)
        {
            ActiveBonus oldBonus;
            if ((oldBonus = Bonuses.FirstOrDefault(b => b.BonusType == bonus.BonusType)) != null)
            {
                oldBonus.Duration = oldBonus.MaxDuration;
                return;
            }

            Bonuses.Add(bonus);
            ApplyBonus(bonus);
        }

        public void Dead()
        {
            PlayerCharacterController.AnimateDead();
        }
    }
}
