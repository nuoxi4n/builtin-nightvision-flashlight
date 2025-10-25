using Cysharp.Threading.Tasks;
using Duckov.UI.DialogueBubbles;
using ItemStatsSystem;
using UnityEngine;

namespace builtin_nightvision_flashlight
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private const string NightVisionTypeStatName = "NightVisionType";

        void Awake()
        {
            Debug.Log("builtin_nightvision_flashlight Loaded!!!");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                ToggleFlashlight();
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                ToggleNightVision();
            }
        }
        void OnDestroy()
        {
            ResetNightVisionState();
        }
        void OnEnable()
        {
        }
        void OnDisable()
        {
            ResetNightVisionState();
        }
        private void ToggleFlashlight()
        {
            var character = CharacterMainControl.Main;
            DialogueBubblesManager.Show("暂时没有这个功能", character.transform,
                yOffset: 1f,
                needInteraction: false,
                skippable: true,
                speed: -1f,
                duration: 2f).Forget();
        }
        // 开关夜视仪
        private void ToggleNightVision()
        {
            var character = CharacterMainControl.Main;
            if (character == null || character.CharacterItem == null) return;

            Stat nightVisionStat = character.CharacterItem.GetStat(NightVisionTypeStatName);
            if (nightVisionStat != null)
            {
                float currentValue = nightVisionStat.BaseValue;
                float newValue = (currentValue + 1) % 3;
                nightVisionStat.BaseValue = newValue;

                Debug.Log($"Night Vision Mode: {newValue}");
                string message = "";
                switch (newValue)
                {
                    case 0: message = "关闭夜视仪"; break;
                    case 1: message = "开启夜视仪"; break;
                    case 2: message = "开启热成像"; break;
                }

                if (!string.IsNullOrEmpty(message))
                {
                    DialogueBubblesManager.Show(message, character.transform,
                        yOffset: 1f,
                        needInteraction: false,
                        skippable: true,
                        speed: -1f,
                        duration: 2f).Forget();
                }

                if (GameManager.NightVision != null)
                {
                    GameManager.NightVision.Refresh();
                }
            }
        }
        // 重置夜视仪
        private void ResetNightVisionState()
        {
            var character = CharacterMainControl.Main;
            if (character != null && character.CharacterItem != null)
            {
                Stat nightVisionStat = character.CharacterItem.GetStat(NightVisionTypeStatName);
                if (nightVisionStat != null)
                {
                    nightVisionStat.BaseValue = 0;
                    Debug.Log("Night vision state reset to off on mod disable.");
                }
            }

            if (GameManager.NightVision != null)
            {
                GameManager.NightVision.Refresh();
            }
        }
    }
}