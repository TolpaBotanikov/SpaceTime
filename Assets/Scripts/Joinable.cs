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
    }
}
