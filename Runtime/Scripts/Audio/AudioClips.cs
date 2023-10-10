using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ruffgames.core.Runtime.Scripts.Audio
{
   [Serializable]
   public class AudioClipHolder
   {
      public AudioClipType Type;
      public AudioClip Clip;
      [Range(0,1)]
      public float Volume =0.5f;
      public bool Loop;
   }
   [CreateAssetMenu(fileName = "AudioClips",menuName = "Audio System/Audio Clips",order = 0)]
   public class AudioClips : ScriptableObject
   {
      [SerializeField, TableList(AlwaysExpanded = true)]
      private List<AudioClipHolder> _clips;

      public AudioClipHolder GetClip(AudioClipType type)
      {
         var clip = _clips.Find(x=>x.Type == type);
         return clip;
      }
   }
}