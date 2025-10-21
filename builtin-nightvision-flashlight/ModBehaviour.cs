using Duckov.UI;
using Duckov.Utilities;
using ItemStatsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
            if (Input.GetKeyDown(KeyCode.F))
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
            nightVisionActive = false;
            FlashlightActive = false;
            ApplyFlashlightEffect();
            ApplyNightVisionEffect();
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
            nightVisionActive = !nightVisionActive;
            ApplyNightVisionEffect();
        }
        private void ApplyFlashlightEffect()
        {
            var character = CharacterMainControl.Main;
            if (character == null) return;

            Debug.Log($"Flash Light {(FlashlightActive ? "Enabled" : "Disabled")}");
        }
        private void ApplyNightVisionEffect()
        {
            var character = CharacterMainControl.Main;
            if (character == null || character.CharacterItem == null) return;

            Stat nightVisionStat = character.CharacterItem.GetStat(nightVisionTypeHash);
            if (nightVisionStat != null)
            {
                nightVisionStat.BaseValue = nightVisionActive ? 1f : 0f;
                Debug.Log($"Night Vision {(nightVisionActive ? "Enabled" : "Disabled")}");
            }

            if (GameManager.NightVision != null)
            {
                GameManager.NightVision.Refresh();
            }
        }
    }
}