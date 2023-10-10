using System;
using System.Collections.Generic;
using System.Linq;

namespace com.ruffgames.core.Runtime.Scripts.Storage
{
    public class TutorialDatas
    {
        public bool IsTutorialCompleted;
        public List<TutorialData> Tutorials = new List<TutorialData>();
        public TutorialData GetTutorial(string id)
        {
            var tutorialData = Tutorials.FirstOrDefault(x => x.Id == id);
            if (tutorialData == null)
            {
                tutorialData = new TutorialData()
                {
                    IsCompleted = false,
                };
            
                Tutorials.Add(tutorialData);
            }

            return tutorialData;
        }
    }

    [Serializable]
    public class TutorialData
    {
        public string Id { get; private set; }
        public bool IsCompleted;
    }
}