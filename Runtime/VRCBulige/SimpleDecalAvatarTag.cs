#if UNITY_EDITOR
using UnityEngine;
using VRC.SDKBase;
using Rs64.TexTransTool.Decal;
namespace Rs64.TexTransTool.VRCBulige
{
    [AddComponentMenu("TexTransTool/SimpleDecal")]

    public class SimpleDecalAvatarTag : SimpleDecal, IEditorOnly
    {

    }
}
#endif