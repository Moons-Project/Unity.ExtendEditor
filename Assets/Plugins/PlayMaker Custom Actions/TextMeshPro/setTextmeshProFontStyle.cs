// (c) Copyright HutongGames, LLC. All rights reserved.
// Author Eric Vander Wal www.dumbgamedev.com

using UnityEngine;
using TMPro;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMesh Pro")]
    [Tooltip("Set Text Mesh Pro text color.")]
    public class setTextmeshProFontStyle : ComponentAction<TextMeshPro>
    {
        [RequiredField]
        [CheckForComponent(typeof(TextMeshPro))]
        [Tooltip("Textmesh Pro component is required.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [ObjectType(typeof(FontStyles))]
        [TitleAttribute("Font Style")]
        [Tooltip("The font style for Textmesh Pro text.")]
        public FsmEnum FontStyle;

        [Tooltip("Check this box to preform this action every frame.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            FontStyle = null;
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

            this.cachedComponent.fontStyle = (FontStyles) FontStyle.Value;
        }
    }
}