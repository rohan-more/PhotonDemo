using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    public enum MeshName {BATHTUB, SACK_OPEN, BIRD_HOUSE, CHAIR, VASE}
   
    [CreateAssetMenu(fileName = "MeshConfig", menuName = "ScriptableObjects/MeshConfig", order = 1)] 
    public class MeshConfig : ScriptableObject
    {
        [SerializeField] private List<MeshName> meshNames;
        [SerializeField] private List<Mesh> meshList;
        [SerializeField] private List<Material> materialList;
    
    
        private Dictionary<MeshName, Mesh> nameToMeshMap = new Dictionary<MeshName, Mesh>();
        private Dictionary<MeshName, Material> nameToMaterialMap = new Dictionary<MeshName, Material>();

        public Mesh GetMesh(MeshName meshName)
        {
            nameToMeshMap.TryGetValue(meshName, out Mesh mesh);
            return mesh;
        }
        
        public Material GetMaterial(MeshName meshName)
        {
            nameToMaterialMap.TryGetValue(meshName, out Material mat);
            return mat;
        }
        
        public void CreateData()
        {
            CreateMeshMap();
            CreateMaterialMap();
        }
        
        private void CreateMeshMap()
        {
            int count = Mathf.Min(meshNames.Count, meshList.Count);
        
            for (int i = 0; i < count; i++)
            {
                nameToMeshMap[meshNames[i]] = meshList[i];
            }
        }
        
        private void CreateMaterialMap()
        {
            int count = Mathf.Min(meshNames.Count, materialList.Count);
        
            for (int i = 0; i < count; i++)
            {
                nameToMaterialMap[meshNames[i]] = materialList[i];
            }
        }

    }
}


