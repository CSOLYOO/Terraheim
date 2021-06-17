﻿using Jotunn.Managers;
using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects
{
    public class SE_Adrenaline : StatusEffect
    {
        private bool m_hasTriggered = false;
        private float m_moveSpeed = 0f;
        private float m_attackSpeed = 0f;

        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Adrenaline";
            base.name = "Adrenaline";
            m_tooltip = "";
        }

        public override void Setup(Character character)
        {
            m_attackSpeed = (float)Terraheim.balance["boonSettings"]["adrenaline"]["attackspeed"];
            m_moveSpeed = (float)Terraheim.balance["boonSettings"]["adrenaline"]["movespeed"];
            m_icon = AssetHelper.SpriteChosenSlaaneshBoon;
            Log.LogWarning("Adding Adrenaline");
            base.Setup(character);
        }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_time >= TTL - 0.1f && !m_hasTriggered)
            {
                SEMan seman = m_character.GetSEMan();
                if (seman.HaveStatusEffect("Chosen"))
                    (seman.GetStatusEffect("Chosen") as SE_Chosen).m_currentBoons.Remove(m_name);
            }
            base.UpdateStatusEffect(dt);
        }

        public void SetTTL(float newTTL)
        {
            TTL = newTTL;
            m_time = 0;
        }

        public void IncreaseTTL(float increase)
        {
            TTL += increase;
            m_time -= increase;
        }

        public float GetAttackSpeed() { return m_attackSpeed; }

        public override void ModifySpeed(ref float speed)
        {
            speed *= 1 + m_moveSpeed;
            base.ModifySpeed(ref speed);
        }
    }
}
