using Cysharp.Threading.Tasks;
using Duckov.UI;
using Duckov.UI.DialogueBubbles;
using Duckov.Utilities;
using ItemStatsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputRemoting;

namespace builtin_nightvision_flashlight
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private bool FlashlightActive = false;
        private bool nightVisionActive = false;
        private int flashLightHash;
        private int nightVisionTypeHash;

        void Awake()
        {
            Debug.Log("builtin_nightvision_flashlight Loaded!!!");

            flashLightHash = "FlashLightType".GetHashCode();
            nightVisionTypeHash = "NightVisionType".GetHashCode();
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
            FlashlightActive = false;
            nightVisionActive = false;
            ApplyFlashlightEffect();
        }
        void OnEnable()
        {
        }
        void OnDisable()
        {
        }
        private void ToggleFlashlight()
        {
            FlashlightActive = !FlashlightActive;
            ApplyFlashlightEffect();
        }
        private void ToggleNightVision()
        {
            var character = CharacterMainControl.Main;
            if (character == null || character.CharacterItem == null) return;

            Stat nightVisionStat = character.CharacterItem.GetStat(nightVisionTypeHash);
            if (nightVisionStat != null)
            {
                float currentValue = nightVisionStat.BaseValue;
                float newValue = (currentValue + 1) % 3;
                nightVisionStat.BaseValue = newValue;

                Debug.Log($"Night Vision Mode: {newValue}");
                string message = "";
                switch (newValue)
                {
                    case 0: message = "关闭夜视"; break;
                    case 1: message = "开启夜视"; break;
                    case 2: message = "开启热成像"; break;
                }

                DialogueBubblesManager.Show(message,character.transform,
                    yOffset: 1f,
                    needInteraction: false,
                    skippable: true,
                    speed: -1f,
                    duration: 2f).Forget();

                if (GameManager.NightVision != null)
                {
                    GameManager.NightVision.Refresh();
                }
            }
        }
        private void ApplyFlashlightEffect()
        {
            var character = CharacterMainControl.Main;

            Debug.Log($"Flash Light {(FlashlightActive ? "Enabled" : "Disabled")}");
            DialogueBubblesManager.Show("暂时没有这个功能", character.transform,
                    yOffset: 1f,
                    needInteraction: false,
                    skippable: true,
                    speed: -1f,
                    duration: 2f).Forget();
        }
    }
}