#if UNITY_EDITOR
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Rs64.TexTransTool
{
    [RequireComponent(typeof(AbstractTexTransGroup))]
    public class AvatarDomainDefinition : MonoBehaviour
    {
        public GameObject Avatar;
        [SerializeField] public AbstractTexTransGroup TexTransGroup;
        [SerializeField] protected AvatarDomain CacheDomain;

        [SerializeField] bool _IsSelfCallApply;
        public virtual bool IsSelfCallApply => _IsSelfCallApply;
        public virtual AvatarDomain GetDomain()
        {
            return new AvatarDomain(Avatar.GetComponentsInChildren<Renderer>(true).ToList());
        }
        protected void Reset()
        {
            TexTransGroup = GetComponent<AbstractTexTransGroup>();
        }
        public virtual void Apply()
        {
            if (TexTransGroup == null) Reset();
            if (TexTransGroup.IsApply) return;
            if (Avatar == null) return;
            CacheDomain = GetDomain();
            _IsSelfCallApply = true;
            TexTransGroup.Apply(CacheDomain);
        }

        public virtual void Revart()
        {
            if (_IsSelfCallApply == false) return;
            if (TexTransGroup == null) Reset();
            if (!TexTransGroup.IsApply) return;
            _IsSelfCallApply = false;
            TexTransGroup.Revart(CacheDomain);
            CacheDomain = null;
        }
    }
    [System.Serializable]
    public class AvatarDomain
    {
        public AvatarDomain(List<Renderer> Renderers)
        {
            _Renderers = Renderers;
            _initialMaterials = Utils.GetMaterials(Renderers);
        }
        [SerializeField] List<Renderer> _Renderers;
        [SerializeField] List<Material> _initialMaterials;
        public AvatarDomain GetBackUp()
        {
            return new AvatarDomain(_Renderers);
        }
        private List<Material> _GetMaterials()
        {
            return Utils.GetMaterials(_Renderers).Distinct().Where(I => I != null).ToList();
        }
        public void SetMaterial(Material Target, Material SetMat)
        {
            foreach (var Renderer in _Renderers)
            {
                var Materials = Renderer.sharedMaterials;
                var IsEdit = false;
                foreach (var Index in Enumerable.Range(0, Materials.Length))
                {
                    if (Materials[Index] == Target)
                    {
                        Materials[Index] = SetMat;
                        IsEdit = true;
                    }
                }
                if (IsEdit)
                {
                    Renderer.sharedMaterials = Materials;
                }
            }
        }
        public void SetMaterial(MatPea Pea)
        {
            SetMaterial(Pea.Material, Pea.SecndMaterial);
        }

        public void SetMaterials(List<Material> Target, List<Material> SetMat)
        {
            foreach (var index in Enumerable.Range(0, Target.Count))
            {
                SetMaterial(Target[index], SetMat[index]);
            }
        }
        public void SetMaterials(IEnumerable<Material> Target, IEnumerable<Material> SetMat)
        {
            SetMaterials(Target.ToList(), SetMat.ToList());
        }
        public void SetMaterials(Dictionary<Material, Material> TargetAndSet)
        {
            foreach (var KVP in TargetAndSet)
            {
                SetMaterial(KVP.Key, KVP.Value);
            }
        }
        public void SetMaterials(List<Material> TargetMat, Material SetMat)
        {
            foreach (var Target in TargetMat)
            {
                SetMaterial(Target, SetMat);
            }
        }
        public void SetMaterials(IEnumerable<MatPea> peas)
        {
            foreach (var pea in peas)
            {
                SetMaterial(pea);
            }
        }

        public void ResetMaterial()
        {
            Utils.SetMaterials(_Renderers, _initialMaterials);
        }
        /// <summary>
        /// ドメイン内のすべてのマテリアルのtextureをtargetからsetTexに変更する
        /// </summary>
        /// <param name="Target">差し替え元</param>
        /// <param name="SetTex">差し替え先</param>
        /// <returns>どこにも保存されていない変更したマテリアルの配列</returns>
        public Dictionary<Material, Material> SetTexture(Texture2D Target, Texture2D SetTex)
        {
            var Mats = _GetMaterials();
            Dictionary<Material, Material> TargetAndSet = new Dictionary<Material, Material>();
            foreach (var Mat in Mats)
            {
                var Textures = MaterialUtil.FiltalingUnused(MaterialUtil.GetPropAndTextures(Mat), Mat);

                if (Textures.ContainsValue(Target))
                {
                    var NewMat = UnityEngine.Object.Instantiate<Material>(Mat);

                    foreach (var KVP in Textures)
                    {
                        if (KVP.Value == Target)
                        {
                            NewMat.SetTexture(KVP.Key, SetTex);
                        }
                    }

                    TargetAndSet.Add(Mat, NewMat);
                }
            }

            SetMaterials(TargetAndSet);

            return TargetAndSet;
        }
        /// <summary>
        /// ドメイン内のすべてのマテリアルのtextureをKeyからValueに変更する
        /// </summary>
        /// <param name="TargetAndSet"></param>
        /// <returns>どこにも保存されていない変更したマテリアルの辞書</returns>
        public Dictionary<Material, Material> SetTexture(Dictionary<Texture2D, Texture2D> TargetAndSet)
        {
            Dictionary<Material, Material> KeyAndNotSavedMat = new Dictionary<Material, Material>();
            foreach (var KVP in TargetAndSet)
            {
                var NotSavedMat = SetTexture(KVP.Key, KVP.Value);

                foreach (var MatPar in NotSavedMat)
                {
                    if (KeyAndNotSavedMat.ContainsKey(MatPar.Key) == false)
                    {
                        KeyAndNotSavedMat.Add(MatPar.Key, MatPar.Value);
                    }
                    else
                    {
                        KeyAndNotSavedMat[MatPar.Key] = MatPar.Value;
                    }
                }
            }
            return KeyAndNotSavedMat;
        }
    }
}
#endif