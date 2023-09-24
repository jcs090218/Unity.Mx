using UnityEditor;
using UnityEngine;

namespace Mx
{
    public static class MxUtil
    {
        /// <summary>
        /// Get a texture from its source filename.
        /// </summary>
        public static Texture FindTexture(string texName)
        {
            Texture tex = (texName == "") ? null : EditorGUIUtility.FindTexture(texName);
            return tex;
        }
    }
}
