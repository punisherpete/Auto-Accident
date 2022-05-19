using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace RayFire
{
    [CanEditMultipleObjects]
    [CustomEditor (typeof(RayfireRigidRoot))]
    public class RayfireRigidRootEditor : Editor
    {
        // Target
        RayfireRigidRoot root;

        public override void OnInspectorGUI()
        {
            // Get target
            root = target as RayfireRigidRoot;
            if (root == null)
                return;
            
            // Space
            GUILayout.Space (8);
            
            // Initialize
            if (Application.isPlaying == true)
            {
                if (root.initialized == false)
                {
                    if (GUILayout.Button ("Initialize", GUILayout.Height (25)))
                        foreach (var targ in targets)
                            if (targ as RayfireRigidRoot != null)
                                if ((targ as RayfireRigidRoot).initialized == false)
                                    (targ as RayfireRigidRoot).Initialize();
                }
                
                // Reuse
                else
                {
                    if (GUILayout.Button ("Reset Rigid Root", GUILayout.Height (25)))
                            foreach (var targ in targets)
                                if (targ as RayfireRigidRoot != null)
                                    if ((targ as RayfireRigidRoot).initialized == true)
                                        (targ as RayfireRigidRoot).ResetRigidRoot();
                }
                GUILayout.Space (2);
            }
            
            RigidRootSetupUI();
            
            GUILayout.Space (3);

            if (root.cluster.shards.Count > 0)
            {
                GUILayout.Label ("    Cluster Shards: " + root.cluster.shards.Count);
                // GUILayout.Label ("    Amount Integrity: " + conn.AmountIntegrity + "%");
            }
            
            if (root.physics.HasIgnore == true)
                GUILayout.Label ("    Ignore Pairs: " + root.physics.ignoreList.Count / 2);
            
  //          if (Application.isPlaying == true)
  //              RigidManUI();
            
            GUILayout.Space (3);
            DrawDefaultInspector();
        }
        
        void RigidRootSetupUI()
        {
            if (Application.isPlaying == false)
            {
                GUILayout.Space (2);
                GUILayout.BeginHorizontal();

                if (root.cluster.shards.Count == 0)
                    if (GUILayout.Button ("Editor Setup", GUILayout.Height (25)))
                        foreach (var targ in targets)
                            if (targ as RayfireRigidRoot != null)
                            {
                                (targ as RayfireRigidRoot).EditorSetup();
                                SetDirty (targ as RayfireRigidRoot);
                            }
                    
                if (root.cluster.shards.Count > 0)
                    if (GUILayout.Button ("Reset Setup", GUILayout.Height (25)))
                        foreach (var targ in targets)
                            if (targ as RayfireRigidRoot != null)
                            {
                                //RFPhysic.DestroyColliders (targ as RayfireRigidRoot);
                                (targ as RayfireRigidRoot).ResetSetup();
                                SetDirty (targ as RayfireRigidRoot);
                            }

                EditorGUILayout.EndHorizontal();
                GUILayout.Space (2);
            }
        }
        
        void SetDirty(RayfireRigidRoot scr)
        {
            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty (scr);
                EditorSceneManager.MarkSceneDirty (scr.gameObject.scene);
            }
        }
        
/*
        void RigidManUI()
        {
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button ("Activate", GUILayout.Height (25)))
                Activate();
            
            if (GUILayout.Button ("Fade", GUILayout.Height (25)))
                Fade();
            
            EditorGUILayout.EndHorizontal();
        }
        
        void Activate()
        {
            if (Application.isPlaying == true)
                foreach (var targ in targets)
                    if (targ as RayfireRigidRoot != null)
                        if ((targ as RayfireRigidRoot).simulationType == SimType.Inactive || (targ as RayfireRigidRoot).simulationType == SimType.Kinematic)
                            (targ as RayfireRigidRoot).ac();
        }
        
        void Fade()
        {
            if (Application.isPlaying == true)
                foreach (var targ in targets)
                    if (targ as RayfireRigidRoot != null)
                        (targ as RayfireRigidRoot).Fade();
        }
        */
    }
}