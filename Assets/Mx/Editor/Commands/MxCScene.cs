#if UNITY_EDITOR
/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using UnityEditor.SceneManagement;

namespace Mx
{
    public class MxCScene : Mx
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public override bool Enable() { return true; }

        [Interactive(
            Icon: "UnityLogo",
            Summary: "Switch to scene")]
        public static void SwitchToScene()
        {
            CompletingRead("Switch to scene: ", MxUtil.GetFiles("*.unity"),
                (answer, summary) =>
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(answer);
                });
        }
    }
}
#endif
