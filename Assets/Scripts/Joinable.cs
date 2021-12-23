using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class Joinable : MonoBehaviour
    {
        public ModuleJoint[] joints;
        public Direction[] directions;
        public int number = 100;
        public bool unique;
        public PrefabExamples variants;
        public ModuleJoint joint;

        private void OnDrawGizmos()
        {
            Collider coll = GetComponent<Collider>(); 
            Gizmos.color = Color.red;
            Gizmos.DrawCube(coll.bounds.center, coll.bounds.size);
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (LevelGenerator.S.IntersectionIgnorable.Contains(gameObject)) return;
            if (joint == null) print("NOT JOINED!!!" + "In " + name);
            if (transform.position.x < 0)
            {
                RemoveModule();
                return;
            }
            Collider collider = GetComponent<Collider>();
            foreach (Collider coll in LevelGenerator.S.colliders)
                if (coll.bounds.Intersects(collider.bounds))
                {
                    if (coll != collider)
                    {
                        try
                        {
                            if (number < coll.gameObject.GetComponent<Joinable>().number)
                            {
                                print("Intersection!!! In " + this + " with " + coll/*s[0]*/);
                                RemoveModule();
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            print("Exception!!!");
                        }
                    }
                    else
                    {
                        print("Self intersection");
                    }
                }
        }

        void RemoveModule()
        {
            foreach (ModuleJoint joint in joints)
            {
                LevelGenerator.S.joints.Remove(joint);
            }
            LevelGenerator.S.joints.Add(joint);
            LevelGenerator.S.colliders.Remove(GetComponent<BoxCollider>());
            LevelGenerator.S.generatedModules.Remove(this.gameObject);
            LevelGenerator.S.roomCount++;
            if (variants != null)
                LevelGenerator.S.modules.AddRange(variants.examples);
            Destroy(this.gameObject);
        }
    }
}
