// (c) Copyright HutongGames, LLC. All rights reserved.
// Author Eric Vander Wal www.dumbgamedev.com

using UnityEngine;
using TMPro;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMesh Pro")]
    [Tooltip("Enable and set Text Mesh Pro wrapping and overflow type.")]
    public class enableTextmeshProWrappingAndOverflow : ComponentAction<TextMeshPro>
    {
        [RequiredField]
        [CheckForComponent(typeof(TextMeshPro))]
        [Tooltip("Textmesh Pro component is required.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("Check this box to preform this action every frame.")]
        public FsmBool enableTextOverflow;

        [ObjectType(typeof(TextOverflowModes))]
        [TitleAttribute("Wrapping and Overflow Type")]
        [Tooltip("Set wrapping and overflow type for Textmesh Pro.")]
        public FsmEnum wrapping;

        [Tooltip("Enable overflow and wrapping mode.")]
        public FsmBool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            wrapping = null;
            enableTextOverflow = false;
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

            this.cachedComponent.overflowMode = (TextOverflowModes) wrapping.Value;
            this.cachedComponent.enableWordWrapping = enableTextOverflow.Value;
        }
    }
}