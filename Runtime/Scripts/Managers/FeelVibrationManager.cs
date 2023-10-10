using Lofelt.NiceVibrations;

namespace com.ruffgames.core.Runtime.Scripts.Managers
{
    public class FeelVibrationManager : VibrationManager
    {
        protected override void OnVibrate(VibrationType type, float duration)
        {
            switch (type)
            {
                case VibrationType.Light:
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
                    break;
                case VibrationType.Medium:
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.MediumImpact);
                    break;
                case VibrationType.Heavy:
                    HapticPatterns.PlayPreset(HapticPatterns.PresetType.HeavyImpact);
                    break;
                case VibrationType.Continuous:
                    HapticPatterns.PlayConstant(0.5f, 1f, duration);
                    break;
            }
        }
        
    }
}
