using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RayFire
{
    [Serializable]
    public class RFActivation
    {
        [Header ("  Activation")]
        [Space (3)]
        
        [Tooltip ("Activation By Local Offset relative to parent.")]
        public bool local;
        
        [Space (1)]
        [Tooltip ("Inactive object will be activated if will be pushed from it's original position farther than By Offset value.")]
        public float byOffset;
        
        [Space (3)]
        [Tooltip ("Inactive object will be activated when it's velocity will be higher than By Velocity value when pushed by other dynamic objects.")]
        public float byVelocity;
        
        [Space (1)]
        [Tooltip ("Inactive object will be activated if will get total damage higher than this value.")]
        public float byDamage;

        [Space (1)]
        [Tooltip ("Inactive object will be activated by overlapping with object with RayFire Activator component.")]
        public bool byActivator;

        [Space (1)]
        [Tooltip ("Inactive object will be activated when it will be shot by RayFireGun component.")]
        public bool byImpact;

        [Space (1)]
        [Tooltip ("Inactive object will be activated by Connectivity component if it will not be connected with Unyielding zone.")]
        public bool byConnectivity;

        [Header ("  Connectivity")]
        [Space (3)]
        [Tooltip ("Allows to define Inactive/Kinematic object as Unyielding to check for connection with other Inactive/Kinematic objects with enabled By Connectivity activation type.")]
        public bool unyielding;
        [Space (1)]
        [Tooltip ("Unyielding object can not be activate by default. When On allows to activate Unyielding objects as well.")]
        public bool activatable;

        [Header ("  Post Activation")]
        [Space (2)]
		
        [Tooltip ("Custom layer for fragments")]
        public string layer;
        
        // Nom serialized
        [HideInInspector] public RayfireConnectivity connect;
        
        [NonSerialized] public bool                activated;
        [NonSerialized] public bool                inactiveCorState;
        [NonSerialized] public bool                velocityCorState;
        [NonSerialized] public bool                offsetCorState;
        [NonSerialized] public IEnumerator         velocityEnum;
        [NonSerialized] public IEnumerator         offsetEnum;

        /// /////////////////////////////////////////////////////////
        /// Constructor
        /// /////////////////////////////////////////////////////////

        // Constructor
        public RFActivation()
        {
            byVelocity     = 0f;
            byOffset       = 0f;
            byDamage       = 0f;
            byActivator    = false;
            byImpact       = false;
            byConnectivity = false;
            unyielding     = false;
            activatable    = false;
            activated      = false;

            // unyList        = new List<int>();
            Reset();
        }

        // Copy from
        public void CopyFrom (RFActivation act)
        {
            byActivator    = act.byActivator;
            byImpact       = act.byImpact;
            byVelocity     = act.byVelocity;
            byOffset       = act.byOffset;
            local          = act.local;
            byDamage       = act.byDamage;
            byConnectivity = act.byConnectivity;
            unyielding     = act.unyielding;
            activatable    = act.activatable;

            layer = act.layer;
        }

        /// /////////////////////////////////////////////////////////
        /// Methods
        /// /////////////////////////////////////////////////////////

        // Turn of all activation properties
        public void Reset()
        {
            activated        = false;
            inactiveCorState = false;
            velocityCorState = false;
            offsetCorState   = false;

            velocityEnum = null;
            offsetEnum   = null;
        }

        // Connectivity check
        public void CheckConnectivity()
        {
            if (byConnectivity == true && connect != null)
            {
                connect.connectivityCheckNeed = true;
                connect = null;
            }
        }

        /// /////////////////////////////////////////////////////////
        /// Coroutines
        /// /////////////////////////////////////////////////////////

        // Check velocity for activation
        public IEnumerator ActivationVelocityCor (RayfireRigid scr)
        {
            // Skip not activatable uny objects
            if (scr.activation.unyielding == true && scr.activation.activatable == false)
                yield break;
            
            // Stop if running 
            if (velocityCorState == true)
                yield break;

            // Set running state
            velocityCorState = true;
            
            // Check
            while (scr.activation.activated == false && scr.activation.byVelocity > 0)
            {
                if (scr.physics.rigidBody.velocity.magnitude > byVelocity)
                    scr.Activate();
                yield return null;
            }
            
            // Set state
            velocityCorState = false;
        }

        // Check offset for activation
        public IEnumerator ActivationOffsetCor (RayfireRigid scr)
        {
            // Skip not activatable uny objects
            if (scr.activation.unyielding == true && scr.activation.activatable == false)
                yield break;
            
            // Stop if running 
            if (offsetCorState == true)
                yield break;

            // Set running state
            offsetCorState = true;
           
            // Check
            while (scr.activation.activated == false && scr.activation.byOffset > 0)
            {
                if (scr.activation.local == true)
                {
                    if (Vector3.Distance (scr.transForm.localPosition, scr.physics.localPosition) > scr.activation.byOffset)
                        scr.Activate();
                }
                else
                {
                    if (Vector3.Distance (scr.transForm.position, scr.physics.initPosition) > scr.activation.byOffset)
                        scr.Activate();
                }

                yield return null;
            }
            
            // Set state
            offsetCorState = false;
        }

        // Exclude from simulation, move under ground, destroy
        public IEnumerator InactiveCor (RayfireRigid scr)
        {
            // Stop if running 
            if (inactiveCorState == true)
                yield break;

            // Set running state
            inactiveCorState = true;

            //scr.transForm.hasChanged = false;
            while (scr.simulationType == SimType.Inactive)
            {
                scr.physics.rigidBody.velocity        = Vector3.zero;
                scr.physics.rigidBody.angularVelocity = Vector3.zero;
                yield return null;
            }

            // Set state
            inactiveCorState = false;
        }

        // Activation by velocity and offset
        public IEnumerator InactiveCor  (RayfireRigidRoot scr)
        {
            // Stop if running 
            if (inactiveCorState == true)
                yield break;

            // Set running state
            inactiveCorState = true;
            
            while (scr.inactiveShards.Count > 0)
            {
                // Timestamp
                // float t1 = Time.realtimeSinceStartup;
                
                // Remove activated shards
                for (int i = scr.inactiveShards.Count - 1; i >= 0; i--)
                    if (scr.inactiveShards[i].tm == null || scr.inactiveShards[i].sm == SimType.Dynamic)
                        scr.inactiveShards.RemoveAt (i);

                // Velocity activation
                if (scr.activation.byVelocity > 0)
                {
                    for (int i = scr.inactiveShards.Count - 1; i >= 0; i--)
                    {
                        if (scr.inactiveShards[i].tm.hasChanged == true)
                            if (scr.inactiveShards[i].rb.velocity.magnitude > scr.activation.byVelocity)
                                if (ActivateShard (scr.inactiveShards[i], scr) == true)
                                    scr.inactiveShards.RemoveAt (i);
                    }

                    // Stop 
                    if (scr.inactiveShards.Count == 0)
                        yield break;
                }

                // Offset activation
                if (scr.activation.byOffset > 0)
                {
                    // By global offset
                    if (scr.activation.local == false)
                    {
                        for (int i = scr.inactiveShards.Count - 1; i >= 0; i--)
                        {
                            if (scr.inactiveShards[i].tm.hasChanged == true)
                                if (Vector3.Distance (scr.inactiveShards[i].tm.position, scr.inactiveShards[i].pos) > scr.activation.byOffset)
                                    if (ActivateShard (scr.inactiveShards[i], scr) == true)
                                        scr.inactiveShards.RemoveAt (i);
                        }
                    }
                    
                    // By local offset
                    else
                    {
                        for (int i = scr.inactiveShards.Count - 1; i >= 0; i--)
                        {
                            if (scr.inactiveShards[i].tm.hasChanged == true)
                                if (Vector3.Distance (scr.inactiveShards[i].tm.localPosition, scr.inactiveShards[i].los) > scr.activation.byOffset)
                                    if (ActivateShard (scr.inactiveShards[i], scr) == true)
                                        scr.inactiveShards.RemoveAt (i);
                        }
                    }

                    // Stop 
                    if (scr.inactiveShards.Count == 0)
                        yield break;
                }
                
                
                // Stop velocity
                for (int i = scr.inactiveShards.Count - 1; i >= 0; i--)
                {
                    scr.inactiveShards[i].rb.velocity        = Vector3.zero;
                    scr.inactiveShards[i].rb.angularVelocity = Vector3.zero;
                }
                
                // Debug.Log (Time.realtimeSinceStartup - t1);
                
                // TODO repeat 30 times per second, not every frame
                yield return null;
            }
            
            // Set state
            inactiveCorState = false;
        }
        
        /// /////////////////////////////////////////////////////////
        /// Activate Rigid / Shard
        /// /////////////////////////////////////////////////////////

        // Activate inactive object
        public static void ActivateRigid (RayfireRigid scr, bool connCheck = true)
        {
            // Stop if excluded
            if (scr.physics.exclude == true)
                return;

            // Skip not activatable unyielding objects
            if (scr.activation.activatable == false && scr.activation.unyielding == true)
                return;

            // Initialize if not
            if (scr.initialized == false)
                scr.Initialize();

            // Turn convex if kinematic activation
            if (scr.simulationType == SimType.Kinematic)
            {
                MeshCollider meshCollider = scr.physics.meshCollider as MeshCollider;
                if (meshCollider != null)
                    meshCollider.convex = true;

                // Swap with animated object
                if (scr.physics.rec == true)
                {
                    // Set dynamic before copy
                    scr.simulationType                = SimType.Dynamic;
                    scr.physics.rigidBody.isKinematic = false;
                    scr.physics.rigidBody.useGravity  = scr.physics.useGravity;

                    // Create copy
                    GameObject inst = UnityEngine.Object.Instantiate (scr.gameObject);
                    inst.transform.position = scr.transForm.position;
                    inst.transform.rotation = scr.transForm.rotation;

                    // Save velocity
                    Rigidbody rBody = inst.GetComponent<Rigidbody>();
                    if (rBody != null)
                    {
                        rBody.velocity        = scr.physics.rigidBody.velocity;
                        rBody.angularVelocity = scr.physics.rigidBody.angularVelocity;
                    }

                    // Activate and init rigid
                    scr.gameObject.SetActive (false);
                }
            }

            // Connectivity check
            if (connCheck == true)
                scr.activation.CheckConnectivity();
            
            // Set layer
            SetRigidActivationLayer (scr);
            
            // Set state
            scr.activation.activated = true;

            // Set props
            scr.simulationType                = SimType.Dynamic;
            scr.physics.rigidBody.isKinematic = false; // TODO error at manual activation of stressed connectivity structure
            scr.physics.rigidBody.useGravity  = scr.physics.useGravity;

            // Fade on activation
            if (scr.fading.onActivation == true) 
                scr.Fade();

            // Parent
            if (RayfireMan.inst.parent != null)
                scr.gameObject.transform.parent = RayfireMan.inst.parent.transform;

            // Init particles on activation
            RFParticles.InitActivationParticlesRigid (scr);

            // Activation sound
            RFSound.ActivationSound (scr.sound, scr.limitations.bboxSize);

            // Events
            scr.activationEvent.InvokeLocalEvent (scr);
            RFActivationEvent.InvokeGlobalEvent (scr);

            // Add initial rotation if still TODO put in ui
            if (scr.physics.rigidBody.angularVelocity == Vector3.zero)
            {
                float val = 0.3f;
                scr.physics.rigidBody.angularVelocity = new Vector3 (
                    Random.Range (-val, val), Random.Range (-val, val), Random.Range (-val, val));
            }
        }

        // Activate Rigid Root shard
        public static bool ActivateShard (RFShard shard, RayfireRigidRoot rigidRoot)
        {
            // Skip not activatable unyielding shards
            if (shard.act == false && shard.uny == true)
                return false;
            
            // Set dynamic sim state
            shard.sm = SimType.Dynamic;
            
            // Activate by Rigid if has rigid
            if (shard.rigid != null && shard.rigid.objectType == ObjectType.Mesh)
            {
                ActivateRigid (shard.rigid);
                return true;
            }

            // Physics ops
            if (shard.rb != null)
            {
                // Set props
                if (shard.rb.isKinematic == true)
                    shard.rb.isKinematic = false;

                // Turn On Gravity
                shard.rb.useGravity = rigidRoot.physics.useGravity;
                
                // Add initial rotation if still TODO put in ui
                float val = 0.3f;
                if (shard.rb.angularVelocity == Vector3.zero)
                    shard.rb.angularVelocity = new Vector3 (
                        Random.Range (-val, val), Random.Range (-val, val), Random.Range (-val, val));
            }

            // Activation Fade TODO input Fade class by RigidRoot or MeshRoot
            if (rigidRoot.fading.onActivation == true)
                RFFade.FadeShard (rigidRoot, shard);

            // Parent
            if (RayfireMan.inst.parent != null)
                shard.tm.parent = RayfireMan.inst.parent.transform;

            // Connectivity check if shards was activated: TODO check only neibs of activated?
            if (rigidRoot.activation.byConnectivity == true && rigidRoot.activation.connect != null)
                rigidRoot.activation.connect.connectivityCheckNeed = true;

            // Init particles on activation
            RFParticles.InitActivationParticlesShard(rigidRoot, shard);
            
            // Activation sound
            RFSound.ActivationSound (rigidRoot.sound, rigidRoot.cluster.bound.size.magnitude);
            
            // Events
            rigidRoot.activationEvent.InvokeLocalEventRoot (rigidRoot);
            RFActivationEvent.InvokeGlobalEventRoot (rigidRoot);
            
            return true;
        }
        
        /// /////////////////////////////////////////////////////////
        /// Activation Layer
        /// /////////////////////////////////////////////////////////
        
        // Set activation layer
        static void SetRigidActivationLayer (RayfireRigid scr)
        {
            // Disabled
            if (scr.activation.layer.Length == 0)
                return;
            
            // No custom layer
            if (RayfireMan.inst.layersHash.Contains (scr.activation.layer) == false)
                return;

            // Set
            scr.gameObject.layer = LayerMask.NameToLayer (scr.activation.layer);
        }
        
    }
}