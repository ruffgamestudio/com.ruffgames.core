using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace com.ruffgames.core.Runtime.Scripts.UI
{
    public enum PanelType
    {
        Game = 0,
        Win = 1,
        Fail = 2,
    }
    public class PanelController : MonoBehaviour
    {
        
        [Serializable]
        public class PanelTypePair
        {
            public PanelType type;
            public Panel panel;
        }

        [SerializeField, TableList(AlwaysExpanded = true)] private List<PanelTypePair> _panels;

        public void Show(PanelType type)
        {
            var panelToActive = _panels.FirstOrDefault(x => x.type == type);
            if (panelToActive == null) return;
           
            foreach (var pair in _panels)
            {
                if (pair.type == type)
                {
                    pair.panel.Show();
                }
                else
                {
                    pair.panel.Hide();
                }
            }
        }
    }
}