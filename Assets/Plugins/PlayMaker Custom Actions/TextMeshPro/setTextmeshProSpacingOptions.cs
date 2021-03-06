// (c) Copyright HutongGames, LLC. All rights reserved.
// Author Eric Vander Wal www.dumbgamedev.com

using UnityEngine;
using TMPro;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMesh Pro")]
    [Tooltip("set Text Mesh Pro spacing options.")]
    public class setTextmeshProSpacingOptions : ComponentAction<TextMeshPro>
    {
        [RequiredField]
        [CheckForComponent(typeof(TextMeshPro))]
        [Tooltip("Textmesh Pro component is required.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Character spacing.")]
        public FsmFloat character;

        [Tooltip("Word spacing.")]
        public FsmFloat word;

        [Tooltip("Line spacing.")]
        public FsmFloat line;

        [Tooltip("Paragraph spacing.")]
        public FsmFloat paragraph;

        [Tooltip("Check this box to preform this action every frame.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            
            word = new FsmFloat(){UseVariable = true};
            character = new FsmFloat(){UseVariable = true};
            line = new FsmFloat(){UseVariable = true};
            paragraph = new FsmFloat(){UseVariable = true};
            
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoMeshChange();
            
            if (!everyFrame.Value)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (everyFrame.Value)
            {
                DoMeshChange();
            }
        }

        void DoMeshChange()
        {
            if (!UpdateCache(Fsm.GetOwnerDefaultTarget(gameObject)))
            {
                Debug.LogError("No textmesh pro component was found on " + gameObject);
                return;
            }

            if(!word.IsNone) this.cachedComponent.wordSpacing = word.Value;
            if(!character.IsNone) this.cachedComponent.characterSpacing = character.Value;
            if(!line.IsNone) this.cachedComponent.lineSpacing = line.Value;
            if(!paragraph.IsNone) this.cachedComponent.paragraphSpacing = paragraph.Value;
        }
    }
}